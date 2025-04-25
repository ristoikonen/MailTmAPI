# MailTmAPI 

 Create and use api.mail.tm's temp mailboxes


### Templated async POST method
POST's  parametres are in Dictionary<string, string> and converted to JSON.
> Target type T  is a JSON model of the response.

```csharp

internal class HttpGenericClient<T>
    {
        // ...

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

    }
}

```