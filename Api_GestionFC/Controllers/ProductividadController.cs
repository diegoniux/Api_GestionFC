using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductividadController
    {

        [HttpGet("metodo/{nombre}")]
        [AllowAnonymous]
        public string metodo(string nombre, [FromBody] string apellido)
        {
            return $"hola {nombre} {apellido}";
        }

        [HttpGet("otro")]
        [AllowAnonymous]
        public string otro()
        {
            return "hola otro";
        }

    }
}
