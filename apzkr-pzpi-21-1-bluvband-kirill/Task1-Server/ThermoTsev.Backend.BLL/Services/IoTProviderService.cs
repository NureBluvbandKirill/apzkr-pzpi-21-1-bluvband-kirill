using FluentResults;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.DAL;
using ThermoTsev.Backend.Domain.DTO.Shipment;
using ThermoTsev.Backend.Domain.Entities;

namespace ThermoTsev.Backend.BLL.Services;

public class IoTProviderService(DataContext context, IConfiguration configuration) : IIoTProviderService
{
    private readonly HttpClient _httpClient = new();

    private readonly string _apiUrl = configuration.GetSection("IoT:ApiUrl")
        .Value!;

    public async Task<Result<ShipmentLocationDto?>> GetCurrentShipmentLocation(int shipmentId)
    {
        try
        {
            // Get shipment by id
            Shipment? shipment = context.Shipments.FirstOrDefault(s => s.Id == shipmentId);

            if (shipment == null)
                return Result.Fail<ShipmentLocationDto?>("Shipment not found");

            // Make an http request to API
            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}/getCurrentShipmentLocation?shipmentId={shipmentId}");

            if (!response.IsSuccessStatusCode)
                return Result.Fail<ShipmentLocationDto?>($"Failed to retrieve location from API. Status code: {response.StatusCode}");

            // Get content as JSON string
            string content = await response.Content.ReadAsStringAsync();
            ShipmentLocationDto? locationDto = JsonConvert.DeserializeObject<ShipmentLocationDto>(content);

            // Return mapped success response as DTO
            return Result.Ok(locationDto);
        }
        catch (Exception ex)
        {
            return Result.Fail<ShipmentLocationDto?>(ex.Message);
        }
    }

    public async Task<Result<ShipmentInfoDto?>> GetCurrentShipmentInfo(int shipmentId)
    {
        try
        {
            // Get shipment by id
            Shipment? shipment = context.Shipments.FirstOrDefault(s => s.Id == shipmentId);

            if (shipment == null)
                return Result.Fail<ShipmentInfoDto?>("Shipment not found");

            // Make an http request to API
            HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}/getCurrentShipmentCondition?shipmentId={shipmentId}");

            if (!response.IsSuccessStatusCode)
                return Result.Fail<ShipmentInfoDto?>($"Failed to retrieve condition from API. Status code: {response.StatusCode}");

            // Get content as JSON string
            string content = await response.Content.ReadAsStringAsync();
            ShipmentInfoDto? conditionDto = JsonConvert.DeserializeObject<ShipmentInfoDto>(content);

            // Return mapped success response as DTO
            return Result.Ok(conditionDto);
        }
        catch (Exception ex)
        {
            return Result.Fail<ShipmentInfoDto?>(ex.Message);
        }
    }
}
