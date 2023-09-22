using System.Net;
using System.IO;
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
            "/" => PageResponseGenerator.Generate(ReadFileContents("DashboardComponent.html")),
            "/users" => PageResponseGenerator.Generate(ReadFileContents("LatestUsersTable.html")),
            "/acquisition" => PageResponseGenerator.Generate(ReadFileContents("AcquisitionOverview.html")),
            _ => new HttpResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                BodyAsString = "Not found",
            },
        };
    }

    public static string ReadFileContents(string filePath)
    {
        return File.ReadAllText($"/assets/{filePath}");
    }
}