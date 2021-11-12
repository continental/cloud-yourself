using Fancy.ResourceLinker;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Fancy.ResourceLinker.Json;
using System.Text;
using System.Collections.Generic;
using Fancy.ResourceLinker.Models;

namespace CloudYourself.Backend.Gateways.AutomationApi.Infrastructure
{
    /// <summary>
    /// Base class for a controller which shall act as proxy to an inner microservice.
    /// </summary>
    /// <seealso cref="Fancy.ResourceLinker.HypermediaController" />
    public abstract class ProxyController : HypermediaController
    {
        /// <summary>
        /// The HTTP client.
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// The serializer options.
        /// </summary>
        private readonly JsonSerializerOptions _serializerOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyController"/> class.
        /// </summary>
        /// <param name="defaultBaseAddress">The default base address.</param>
        public ProxyController(string defaultBaseAddress)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(defaultBaseAddress);
            _serializerOptions = new JsonSerializerOptions();
            _serializerOptions.AddResourceConverter();
        }

        /// <summary>
        /// Gets the data from the microservice behind.
        /// </summary>
        /// <typeparam name="TResource">The type of the resource.</typeparam>
        /// <param name="baseAddress">The base address to use if different than proxy parameters.</param>
        /// <param name="url">The URL to use if different than proxy parameters.</param>
        /// <returns>
        /// The result deserialized into the specified resource type.
        /// </returns>
        protected async Task<TResource> GetAsync<TResource>(string baseAddress = null, string url = null)
        {
            // Set up request url
            string requestUrl = baseAddress ?? "";
            requestUrl = requestUrl + (url ?? HttpContext.Request.Path + HttpContext.Request.QueryString);

            // Get data from microservice
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(requestUrl);
            responseMessage.EnsureSuccessStatusCode();

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                // Create result
                return JsonSerializer.Deserialize<TResource>(jsonResponse, _serializerOptions);
            }
            else return default(TResource);
        }

        /// <summary>
        /// Gets the collection asynchronous.
        /// </summary>
        /// <typeparam name="TResource">The type of the resource.</typeparam>
        /// <param name="baseAddress">The base address to use if different than proxy parameters.</param>
        /// <param name="url">The URL to use if different than proxy parameters.</param>
        /// <returns>
        /// The result deserialized into a collection of the specified resource type.
        /// </returns>
        protected async Task<List<TResource>> GetCollectionAsync<TResource>(string baseAddress = null, string url = null) where TResource : ResourceBase
        {
            // Set up request url
            string requestUrl = baseAddress ?? "";
            requestUrl = requestUrl + (url ?? HttpContext.Request.Path + HttpContext.Request.QueryString);

            // Get data from microservice
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(requestUrl);
            responseMessage.EnsureSuccessStatusCode();

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                // Create result
                return JsonSerializer.Deserialize<List<TResource>>(jsonResponse, _serializerOptions);
            }
            else return null;
        }

        /// <summary>
        /// Proxies a GET request to its microservice.
        /// </summary>
        /// <typeparam name="TResource">The type of the resource.</typeparam>
        /// <param name="baseAddress">The base address to use if different than proxy parameters.</param>
        /// <param name="url">The URL to use if different than proxy parameters.</param>
        /// <returns>
        /// The action result for the proxy request.
        /// </returns>
        protected async Task<IActionResult> ProxyGetAsync<TResource>(string baseAddress = null, string url = null) where TResource: ResourceBase
        {
            // Get data from microservice
            TResource result = await GetAsync<TResource>(baseAddress, url);

            if (result != null)
            {
                // Create result
                return Hypermedia(result);
            }
            else return NoContent();
        }

        /// <summary>
        /// Proxies a GET request for a collection to a microservice.
        /// </summary>
        /// <typeparam name="TResource">The type of the resource.</typeparam>
        /// <param name="baseAddress">The base address to use if different than proxy parameters.</param>
        /// <param name="url">The URL to use if different than proxy parameters.</param>
        /// <returns></returns>
        protected async Task<IActionResult> ProxyGetCollectionAsync<TResource>(string baseAddress = null, string url = null) where TResource : ResourceBase
        {
            // Get data from microservice
            ICollection<TResource> result = await GetCollectionAsync<TResource>(baseAddress, url);

            if (result != null)
            {
                // Create result
                return Hypermedia(result);
            }
            else return NoContent();
        }


        /// <summary>
        /// Proxies a POST request to its microservice.
        /// </summary>
        /// <param name="baseAddress">The base address to use if different than proxy parameters.</param>
        /// <param name="url">The URL to use if different than proxy parameters.</param>
        /// <returns>The action result for the proxy request.</returns>
        protected async Task<string> ProxyPostAsync(string baseAddress = null, string url = null)
        {
            // Set up request url
            string requestUrl = baseAddress ?? "";
            requestUrl = requestUrl + (url ?? (HttpContext.Request.Path + HttpContext.Request.QueryString));

            // Read body of request
            using StreamReader reader = new StreamReader(HttpContext.Request.Body);
            string requestContent = await reader.ReadToEndAsync();

            // Put data to microservice
            HttpResponseMessage responseMessage = await _httpClient.PostAsync(requestUrl, new StringContent(requestContent, Encoding.Default, "application/json"));
            responseMessage.EnsureSuccessStatusCode();

            return await responseMessage.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Proxies a PUT request to its microservice.
        /// </summary>
        /// <param name="baseAddress">The base address to use if different than proxy parameters.</param>
        /// <param name="url">The URL to use if different than proxy parameters.</param>
        /// <returns>
        /// The action result for the proxy request.
        /// </returns>
        protected async Task<IActionResult> ProxyPutAsync(string baseAddress = null, string url = null)
        {
            // Set up request url
            string requestUrl = baseAddress ?? "";
            requestUrl = requestUrl + (url ?? HttpContext.Request.Path + HttpContext.Request.QueryString);

            // Read body of request
            using StreamReader reader = new StreamReader(HttpContext.Request.Body);
            string requestContent = await reader.ReadToEndAsync();

            // Put data to microservice
            HttpResponseMessage responseMessage = await _httpClient.PutAsync(requestUrl, new StringContent(requestContent, Encoding.Default, "application/json"));
            responseMessage.EnsureSuccessStatusCode();

            return await ProcessResponseAsync(responseMessage);
        }

        /// <summary>
        /// Proxies a DELETE request to its microservice.
        /// </summary>
        /// <param name="baseAddress">The base address to use if different than proxy parameters.</param>
        /// <param name="url">The URL to use if different than proxy parameters.</param>
        /// <returns>The action result for the proxy request.</returns>
        protected async Task<IActionResult> ProxyDeleteAsync(string baseAddress = null, string url = null)
        {
            // Set up request url
            string requestUrl = baseAddress ?? "";
            requestUrl = requestUrl + (url ?? HttpContext.Request.Path + HttpContext.Request.QueryString);

            // Get data from microservice
            HttpResponseMessage responseMessage = await _httpClient.DeleteAsync(requestUrl);
            responseMessage.EnsureSuccessStatusCode();

            return await ProcessResponseAsync(responseMessage);
        }

        /// <summary>
        /// Processes a response message to create a proper response for the proxied request.
        /// </summary>
        /// <param name="responseMessage">The response message.</param>
        /// <returns>An action result.</returns>
        private async Task<IActionResult> ProcessResponseAsync(HttpResponseMessage responseMessage)
        {
            string responseContent = await responseMessage.Content.ReadAsStringAsync();

            // Process response content of other microservice
            if (!string.IsNullOrEmpty(responseContent))
            {
                return new ContentResult { StatusCode = (int)responseMessage.StatusCode, Content = responseContent, ContentType = "application/json" };
            }
            else
            {
                return StatusCode((int)responseMessage.StatusCode);
            }
        }
    }
}
