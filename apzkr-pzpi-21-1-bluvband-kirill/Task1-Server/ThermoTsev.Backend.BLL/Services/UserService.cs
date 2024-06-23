using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.DAL;
using ThermoTsev.Backend.Domain.DTO.User;
using ThermoTsev.Backend.Domain.Entities;
using ThermoTsev.Backend.Domain.Enums;

namespace ThermoTsev.Backend.BLL.Services;

public class UserService(DataContext context) : IUserService
{
    public async Task<IEnumerable<User?>> GetUsersAsync()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<User?> GetUserAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task CreateUserAsync(CreateUserDto user)
    {
        ArgumentNullException.ThrowIfNull(user);

        User? foundedUser = await context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        if (foundedUser is not null)
        {
            throw new ArgumentException("User already exists.");
        }

        (string hashedPassword, string salt) = HashPassword(user.Password);

        context.Users.Add(
            new User
            {
                Name = user.Name,
                Role = user.Role,
                Email = user.Email,
                HashedPassword = hashedPassword,
                PasswordSalt = salt,
            }
        );

        await context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(int id, UpdateUserDto user)
    {
        ArgumentNullException.ThrowIfNull(user);

        User? userDb = await context.FindAsync<User>(id);

        if (userDb == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        userDb.Name = user.Name;
        userDb.Email = user.Email;
        userDb.Role = user.Role;

        context.Users.Update(userDb);

        await context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        User? user = await context.Users.FindAsync(id);
        if (user != null)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }
    }

    public async Task<bool> SignUpAsync(SignUpDto signUpDto)
    {
        User? existingUser = await context.Users.FirstOrDefaultAsync(u => u.Email == signUpDto.Email);

        if (existingUser != null)
        {
            return false;
        }

        (string hashedPassword, string salt) = HashPassword(signUpDto.Password);

        User newUser = new User
        {
            Name = signUpDto.Name,
            Role = Role.User,
            Email = signUpDto.Email,
            HashedPassword = hashedPassword,
            PasswordSalt = salt,
        };

        context.Users.Add(newUser);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        User? user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user != null && IsPasswordValid(
                password,
                user.HashedPassword,
                user.PasswordSalt
            ))
        {
            return user;
        }

        return null;
    }

    private static (string hashedPassword, string salt) HashPassword(string password)
    {
        byte[] saltBytes = RandomNumberGenerator.GetBytes(16);
        byte[] combinedBytes = Encoding.UTF8
            .GetBytes(password)
            .Concat(saltBytes)
            .ToArray();
        byte[] hashedBytes = SHA256.HashData(combinedBytes);
        string hashedPassword = Convert.ToBase64String(hashedBytes);

        return (hashedPassword, Convert.ToBase64String(saltBytes));
    }

    private static bool IsPasswordValid(string enteredPassword, string storedPassword, string salt)
    {
        byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);
        byte[] saltBytes = Convert.FromBase64String(salt);
        byte[] saltedPasswordBytes = new byte[enteredPasswordBytes.Length + saltBytes.Length];
        Array.Copy(
            enteredPasswordBytes,
            saltedPasswordBytes,
            enteredPasswordBytes.Length
        );
        Array.Copy(
            saltBytes,
            0,
            saltedPasswordBytes,
            enteredPasswordBytes.Length,
            saltBytes.Length
        );
        byte[] hashedBytes = SHA256.HashData(saltedPasswordBytes);
        string enteredHash = Convert.ToBase64String(hashedBytes);

        return string.Equals(enteredHash, storedPassword);
    }
}
