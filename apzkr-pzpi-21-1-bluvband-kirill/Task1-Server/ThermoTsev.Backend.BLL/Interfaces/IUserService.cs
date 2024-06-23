using ThermoTsev.Backend.Domain.DTO.User;
using ThermoTsev.Backend.Domain.Entities;

namespace ThermoTsev.Backend.BLL.Interfaces;

public interface IUserService
{
    Task<User?> LoginAsync(string email, string password);

    Task<bool> SignUpAsync(SignUpDto signUpDto);

    Task<IEnumerable<User?>> GetUsersAsync();

    Task<User?> GetUserAsync(int id);

    Task CreateUserAsync(CreateUserDto user);

    Task UpdateUserAsync(int id, UpdateUserDto user);

    Task DeleteUserAsync(int id);
}
