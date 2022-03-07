using EventSourcingCqrs.SharedKernel.DomainValidations;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcingCqrs.API.Common;

[ApiController]
[Route("[controller]")]
public class BaseController : ControllerBase
{
    private readonly IDomainValidationPort _domainValidator;

    public BaseController(IDomainValidationPort domainValidator)
    {
        _domainValidator = domainValidator;
    }

    protected IActionResult OkResult(object content = null)
    {
        var errors = _domainValidator.GetFails().ToList();

        if (errors.Any(e => e.Type == FailType.NotFound))
            return NotFound("Not Found!");

        if (errors.Any())
            return BadRequest(errors);

        return content != null ? 
            Ok(content): 
            Ok();
    }
}