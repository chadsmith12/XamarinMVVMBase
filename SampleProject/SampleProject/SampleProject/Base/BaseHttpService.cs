using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModernHttpClient;
using Newtonsoft.Json;
using SampleProject.Models;

namespace SampleProject.Base
{
    /// <summary>
    /// Base Http Service to help send requests to an api
    /// Can derive your services from this if your service needs to call out to an api at some point.
    /// </summary>
    public abstract class BaseHttpService
    {
        /// <summary>
        /// Sends the request asynchronous.
        /// </summary>
        /// <typeparam name="TData">The type of the data you expect back from the request.</typeparam>
        /// <param name="url">The URL to send the request.</param>
        /// <param name="httpMethod">The HTTP method. If this is null default to GET.</param>
        /// <param name="headers">The headers to send along with the request.</param>
        /// <param name="requestData">The request data that you need to send along witht he request. This will be serialized to JSON.</param>
        /// <returns></returns>
        protected async Task<Response<TData>> SendRequestAsync<TData>(Uri url, HttpMethod httpMethod = null, IDictionary<string, string> headers = null, object requestData = null)
        {
            Response<TData> result;

            // default to GET if the method is null
            var method = httpMethod ?? HttpMethod.Get;
            // convert a requestData to json to send it
            var data = requestData == null ? null : JsonConvert.SerializeObject(requestData);

            using (var request = new HttpRequestMessage(method, url))
            {
                if (data == null)
                {
                    request.Content = new StringContent(data, Encoding.UTF8, "application/json");
                }
                // add the headers
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }

                // create the client to send the request out
                // because we are using ModernHttpClient instead of Mono's HttpClient we use native message handler inside of the constructor
                // This really improves the performance by using Android's native OkHttp and iOS's native NSURLSession internally instead of always relying on mono.
                using (var client = new HttpClient(new NativeMessageHandler()))
                {
                    using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            result = new Response<TData>
                            {
                                Success = true,
                                Data = JsonConvert.DeserializeObject<TData>(await response.Content.ReadAsStringAsync())
                            };
                        }
                        else
                        {
                            result = new Response<TData>
                            {
                                ErrorCode = response.StatusCode.ToString(),
                                Message = response.ReasonPhrase,
                                Success = false
                            };
                        }
                    }
                }
            }
            return result;
        }
    }
}
