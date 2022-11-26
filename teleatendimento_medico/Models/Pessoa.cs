namespace teleatendimento_medico.Models
{
    public class Pessoa
    {
        public int _id { get; protected set; }
        public string _nome { get; protected set; }
        public long _cpf { get; protected set; }
        public Endereco _endereco { get; protected set; }
        public Telefone[] _telefones { get; protected set; }

        public Pessoa(int id, string nome, long cpf, Endereco endereco, Telefone[] telefones)
        {
            _id = id;
            _nome = nome;
            _telefones = telefones;
            _cpf = cpf;
            _endereco = endereco;
        }
    }
}
