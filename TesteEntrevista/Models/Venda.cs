using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteEntrevista.Models
{
    public class Venda
    {
        public Venda()
        {
            vlrTotalVenda = 0;
        }
        [Key]
        [DisplayName("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idVenda { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [DisplayName("Cliente")]
        public int idCliente { get; set; }
        [DisplayName("Cliente")]
        public virtual Cliente Cliente { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [DisplayName("Data da venda")]
        public DateTime dthVenda { get; set; }
        [Required]
        [DisplayName("Produto")]
        public int idProduto { get; set; }
        public virtual Produto Produto { get; set; }
        [Required]
        [DisplayName("Valor unitário")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public float vlrUnitarioVenda { get; set; }
        [Required]
        [DisplayName("Quantidade")]
        [Range(1, int.MaxValue, ErrorMessage = "Valor deve ser positivo")]
        public int qtdVenda { get; set; }
        [Required]
        [DisplayName("Valor total")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public float vlrTotalVenda { get; set; }
        public int? IdImportacao { get; set; }
    }
}
