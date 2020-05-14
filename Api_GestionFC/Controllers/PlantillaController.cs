using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_GestionFC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantillaController : ControllerBase
    {
        private readonly Repository.PlantillaRepository _repository;

        public PlantillaController(Repository.PlantillaRepository repository) {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{nomina}")]
        public async Task<DTO.PromotoresDTO> Get(int nomina)
        {
            var response = new DTO.PromotoresDTO();
            try
            { 
                response = await _repository.GetPlantilla(nomina);
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
