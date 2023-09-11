using CleanArchitecture.Domain.Entities;
using GenericRepository;

namespace CleanArchitecture.Domain.Repositories;

public interface IUserRoleRepository : IRepository<UserRole>
{
    //Task<bool> UserHasRoleAsync(string userId, string roleId);
}
