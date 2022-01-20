using Entities;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

//using System.Web.Http;

namespace APINubimetrics.Controllers
{
    [Route("api/Paises")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryServieces;
        private readonly IConfiguration _config;
        public CountryController(ICountryService countryServices, IConfiguration config)
        {
            _countryServieces = countryServices;
            _config = config;
        }

        [HttpGet("{country}")]
        [Produces("application/json")]
        public async Task<IActionResult> Get(string country)
        {
            try
            {
                string unauthorizedCountries = _config["MyConfigs:UnauthorizedCountries"];
                if (unauthorizedCountries.Contains(country))
                    return Unauthorized();

                Country response = await _countryServieces.GetCountry(country);

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
