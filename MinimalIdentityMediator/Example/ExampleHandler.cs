using Mediator;

namespace MinimalIdentityMediator.Example;

public sealed class ExampleHandler : IRequestHandler<ExampleRequest, IResult>
{
  public async ValueTask<IResult> Handle(ExampleRequest request, CancellationToken cancellationToken)
  {
    await ValueTask.CompletedTask;

    return Results.Ok(new
    {
      Message = $"The age was {request.Age} and the name was {request.Name}"
    });
  }
}
