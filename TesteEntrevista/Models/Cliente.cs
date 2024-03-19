using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TesteEntrevista.Models
{
    public class Cliente
    {
        [DisplayName("ID")]
        public int idCliente { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [DisplayName("Nome")]
        public string nmCliente { get; set; }
        [Required]
        public string Cidade { get; set; }
        public ICollection<Venda> Vendas { get; set; }
    }
}
