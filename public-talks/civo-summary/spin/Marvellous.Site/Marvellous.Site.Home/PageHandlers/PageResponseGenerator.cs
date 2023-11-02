using System.Collections.Generic;
using System.Net;
using Fermyon.Spin.Sdk;

namespace Marvellous.Site.Home.PageHandlers;

public static class PageResponseGenerator
{
    public static HttpResponse Generate(string? body)
    {
        var newPage = new PageBuilder("base.html")
            .WithBody(body)
            .Build();
        
        return new HttpResponse
        {
            Headers = new Dictionary<string, string>
            {
                ["Content-Type"] = "text/html",
            },
            StatusCode = HttpStatusCode.OK,
            BodyAsString = newPage
        };
    }
}