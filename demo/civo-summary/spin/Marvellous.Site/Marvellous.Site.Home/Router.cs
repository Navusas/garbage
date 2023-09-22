using Fermyon.Spin.Sdk;
using Marvellous.Site.Home.PageHandlers;

namespace Marvellous.Site.Home;

public static class Router
{
    public static HttpResponse Handle(HttpRequest request)
    {
        // find which path was hit, and switch on it
        return request.Url switch
        {
            "/" => PageResponseGenerator.Generate("Hello from .NET"),
            "/hello" => new HttpResponse
            {
                StatusCode = HttpStatusCode.OK
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