using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetAllCar;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistance.Context;
using CleanArchitecture.Presentation.Abstraction;
using CleanArcihtecture.Infrastructure.Authorization;
using EntityFrameworkCorePagination.Nuget.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers;

//[Authorize(AuthenticationSchemes = "Bearer")] // tek tek buraya yazmak yerine Presentat,on.Abstraction içinde ApiControllerA eklersek tekrarları önlemiş olacağız.
public sealed class CarsController : ApiController
{
    private readonly AppDbContext _context; //Repository pattern öncesi bunu kullanıyorduk
    public CarsController(AppDbContext context, IMediator mediator) : base(mediator) {
        _context = context;  }

    //[TypeFilter(typeof(RoleAttribute), Arguments = new Object[] {"Create"})] //RoleFilter'i oluşturnadan önceki hali
    [RoleFilter("Create")]
    [HttpPost("[action]")]
    public async Task<IActionResult> Create(CreateCarCommand request, CancellationToken cancellationToken)
    {
        MessageResponse response =  await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [RoleFilter("GetAll")]
    [HttpPost("[action]")]
    public async Task<IActionResult> GetAll(GetAllCarQuery request, CancellationToken cancellationToken)
    {
        IList<Car> cars = new  List<Car>();

        for (int i = 0; i < 10000; i++)
        {
            Car car = new()
            {
                Name = "Car-" + i,
                Model = "Model-" + i,
                EnginePower = i + 10,
            };
            cars.Add(car);
        }
        await _context.Set<Car>().AddRangeAsync(cars);
        await _context.SaveChangesAsync(cancellationToken);

        PaginationResult<Car> response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    public IActionResult Calculate() // Exception Middleware'in çalışma şeklini görmek için ürettik. Swagger'da GEt methoduna try yaptığımızda hataları görebileceğiz.
    {
        int x = 0;
        int y = 0;
        int result = x / y;

        return Ok();
    }
}
