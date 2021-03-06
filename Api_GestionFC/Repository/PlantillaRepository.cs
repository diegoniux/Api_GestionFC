﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Api_GestionFC.Repository
{

    public class PlantillaRepository : Comun
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
                                    string foto = reader["Foto"].ToString();
                                    response.Promotores.Add(new Models.Progreso
                                    {
                                        NominaPromotor = Convert.ToInt32(reader["NominaPromotor"]),                                      
                                        Nombre = reader["Nombre"].ToString(),
                                        Apellidos = reader["Apellidos"].ToString(),
                                        Foto = foto == "capi_circulo.png" ? foto : obtieneFoto(foto, _configuration),
                                        Genero = reader["Genero"].ToString(),
                                        ColorIndicadorMeta = reader["ColorIndicadorMeta"].ToString(),
                                        SaldoVirtual = Convert.ToDecimal(reader["SaldoVirtual"]).ToString("C"),
                                        SaldoCantadoFCT = Convert.ToDecimal(reader["SaldoCantadoFCT"]).ToString("C"),
                                        SaldoAcumulado = Convert.ToDecimal(reader["SaldoAcumulado"]).ToString("C"),
                                        PorcentajeSaldoAcumulado = reader["PorcentajeSaldoAcumulado"].ToString(),
                                        PorcentajeSaldoVirtual = reader["PorcentajeSaldoVirtual"].ToString(),
                                        FCTInactivos = Convert.ToInt32(reader["FCTInactivos"]),
                                        TramitesCertificados = Convert.ToInt32(reader["TramitesCertificados"]),
                                        PorcentajeSaldoVirtualDesc = Convert.ToDecimal(reader["PorcentajeSaldoVirtual"]).ToString("0%")
                                    });;
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
