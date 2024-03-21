# Teste João Capoani

O projeto foi desenvolvido com .NET 8 e para conseguir rodar o projeto, será necessário configurar a string de conexão com o seu banco de dados e rodar a migrations para criar o banco de dados.

## Passo a passo
- Abrir o arquivo TesteEntrevista > [appsettings.json](./TesteEntrevista/appsettings.json);
- Substituir `ADICIONE_SUA_STRING_DE_CONEXAO_AQUI` pela sua string de conexão, na propriedade `Exemplo_DbConnection` existe um exemplo do formato da string de conexão;
- Para atualizar o banco de dados abrir o `Package Manager Console` do Visual Studio;
- Rodar o comando `Update-Database`;
