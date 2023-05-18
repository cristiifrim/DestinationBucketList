using DBLApi.Models;
using DBLApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DBLApi.DTOs;
using Microsoft.EntityFrameworkCore;

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
            var destination = await _destinationRepository.GetById(id);

            if(destination == null)
            {
                return NotFound();
            }

            return Ok(destination);
        }

        [HttpPost]
        public async Task<ActionResult<Destination>> PostDestination(DestinationDTO destinationDTO)
        {
            //check if destination already exists in the database
            
            if(await _destinationRepository.DestinationExists(destinationDTO.Title))
            {
                return Conflict();
            }

            var destination = new Destination
            {
                Geolocation = destinationDTO.Geolocation,
                Title = destinationDTO.Title,
                Image = destinationDTO.Image,
                Description = destinationDTO.Description,
                StartDate = destinationDTO.StartDate,
                EndDate = destinationDTO.EndDate
            };

            await _destinationRepository.Add(destination);
            return CreatedAtAction(nameof(GetDestination), new { id = destination.Id }, destination);

        }
        
        [HttpPut]
        public async Task<ActionResult<Destination>> PutDestination([FromRoute] int id,[FromBody] DestinationDTO destinationDTO)
        {
            var destination = await _destinationRepository.GetById(id);
            if(destination == null)
            {
                return NotFound();
            }

            destination.Geolocation= destinationDTO.Geolocation;
            destination.Title = destinationDTO.Title;
            destination.Image = destinationDTO.Image;
            destination.Description = destinationDTO.Description;
            destination.StartDate = destinationDTO.StartDate;
            destination.EndDate = destinationDTO.EndDate;

            return NoContent();
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
