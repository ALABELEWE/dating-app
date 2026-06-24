using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers;



[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> GetUsers()
    {
        return Ok("users endpoint working");
    }   
}