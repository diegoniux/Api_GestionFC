using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Api_GestionFC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        private readonly Repository.CatalogoRepository _repository;
        public CatalogoController(Repository.CatalogoRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{Clave}")]
        public async Task<DTO.CatalogoDTO> GetCatalogo(string Clave)
        {
            var response = new DTO.CatalogoDTO();
            try
            {
                response = await _repository.GetCatalogo(Clave);
            }
            catch (Exception ex)
            {
                response.ResultadoEjecucion.EjecucionCorrecta = false;
                response.ResultadoEjecucion.ErrorMessage = ex.Message;
                response.ResultadoEjecucion.FriendlyMessage = ex.Message;
            }
            return response;
        }
    }
}
