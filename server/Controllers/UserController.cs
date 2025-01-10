using Microsoft.AspNetCore.Mvc;
using server.Models;
using AutoMapper;
using server.Services.Interfaces;
using server.DTOs;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper) 
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            var users = await _userService.Get();
            var userDto = _mapper.Map<List<UserDTO>>(users);

            return Ok(userDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            var user = await _userService.GetById(id);
            var userDto = _mapper.Map<UserDTO>(user);

            return user is not null ? Ok(userDto) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Create(UserDTO newUserDto)
        {
            Console.WriteLine(newUserDto);

            var newUser = _mapper.Map<User>(newUserDto);
            var createdUser = await _userService.Create(newUser);
            var createdUserDto = _mapper.Map<UserDTO>(createdUser);

            return CreatedAtAction(nameof(GetById), new { id = createdUserDto.Id }, createdUserDto);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, UserUpdateDTO userUpdateDto)
        {
            var user = await _userService.GetById(id);

            if (user is null) return NotFound();

            _mapper.Map(userUpdateDto, user);

            await _userService.UpdateById(id, user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetById(id);
            if (user is null) return NotFound();

            await _userService.DeleteById(id);
            return Ok();
        }
    }
}
