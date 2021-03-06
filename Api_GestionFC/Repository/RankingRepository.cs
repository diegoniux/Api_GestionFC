﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DTO = Api_GestionFC.DTO;

namespace Api_GestionFC.Repository
{
    public class RankingRepository : Comun
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public RankingRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AfiliacionDB");
            this._configuration = configuration;
        }
        public async Task<DTO.RankingDTO> GetRanking(int nomina)
        {
            var response = new DTO.RankingDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Spp_RankingSaldoVirtual", sqlConn))
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
                                    response.TopGerentes.Add(new Models.RankingGte
                                    {
                                        Nombre = reader["Nombre"].ToString(),
                                        Apellidos = reader["Apellidos"].ToString(),
                                        Foto = foto == "capi_circulo.png" ? foto : obtieneFoto(foto, _configuration),
                                        Posicion = reader["Posicion"].ToString(),
                                        Saldo = reader["Saldo"].ToString(),
                                        TipoSaldo = string.Empty,
                                        NumTraspaso = Convert.ToInt32(reader["NumTraspaso"]),
                                        ImgPosicionSemAnt = string.Empty,
                                        ColorPosicion = reader["ColorPosicion"].ToString(),
                                        ColorTextoSaldo = string.Empty,
                                        Estrellas = new Models.RankSemanal
                                        {
                                            semana1 = reader["EstrellasSem1"].ToString(),
                                            semana2 = reader["EstrellasSem2"].ToString(),
                                            semana3 = reader["EstrellasSem3"].ToString(),
                                            semana4 = reader["EstrellasSem4"].ToString()
                                        }
                                    });
                                }
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    string foto = reader["Foto"].ToString();
                                    response.Gerentes.Add(new Models.RankingGte
                                    {
                                        Nombre = reader["Nombre"].ToString(),
                                        Apellidos = reader["Apellidos"].ToString(),
                                        Foto = foto == "capi_circulo.png" ? foto : obtieneFoto(foto, _configuration),
                                        Posicion = reader["Posicion"].ToString(),
                                        Saldo = reader["Saldo"].ToString(),
                                        TipoSaldo = reader["TipoSaldo"].ToString(),
                                        NumTraspaso = Convert.ToInt32(reader["NumTraspaso"]),
                                        ImgPosicionSemAnt = reader["ImgPosicionSemAnt"].ToString(),
                                        ColorPosicion = string.Empty,
                                        ColorTextoSaldo = reader["ColorTextoSaldo"].ToString(),
                                        Estrellas = new Models.RankSemanal
                                        {
                                            semana1 = reader["EstrellasSem1"].ToString(),
                                            semana2 = reader["EstrellasSem2"].ToString(),
                                            semana3 = reader["EstrellasSem3"].ToString(),
                                            semana4 = reader["EstrellasSem4"].ToString()
                                        }
                                    });
                                }
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.PosicionDireccion = Convert.ToInt32(reader["PosicionDireccion"]);
                                    response.ImgPosicionSemAntDireccion = reader["ImgPosicionSemAntDireccion"].ToString();
                                    response.PosicionNacional = Convert.ToInt32(reader["PosicionNacional"]);
                                    response.ImgPosicionSemAntNacional = reader["ImgPosicionSemAntNacional"].ToString();
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
        public async Task<DTO.RankingEspecialistasDTO> GetRankingEspecialistas(int nomina)
        {
            var response = new DTO.RankingEspecialistasDTO();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GFC.Spp_RankingSaldoVirtualEspecialistas", sqlConn))
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
                                    response.TopEspecialistas.Add(new Models.RankingEspecialista
                                    {
                                        Nomina = Convert.ToInt32(reader["Nomina"]),
                                        Nombre = reader["Nombre"].ToString(),
                                        Apellidos = reader["Apellidos"].ToString(),
                                        Foto = foto == "capi_circulo.png" ? foto : obtieneFoto(foto, _configuration),
                                        Posicion = reader["Posicion"].ToString(),
                                        SaldoVirtual = reader["Saldo"].ToString(),
                                        TipoSaldo = string.Empty,
                                        NumTraspaso = Convert.ToInt32(reader["NumTraspaso"]),
                                        ImgPosicionSemAnt = reader["ImgPosicionSemAnt"].ToString(),
                                        ColorPosicion = reader["ColorPosicion"].ToString(),
                                        ColorTextoSaldo = string.Empty,
                                        Monedas = new Models.RankSemanal
                                        {
                                            semana1 = reader["MonedasSem1"].ToString(),
                                            semana2 = reader["MonedasSem2"].ToString(),
                                            semana3 = reader["MonedasSem3"].ToString(),
                                            semana4 = reader["MonedasSem4"].ToString()
                                        }
                                    });
                                }
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    string foto = reader["Foto"].ToString();
                                    response.Especialistas.Add(new Models.RankingEspecialista
                                    {
                                        Nombre = reader["Nombre"].ToString(),
                                        Apellidos = reader["Apellidos"].ToString(),
                                        Foto = foto == "capi_circulo.png" ? foto : obtieneFoto(foto, _configuration),
                                        Posicion = reader["Posicion"].ToString(),
                                        SaldoVirtual = reader["Saldo"].ToString(),
                                        TipoSaldo = reader["TipoSaldo"].ToString(),
                                        NumTraspaso = Convert.ToInt32(reader["NumTraspaso"]),
                                        ImgPosicionSemAnt = reader["ImgPosicionSemAnt"].ToString(),
                                        ColorPosicion = string.Empty,
                                        ColorTextoSaldo = reader["ColorTextoSaldo"].ToString(),
                                        Monedas = new Models.RankSemanal
                                        {
                                            semana1 = reader["MonedasSem1"].ToString(),
                                            semana2 = reader["MonedasSem2"].ToString(),
                                            semana3 = reader["MonedasSem3"].ToString(),
                                            semana4 = reader["MonedasSem4"].ToString()
                                        }
                                    });
                                }
                                reader.NextResult();
                                while (await reader.ReadAsync())
                                {
                                    response.Dias = reader["Dias"].ToString();
                                    response.Horas = reader["Horas"].ToString();
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
