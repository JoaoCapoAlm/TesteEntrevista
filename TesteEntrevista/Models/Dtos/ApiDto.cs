namespace TesteEntrevista.Models.Dtos
{
    public class ProdutoApiDto
    {
        public int idProduto { get; set; }
        public string dscProduto { get; set; }
        public float vlrUnitario { get; set; }
    }

    public class ClienteApiDto
    {
        public int idCliente { get; set; }
        public string nmCliente { get; set; }
        public string Cidade { get; set; }
    }

    public class VendaApiDto
    {
        public int idVenda { get; set; }
        public DateTime dthVenda { get; set; }
        public int idCliente { get; set; }
        public int idProduto { get; set; }
        public int qtdVenda { get; set; }
        public float vlrUnitarioVenda { get; set; }
    }
}
