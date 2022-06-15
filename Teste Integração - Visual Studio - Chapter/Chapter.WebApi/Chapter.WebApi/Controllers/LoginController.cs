using Chapter.WebApi.Interfaces;
using Chapter.WebApi.Models;
using Chapter.WebApi.Repositories;
using Chapter.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Chapter.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController: ControllerBase
    {
        private readonly IUsuarioRepository _iUsuarioRepository;

        public LoginController(IUsuarioRepository iUsuarioRepository)
        {
            _iUsuarioRepository = iUsuarioRepository;
        }

        /// <summary>
        /// Método para fazer o login no sistema
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            Usuario usuarioEncontrado = _iUsuarioRepository.Login(login.Email, login.Senha);

            if (usuarioEncontrado == null)
            {
                return Unauthorized(new {msg = "E-mail e/ou senha inválidos"});
            }

            var myClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuarioEncontrado.Email),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioEncontrado.Id.ToString()),
                new Claim(ClaimTypes.Role, usuarioEncontrado.Tipo)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chapter-chave-autenticacao"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                   issuer: "chapter.webapi",
                   audience: "chapter.webapi",
                   claims: myClaims,
                   expires: DateTime.Now.AddMinutes(60),
                   signingCredentials: creds
            );

            return Ok( new { token = new JwtSecurityTokenHandler().WriteToken(token) } );
        }

    }
}
