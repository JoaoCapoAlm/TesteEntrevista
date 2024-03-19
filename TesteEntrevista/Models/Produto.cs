using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteEntrevista.Models
{
    public class Produto
    {
        [Key]
        [DisplayName("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idProduto { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [DisplayName("Descrição")]
        public string dscProduto { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [DisplayName("Valor unitário")]
        public float vlrUnitario { get; set; }
        public virtual ICollection<VendaProduto> VendaProduto { get; set; }
    }
}
