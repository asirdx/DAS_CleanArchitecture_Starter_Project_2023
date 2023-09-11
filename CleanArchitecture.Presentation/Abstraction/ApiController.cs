using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Abstraction;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public abstract class ApiController : ControllerBase // bu yapı bütün kontrollerda tekrar edeceği için Presentation içinde Abstract olarak oluşturduk
{
    public readonly IMediator _mediator;

    protected ApiController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
