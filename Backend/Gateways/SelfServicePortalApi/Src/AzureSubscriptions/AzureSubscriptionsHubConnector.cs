using Microsoft.AspNetCore.SignalR;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.AzureSubscriptions
{
    /// <summary>
    /// Connector class to wrap calls to the hub context.
    /// </summary>
    public class AzureSubscriptionsHubConnector
    {
        /// <summary>
        /// The hub context.
        /// </summary>
        private readonly IHubContext<AzureSubscriptionsHub> _hubContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureSubscriptionsHubConnector"/> class.
        /// </summary>
        /// <param name="hubContext">The hub context.</param>
        public AzureSubscriptionsHubConnector(IHubContext<AzureSubscriptionsHub> hubContext)
        {
            _hubContext = hubContext;
        }

        /// <summary>
        /// Updates the specifed subscription on clients.
        /// </summary>
        /// <param name="newData">The new data of the subscription.</param>
        public void UpdateSubscription(DisplayAzureSubscriptionVm newData)
        {
            newData.RemoveMetadata();
            _hubContext.Clients.All.SendAsync($"self({newData.Id})", newData);
        }
    }
}
