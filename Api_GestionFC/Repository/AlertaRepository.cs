using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_GestionFC.Repository
{
    public class AlertaRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public AlertaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AfiliacionDBDEV");
            this._configuration = configuration;
        }

        public async Task<DTO.AlertaImproductividadDTO> GetAlertaImproductividad(int nomina)
        {
            var response = new DTO.AlertaImproductividadDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Spp_GenerarAlertasPlantillaImproductiva", sqlConn))
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
                                    response.Cantidad = Convert.ToInt32(reader["Cantidad"]);
                                }
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.ResultDatos.Add(new Models.AlertaImproductividad
                                    {
                                        Foto = reader["Foto"].ToString(),
                                        IdAlerta = Convert.ToInt32(reader["IdAlerta"]),
                                        IdTipoAlerta = Convert.ToInt32(reader["IdTipoAlerta"]),
                                        IdEstatusAlerta = Convert.ToInt32(reader["IdEstatusAlerta"]),
                                        NominaAP = Convert.ToInt32(reader["NominaAP"]),
                                        NombreAP = reader["NombreAP"].ToString(),
                                        ApellidoAP  = reader["ApellidoAP"].ToString()
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
