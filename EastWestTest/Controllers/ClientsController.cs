using EastWestTest.Service.Abstract;
using EastWestTest.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EastWestTest.Controllers
{
    /// <summary>
    /// Clients controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService clientService;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceArg">client service</param>
        public ClientsController(IClientService serviceArg)
        {
            clientService = serviceArg;
        }

        /// <summary>
        /// Getting all clients with related sales
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllClientInfo")]
        [ProducesResponseType(typeof(List<ClientInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllClientInfo()
        {
            try
            {
                var allClients = await clientService.GetAllClientsInfoAsync();

                if (allClients == null || !allClients.Any())
                    return NotFound();

                return Ok(allClients);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Getting client info by clientId with related sales 
        /// </summary>
        /// <param name="clientId">client id in system</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetClientInfoById/{clientId}")]
        [ProducesResponseType(typeof(ClientInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClientInfoById(int clientId)
        {
            try
            {
                var clientInfo = await clientService.GetClientInfoByIdAsync(clientId);

                if (clientInfo == null)
                    return NotFound();

                return Ok(clientInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
