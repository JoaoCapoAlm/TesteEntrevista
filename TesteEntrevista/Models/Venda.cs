using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteEntrevista.Models
{
    public class Venda
    {
        [Key]
        [DisplayName("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idVenda { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [DisplayName("ID do Cliente")]
        public int idCliente { get; set; }
        public virtual Cliente Cliente { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [DisplayName("Data da venda")]
        public DateTime dthVenda { get; set; }
        [Required]
        [DisplayName("Valor total")]
        public float vlrTotalVenda { get; set; }
        public virtual ICollection<VendaProduto> VendaProduto { get; set; }
    }
}
