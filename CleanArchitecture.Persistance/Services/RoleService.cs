using CleanArchitecture.Application.Features.RoleFeatures.Commands.CreateRole;
using CleanArchitecture.Application.Features.RoleFeatures.Queries;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistance.Services;

public sealed class RoleService : IRoleService
{
    private readonly RoleManager<Role> _roleManager;

    public RoleService(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<MessageResponse> CreateAsync(CreateRoleCommand request)
    {
        bool roleExists = await RoleExistsAsync(request.Name);
        if (roleExists)
        {
            return new MessageResponse("Role already exists.");
        }

        Role role = new()
        {
            Name = request.Name
        };

        await _roleManager.CreateAsync(role);
        return new MessageResponse("Rol kaydı başarıyla tamamlandı!");
    }

    public async Task<bool> RoleExistsAsync(string roleName)
    {
        return await _roleManager.RoleExistsAsync(roleName);
    }
}
