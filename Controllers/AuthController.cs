using CreditCardAPI;
using CreditCardAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
             var msg =await _service.Register(dto);
            return Ok(msg);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    { 

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        
            var token = await _service.Login(dto);
            return Ok(new { token });
        
       
    }
}