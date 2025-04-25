# MailTmAPI 

 Create and use api.mail.tm's temp mailboxes.
 Docker ready.

> Use MailTmAPI.http for easy testing. 
> Every request listed.

### Templated PostAsync -method
POST's  parametres are in a Dictionary<string, string>.
> Target type T  is a JSON model of the response.

```csharp

public async Task<T?> PostAsync(string uri, Dictionary<string, string> contentParams)
{
    try
    {
        var content = new FormUrlEncodedContent(contentParams);
        string paramAsJSON = JsonConvert.SerializeObject(content);

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

```