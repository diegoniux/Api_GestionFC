using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DTO = Api_GestionFC.DTO;
using Models = Api_GestionFC.Models;

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
            var response = new DTO.HeaderDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                { 
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Sps_Datos_Header", sqlConn))
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
                                    response.Plantilla = Convert.ToInt32(reader["Plantilla"]);
                                    response.APsMetaAlcanzada = Convert.ToInt32(reader["APsMetaAlcanzada"]);
                                    response.Progreso.Nombre = reader["Nombre"].ToString();
                                    response.Progreso.Apellidos = reader["Apellidos"].ToString();
                                    response.Progreso.Foto = reader["Foto"].ToString();
                                    response.Progreso.Genero = reader["Genero"].ToString();
                                    response.Progreso.ColorIndicadorMeta = reader["ColorIndicadorMeta"].ToString();
                                    response.Progreso.SaldoVirtual = reader["SaldoVirtual"].ToString();
                                    response.Progreso.SaldoCantadoFCT = reader["SaldoCantadoFCT"].ToString();
                                    response.Progreso.SaldoAcumulado = reader["SaldoAcumulado"].ToString();
                                    response.Progreso.PorcentajeSaldoAcumulado = Convert.ToDecimal(reader["PorcentajeSaldoAcumulado"]);
                                    response.Progreso.PorcentajeSaldoVirtual = Convert.ToDecimal(reader["PorcentajeSaldoVirtual"]);
                                    response.Progreso.FCTInactivos = Convert.ToInt32(reader["FCTInactivos"]);
                                    response.Progreso.TramitesCertificados = Convert.ToInt32(reader["TramitesCertificados"]);
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
