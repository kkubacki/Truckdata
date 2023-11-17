using ErpSystem.TruckData.Contracts;
using ErpSystem.TruckData.WebApi.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.TruckData.WebApi.Controllers
{
    [Route("api/trucks")]
    [ApiController]
    public class TrucksController : ControllerBase
    {
        private readonly ITruckApplicationService _truckApplicationService;

        public TrucksController(ITruckApplicationService truckApplicationService)
        {
            _truckApplicationService = truckApplicationService;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TruckDto), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 404)]
        public async Task<ActionResult<OperationResultDto<TruckDto>>> GetTruck(Guid id)
        {
            var result = await _truckApplicationService.GetTruckById(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Errors);
            }

            return Ok(result.Data);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TruckDto), 201)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]

        public async Task<ActionResult<OperationResultDto<TruckDto>>> PostTruck([FromBody] SaveTruckRequestModel request)
        {
            var truckDto = MapToDto(request);

            var result = await _truckApplicationService.AddTruck(truckDto);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetTruck), new { id = truckDto.Id }, truckDto);
            }

            return BadRequest(result.Errors);
        }

        [HttpPut]
        [ProducesResponseType(typeof(TruckDto), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]

        public async Task<ActionResult<OperationResultDto<TruckDto>>> PutTruck(Guid id,[FromBody] SaveTruckRequestModel request)
        {
            var truckDto = MapToDto(id, request);

            var result = await _truckApplicationService.UpdateTruck(truckDto);

            if (!result.Errors.Any())
            {
                return Ok(truckDto);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TruckDto>), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        public async Task<IActionResult> GetTruck([FromQuery] TruckQuery truckQuery)
        {
            var result = await _truckApplicationService.GetTrucksAsync(truckQuery);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(TruckDto), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        public async Task<IActionResult> DeleteTruck(Guid id)
        {
            var result = await _truckApplicationService.Delete(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

           
            return Ok(result.Data);
        }

        private static TruckDto MapToDto(Guid truckId,SaveTruckRequestModel request)
        {
            var truckDto = new TruckDto(truckId,request.Code, request.Name, request.Status, request.Description);

            return truckDto;
        }

        private static TruckDto MapToDto(SaveTruckRequestModel request)
        {
            var truckDto = new TruckDto(request.Code, request.Name, request.Status, request.Description);

            return truckDto;
        }
    }
}