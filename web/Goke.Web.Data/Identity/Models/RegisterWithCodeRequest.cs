namespace Goke.AspNetCore.Identity.Models;

public class RegisterWithCodeRequest
{
    required public string Email { get; set; }
    required public string Password { get; set; }
    required public string Code { get; set; }
}
