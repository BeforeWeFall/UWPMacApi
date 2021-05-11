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
    enum enumReq
    {
        Token,
        Error,
        Cities,
        Product,
        Resturan,
        Catalog,
        TokenRest,
        Price,
        PriceRest
    }

    class Response
    {

        private HttpClient client;
        private JsonInfo jsonInfo;
        private Random rnd = new Random();

        public Response(string host, int port, JsonInfo js, string login, string password)
        {
            jsonInfo = js;

            ServicePointManager.ServerCertificateValidationCallback +=
            delegate (object sender, X509Certificate certificate, X509Chain chain,
                   SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            var httpClientHandler = new HttpClientHandler
            {
                Proxy = new WebProxy($"http://{host}:{port}") { Credentials = new NetworkCredential(login, password) },
                UseProxy = true

            };

            client = new HttpClient(httpClientHandler);
            var byteArray = Encoding.ASCII.GetBytes("username:password1234");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public string GetToken(string idcity, string timeZ)
        {
            int timespan = ConvertToUnixTimestamp(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
            var taskRrq = HttpReqAsync((int)enumReq.Token, timespan, idcity: idcity, timeZ: timeZ);
            var res = TryWaitResult(taskRrq);
            return res;
        }
        public string GetCities(string idcity, string timeZ)
        {
            var taskRrq = HttpReqAsync((int)enumReq.Cities, 0, idcity: idcity, timeZ: timeZ);
            var res = TryWaitResult(taskRrq);
            return res;
        }
        public string GetProduct(string idcity, string timeZ)
        {
            var taskRrq = HttpReqAsync((int)enumReq.Product, 0, idcity: idcity, timeZ: timeZ);
            var res = TryWaitResult(taskRrq);
            return res;
        }
        public string GetRestourans(string idcity, string timeZ)
        {
            var taskRrq = HttpReqAsync((int)enumReq.Resturan, 0, idcity: idcity, timeZ: timeZ);
            var res = TryWaitResult(taskRrq);
            return res;
        }
        public string GetCatalog(int timespan, string idcity, string timeZ)
        {
            var taskRrq = HttpReqAsync((int)enumReq.Catalog, 0, idcity: idcity, timeZ: timeZ);
            var res = TryWaitResult(taskRrq);
            return res;
        }
        public string GetTokenRest(int timespan, string idcity, string timeZ)
        {

            var taskRrq = HttpReqAsync((int)enumReq.TokenRest, timespan, idcity: idcity, timeZ: timeZ);
            var res = TryWaitResult(taskRrq);
            return res;
        }
        public string GetPrices(string idCity, string timeZ)
        {
            int timespan = ConvertToUnixTimestamp(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
            var taskRrq = HttpReqAsync((int)enumReq.Price, timespan, idcity: idCity, timeZ: timeZ);
            var res = TryWaitResult(taskRrq);
            return res;
        }
        public string GetPriceRest(string idRest, string idcity, string timeZ)
        {
            int timespan = ConvertToUnixTimestamp(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
            var taskRrq = HttpReqAsync((int)enumReq.PriceRest, timespan, idRest, idcity, timeZ);
            var res = TryWaitResult(taskRrq);
            return res;
        }

        private async Task<HttpResponseMessage> HttpReqAsync(int i, int timespan, string replaceTo = "0", string idcity = "0", string timeZ = "")
        {
            Thread.Sleep(rnd.Next(1000, 2250));

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
        static int ConvertToUnixTimestamp(string date_str)
        {
            DateTime date = DateTime.Parse(date_str, new CultureInfo("ru-RU"));
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return (int)Math.Floor(diff.TotalSeconds);
        }
        private string TryWaitResult(Task<HttpResponseMessage> task)
        {
            task.Wait();
            return task.Result.Content.ReadAsStringAsync().Result;
        }
    }
}
