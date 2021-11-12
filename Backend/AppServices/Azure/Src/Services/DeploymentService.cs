using CloudYourself.Backend.AppServices.Azure.Aggregates.Subscription;
using CloudYourself.Backend.AppServices.Azure.Aggregates.Tennant;
using CloudYourself.Backend.AppServices.Azure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudYourself.Backend.AppServices.Azure.Services
{
    /// <summary>
    /// Service to work with azure deployments.
    /// </summary>
    /// <seealso cref="CloudYourself.Backend.AppServices.Azure.Services.AzureServiceBase" />
    public class DeploymentService : AzureServiceBase
    {
        /// <summary>
        /// Gets the parameter metadata.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <returns>A list ob parameter meta objects.</returns>
        public List<DeploymentParamMetaDto> GetParameterMetadata(string template)
        {
            List<DeploymentParamMetaDto> result = new List<DeploymentParamMetaDto>();

            JsonDocument templateDocument = JsonDocument.Parse(template);

            foreach(JsonProperty paramElement in templateDocument.RootElement.GetProperty("parameters").EnumerateObject())
            {
                DeploymentParamMetaDto deployParam = new DeploymentParamMetaDto();
                deployParam.Name = paramElement.Name;
                deployParam.Type = paramElement.Value.GetProperty("type").GetString();

                JsonElement allowedValues;
                if(paramElement.Value.TryGetProperty("allowedValues", out allowedValues))
                {
                    deployParam.AllowedValues = allowedValues.EnumerateArray().Select(av => av.GetString()).ToList();
                }

                JsonElement defaultValue;
                if (paramElement.Value.TryGetProperty("defaultValue", out defaultValue))
                {
                    deployParam.Value = defaultValue.GetString();
                }

                JsonElement metadata;
                if (paramElement.Value.TryGetProperty("metadata", out metadata))
                {
                    JsonElement label;
                    if (metadata.TryGetProperty("label", out label))
                    {
                        deployParam.Label = label.GetString();
                    }

                    JsonElement desciption;
                    if (metadata.TryGetProperty("desciption", out desciption))
                    {
                        deployParam.Label = desciption.GetString();
                    }
                }

                result.Add(deployParam);
            }

            return result;
        }

        /// <summary>
        /// Builds the parameters json.
        /// </summary>  
        /// <param name="deploymentParams">The deployment parameters.</param>
        /// <returns></returns>
        public string BuildParametersJson(List<DeploymentParamMetaDto> deploymentParams)
        {
            Dictionary<string, object> paramDictonary = new Dictionary<string, object>();

            foreach(DeploymentParamMetaDto param in deploymentParams)
            {
                paramDictonary.Add(param.Name, new { value = param.Value });
            }

            return JsonSerializer.Serialize(paramDictonary);
        }

        /// <summary>
        /// Creates the resource group asynchronous.
        /// </summary>
        /// <param name="tenantSettings">The tenant settings.</param>
        /// <param name="subscriptionId">The subscription identifier.</param>
        /// <param name="resourceGroupName">Name of the resource group.</param>
        public async Task CreateResourceGroupAsync(TenantSettings tenantSettings, string subscriptionId, string resourceGroupName)
        {
            HttpClient httpClient = await GetAuthenticatedClientAsync(tenantSettings);

            string url = $"https://management.azure.com/subscriptions/{subscriptionId}/resourcegroups/{resourceGroupName}?api-version=2021-04-01";

            string payload = $"{{ \"location\":  \"westeurope\" }}";

            HttpResponseMessage createResourceGroupResponse = await httpClient.PutAsync(url, new StringContent(payload, Encoding.UTF8, "application/json"));
            try
            {
                createResourceGroupResponse.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                string errorObjectJson = await createResourceGroupResponse.Content.ReadAsStringAsync();
                throw new ArgumentException(errorObjectJson);
            }
        }

        /// <summary>
        /// Creates a initiative assignment to a subscription.
        /// </summary>
        /// <param name="tenantSettings">The tenant settings.</param>
        /// <param name="subscriptionId">The subscription identifier.</param>
        /// <param name="initiativeDefinitionId">The initiative definition identifier.</param>
        /// <param name="assignmentName">Name of the assignment.</param>
        public async Task CreateInitiativeAssignment(TenantSettings tenantSettings, string subscriptionId, string initiativeDefinitionId, string assignmentName)
        {
            HttpClient httpClient = await GetAuthenticatedClientAsync(tenantSettings);

            string url = $"https://management.azure.com/subscriptions/{subscriptionId}/providers/Microsoft.Authorization/policyAssignments/{assignmentName}?api-version=2019-09-01";

            string payload = $"{{ \"properties\": {{ \"displayName\": \"{assignmentName}\", \"description\": \"Assigned via CloudYourself\", \"policyDefinitionId\": \"{initiativeDefinitionId}\" }} }}";

            HttpResponseMessage createAssignmentResponse = await httpClient.PutAsync(url, new StringContent(payload, Encoding.UTF8, "application/json"));
            
            try
            {
                createAssignmentResponse.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                string errorObjectJson = await createAssignmentResponse.Content.ReadAsStringAsync();
                ThrowErrorObjectException(errorObjectJson);
            }
        }

        /// <summary>
        /// Deploys a managed resource asynchronously.
        /// </summary>
        /// <param name="tenantSettings">The tenant settings.</param>
        /// <param name="subscriptionId">The subscription identifier.</param>
        /// <param name="resourceGroupName">Name of the resource group to deploy resources to.</param>
        /// <param name="templateJson">The template json.</param>
        /// <param name="parametersJson">The parameters json.</param>
        /// <returns>
        /// A task indicating the completion of the asynchronous operation.
        /// </returns>
        public async Task DeployManagedResourceAsync(TenantSettings tenantSettings, string subscriptionId, string resourceGroupName, string templateJson, string parametersJson)
        {
            HttpClient httpClient = await GetAuthenticatedClientAsync(tenantSettings);

            string url = $"https://management.azure.com/subscriptions/{subscriptionId}/resourcegroups/{resourceGroupName}/providers/Microsoft.Resources/deployments/cloudYourselfDeployment?api-version=2020-10-01";

            string payload = $"{{ \"properties\": {{ \"mode\": \"Complete\", \"template\": {templateJson}, \"parameters\": {parametersJson} }} }}";

            HttpResponseMessage createDeploymentResponse = await httpClient.PutAsync(url, new StringContent(payload, Encoding.UTF8, "application/json"));
            
            try
            {
                createDeploymentResponse.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                string errorObjectJson = await createDeploymentResponse.Content.ReadAsStringAsync();
                ThrowErrorObjectException(errorObjectJson);
            }
        }

        /// <summary>
        /// Triggers the policy evaluation for a subscription.
        /// </summary>
        /// <param name="tenantSettings">The tenant settings.</param>
        /// <param name="subscriptionId">The subscription identifier.</param>
        public async Task<string> TriggerPolicyEvaluation(TenantSettings tenantSettings, string subscriptionId)
        {
            HttpClient httpClient = await GetAuthenticatedClientAsync(tenantSettings);

            string url = $"https://management.azure.com/subscriptions/{subscriptionId}/providers/Microsoft.PolicyInsights/policyStates/latest/triggerEvaluation?api-version=2019-10-01";

            string payload = $"";

            HttpResponseMessage triggerPolicyEvaluationResponse = await httpClient.PostAsync(url, new StringContent(payload, Encoding.UTF8, "application/json"));

            try
            {
                triggerPolicyEvaluationResponse.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                string errorObjectJson = await triggerPolicyEvaluationResponse.Content.ReadAsStringAsync();
                ThrowErrorObjectException(errorObjectJson);
            }

            string asyncOperationResultLocation = triggerPolicyEvaluationResponse.Headers.Location.ToString();
            return asyncOperationResultLocation;
        }

        /// <summary>
        /// Reads the policy compliance state of a subscription.
        /// </summary>
        /// <param name="tenantSettings">The tenant settings.</param>
        /// <param name="policyEvaluationOperationUrl">The policy evaluation operation URL.</param>
        /// <param name="subscriptionId">The subscription identifier.</param>
        /// <returns>
        /// The current compliance state of the subscription.
        /// </returns>
        public async Task<ComplianceState> ReadPolicyEvaluationResult(TenantSettings tenantSettings, string policyEvaluationOperationUrl, string subscriptionId)
        {
            HttpClient httpClient = await GetAuthenticatedClientAsync(tenantSettings);

            if (!string.IsNullOrEmpty(policyEvaluationOperationUrl))
            {
                // Check if evaluation scan is completed
                HttpResponseMessage policyEvaluationOperationResponse = await httpClient.GetAsync(policyEvaluationOperationUrl);

                try
                {
                    policyEvaluationOperationResponse.EnsureSuccessStatusCode();
                }
                catch (Exception)
                {
                    string errorObjectJson = await policyEvaluationOperationResponse.Content.ReadAsStringAsync();
                    ThrowErrorObjectException(errorObjectJson);
                }

                if (policyEvaluationOperationResponse.StatusCode == HttpStatusCode.Accepted)
                {
                    return ComplianceState.Evaluating;
                }
            }

            // Read the compliance summary data
            string url = $"https://management.azure.com/subscriptions/{subscriptionId}/providers/Microsoft.PolicyInsights/policyStates/latest/summarize?api-version=2019-10-01";

            string payload = $"";

            HttpResponseMessage complianceSummaryResponse = await httpClient.PostAsync(url, new StringContent(payload, Encoding.UTF8, "application/json"));

            try
            {
                complianceSummaryResponse.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                string errorObjectJson = await complianceSummaryResponse.Content.ReadAsStringAsync();
                ThrowErrorObjectException(errorObjectJson);
            }

            string complianceSummaryJson = await complianceSummaryResponse.Content.ReadAsStringAsync();
            JsonElement complianceSummary = JsonDocument.Parse(complianceSummaryJson).RootElement;

            int nonCompliantResourcesCount = complianceSummary.GetProperty("value")[0].GetProperty("results").GetProperty("nonCompliantResources").GetInt32();

            return nonCompliantResourcesCount == 0 ? ComplianceState.Compliant : ComplianceState.NonCompliant;
        }
    }
}
