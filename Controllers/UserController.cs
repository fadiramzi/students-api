using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsManagerMW.Models.Inputs;
using StudentsManagerMW.Services;

namespace StudentsManagerMW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _userService.RegisterUser(model);
            return Ok(result);
        }
    }
}
