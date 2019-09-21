using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Banco.WebApi.DTOs.Requests;
using Banco.WebApi.DTOs.Responses;
using Banco.WebApi.Models;
using Banco.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banco.WebApi.Controllers
{
    [Route("api/[controller]/{action?}")]
    [ApiController]
    [Authorize(Roles = nameof(Rol.Administrador))]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClientesController(IClienteService clienteService, IMapper mapper)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        // GET: api/Clientes
        /// <summary>
        /// Trae todos los clientes.
        /// </summary>
        /// <returns>Lista de todos los clientes.</returns>
        [HttpGet]
        public async Task<IEnumerable<ClienteDTO>> Get()
        {
            var clientes = await _clienteService.GetAsync();
            var resources = _mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteDTO>>(clientes);
            return resources;
        }

        /// <summary>
        /// Retorna un cliente.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Un cliente</returns>
        /// <response code="404">Si el cliente no existe</response> 
        /// <response code="200">Devuelve el cliente solicitado</response>
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var cliente = await _clienteService.GetAsync(id);
            if (cliente == null)
                return NotFound();
            var resources = _mapper.Map<Cliente, ClienteDTO>(cliente);
            return Ok(resources);
        }

        /// <summary>
        /// Crea un cliente.
        /// </summary>
        /// <remarks>
        /// Ejemplo de request:
        ///
        ///     POST /Cliente
        ///     {
        ///        "apellido": "Perez",
        ///        "nombre": "Juan",
        ///        "fechaNacimiento": "01/01/1990"
        ///     }
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>Un nuevo cliente creado</returns>
        /// <response code="201">Devuelve el nuevo cliente creado</response>
        [HttpPost]
        [ProducesResponseType(typeof(ClienteDTO), StatusCodes.Status201Created)]
        public async Task<ActionResult<ClienteDTO>> Post([FromBody] ClienteAddDTO request)
        {
            var cliente = _mapper.Map<ClienteAddDTO, Cliente>(request);
            await _clienteService.AddAsync(cliente);
            var clienteDTO = _mapper.Map<Cliente, ClienteDTO>(cliente);
            return CreatedAtAction("Get", new { id = cliente.Id }, clienteDTO);
        }

        /// <summary>
        /// Actualiza un cliente.
        /// </summary>
        /// <remarks>
        /// Ejemplo de request:
        ///
        ///     PUT /Clientes
        ///     {
        ///        "id": "1",
        ///        "apellido": "Perez",
        ///        "nombre": "Juan",
        ///        "fechaNacimiento": "01/01/1990"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <response code="400">Si el id del query string no es igual al del body</response> 
        /// <response code="200">Devuelve el cliente actualizado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ClienteDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(int id, [FromBody] ClienteUpdateDTO request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var clienteUpdate = _mapper.Map<ClienteUpdateDTO, Cliente>(request);
            await _clienteService.UpdateAsync(clienteUpdate);
            var ClienteDTO = _mapper.Map<Cliente, ClienteDTO>(clienteUpdate);
            return Ok(ClienteDTO);
        }

        /// <summary>
        /// Elimina un cliente específico.
        /// </summary>
        /// <remarks>
        /// Ejemplo de request:
        ///
        ///     DELETE /Cliente
        ///     {
        ///        "id": "1"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="204">La operación fue exitosa pero no devuelve nada en el response</response>
        /// <response code="404">Si el cliente es null</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _clienteService.GetAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            await _clienteService.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet("{idCliente}/Cuentas/{idCuenta}", Name = "GetByCuenta")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int idCliente, int idCuenta)
        {
            var cliente = await _clienteService.GetByCuentatAsync(idCliente, idCuenta);
            if (cliente == null)
                return NotFound();
            var resources = _mapper.Map<Cliente, ClienteDTO>(cliente);
            return Ok(resources);
        }

        [HttpGet(Name = "Search")]
        public async Task<IActionResult> Search(string apellido = "", string nombre = "", int pageSize = 10, int pageNumber = 1)
        {
            var clientes = await _clienteService.SearchAsync(apellido, nombre, pageSize, pageNumber);
            if (!clientes.Any())
                return NotFound();
            var clientesDTO = _mapper
                .Map<IEnumerable<Cliente>, IEnumerable<ClienteDTO>>(clientes);
            return Ok(clientesDTO);
            /*var clientes = await _clienteService.GetAsync();
            var resources = _mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteDTO>>(clientes);
            return resources;*/
        }

    }
}
