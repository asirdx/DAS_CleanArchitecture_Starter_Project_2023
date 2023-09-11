using CleanArchitecture.Application.Features.UserRoleFeatures.Commands.CreateUserRole;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using GenericRepository;

namespace CleanArchitecture.Persistance.Services;

public sealed class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    public UserRoleService(IUserRoleRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateAsync(CreateUserRoleCommand request, CancellationToken cancellationToken)
    {

        // Check if the user already has the requested RoleId
        //bool userHasRole = await _repository.UserHasRoleAsync(request.UserId, request.RoleId);
        //if (userHasRole)
        //{
        //    throw new Exception("User already has the requested role.");
        //}

        UserRole userRole = new()
        {
            RoleId = request.RoleId,
            UserId = request.UserId
        };

        await _repository.AddAsync(userRole, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
