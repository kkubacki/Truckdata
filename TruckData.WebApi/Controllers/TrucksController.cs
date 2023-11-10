using Microsoft.AspNetCore.Mvc;
using TruckData.Contracts;
using TruckData.Domain.Entities;
using TruckData.Domain.Enums;

namespace TruckData.WebApi.Controllers
{
    [Route("api/trucks")]
    [ApiController]
    public class TrucksController : ControllerBase
    {
        private readonly ITruckRepository _repository;

        public TrucksController(ITruckRepository repository)
        {
            _repository = repository;
        }

        // GET: api/trucks
        [HttpGet]
        public IEnumerable<Truck> GetTrucks()
        {
            return _repository.GetAll();
        }

        // GET: api/trucks/{id}
        [HttpGet("{id}")]
        public ActionResult<Truck> GetTruck(Guid id)
        {
            var truck = _repository.GetById(id);
            if (truck == null)
            {
                return NotFound();
            }

            return truck;
        }

        // POST: api/trucks
        [HttpPost]
        public ActionResult<Truck> PostTruck(Truck truck)
        {
            _repository.Add(truck);
            return CreatedAtAction(nameof(GetTruck), new { id = truck.Id }, truck);
        }

        // PUT: api/trucks/{id}
        [HttpPut("{id}")]
        public IActionResult PutTruck(Truck updatedTruck)
        {
            var existingTruck = _repository.GetById(updatedTruck.Id);
            if (existingTruck == null)
            {
                return NotFound();
            }            

            _repository.Update(existingTruck);

            return NoContent();
        }

        // DELETE: api/trucks/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTruck(Guid id)
        {
            var truck = _repository.GetById(id);
            if (truck == null)
            {
                return NotFound();
            }

            _repository.Delete(id);

            return NoContent();
        }
    }
}