using teleatendimento_medico.Models;
using Microsoft.Data.Sqlite;

namespace teleatendimento_medico.DAO
{
    public class PessoaDAO
    {
        public bool delete(Pessoa p)
        {
            return true;
        }

        public void create(Pessoa p)
        {
            // using statemant => handle unmanaged resources to not keep using memory unnecessary
            using (SqliteConnection con = new SqliteConnection("Data Source=db.sqlite"))
            {
                using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = @$"INSERT INTO endereco (logradouro,numero,cep,bairro,cidade,estado) 
                    VALUES ({p._endereco._logradouro}, {p._endereco._numero}, {p._endereco._cep}, {p._endereco._bairro}, {p._endereco._cidade}, {p._endereco._estado});
                    INSERT INTO pessoa (nome,cpf,endereco) VALUES ({p._nome}, {p._cpf}, @@IDENTITY);";

                    try
                    {
                        // Executa um comando sem retorno do banco de dados
                        tableCmd.ExecuteNonQuery();
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.Message);
                    }
                }
            }
        }

        public bool update(Pessoa p)
        {
            return true;
        }

        // public Pessoa get(long cpf)
        // {
        //     return new Pessoa();
        // }
    }
}
