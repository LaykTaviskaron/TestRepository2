// See https://aka.ms/new-console-template for more information


using System.Net;

while (true)
{
    var token = new CancellationToken();
    var googleTask = Task.Run(() => DownloadUrl("https://google.com"), token);
    var duckTask = Task.Run(() => DownloadUrl("https://duckduckgo.com/"));
    var yahooTask = Task.Run(() => DownloadUrl("https://localhost:5050"));


    var resultsArray = await Task.WhenAll(googleTask);
    bool allAreUp = true;
    foreach (var item in resultsArray)
    {
        if (item.StatusCode != HttpStatusCode.OK)
        {
            allAreUp = false;
        }
    }

    Console.WriteLine(allAreUp ? "All sites are up" : "Not every site is running");
    await Task.Delay(2000);
}


async Task<HttpResponseMessage> DownloadUrl(string url) 
{
    using (var httpClient = new HttpClient())
    {
        try
        {
            return await httpClient.GetAsync(url);
        }
        catch (Exception e)
        {
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}


Console.ReadLine();