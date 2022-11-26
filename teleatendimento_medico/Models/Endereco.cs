namespace teleatendimento_medico.Models
{
    public class Endereco
    {
        public int _id { get; protected set; }
        public string? _logradouro { get; protected set; }
        public int _numero { get; protected set; }
        public int _cep { get; protected set; }
        public string _bairro { get; protected set; }
        public string _cidade { get; protected set; }
        public string _estado { get; protected set; }

        public Endereco(int id, string logradouro, int numero, int cep, string bairro, string cidade, string estado)
        {
            _id = id;
            _cep = cep;
            _bairro = bairro;
            _cidade = cidade;
            _estado = estado;
            _numero = numero;
            _logradouro = logradouro;
        }
    }
}
