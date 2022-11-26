namespace teleatendimento_medico.Models
{
    public class Telefone
    {
        public int _id { get; protected set; }
        public int _numero { get; protected set; }
        public int _ddd { get; protected set; }
        public TipoTelefone _tipo { get; protected set; }

        public Telefone(int id, int numero, int ddd, TipoTelefone tipo)
        {
            _id = id;
            _numero = numero;
            _ddd = ddd;
            _tipo = tipo;
        }
    }
}
