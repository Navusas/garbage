using System.IO;

namespace Marvellous.Site.Home.PageHandlers;

public class PageBuilder
{
    private readonly string _relativePathToPage;
    private string? _body;

    public PageBuilder(string relativePathToPage)
    {
        _relativePathToPage = relativePathToPage;
    }

    public PageBuilder WithBody(string? body)
    {
        _body = body;
        return this;
    }

    public string Build()
    {
        var template = File.ReadAllText($"/assets/{_relativePathToPage}");

        return template
            .Replace(" {{ body }}", _body);
    }
}