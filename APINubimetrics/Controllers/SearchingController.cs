using Entities;
using IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APINubimetrics.Controllers
{
    [Route("api/Busqueda")]
    [ApiController]
    public class SearchingController : ControllerBase
    {
        private readonly ISearchingService _searchingServieces;
       
        public SearchingController(ISearchingService searchingServices)
        {
            _searchingServieces = searchingServices;
        }

        [HttpGet("{query}")]
        [Produces("application/json")]
        public async Task<IActionResult> Get(string query)
        {
            try
            {
                Searching response = await _searchingServieces.GetSearching(query);

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
