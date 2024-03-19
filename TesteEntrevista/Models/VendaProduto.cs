using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteEntrevista.Models
{
    public class VendaProduto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVendaProduto { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public int idVenda { get; set; }
        public virtual Venda Venda { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public int idProduto { get; set; }
        public virtual Produto Produto { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Quantidade { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public float vlrUnitario { get; set; }
    }
}
