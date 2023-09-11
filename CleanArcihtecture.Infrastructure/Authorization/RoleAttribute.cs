using CleanArchitecture.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CleanArcihtecture.Infrastructure.Authorization;

public sealed class RoleAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _role;
    private readonly IUserRoleRepository _userRoleRepository;
    public RoleAttribute(string role, IUserRoleRepository userRoleRepository)
    {
        _role = role;
        _userRoleRepository = userRoleRepository;
    }

    public void OnAuthorization(AuthorizationFilterContext context) // context ile header içindeki Jwt dahil bütün değerleri okuyabiliyoruz
    {
        var userIdClaim = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if(userIdClaim == null) 
        {
            context.Result = new UnauthorizedResult(); //401 hatası döner
            return;
        }

        var userHasRole = 
            _userRoleRepository
            .GetWhere(p=> p.UserId == userIdClaim.Value)
            .Include(p=> p.Role)
            .Any(p=> p.Role.Name == _role);

        if (!userHasRole)
        {
            context.Result = new UnauthorizedResult(); //401 hatası döner
            return;
        }

        // burada iflerin içine düşmüyorsa demek ki user Authorized dir ve code otomatikman Controller'ın ilgili methodundan devam eder. 
    }
}
