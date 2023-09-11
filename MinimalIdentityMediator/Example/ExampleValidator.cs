using FluentValidation;

namespace MinimalIdentityMediator.Example;

public sealed class ExampleValidator : AbstractValidator<ExampleRequest>
{
  public ExampleValidator()
  {
    RuleFor(x => x.Age).GreaterThan(10);
    RuleFor(x => x.Name).Length(3, 10);
  }
}
