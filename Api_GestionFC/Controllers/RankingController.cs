using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DTO = Api_GestionFC.DTO;
using Repository = Api_GestionFC.Repository;


namespace Api_GestionFC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankingController
    {
        private readonly Repository.RankingRepository _repository;

        public RankingController(Repository.RankingRepository repository) {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("GetRanking/{nomina}")]
        public async Task<DTO.RankingDTO> GetRanking(int nomina)
        {
            var response = new DTO.RankingDTO();
            try
            {
                response = await _repository.GetRanking(nomina);
            }
            catch (Exception ex)
            {
                response.ResultadoEjecucion.EjecucionCorrecta = false;
                response.ResultadoEjecucion.ErrorMessage = ex.Message;
                response.ResultadoEjecucion.FriendlyMessage = ex.Message;
            }
            return response;
        }

        [HttpGet("GetRankingEspecialistas/{nomina}")]
        public async Task<DTO.RankingEspecialistasDTO> GetRankingEspecialistas(int nomina)
        {
            var response = new DTO.RankingEspecialistasDTO();
            try
            {
                response = await _repository.GetRankingEspecialistas(nomina);
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
