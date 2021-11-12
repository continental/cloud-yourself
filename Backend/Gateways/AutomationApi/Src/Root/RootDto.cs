using Fancy.ResourceLinker.Models;

namespace CloudYourself.Backend.Gateways.AutomationApi.Root
{
    /// <summary>
    /// A view model to represent the very root data of the api.
    /// </summary>
    public class RootDto : ResourceBase
    {
        public string Name { get; } = "Cloud Yourself Core API";
    }
}
