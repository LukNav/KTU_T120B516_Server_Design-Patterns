using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server.Helpers
{
    public static class HttpRequests
    {
        public static HttpResponseMessage GetRequest(string Url)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, Url)
            {
                Headers =
                {
                    { HeaderNames.Accept, "*/*" },
                    { HeaderNames.UserAgent, "Client" }
                }
            };

            using (var httpClient = new HttpClient())
            {
                var response = httpClient.Send(httpRequestMessage);
                if ((int)response.StatusCode >= 400)
                    throw new HttpRequestException(response.Message());
                return response;
            }
        }

        public static HttpResponseMessage PostRequest<T>(string Url, T body)
        {
            string content = JsonSerializer.Serialize(body);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, Url)
            {
                Headers =
                {
                    { HeaderNames.Accept, "*/*" },
                    { HeaderNames.UserAgent, "ButtNet" }
                },
                Content = new StringContent(content,
                                    Encoding.UTF8,
                                    "application/json")
            };

            using (var httpClient = new HttpClient())
            {
                var response = httpClient.Send(httpRequestMessage);
                if ((int)response.StatusCode >= 400)
                    throw new HttpRequestException(response.Message());
                return response;
            }

            return null;
        }

        public static string Message(this HttpResponseMessage httpResponseMessage)
        {
            return httpResponseMessage.Content.ReadAsStringAsync().Result;
        }

        public static T Deserialize<T>(this HttpResponseMessage httpResponseMessage)
        {
            return JsonSerializer.Deserialize<T>(httpResponseMessage.Message(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}