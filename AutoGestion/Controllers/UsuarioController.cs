using AutoGestion.interfaces.IUsuario;
using AutoGestion.models.Usuario;
using AutoGestion.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoGestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            try
            {

                var usuarios = await _usuarioService.GetUsuarios();

                return Ok(usuarios);
            }
            catch (Exception)
            {
                throw;
            }


        }
        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuariosActivos()
        {
            try
            {
                var usuarios = await _usuarioService.GetUsuariosActivos();

                return Ok(usuarios);
            }
            catch (Exception)
            {
                throw;
            }


        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarioById(string id)
        {
            try
            {
                var usuarios = await _usuarioService.GetUsuarioById(id);

                return Ok(usuarios);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult<Usuario>> CreateUsuarios(Usuario usuario)
        {
            try
            {

                var usuarios = await _usuarioService.PostUsuario(usuario);
                return CreatedAtAction(nameof(CreateUsuarios), new { id = usuarios.id }, usuarios);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioDto>> UpdateUsuarios(string id, Usuario usuario)
        {
            try
            {
                var usuarioActualizado = await _usuarioService.PutUsuario(id, usuario);

                return Ok(usuarioActualizado);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
