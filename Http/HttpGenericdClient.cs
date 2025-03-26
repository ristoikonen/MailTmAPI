using MailTmAPI.Properties;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Components;
using MailTmAPI.Models;
using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace MailTmAPI.Http
{

    //internal class HttpCustomClient
    //{

    //    public async Task<AccountInfo?> PostAsync(string uri, Dictionary<string, string> paramdict)
    //    {
    //        try
    //        {
    //            var content = new FormUrlEncodedContent(paramdict);

    //            string paramAsJSON = JsonConvert.SerializeObject(paramdict);
    //            Console.WriteLine(paramAsJSON);

    //            using (var client = new System.Net.Http.HttpClient())
    //            {

    //                client.DefaultRequestHeaders.Accept.Clear();
    //                client.DefaultRequestHeaders.Add("Accept", "*/*");
    //                client.DefaultRequestHeaders.Add("Host", Endpoints.Host);
    //                client.DefaultRequestHeaders.Add("Connection", "keep-alive");

    //                var contents = new StringContent(paramAsJSON, Encoding.UTF8, "application/json");

    //                HttpResponseMessage response = await client.PostAsync(uri, contents);
    //                if (response.Content is object)
    //                {
    //                    //var accountinfos = response.Content.ReadAsAsync<AccountInfo>();
    //                    var accountinfo = response.Content.ReadFromJsonAsync<AccountInfo?>();
    //                    return accountinfo.Result;
    //                }
    //                return null;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine(ex.Message);
    //            return null; ;
    //        }
    //    }
    //}


    internal class HttpGenericClient<T>
    {
        public async Task<JsonDocument?> GetAsync(string requestUri, Dictionary<string, string>? contentsParams = null, string token = "")
        {
            try
            {
                using (var client = new HttpClient())
                {
                    //client.BaseAddress = new Uri(requestUri);

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                    client.DefaultRequestHeaders.Add("Host", Endpoints.CoinLoreHost);
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    if (token.Length > 0)
                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                    if (contentsParams is not null)
                    {
                        var content = new FormUrlEncodedContent(contentsParams);
                        string paramAsJSON = System.Text.Json.JsonSerializer.Serialize(content);
                        var contents = new StringContent(paramAsJSON, Encoding.UTF8, "application/json");
                    }

                    HttpResponseMessage response = await client.GetAsync(requestUri);
                    if (response.IsSuccessStatusCode && response.Content is object)
                    {
                        try
                        {
                            //Root data = JsonSerializer.Deserialize<Root>(response.Conten);
                            //var res= System.Text.Json.JsonSerializer.Deserialize<CoinData[]>(response.Content.ReadAsStream());
                            var contentStream = await response.Content.ReadAsStreamAsync();
                            var result = await System.Text.Json.JsonSerializer.DeserializeAsync<CoinData[]>(contentStream);
                            //var info = await response.Content.ReadFromJsonAsync<CoinData[]>();
                            Console.WriteLine(result?.ToString());
                        }
                        catch (Exception ex2)
                        {
                            Console.WriteLine(ex2.Message);
                        }

                        //var domaininfo = response.Content.ReadFromJsonAsAsyncEnumerable<T?>();
                        //var stream = response.Content.ReadFromJsonAsAsyncEnumerable<T>();
                        //await foreach (var titem in stream)
                        //{
                        //     Console.WriteLine( titem?.ToString());
                        //}   


                        return JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<T?> PostAsync(string uri, Dictionary<string, string> paramdict)
        {
            try
            {
                var content = new FormUrlEncodedContent(paramdict);
                string paramAsJSON = JsonConvert.SerializeObject(paramdict);

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Accept", "*/*");
                    client.DefaultRequestHeaders.Add("Host", Endpoints.Host);
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");

                    var contents = new StringContent(paramAsJSON, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(uri, contents);
                    if (response.IsSuccessStatusCode && response.Content is object)
                    {
                        var accountinfo = response.Content.ReadFromJsonAsync<T?>();
                        return accountinfo.Result;
                    }
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default(T);
            }
        }


        public async Task<T?> PostAsyncOld(string uri, Dictionary<string, string> contentsParams, string token = "")
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                    client.DefaultRequestHeaders.Add("Host", Endpoints.Host);
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    if (token.Length > 0)
                        client.DefaultRequestHeaders.Add("Authorization", $"token {token}");

                    if (contentsParams is not null)
                    {
                        var content = new FormUrlEncodedContent(contentsParams);
                        string paramAsJSON = System.Text.Json.JsonSerializer.Serialize(content);

                        var contents = new StringContent(paramAsJSON, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(uri, contents);

                        if (response.IsSuccessStatusCode && response.Content is object)
                        {
                            var tinfo = response.Content.ReadFromJsonAsync<T?>();
                            return tinfo.Result;
                        }
                    }
                    //TODO: else is error

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return default(T);

            //try
            //{
            //    var content = new FormUrlEncodedContent(paramdict);
            //    string paramAsJSON = JsonSerializer.Serialize(paramdict);

            //    var client = new HttpClient();

            //    client.DefaultRequestHeaders.Add("Accept", "*/*");
            //    client.DefaultRequestHeaders.Add("Host", Endpoints.Host);
            //    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            //    if (token.Length > 0)
            //        client.DefaultRequestHeaders.Add("Authorization", $"token {token}");
            //    var contents = new StringContent(paramAsJSON, Encoding.UTF8, "application/json");

            //    HttpResponseMessage response = await client.PostAsync("https://api.mail.tm/token", contents);

            //    if (response.IsSuccessStatusCode && response.Content is object)
            //    {
            //        var jd = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            //        var jd2 = await response.Content.ReadFromJsonAsync<T?>();
            //    }

            //    if (response.Content is object)
            //    {
            //        var accountinfo = response.Content.ReadFromJsonAsync<T?>();
            //        return accountinfo.Result;
            //    }
            //    return default(T);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    return default(T);
            //}
        }
    }
}
