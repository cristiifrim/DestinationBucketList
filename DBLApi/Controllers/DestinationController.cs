using DBLApi.Models;
using DBLApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DBLApi.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using DBLApi.Services;
using System.Security.Claims;
using DBLApi.Errors;

namespace DBLApi.Controllers;

[ApiController]
[Route("api/[controller]")]

    public class DestinationController : ControllerBase
    {
        private readonly IDestinationRepository _destinationRepository;
        private readonly JwtTokenService _jwtTokenService;

        public DestinationController(IDestinationRepository destinationRepository, JwtTokenService jwtTokenService)
        {
            _destinationRepository = destinationRepository;
            _jwtTokenService = jwtTokenService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Destination>>> GetDestinations([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await _destinationRepository.GetAll(page, pageSize));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Destination>> GetDestination(int id)
        {
            var destination = await _destinationRepository.GetById(id);

            if(destination == null)
            {
                return NotFound();
            }

            return Ok(destination);
        }

        [HttpGet("public")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Destination>>> GetPublicDestinations([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await _destinationRepository.GetPublicDestinations(page, pageSize));
        }

        [HttpGet("user/{userId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Destination>>> GetUserDestinations(int userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await _destinationRepository.GetUserDestinations(userId, page, pageSize));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<string>> PostDestination(DestinationDto destinationDTO)
        {   
            var token = await HttpContext.GetTokenAsync("access_token");

            if(await _destinationRepository.DestinationTitleExists(destinationDTO.Title))
            {
                throw new DestinationAlreadyExistsException(destinationDTO.Title);
            }

            if(await _destinationRepository.DestinationExists(destinationDTO.Geolocation))
            {
                throw new DestinationAlreadyExistsException(destinationDTO.Geolocation);
            }

            var destination = DestinationDto.ToDestination(destinationDTO);
            var role = _jwtTokenService.GetUserRoleFromToken(token!);

            if(role == Roles.Admin)
            {
                destination.IsPublic = true;
            }
            else
            {
                if(destinationDTO.StartDate == DateTime.MinValue || destinationDTO.EndDate == DateTime.MinValue)
                {
                    throw new InvalidDateException();
                }

                destination.IsPublic = false;
            }

            await _destinationRepository.Add(destination);

            if(role == Roles.User)
            {
                var userId = Int32.Parse(_jwtTokenService.GetUserIdFromToken(token!));
                var userDestination = _destinationRepository.GetDestinationByTitle(destination.Title);

                await _destinationRepository.AddUserDestination(userId, userDestination.Id, destinationDTO.StartDate, destinationDTO.EndDate);
            }

            return Ok(new { Message = "Destination added successfully" });
        }
        
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Destination>> PutDestination([FromRoute] int id,[FromBody] DestinationDto destinationDTO)
        {
            var destination = await _destinationRepository.GetById(id);
            
            if(destination == null)
            {
                throw new DestinationNotFoundException($"id: {id}");
            }

            var token = await HttpContext.GetTokenAsync("access_token");

            var role = _jwtTokenService.GetUserRoleFromToken(token!);

            destination = DestinationDto.ToDestination(destinationDTO);

            await _destinationRepository.Update(destination);

            return Ok(destination);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Destination>> DeleteDestination(int id)
        {
            var destination = await _destinationRepository.GetById(id);

            if(destination == null)
            {
                return NotFound();
            }

            await _destinationRepository.Delete(destination);

            return Ok();
        }

        
        
    }
