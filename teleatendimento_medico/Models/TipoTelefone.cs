namespace teleatendimento_medico.Models
{
    public class TipoTelefone
    {

        public int _id { get; protected set; }
        public string _tipo { get; protected set; }

        public TipoTelefone(int id, string tipo)
        {
            _id = id;
            _tipo = tipo;
        }
    }
}
