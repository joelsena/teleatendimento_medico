﻿namespace teleatendimento_medico.Models
{
    public class Pessoa
    {
        public int id { get; set; }
        public string? nome { get; set; }
        public string? cpf { get; set; }
        public Endereco? endereco { get; set; }
        // public Telefone[] _telefones { get; set; }
    }
}
