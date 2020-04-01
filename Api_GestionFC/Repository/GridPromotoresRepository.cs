using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public async Task<DTO.GridPromotoresDTO> GetGridPromotores(int nomina)
        {
            var response = new DTO.GridPromotoresDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Sps_Datos_Promotores", sqlConn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCmd.Parameters.AddWithValue("@p_Nomina", nomina);
                        await sqlConn.OpenAsync();

                        using (var reader = await sqlCmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                response.ResultadoEjecucion.EjecucionCorrecta = Convert.ToBoolean(reader["EjecucionCorrecta"]);
                                response.ResultadoEjecucion.ErrorMessage = reader["Mensaje"].ToString();
                                response.ResultadoEjecucion.FriendlyMessage = reader["Mensaje"].ToString();
                            }

                            //Si la ejecución es exitosa                                 
                            if (response.ResultadoEjecucion.EjecucionCorrecta)
                            {
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.Promotores.Add(
                                        new Models.Promotor
                                        {
                                            Nomina = Convert.ToInt32(reader["Nomina"]),
                                            Nombre = reader["Nombre"].ToString(),
                                            ApellidoPaterno = reader["ApellidoPaterno"].ToString(),
                                            ApellidoMaterno = reader["ApellidoMaterno"].ToString(),
                                            Foto = reader["Foto"].ToString(),
                                            FotoColor = reader["FotoColor"].ToString(),
                                            PorcentajeMeta = reader["PorcentajeMeta"].ToString(),
                                            PorcentajeMetaColor = reader["PorcentajeMetaColor"].ToString()
                                        });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
    }
}
