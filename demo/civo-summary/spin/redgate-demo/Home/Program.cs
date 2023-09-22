using System.Net;
using Fermyon.Spin.Sdk;
using System.Runtime.InteropServices;

namespace Microservice;

public static class Handler
{
    [HttpHandler]
    public static HttpResponse HandleHttpRequest(HttpRequest request)
        => Router.Handle(request);
}


public static class Router
{
    public static HttpResponse Handle(HttpRequest request)
    {
        // find which path was hit, and switch on it
        return request.Url switch
        {
            "/" => CurrentTimeResponse.Generate(),
            "/hello" => new HttpResponse
            {
                StatusCode = HttpStatusCode.OK,
                BodyAsString = "Hello from .NET",
            },
            "/goodbye" => new HttpResponse
            {
                StatusCode = HttpStatusCode.OK,
                BodyAsString = "Goodbye from .NET",
            },
            _ => new HttpResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                BodyAsString = "Not found",
            },
        };
    }
}

public static class CurrentTimeResponse
{
    public static HttpResponse Generate()
    {
        var indexPage = new PageBuilder("index.html")
            .WithBody("Welcome Sami!")
            .Build();
        
        return new HttpResponse
        {
            Headers = new Dictionary<string, string>
            {
                ["Content-Type"] = "text/html",
            },
            StatusCode = HttpStatusCode.OK,
            BodyAsString = indexPage
            // <p>Current architecture: {RuntimeInformation.OSArchitecture}</p>
        };
    }
}