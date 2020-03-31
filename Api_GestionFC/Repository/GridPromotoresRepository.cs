using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DTO = Api_GestionFC.DTO;

namespace Api_GestionFC.Repository
{

    public class GridPromotoresRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
    
        public GridPromotoresRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AfiliacionDB");
            this._configuration = configuration;
        }

        public DTO.GridPromotoresDTO GetGridPromotores(int nomina)
        {
            return new DTO.GridPromotoresDTO();
        }
    }
}
