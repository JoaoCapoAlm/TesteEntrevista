namespace TesteEntrevista.Models.Dtos
{
    public record CreateClienteDto(string nmCliente, string Cidade);

    public record UpdateClienteDto(int idCliente, string nmCliente, string Cidade);
}
