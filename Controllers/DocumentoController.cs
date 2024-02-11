using PersonaCompleta.Models;
using PersonaCompleta.Models.DTO;
using PersonaCompleta.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using PersonaCompleta.Repositorio.IRepositorio;
using PersonaCompleta.Repositorio;
using System.Net;

namespace PersonaCompleta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private readonly IPersonaRepositorio _persorepo;
        private readonly ILogger<DocumentoController> _logger;
        protected ApiResponse _Response;
        private readonly IMapper _mapper;
        public DocumentoController (ILogger<DocumentoController> logger, IPersonaRepositorio repos , IMapper map )
        {
            _persorepo = repos;
            _logger = logger;
            _mapper = map;
            _Response = new();

        }

        
          


        [HttpGet]
        [ProducesResponseType(200)]
        public async Task <ActionResult<ApiResponse>> Get()
        {
            try
            {
                _logger.LogInformation("Obtener Documentos");

            IEnumerable<Persona> per = await _persorepo.ObtenerTodos();

            
                _Response.Resultado = per;
                _Response.statuscode =HttpStatusCode.OK;
                return Ok(_Response);
            }
            catch (Exception error)
            {
                _Response.IsSucceful    = false;
                _Response.ErrorMessages = new List<string>() { error.ToString() };
                
            }
            return _Response;
        }


        [HttpGet("Nombre:string", Name = "RUTA")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<ApiResponse>> Coincidencias(string Nomrbe)
        {
            try
            {
                if (Nomrbe == "")
                {
                    _Response.statuscode = HttpStatusCode.BadRequest;
                    return BadRequest(_Response);
                }
                var perso = await _persorepo.Obtener(v => v.nombre == Nomrbe);
                if (perso == null)
                {
                    _Response.statuscode = HttpStatusCode.NotFound;
                    return NotFound(_Response);
                }
                _Response.Resultado = _mapper.Map<PersonaDTO>(perso);
                _Response.statuscode = System.Net.HttpStatusCode.OK;
                return Ok(_Response);
            }
            catch (Exception error)
            {
                _Response.ErrorMessages = new List<string>() {error.ToString()};
                _Response.IsSucceful = false;
                
            }
            return _Response;


        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task <ActionResult<ApiResponse>> CrearDocumento([FromBody] PersonaDTO tumulto)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("nombre", "Maximo de caracteres");
                    return BadRequest(ModelState);
                }

                var perso = await _persorepo.Obtener(v => v.documento == tumulto.documento);

                if (perso != null)
                {
                    ModelState.AddModelError("DocRepetido", "EL DOCUMENTO YA EXISTE, ME ESTAS CAGANDO");
                    return BadRequest(ModelState);
                }
                if (tumulto == null)
                {
                    _logger.LogInformation("Carga erronea de datos");
                    return BadRequest(tumulto);
                }
                if (tumulto.edad > 100 && tumulto.edad <= 0)
                {
                    _logger.LogInformation("EDAD No aceptada");
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                Persona per = _mapper.Map<Persona>(tumulto);
                per.imagen = "";
                await _persorepo.Crear(per);
                _Response.Resultado = per;
                _Response.statuscode = HttpStatusCode.Created;
                return CreatedAtRoute("Coincidencias",new { id=per.id }, _Response);
            }
            catch (Exception error)
            {
                _Response.ErrorMessages = new List<string>() { error.ToString() };
                _Response.IsSucceful = false;
                
            }
            return _Response;

        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    _Response.statuscode = HttpStatusCode.BadRequest;
                    _Response.IsSucceful = false;
                    return BadRequest(_Response);
                }
                var per = await _persorepo.Obtener(v => v.id == id);
                if (per == null)
                {
                    _Response.statuscode = HttpStatusCode.NotFound;
                    return NotFound(_Response);
                }
                await _persorepo.Remove(per);
                _persorepo.Grabar();
                _Response.statuscode = HttpStatusCode.NoContent;
                return Ok(_Response);
            }
            catch (Exception error)
            {
                _Response.ErrorMessages = new List<string> { error.ToString() };
                _Response.IsSucceful = false;        
            }
            return BadRequest(_Response);

        }


        [HttpPut("doc:int")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task <IActionResult> UpdatePerso(int DNI , [FromBody] PersonaUpdateDTO perso)
        {

                if (perso == null || DNI != perso.documento)
                {
                    _Response.statuscode = HttpStatusCode.BadRequest;
                    _Response.IsSucceful = false;
                    return BadRequest(_Response);
                }
                var Id = await _persorepo.Obtener(v => v.documento == DNI, track: false);
                Persona modelo = _mapper.Map<Persona>(perso);
                modelo.id = Id.id;
                modelo.imagen = "";
                await _persorepo.Actualizar(modelo);
                _Response.statuscode = HttpStatusCode.NoContent;
                _Response.Resultado = modelo;
                return Ok(_Response);            

        }

        
        [HttpPatch("doc:int")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task <IActionResult> UpPerso(int id, JsonPatchDocument <PersonaDTO> perso)
        {
            if (perso == null)
            {
                return BadRequest();
            }
            var per = await _persorepo.Obtener(v => v.id == id, track:false);

            PersonaDTO modelo = new()
            {
                nombre = per.nombre,
                segundonombre = per.segundonombre,
                apellido = per.apellido,
                edad = per.edad,
                sexo = per.sexo,
                documento = per.documento,
                nacionalidad = per.nacionalidad,
                fecha = per.fecha,
            };

            if (per == null)
            {
                return BadRequest();
            }
            perso.ApplyTo(modelo, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Persona reem = new()
            {
                id=id,
                nombre=modelo.nombre,
                apellido=modelo.apellido,
                segundonombre=modelo.segundonombre,
                edad=modelo.edad,
                sexo=modelo.sexo,
                documento=modelo.documento,
                fecha=modelo.fecha,
                nacionalidad=modelo.nacionalidad,
                imagen=""
            };

            _persorepo.Actualizar(reem);
            _Response.statuscode = HttpStatusCode.NoContent;

            return Ok(_Response);

        }









    }
}
