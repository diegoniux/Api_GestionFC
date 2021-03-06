﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DTO = Api_GestionFC.DTO;
using Models = Api_GestionFC.Models;

namespace Api_GestionFC.Repository
{
    public class HeaderRepository : Comun
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
                                    string foto = reader["Foto"].ToString();
                                    response.Plantilla = Convert.ToInt32(reader["Plantilla"]);
                                    response.APsMetaAlcanzada = Convert.ToInt32(reader["APsMetaAlcanzada"]);
                                    response.Progreso.Nombre = reader["Nombre"].ToString();
                                    response.Progreso.Apellidos = reader["Apellidos"].ToString();
                                    response.Progreso.Foto = foto == "capi_circulo.png" ? foto : obtieneFoto(foto, _configuration);
                                    response.Progreso.Genero = reader["Genero"].ToString();
                                    response.Progreso.ColorIndicadorMeta = reader["ColorIndicadorMeta"].ToString();
                                    response.Progreso.SaldoVirtual = Convert.ToDecimal(reader["SaldoVirtual"]).ToString("C0");
                                    response.Progreso.SaldoCantadoFCT = Convert.ToDecimal(reader["SaldoCantadoFCT"]).ToString("C0");
                                    response.Progreso.SaldoAcumulado = Convert.ToDecimal(reader["SaldoAcumulado"]).ToString("C0");
                                    response.Progreso.PorcentajeSaldoAcumulado = reader["PorcentajeSaldoAcumulado"].ToString();
                                    response.Progreso.PorcentajeSaldoVirtual = reader["PorcentajeSaldoVirtual"].ToString();
                                    response.Progreso.FCTInactivos = Convert.ToInt32(reader["FCTInactivos"]);
                                    response.Progreso.TramitesCertificados = Convert.ToInt32(reader["TramitesCertificados"]);
                                    response.Progreso.PorcentajeSaldoVirtualDesc = Convert.ToDecimal(reader["PorcentajeSaldoVirtual"]).ToString("0%");
                                    response.Perfil = reader["Perfil"].ToString();
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
