using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TesteEntrevista.Models;

namespace TesteEntrevista.Controllers
{
    public class VendaController : Controller
    {
        private readonly AppTesteContext _context;

        public VendaController(AppTesteContext context)
        {
            _context = context;
        }

        // GET: Venda
        public async Task<IActionResult> Index([Bind("busca")] string? busca)
        {
            var venda = await _context.Venda.AsNoTracking()
                .Include(v => v.Cliente)
                .Include(v => v.Produto)
                .Where(x => string.IsNullOrEmpty(busca) || x.Cliente.nmCliente.Contains(busca) || x.Produto.dscProduto.Contains(busca))
                .ToListAsync();
            return View(venda);
        }


        public IActionResult Create()
        {
            ViewData["idCliente"] = new SelectList(_context.Cliente, "idCliente", "nmCliente");

            var produtos = _context.Produto.AsNoTracking().Select(x => new { x.idProduto, dscProduto = $"{x.dscProduto} (R$ {x.vlrUnitario:N2})" });
            ViewData["idProduto"] = new SelectList(produtos, "idProduto", "dscProduto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idCliente,dthVenda,idProduto,qtdVenda")] Venda venda)
        {
            ModelState.Remove("Cliente");
            ModelState.Remove("Produto");
            if (ModelState.IsValid)
            {
                venda.vlrUnitarioVenda = await _context.Produto.Where(x => x.idProduto.Equals(venda.idProduto))
                    .Select(x => x.vlrUnitario)
                    .FirstOrDefaultAsync();

                venda.vlrTotalVenda = venda.qtdVenda * venda.vlrUnitarioVenda;

                await _context.AddAsync(venda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idCliente"] = new SelectList(_context.Cliente, "idCliente", "nmCliente", venda.idCliente);
            ViewData["idProduto"] = new SelectList(_context.Produto, "idProduto", "dscProduto", venda.idProduto);
            return View(venda);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var venda = await _context.Venda
                .AsNoTracking()
                .Include(x => x.Produto)
                .Include(x => x.Cliente)
                .Where(x => x.idVenda.Equals(id))
                .FirstOrDefaultAsync();

            if (venda == null)
                return NotFound();

            ViewData["idCliente"] = new SelectList(_context.Cliente, "idCliente", "nmCliente", venda.idCliente);
            var produtos = _context.Produto.AsNoTracking().Select(x => new { x.idProduto, dscProduto = $"{x.dscProduto} (R$ {x.vlrUnitario:N2})" });
            ViewData["idProduto"] = new SelectList(produtos, "idProduto", "dscProduto");
            return View(venda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idVenda,idCliente,dthVenda,idProduto,qtdVenda")] Venda venda)
        {
            if (id != venda.idVenda)
                return NotFound();

            venda.vlrUnitarioVenda = await _context.Produto.AsNoTracking()
                .Where(x => x.idProduto.Equals(venda.idProduto))
                .Select(x => x.vlrUnitario)
                .FirstOrDefaultAsync();

            venda.vlrTotalVenda = venda.qtdVenda * venda.vlrUnitarioVenda;

            ModelState.Remove("Cliente");
            ModelState.Remove("Produto");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendaExists(venda.idVenda))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["idCliente"] = new SelectList(_context.Cliente, "idCliente", "nmCliente", venda.idCliente);
            var produtos = _context.Produto.AsNoTracking().Select(x => new { x.idProduto, dscProduto = $"{x.dscProduto} (R$ {x.vlrUnitario:N2})" });
            ViewData["idProduto"] = new SelectList(produtos, "idProduto", "dscProduto");
            return View(venda);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var venda = await _context.Venda
                .Include(v => v.Cliente)
                .Include(v => v.Produto)
                .FirstOrDefaultAsync(m => m.idVenda == id);
            if (venda == null)
                return NotFound();

            return View(venda);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venda = await _context.Venda.FindAsync(id);
            if (venda != null)
            {
                _context.Venda.Remove(venda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendaExists(int id)
        {
            return _context.Venda.AsNoTracking().Any(e => e.idVenda == id);
        }
    }
}
