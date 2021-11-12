import { AzureFunction, Context } from "@azure/functions";
import axios from "axios";
import { ResourceBase } from "fancy-hateoas-client";
import { Common } from "../common";

const syncAzureSubscriptionState: AzureFunction = async function (context: Context): Promise<void> {
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
            const subscriptionDetailsQueryUrl = "https://management.azure.com/subscriptions/" + azureSubscription.subscriptionId + "?api-version=2020-01-01";
            const policySummaryQueryUrl = "https://management.azure.com/subscriptions/" + azureSubscription.subscriptionId + "/providers/Microsoft.PolicyInsights/policyStates/latest/summarize?api-version=2019-10-01"

            const subscriptionDetailsResponse = await axios.get(subscriptionDetailsQueryUrl, { headers });
            const currentState = subscriptionDetailsResponse.data.state;

            if(currentState === 'Canceled') {
                // Cancel the subscription
                context.log("Canceling subscription")
                await azureSubscription.cancel();
            } else {
                // Get the current compliance state
                const policySummaryResponse = await axios.post(policySummaryQueryUrl, null, { headers });
                const nonCompliantResourcesCount = policySummaryResponse.data.value[0].results.nonCompliantResources;
                if(nonCompliantResourcesCount === 0) {
                    context.log("Subscription is fully compliant");
                    azureSubscription.compliance.state = "compliant";
                } else {
                    context.log("Subscription is not compliant");
                    azureSubscription.compliance.state = "nonCompliant";
                }

                await azureSubscription.compliance.update();
            } 
        }
    }
};

export default syncAzureSubscriptionState;
