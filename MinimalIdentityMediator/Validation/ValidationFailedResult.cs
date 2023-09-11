using FluentValidation.Results;

namespace MinimalIdentityMediator;

public sealed class ValidationFailedResult : IResult
{
  public int Code { get; }

  public string Message { get; }

  public string TraceId { get; }

  public string Type { get; }

  public IEnumerable<string> Errors { get; }

  public ValidationFailedResult(IEnumerable<ValidationFailure> errors)
  {
    Code = 400;
    Message = "Validation failure.";
    TraceId = $"00-{Guid.NewGuid():N}-{Guid.NewGuid().ToString("N")[..16]}-00";
    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
    Errors = errors.Select(x => x.ErrorMessage);
  }

  public async Task ExecuteAsync(HttpContext httpContext)
  {
    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
    httpContext.Response.ContentType = "application/json";

    await httpContext.Response.WriteAsJsonAsync(this);
  }
}
