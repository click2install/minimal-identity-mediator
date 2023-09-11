using Mediator;

namespace MinimalIdentityMediator.Extensions;

public static class WebApplicationExtensions
{
  public delegate RouteHandlerBuilder VerbEndpointDelegate(string template, Delegate handler);

  public static WebApplication Mediate<TRequest>(
    this WebApplication app,
    Func<IEndpointRouteBuilder, VerbEndpointDelegate> expression,
    string template) where TRequest : notnull, IHttpRequest
  {
    var endpoint = expression.Invoke(app);
    endpoint.Invoke(template, async (IMediator mediator, [AsParameters] TRequest request) => await mediator.Send(request));

    return app;
  }
}
