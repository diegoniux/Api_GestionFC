using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DTO = Api_GestionFC.DTO;

namespace Api_GestionFC.Repository
{
    public class HeaderRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public HeaderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AfiliacionDB");
            this._configuration = configuration;
        }

        public async Task<DTO.HeaderDTO> GetHeader(int nomina)
        {
            var response = new DTO.GridPromotoresDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Spi_LogSistema", sqlConn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCmd.Parameters.AddWithValue("@p_Nomina", nomina);

                        await sqlConn.OpenAsync();

                        using (var reader = await sqlCmd.ExecuteReaderAsync())
                        {
                            await reader.ReadAsync();
                            response.Promotores.Add();

                            response.ResultadoEjecucion.EjecucionCorrecta = (bool)reader["EjecucionCorrecta"];
                            //Si la ejecución es exitosa
                            if (response.ResultadoEjecucion.EjecucionCorrecta)
                            {
                                response.ResultadoEjecucion.ErrorMessage = reader["Mensaje"].ToString();
                                response.ResultadoEjecucion.FriendlyMessage = reader["Mensaje"].ToString();
                            }
                            else
                            {
                                response.ResultadoEjecucion.ErrorMessage = reader["Mensaje"].ToString();
                                response.ResultadoEjecucion.FriendlyMessage = reader["Mensaje"].ToString();
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
