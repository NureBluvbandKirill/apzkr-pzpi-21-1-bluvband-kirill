using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ThermoTsev.Backend.BLL.Interfaces;
using ThermoTsev.Backend.Domain.DTO.Shipment;
using ThermoTsev.Backend.Domain.Entities;

namespace ThermoTsev.Backend.API.Controllers;

[Authorize]
[ApiController]
[Route("Api/[controller]")]
public class ShipmentsController(IShipmentService shipmentService, IIoTProviderService ioTProviderService) : ControllerBase
{
    [HttpGet("GetCurrentShipmentLocation/{shipmentId:int}")]
    public async Task<IActionResult> GetCurrentShipmentLocation(int shipmentId)
    {
        Result<ShipmentLocationDto?> result = await ioTProviderService.GetCurrentShipmentLocation(shipmentId);

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors);
    }
    
    [HttpGet]
    public IActionResult GetAllShipments()
    {
        Result<List<Shipment>> result = shipmentService.GetAllShipments();

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetShipmentById(int id)
    {
        Result<Shipment> result = shipmentService.GetShipmentById(id);

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors);
    }

    [HttpPost]
    public IActionResult CreateShipment([FromBody] CreateShipmentDto shipment)
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        Result<CreateShipmentDto> result = shipmentService.CreateShipment(userId, shipment);

        return result.IsSuccess ? Ok("Successfully created") : BadRequest(result.Errors);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateShipment(int id, [FromBody] UpdateShipmentDto updatedShipment)
    {
        if (id != updatedShipment.Id)
            return BadRequest("Invalid Id");

        Result<UpdateShipmentDto> result = shipmentService.UpdateShipment(updatedShipment);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteShipment(int id)
    {
        Result result = shipmentService.DeleteShipment(id);

        return result.IsSuccess ? NoContent() : BadRequest(result.Errors);
    }
}
