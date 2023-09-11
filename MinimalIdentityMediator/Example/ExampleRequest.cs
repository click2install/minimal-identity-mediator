
namespace MinimalIdentityMediator.Example;

public record class ExampleRequest : IHttpRequest
{
  public string Name { get; init; }

  public int Age { get; init; }
}