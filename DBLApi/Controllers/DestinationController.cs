using DBLApi.Models;
using DBLApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DBLApi.Controllers;

[ApiController]
[Route("api/[controller]")]

    public class DestinationController : ControllerBase
    {
        private readonly IDestinationRepository _destinationRepository;

        public DestinationController(IDestinationRepository destinationRepository)
        {
            _destinationRepository = destinationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Destination>>> GetDestinations()
        {
            return Ok(await _destinationRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Destination>> GetDestination(int id)
        {
            return Ok(await _destinationRepository.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult<Destination>> PostDestination(Destination destination)
        {
            await _destinationRepository.Add(destination);
            return Ok(destination);
        }

        [HttpPut]
        public async Task<ActionResult<Destination>> PutDestination(Destination destination)
        {
            await _destinationRepository.Update(destination);
            return Ok(destination);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Destination>> DeleteDestination(int id)
        {
            var destination = await _destinationRepository.GetById(id);
            if(destination == null){
                return NotFound();
            }
            await _destinationRepository.Delete(destination);
            return Ok(destination);
        }
        
    }
