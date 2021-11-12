using Fancy.ResourceLinker.Models;

namespace CloudYourself.Backend.Gateways.SelfServicePortalApi.Root
{
    /// <summary>
    /// A view model to represent the very root data of the api.
    /// </summary>
    public class RootVm : ResourceBase
    {
        public string Name { get; } = "Cloud Yourself Core API";
    }
}
