using Microsoft.AspNetCore.Mvc;
using ApiAuth.Models;
using ApiAuth.Repositorio;
using Microsoft.AspNetCore.Authorization;
using ApiAuth.Services;

namespace ApiAuth.Controllers
{
    [Route("v1/account")]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("login")]

        public async Task<ActionResult<dynamic>> Authenticate([FromBody]User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);

            if (user == null)
                return NotFound(new { message = "Usuario ou senha invalidos"});

                var token = TokenService.GenerateToken(user);
                user.Password = "";
                return new
                {
                    user = user,
                    token = token
                };
        }
        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";
    }
}