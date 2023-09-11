using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Features.RoleFeatures.Queries
{
    public sealed record CheckRoleExistsQuery(string Name) : IRequest<bool>;
}
