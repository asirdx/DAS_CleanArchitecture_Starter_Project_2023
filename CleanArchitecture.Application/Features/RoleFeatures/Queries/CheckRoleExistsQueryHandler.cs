using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.RoleFeatures.Queries
{
    public class CheckRoleExistsQueryHandler : IRequestHandler<CheckRoleExistsQuery, bool>
    {
        private readonly IRoleService _roleService;

        public CheckRoleExistsQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<bool> Handle(CheckRoleExistsQuery request, CancellationToken cancellationToken)
        {
            return await _roleService.RoleExistsAsync(request.Name);
        }
    }
}
