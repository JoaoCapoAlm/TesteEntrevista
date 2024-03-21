using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TesteEntrevista.Models;

namespace TesteEntrevista.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly AppTesteContext _context;

        public ProdutoController(AppTesteContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Produto.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idProduto,dscProduto,vlrUnitario")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
                return NotFound();
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int idProduto, string dscProduto, float vlrUnitario)
        {
            if (id != idProduto)
                return NotFound();

            if (dscProduto.IsNullOrEmpty())
                ModelState.AddModelError("dscProduto", "Campo Obribatório");

            if (vlrUnitario <= 0)
                ModelState.AddModelError("vlrUnitario", "Valor deve ser positivo");

            if (!ModelState.IsValid)
            {
                return View(new Produto()
                {
                    idProduto = idProduto,
                    dscProduto = dscProduto,
                    vlrUnitario = vlrUnitario
                });
            }

            var produto = await _context.Produto.Where(x => x.idProduto.Equals(idProduto)).FirstOrDefaultAsync();
            if(produto is null)
                return NotFound();

            produto.dscProduto = dscProduto;
            produto.vlrUnitario = vlrUnitario;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var produto = await _context.Produto.AsNoTracking()
                .Where(x => x.idProduto.Equals(id)).FirstOrDefaultAsync();
            if (produto == null)
                return NotFound();

            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.Produto.AsNoTracking().Where(x => x.idProduto.Equals(id)).ExecuteDeleteAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
