namespace teleatendimento_medico.Models
{
    public class PessoaDTO
    {
        public int id { get; set; }
        public string? nome { get; set; }
        public string? cpf { get; set; }
        public Endereco? endereco { get; set; }
    }
}
