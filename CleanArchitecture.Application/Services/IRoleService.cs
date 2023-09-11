using CleanArchitecture.Application.Features.RoleFeatures.Commands.CreateRole;
using CleanArchitecture.Application.Features.RoleFeatures.Queries;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Services;

public interface IRoleService
{
    Task<MessageResponse> CreateAsync(CreateRoleCommand request);
    Task<bool> RoleExistsAsync(string roleName);
}
