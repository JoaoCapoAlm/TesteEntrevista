﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteEntrevista.Models
{
    public class Cliente
    {
        [Key]
        [DisplayName("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCliente { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [DisplayName("Nome")]
        public string nmCliente { get; set; }
        [Required]
        public string Cidade { get; set; }
        public ICollection<Venda> Vendas { get; set; }
    }
}
