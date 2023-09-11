using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Features.RoleFeatures.Commands.CreateRole;

public sealed class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, MessageResponse>
{
    private readonly IRoleService _roleService;

    public CreateRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<MessageResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        //Deneme için check'i burada yaptım ancak best practice olarak sonradan RoleService içine taşıdım.
        //// Check if the role already exists
        //bool roleExists = await _roleService.RoleExistsAsync(request.Name);
        //if (roleExists)
        //{
        //    return new MessageResponse("Role already exists.");
        //}

        //// Role doesn't exist, create it
        //await _roleService.CreateAsync(request);
        //return new MessageResponse("Rol kaydı başarıyla tamamlandı!");

        // kontrol eklediğimizden, bu mesajı RoleService'den dönüyoruz.
        return await _roleService.CreateAsync(request);
    }
}
