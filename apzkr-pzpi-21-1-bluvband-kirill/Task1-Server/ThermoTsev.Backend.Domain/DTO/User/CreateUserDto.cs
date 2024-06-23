using ThermoTsev.Backend.Domain.Enums;

namespace ThermoTsev.Backend.Domain.DTO.User;

public record CreateUserDto(string Name, string Email, string Password, Role Role);
