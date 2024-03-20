using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteEntrevista.Models
{
    public class Produto
    {
        public Produto()
        {
            VendaProduto = [];
        }
        [Key]
        [DisplayName("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idProduto { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [DisplayName("Descrição")]
        public string dscProduto { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(0, float.MaxValue, ErrorMessage = "O valor deve ser um número")]
        [DisplayName("Valor unitário")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public float vlrUnitario { get; set; }
        public virtual ICollection<VendaProduto> VendaProduto { get; set; }
    }
}
