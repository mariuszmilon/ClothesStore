using Microsoft.AspNetCore.Mvc;
using ClothesStore.Models;
using ClothesStore.Services;

namespace NoticeBoard.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accauntService;

        public AccountController(IAccountService accountService)
        {
            _accauntService = accountService;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody]RegisterUserDto dto)
        {
            _accauntService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginUserDto dto)
        {
            string token = _accauntService.GenerateJwt(dto);
            return Ok(token);
        }
    }
}
