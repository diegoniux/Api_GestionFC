using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DTO = Api_GestionFC.DTO;
using Repository = Api_GestionFC.Repository;

namespace Api_GestionFC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HeaderController : ControllerBase
    {
        private readonly Repository.HeaderRepository _repository;
        public HeaderController(Repository.HeaderRepository repository) 
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("GetHeader/{nomina}")]
        public async Task<DTO.GridPromotoresDTO> GetHeader(int nomina)
        {
            return await _repository.GetHeader(nomina);
        }
    }
}
