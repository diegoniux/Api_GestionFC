using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Api_GestionFC.Repository
{

    public class PlantillaRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
    
        public PlantillaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AfiliacionDB");
            this._configuration = configuration;
        }

        public async Task<DTO.PromotoresDTO> GetPlantilla(int nomina)
        {
            var response = new DTO.PromotoresDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Sps_Datos_Plantilla", sqlConn))
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
                                    response.Promotores.Add(new Models.Progreso { 
                                        Nombre = reader["Nombre"].ToString(),
                                        Apellidos = reader["Apellidos"].ToString(),
                                        Foto = reader["Foto"].ToString(),
                                        Genero = reader["Genero"].ToString(),
                                        ColorIndicadorMeta = reader["ColorIndicadorMeta"].ToString(),
                                        SaldoVirtual = reader["SaldoVirtual"].ToString(),
                                        SaldoCantadoFCT = reader["SaldoCantadoFCT"].ToString(),
                                        SaldoAcumulado = reader["SaldoAcumulado"].ToString(),
                                        PorcentajeSaldoAcumulado = reader["PorcentajeSaldoAcumulado"].ToString(),
                                        PorcentajeSaldoVirtual = reader["PorcentajeSaldoVirtual"].ToString(),
                                        FCTInactivos = Convert.ToInt32(reader["FCTInactivos"]),
                                        TramitesCertificados = Convert.ToInt32(reader["TramitesCertificados"])
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
