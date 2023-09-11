using FluentValidation;
using Mediator;

namespace MinimalIdentityMediator;

public sealed class RequestValidatorBehavior<TMessage, TResult> : IPipelineBehavior<TMessage, IResult>
  where TMessage : IHttpRequest
{
  public IValidator<TMessage> _validator { get; }

  public RequestValidatorBehavior(IValidator<TMessage> validator)
  {
    _validator = validator;
  }

  public async ValueTask<IResult> Handle(
    TMessage message,
    MessageHandlerDelegate<TMessage, IResult> next,
    CancellationToken cancellationToken
  )
  {
    var result = await _validator.ValidateAsync(message);
    if (!result.IsValid)
    {
      return new ValidationFailedResult(result.Errors);
    }

    return await next(message, cancellationToken);
  }
}
