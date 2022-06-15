using Chapter.WebApi.Interfaces;
using Chapter.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chapter.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _iUsuarioRepository;
        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _iUsuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Lista os usuários cadastrados.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_iUsuarioRepository.Listar());
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Busca um usuário por id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                Usuario usuarioEncontrado = _iUsuarioRepository.BuscarPorId(id);

                if (usuarioEncontrado == null)
                    return NotFound();

                return Ok(usuarioEncontrado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Cadastra um usuário.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            try
            {
                _iUsuarioRepository.Cadastrar(usuario);

                return StatusCode(201);
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Altera um usuário.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Usuario usuario)
        {
            try
            {
                Usuario usuarioEncontrado = _iUsuarioRepository.BuscarPorId(id);

                if (usuarioEncontrado == null)
                    return NotFound();


                _iUsuarioRepository.Atualizar(id, usuario);

                return Ok("Usuário alterado");
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Apaga um usuário.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                Usuario usuarioEncontrado = _iUsuarioRepository.BuscarPorId(id);

                if (usuarioEncontrado == null)
                    return NotFound();


                _iUsuarioRepository.Deletar(id);

                return Ok("Usuário deletado");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
