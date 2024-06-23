using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ThermoTsev.IoT;

public static class Endpoints
{
    public static void RegisterCurrentShipmentDataEndpoints(this WebApplication webApplication)
    {
        // Ендпоінт для отримання місцезнаходження вантажу
        webApplication.MapGet("/getCurrentShipmentLocation", () =>
        {
            // Отримання даних про місцезнаходження вантажу
            ShipmentLocation location = DataGenerator.GetShipmentLocation();

            // Перетворення в JSON та відправлення відповіді
            return Results.Json(location);
        });

        // Ендпоінт для отримання стану вантажу
        webApplication.MapGet("/getCurrentShipmentCondition", () =>
        {
            // Отримання даних про стан вантажу
            ShipmentCondition condition = DataGenerator.GetShipmentCondition();

            // Перетворення в JSON та відправлення відповіді
            return Results.Json(condition);
        });
    }
}
