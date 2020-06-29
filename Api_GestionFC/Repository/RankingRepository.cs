using Microsoft.Extensions.Configuration;
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
                                    response.TopGerentes.Add(new Models.RankingAP
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
                                        Estrellas = new Models.RankEstrellas
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
                                    response.Gerentes.Add(new Models.RankingAP
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
                                        Estrellas = new Models.RankEstrellas
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
    }
}
