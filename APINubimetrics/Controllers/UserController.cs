using Entities;
using IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APINubimetrics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userServiece;

        public UserController(IUserService userService)
        {
            _userServiece = userService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                int id = Request.Query.ContainsKey("id") ? Convert.ToInt32(Request.Query["id"]) : 0;
                string nombre = Request.Query.ContainsKey("nombre") ? Request.Query["nombre"].ToString() : string.Empty;
                string apellido = Request.Query.ContainsKey("apellido") ? Request.Query["apellido"].ToString() : string.Empty;
                string email = Request.Query.ContainsKey("email") ? Request.Query["email"].ToString() : string.Empty;

                List<User> response = await _userServiece.Get(id, nombre, apellido, email);

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

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                List<User> response = await _userServiece.Get(id, string.Empty,string.Empty,string.Empty);

                if (response != null)
                    return Ok(response.FirstOrDefault());
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: (int)System.Net.HttpStatusCode.InternalServerError);
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {

            try
            {
                User response = await _userServiece.Save(user);
                
                if (response != null)
                    return Ok(response);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            try
            {
                user.Id = id;
                User response = await _userServiece.Save(user);

                if (response != null)
                    return Ok(response);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // PUT api/<UserController>/5
        [HttpPut("password/{id}")]
        public async Task<IActionResult> PutPassword(int id, [FromHeader] string newPassword)
        {
            try
            {
                bool response = await _userServiece.UpdatePassword(id,newPassword);

                if (response)
                    return Ok(response);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool response = await _userServiece.Delete(id);

                if (response)
                    return Ok(response);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
