using Entities;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APINubimetrics.Controllers
{
    [Route("api/currency")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyServices)
        {
            _currencyService = currencyServices;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetSave()
        {
            try
            {
                List<Currency> response = await _currencyService.GetSave();

                if (response != null)
                    return Ok(response);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: (int)System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
