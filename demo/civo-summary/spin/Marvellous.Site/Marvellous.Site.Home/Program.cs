using Fermyon.Spin.Sdk;

namespace Marvellous.Site.Home;

public static class Handler
{
    [HttpHandler]
    public static HttpResponse HandleHttpRequest(HttpRequest request)
        => Router.Handle(request);
}