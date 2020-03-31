using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models = Api_GestionFC.Models;
using Repository = Api_GestionFC.Repository;
using DTO = Api_GestionFC.DTO;

namespace Api_GestionFC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GridPromotoresController : ControllerBase
    {
        private readonly Repository.GridPromotoresRepository _repository;

        public GridPromotoresController(Repository.GridPromotoresRepository repository) {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("GetGridPromotores/{nomina}")]
        public async Task<DTO.GridPromotoresDTO> GetGridPromotores(int nomina)
        {
            return await _repository.GetGridPromotores(nomina);
        }
    }
}
