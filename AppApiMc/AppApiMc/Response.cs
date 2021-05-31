using AppApiMc.JsonClasses;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AppApiMc
{
    class Response
    {

        private HttpClient client;
        private JsonInfo jsonInfo;
        private Random rnd = new Random();

        public Response(string proxy, JsonInfo js, string login, string password)
        {
            jsonInfo = js;

           

            if (string.IsNullOrEmpty(proxy))
            {
                client = new HttpClient();
            }
            else
            {
                ServicePointManager.ServerCertificateValidationCallback +=
                delegate (object sender, X509Certificate certificate, X509Chain chain,
                   SslPolicyErrors sslPolicyErrors)
                {
                return true;
                };

                var httpClientHandler = new HttpClientHandler
                {
                    Proxy = new WebProxy($"http://{proxy}") { Credentials = new NetworkCredential(login, password) },
                    UseProxy = true

                };

                client = new HttpClient(httpClientHandler);
            }

            var byteArray = Encoding.ASCII.GetBytes("username:password1234");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<HttpResponseMessage> HttpReqAsync(int i, int timespan, string replaceTo = "0", string idcity = "0", string timeZ = "")
        {
            await Task.Delay(rnd.Next(1000, 2250));

            HttpRequestMessage request;
            string reqHeaders;

            ClearAndAddHeaders(client, jsonInfo.item[i].request.header, idcity, timeZ);

            if (i == 0)
            {
                reqHeaders = Regex.Replace(jsonInfo.item[i].request.url.raw,
                    @"(?<=modified=)[^&]*", timespan.ToString()).
                Replace("{{host}}", "mobile-api.mcdonalds.ru/api/v1");

                request = new HttpRequestMessage(HttpMethod.Post, reqHeaders);
            }
            else
            {
                reqHeaders = Regex.Replace(
                    Regex.Replace(
                    Regex.Replace(jsonInfo.item[i].request.url.raw,
                    @"(?<=modified=)[^&]*", timespan.ToString()),
                    @"(?<=store_id=)[^&]*", replaceTo),
                    @"(?<=city_id=)[^&]*", idcity).
                Replace("{{host}}", "mobile-api.mcdonalds.ru/api/v1");

                request = new HttpRequestMessage(HttpMethod.Get, reqHeaders);
            }

            if (jsonInfo.item[i].request.body != null)
            {
                string reqBody = Regex.Replace(jsonInfo.item[i].request.body.raw, "(?<=\"id\":\").*(?=\")", idcity);
                request.Content = new StringContent(reqBody,
                                                Encoding.UTF8,
                                                "application/json");
            }

            return await client.SendAsync(request);
        }

        private void ClearAndAddHeaders(HttpClient client, header[] headers, string idcity = "", string timeZ = "")
        {
            client.DefaultRequestHeaders.Clear();

            foreach (var z in headers)
            {
                switch (z.key.ToString())
                {
                    case "User-Agent":
                    case "Accept-Encoding":
                    case "Content-Type":
                        break;

                    case "X-City-ID":
                        if (!string.IsNullOrWhiteSpace(idcity))
                            client.DefaultRequestHeaders.Add(z.key, idcity);
                        else
                            goto default;
                        break;

                    case "X-Timezone":
                        if (!string.IsNullOrWhiteSpace(timeZ))
                            client.DefaultRequestHeaders.Add(z.key, timeZ);
                        else
                            goto default;
                        break;

                    default:
                        client.DefaultRequestHeaders.Add(z.key, z.value);
                        break;
                }
            }
        }
        public int ConvertToUnixTimestamp(string date_str)
        {
            DateTime date = DateTime.Parse(date_str, new CultureInfo("ru-RU"));
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return (int)Math.Floor(diff.TotalSeconds);
        }
        public async Task<string> TryWaitResult(Task<HttpResponseMessage> task)
        {
            await task;
            
            return await task.Result.Content.ReadAsStringAsync(); // add error cath and info
        }
    }
}
