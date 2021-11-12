import { AzureFunction, Context } from "@azure/functions";
import axios from "axios";
import { ResourceBase } from "fancy-hateoas-client";
import { Common } from "../common";
import { DateTime } from "luxon";

const readAzureSubscriptionCost: AzureFunction = async function (context: Context): Promise<void> {

    // Get the api root
    const apiRoot: ResourceBase = await Common.getRootApi(context);

    // Fetch all Cloud-Yourself tenants
    const tenants: ResourceBase[] = <ResourceBase[]> await apiRoot._links.tenants.fetch();

    // Loop through each Cloud-Yourself tenant
    for(let tenant of tenants) {

        context.log('Processing tenant ' + tenant.label);

        // Get the azure settings of the tenant
        const appRegistration = tenant.azureSettings.appRegistration;

        // Load all azure subscriptions of that tenant
        const azureSubscriptions = (await tenant._links.azureSubscriptions.fetch()) as ResourceBase[];
        context.log('Found ' + azureSubscriptions.length + ' subscriptions in tenant');

        // Get access token for that tenant
        const accessToken = await Common.getAzureTokenViaClientCredentials(appRegistration.azureDirectoryTenantId, 
                                                                     appRegistration.azureAppRegistrationId, 
                                                                     appRegistration.azureAppSecret, 
                                                                     ['https://management.core.windows.net/.default']);

        const headers = { Authorization: 'Bearer ' + accessToken };

        // Loop through each subscription of the tenant
        for(let azureSubscription of azureSubscriptions) {
            
            context.log('Processing subscription with id ' + azureSubscription.subscriptionId);

            // Set up of query urls
            const billingPeriodsQueryUrl = "https://management.azure.com/subscriptions/" + azureSubscription.subscriptionId + "/providers/Microsoft.Billing/billingPeriods?api-version=2017-04-24-preview";
            const costManagementQueryUrl = "https://management.azure.com/subscriptions/" + azureSubscription.subscriptionId + "/providers/Microsoft.CostManagement/query?api-version=2019-11-01"

            // Request of billing periods
            let billingPeriodResponse;
            try {
                billingPeriodResponse = await axios.get(billingPeriodsQueryUrl, { headers });
            } catch(e) {
                context.log('ERROR: Unable to retrieve billing periods. ' + e);
                continue;
            }

            // Iterate through each billng period and get cost data of each
            for(let billingPeriod of billingPeriodResponse.data.value.slice(0, 3)) {

                let now = DateTime.now();
                let periodStart = DateTime.fromISO(billingPeriod.properties.billingPeriodStartDate);
                let queryEndDate = billingPeriod.properties.billingPeriodEndDate;

                // If the billing period starts with the current month, adjust end date to "now" to gate the month to date costs
                if(periodStart.year === now.year && periodStart.month === now.month) {
                    queryEndDate = now.toString();
                }

                // If the billing period is in the future do not request cost for it
                if(DateTime.fromISO(billingPeriod.properties.billingPeriodStartDate).toMillis() > DateTime.now().toMillis()) continue;

                // Set up payload for aggregated cost request
                let payload = {
                    type: "ActualCost",
                    dataSet: {
                        granularity: "Monthly",
                        aggregation: {
                            totalCost: {
                                name: "PreTaxCost",
                                function: "Sum"
                            }
                        }
                    },
                    timeframe: "Custom",
                    timePeriod: {
                        from: billingPeriod.properties.billingPeriodStartDate,
                        to: queryEndDate
                    }
                };

                // Request costs
                let costResponse;
                try {
                    costResponse = await axios.post(costManagementQueryUrl, payload, { headers });
                } catch(e) {
                    context.log('ERROR: Unable to retrieve costs for billing period ' + billingPeriod.name + '. ' + e);
                    continue;
                }

                // If no cost data could be retrieved just continue
                if(costResponse.data.properties.rows.length === 0) continue;

                // Read out the cost properties for this billing period
                const periodId = billingPeriod.name;
                const periodBegin = billingPeriod.properties.billingPeriodStartDate;
                const periodEnd = billingPeriod.properties.billingPeriodEndDate;
                const amount: number = costResponse.data.properties.rows[0][0];
                const currency = costResponse.data.properties.rows[0][2];

                // Try to get an existing cost object to update
                const existingCost = (await azureSubscription._links.costs.fetch({ periodId: periodId })) as ResourceBase;

                if(existingCost) {
                    // Update an existing cost
                    context.log('Updating "' + billingPeriod.name + '" ' + 'with ' + amount + ' ' + currency);
                    existingCost.costDetails.periodBegin = periodBegin;
                    existingCost.costDetails.periodEnd = periodEnd;
                    existingCost.costDetails.currency = currency;
                    existingCost.costDetails.amount = amount;
                    existingCost.costDetails.update();
                } else {
                    // Create a new cost object
                    context.log('Creating "' + billingPeriod.name + '" ' + 'with ' + amount + ' ' + currency);
                    const newCost = (await azureSubscription._links.newCostTemplate.fetch()) as ResourceBase;
                    newCost.costDetails.periodId =  periodId;
                    newCost.costDetails.periodBegin = periodBegin;
                    newCost.costDetails.periodEnd = periodEnd;
                    newCost.costDetails.currency = currency;
                    newCost.costDetails.amount = amount;
                    await newCost.create();
                }
            }
        }
    }
};

export default readAzureSubscriptionCost;
