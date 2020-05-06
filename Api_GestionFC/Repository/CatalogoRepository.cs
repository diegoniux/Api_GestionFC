using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Api_GestionFC.Repository
{
    public class CatalogoRepository : Comun
    {

        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public CatalogoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AfiliacionDB");
            this._configuration = configuration;
        }

        public async Task<DTO.CatalogoDTO> GetCatalogo(string clave)
        {
            var response = new DTO.CatalogoDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("dbo.Sps_Catalogo_Parametros", sqlConn))
                    {
                        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCmd.Parameters.AddWithValue("@p_Clave", clave);
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
                                    response.Clave = reader["Clave"].ToString();
                                    response.Valor = reader["Valor"].ToString();
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
