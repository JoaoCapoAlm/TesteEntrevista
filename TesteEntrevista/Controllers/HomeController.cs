using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using TesteEntrevista.Helpers;
using TesteEntrevista.Models;
using TesteEntrevista.Models.Dtos;

namespace TesteEntrevista.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppTesteContext _context;

        public HomeController(ILogger<HomeController> logger, AppTesteContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Importacao()
        {
            MatchCollection? matches;
            #region produto
            matches = await new ApiHelper().CallApiAsync("produto");

            if (matches is not null)
            {
                ProdutoApiDto? produtoApi;
                Produto produto;
                bool produtoExiste;
                foreach (Match match in matches)
                {
                    produtoApi = JsonConvert.DeserializeObject<ProdutoApiDto>(match.Groups[0].Value);
                    if(produtoApi is not null)
                    {
                        produtoExiste = await _context.Produto.AsNoTracking()
                            .Where(x => x.IdImportacao.Equals(produtoApi.idProduto))
                            .AnyAsync();

                        if (produtoExiste)
                            continue;

                        produto = new Produto()
                        {
                            IdImportacao = produtoApi.idProduto,
                            dscProduto = produtoApi.dscProduto,
                            vlrUnitario = produtoApi.vlrUnitario
                        };

                        await _context.Produto.AddAsync(produto);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            #endregion
            
            #region cliente
            matches = await new ApiHelper().CallApiAsync("cliente");

            if (matches is not null)
            {
                ClienteApiDto? clienteApi;
                Cliente cliente;
                bool clienteExiste;
                foreach (Match match in matches)
                {
                    clienteApi = JsonConvert.DeserializeObject<ClienteApiDto>(match.Groups[0].Value);
                    if (clienteApi is not null)
                    {
                        clienteExiste = await _context.Cliente.AsNoTracking()
                            .Where(x => x.IdImportacao.Equals(clienteApi.idCliente))
                            .AnyAsync();

                        if (clienteExiste)
                            continue;

                        cliente = new Cliente()
                        {
                            IdImportacao = clienteApi.idCliente,
                            nmCliente = clienteApi.nmCliente,
                            Cidade = clienteApi.Cidade
                        };

                        await _context.Cliente.AddAsync(cliente);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            #endregion

            #region venda
            matches = await new ApiHelper().CallApiAsync("venda");

            if (matches is not null)
            {
                VendaApiDto? vendaApi;
                Venda venda;
                bool vendaExiste;
                int idCliente, idProduto;
                foreach (Match match in matches)
                {
                    vendaApi = JsonConvert.DeserializeObject<VendaApiDto>(match.Groups[0].Value);
                    if (vendaApi is not null)
                    {
                        vendaExiste = await _context.Venda.AsNoTracking()
                            .Where(x => x.IdImportacao.Equals(vendaApi.idCliente))
                            .AnyAsync();

                        if (vendaExiste)
                            continue;

                        idCliente = await _context.Cliente
                            .AsNoTracking()
                            .Where(x => x.IdImportacao.Equals(vendaApi.idCliente))
                            .Select(x => x.idCliente)
                            .FirstOrDefaultAsync();

                        idProduto = await _context.Produto.AsNoTracking()
                            .Where(x => x.IdImportacao.Equals(vendaApi.idProduto))
                            .Select(x => x.idProduto)
                            .FirstOrDefaultAsync();

                        venda = new Venda()
                        {
                            IdImportacao = vendaApi.idVenda,
                            dthVenda = vendaApi.dthVenda,
                            idCliente = vendaApi.idCliente,
                            idProduto = idProduto,
                            qtdVenda = vendaApi.qtdVenda,
                            vlrTotalVenda = vendaApi.vlrUnitarioVenda * vendaApi.qtdVenda,
                            vlrUnitarioVenda = vendaApi.vlrUnitarioVenda
                        };

                        await _context.Venda.AddAsync(venda);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            #endregion

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
