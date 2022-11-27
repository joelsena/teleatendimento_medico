using teleatendimento_medico.Models;
using Microsoft.Data.Sqlite;
using teleatendimento_medico.Models.ViewModels;

namespace teleatendimento_medico.DAO
{
    public class PessoaDAO
    {
        const string connectionString = "Data Source=db.sqlite";
        public static void delete(string id)
        {
            using (SqliteConnection con = new SqliteConnection(connectionString))
            {
                SqliteCommand cmd = new SqliteCommand($"DELETE FROM pessoas WHERE pessoaId={id};", con);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void create(Pessoa p)
        {
            // using statement => handle unmanaged resources to not keep using memory unnecessary
            using (SqliteConnection con = new SqliteConnection(connectionString))
            {
                string insertEndereco = @$"INSERT INTO endereco (logradouro,numero,cep,bairro,cidade,estado) 
                    VALUES ('{p.endereco.logradouro}', {p.endereco.numero}, {p.endereco.cep}, '{p.endereco.bairro}', '{p.endereco.cidade}', '{p.endereco.estado}');";

                SqliteCommand insertEndCmd = new SqliteCommand(insertEndereco, con);
                SqliteCommand lastIdCmd = new SqliteCommand("SELECT last_insert_rowid();", con);

                con.Open();

                try
                {
                    // Executa um comando sem retorno do banco de dados
                    insertEndCmd.ExecuteNonQuery();

                    // Retorna o Id do último recorde de uma tabela no banco de dados
                    int lastEndId = (int)Convert.ToInt64(lastIdCmd.ExecuteScalar());

                    string insertPessoa = @$"INSERT INTO pessoas (nome,cpf,enderecoId) VALUES ('{p.nome}', '{p.cpf}', {lastEndId});";
                    SqliteCommand insertPessoaCmd = new SqliteCommand(insertPessoa, con);
                    insertPessoaCmd.ExecuteNonQuery();
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
        }

        public static void update(Pessoa p)
        {
            // using statement => handle unmanaged resources to not keep using memory unnecessary
            using (SqliteConnection con = new SqliteConnection(connectionString))
            {
                string updateEndereco = @$"UPDATE endereco SET 
                    logradouro='{p.endereco.logradouro}', numero={p.endereco.numero}, cep={p.endereco.cep}, bairro='{p.endereco.bairro}', cidade='{p.endereco.cidade}', estado='{p.endereco.estado}'
                    WHERE enderecoId = {p.endereco.id};";

                SqliteCommand updateEndCmd = new SqliteCommand(updateEndereco, con);

                string updatePessoa = @$"UPDATE pessoas SET nome='{p.nome}', cpf='{p.cpf}' WHERE pessoaId = {p.id};";
                SqliteCommand updatePessoaCmd = new SqliteCommand(updatePessoa, con);

                con.Open();

                try
                {
                    // Executa um comando sem retorno do banco de dados
                    updateEndCmd.ExecuteNonQuery();
                    updatePessoaCmd.ExecuteNonQuery();
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
        }

        public static PessoaViewModel getAll()
        {
            List<Pessoa> pessoaList = new();

            using (SqliteConnection con = new SqliteConnection(connectionString))
            {
                using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = @"SELECT p.pessoaId, p.nome, p.cpf, e.enderecoId, e.logradouro, e.numero, e.cep, e.bairro, e.cidade, e.estado FROM pessoas as p 
                            INNER JOIN endereco as e ON p.enderecoId = e.enderecoId;";

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                pessoaList.Add(new Pessoa
                                {
                                    id = reader.GetInt32(0),
                                    nome = reader.GetString(1),
                                    cpf = reader.GetString(2),
                                    endereco = new Endereco
                                    {
                                        id = reader.GetInt32(3),
                                        logradouro = reader.GetString(4),
                                        numero = reader.GetInt32(5),
                                        cep = reader.GetInt32(6),
                                        bairro = reader.GetString(7),
                                        cidade = reader.GetString(8),
                                        estado = reader.GetString(9),
                                    }
                                });
                            }
                        }

                        return new PessoaViewModel
                        {
                            PessoaList = pessoaList
                        };
                    }
                }
            }

        }

        public static Pessoa get(int id)
        {
            Pessoa p = new();

            using (SqliteConnection con = new SqliteConnection(connectionString))
            {
                string sql = @$"SELECT p.pessoaId, p.nome, p.cpf, e.enderecoId, e.logradouro, e.numero, e.cep, e.bairro, e.cidade, e.estado FROM pessoas as p 
                            INNER JOIN endereco as e ON p.enderecoId = e.enderecoId
                            WHERE pessoaId = {id};";
                SqliteCommand cmd = new SqliteCommand(sql, con);

                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        p = new Pessoa
                        {
                            id = reader.GetInt32(0),
                            nome = reader.GetString(1),
                            cpf = reader.GetString(2),
                            endereco = new Endereco
                            {
                                id = reader.GetInt32(3),
                                logradouro = reader.GetString(4),
                                numero = reader.GetInt32(5),
                                cep = reader.GetInt32(6),
                                bairro = reader.GetString(7),
                                cidade = reader.GetString(8),
                                estado = reader.GetString(9),
                            }
                        };
                    }

                    return p;
                }
            }
        }
    }
}
