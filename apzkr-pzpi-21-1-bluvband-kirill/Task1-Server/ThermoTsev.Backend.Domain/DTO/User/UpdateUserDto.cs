using ThermoTsev.Backend.Domain.Enums;

namespace ThermoTsev.Backend.Domain.DTO.User;

public record UpdateUserDto(string Name, string Email, Role Role);
