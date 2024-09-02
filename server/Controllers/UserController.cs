using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        public UserController(UserService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            var users = await _service.Get();
            Console.WriteLine(users);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            User? user = await _service.GetById(id);

            if (user is not null) return Ok(user);
            else return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post(User newUser)
        {
            Console.WriteLine(newUser);
            var user = await _service.Create(newUser);
            return CreatedAtAction(nameof(Post), new { id = newUser.Id }, newUser);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, User user)
        {
            if(id != user.Id) return BadRequest();

            var userToUpdate = await _service.GetById(id);

            if (userToUpdate is not null)
            {
                await _service.UpdateById(id, user);
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _service.GetById(id);
            
            if(user is not null)
            {
                await _service.DeleteById(id);
                return Ok();
            }
            return NotFound();
        }
    }
}
