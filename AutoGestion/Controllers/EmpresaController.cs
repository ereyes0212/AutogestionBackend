using AutoGestion.models.Empresa;
using AutoGestion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoGestion.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;
        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresaDto>>> GetEmpresas()
        {
            try
            {
                var empresas = await _empresaService.GetEmpresas();

                return Ok(empresas);
            }
            catch (Exception ex)
            {
                throw;
            }

        }   
        
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<EmpresaDto>>> GetEmpresasActivas()
        {
            try
            {
                var empresas = await _empresaService.GetEmpresasActivas();

                return Ok(empresas);
            }
            catch (Exception)
            {
                throw;
            }
        }        

        [HttpGet("{id}")]
        public async Task<ActionResult<EmpresaDto>> GetEmpresaById(string id)
        {
            try
            {
                var empresa = await _empresaService.GetEmpresaById(id);

                return Ok(empresa);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Empleado>> CreatePost(Empresa empresa)
        {
            try
            {
                var empresaCreate = await _empresaService.PostEmpresa(empresa);
                return Ok(empresaCreate);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Empleado>> UpdateEmpresa(Empresa empresa, string id)
        {
            try
            {
                var empresaUpdate = await _empresaService.PutEmpresa(id, empresa);

                return Ok(empresaUpdate);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
