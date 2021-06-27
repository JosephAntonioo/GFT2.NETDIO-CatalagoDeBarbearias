using GFT2.NETDIO_CatalagoDeBarbearias.Entity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GFT2.NETDIO_CatalagoDeBarbearias.Repositories
{
    public class BarbeariaSQLServerRepository : IBarbeariaRepository
    {
        private readonly SqlConnection sqlConnection;

        public BarbeariaSQLServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Barbearia>> Obter(int pagina, int quantidade)
        {
            var barbearias = new List<Barbearia>();
            var comando = $"select * from Barbearias order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                barbearias.Add(new Barbearia
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Dono = (string)sqlDataReader["Dono"],
                    ValorMensalidade = (double)sqlDataReader["ValorMensalidade"],
                    MesesContrato = (int)sqlDataReader["MesesContrato"]
                });
            }
            await sqlConnection.CloseAsync();
            return barbearias;
        }

        public async Task<Barbearia> Obter(Guid id)
        {
            Barbearia barbearia = null;
            var comando = $"select * from Barbearias where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                barbearia = new Barbearia
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Dono = (string)sqlDataReader["Dono"],
                    ValorMensalidade = (double)sqlDataReader["ValorMensalidade"],
                    MesesContrato = (int)sqlDataReader["MesesContrato"]
                };
            }
            await sqlConnection.CloseAsync();
            return barbearia;
        }

        public async Task<List<Barbearia>> Obter(string nome, string dono)
        {
            var barbearias = new List<Barbearia>();
            var comando = $"select * from Barbearias where Nome = '{nome}' and Dono = '{dono}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                barbearias.Add(new Barbearia
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Dono = (string)sqlDataReader["Dono"],
                    ValorMensalidade = (double)sqlDataReader["ValorMensalidade"],
                    MesesContrato = (int)sqlDataReader["MesesContrato"]
                });
            }
            await sqlConnection.CloseAsync();
            return barbearias;
        }

        public async Task Atualizar(Barbearia barbearia)
        {
            var comando = $"update Barbearias set Nome = '{barbearia.Nome}', Dono = '{barbearia.Dono}', ValorMensalidade = '{barbearia.ValorMensalidade.ToString().Replace(",", ".")}', MesesContrato = '{barbearia.MesesContrato}' where Id = '{barbearia.Id}'";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Inserir(Barbearia barbearia)
        {
            var comando = $"insert Barbearias(Nome, Dono, ValorMensalidade, MesesContrato) values ('{barbearia.Nome}', '{barbearia.Dono}', {barbearia.ValorMensalidade.ToString().Replace(",", ".")},{barbearia.MesesContrato})";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Remover(Guid id)
        {
            var comando = $"delete from Barbearias where Id = '{id}'";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }





        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}
