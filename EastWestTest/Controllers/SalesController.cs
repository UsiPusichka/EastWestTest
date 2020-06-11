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
    /// Sales controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService saleService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceArg">sale service</param>
        public SalesController(ISaleService serviceArg)
        {
            saleService = serviceArg;
        }

        /// <summary>
        /// Getting all sales filtered by creation date
        /// </summary>
        /// <param name="from">Date from inclusive</param>
        /// <param name="before">Date before inclusive</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSalesByDate/{from}/{before}")]
        [ProducesResponseType(typeof(List<SaleInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSalesByDate(DateTime from, DateTime before)
        {
            try
            {
                var sales = await saleService.GetSalesByDateAsync(from, before);

                if (sales == null || !sales.Any())
                    return NotFound();

                return Ok(sales);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
