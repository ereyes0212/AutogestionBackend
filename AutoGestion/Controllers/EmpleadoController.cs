using AutoGestion.interfaces.IEmpleado;
using AutoGestion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoGestion.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadoController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados()
        {
            try
            {
                var empleados = await _empleadoService.GetEmpleados();

                return Ok(empleados);
            }
            catch (Exception)
            {
                throw;
            }

        }  
        [HttpGet("disponibles")]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleadosdisponibles()
        {
            try
            {
                var empleados = await _empleadoService.GetEmpleadosDisponibles();

                return Ok(empleados);
            }
            catch (Exception)
            {
                throw;
            }

        }       
        [HttpGet("profile")]
        public async Task<ActionResult<Empleado>> GetProfile()
        {
            try
            {
                var empleados = await _empleadoService.GetProfile();

                return Ok(empleados);
            }
            catch (Exception)
            {
                throw;
            }

        }        

        [HttpPost]
        public async Task<ActionResult<EmpleadoDTO>> CreateEmpleados([FromBody] EmpleadoCreateDto empleadoCreateDto)
        {
            try
            {
                var empleadoCreate = await _empleadoService.PostEmpleados(empleadoCreateDto);

                return Ok(empleadoCreate);
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<Empleado>> UpdateEmpleados(EmpleadoCreateDto empleado, string id)
        {
            try
            {
                var empleadoCreate = await _empleadoService.PutEmpleados(id, empleado);

                return Ok(empleadoCreate);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> EmpleadosById(string id)
        {

            try
            {
                var empleadoCreate = await _empleadoService.GetEmpleadoById(id);
                return Ok(empleadoCreate);
            }
            catch (Exception )
            {
                throw;
            }

        }

        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleadosActivos()
        {
            try
            {
                var empleados = await _empleadoService.GetEmpleadosActivos();
                return Ok(empleados);
            }
            catch (Exception )
            {
                throw;
            }

        }        

    }
}
