using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteEntrevista.Models;
using TesteEntrevista.Models.Dtos;

namespace TesteEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        private readonly AppTesteContext _context;

        public ClienteController(AppTesteContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index([Bind("cliente")] string? cliente = "")
        {
            return View(await _context.Cliente.AsNoTracking().Where(x => x.nmCliente.Contains(cliente ?? "")).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpGet("/edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] int? id)
        {
            if (id is null)
                return NotFound();

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente is null)
                return NotFound();

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("nmCliente,Cidade")] CreateClienteDto cliente)
        {
            if (ModelState.IsValid)
            {
                var newCliente = new Cliente(cliente.nmCliente, cliente.Cidade);

                await _context.AddAsync(newCliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        [HttpPost("/edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, [Bind("idCliente,nmCliente,Cidade")] UpdateClienteDto clienteDto)
        {
            if (id != clienteDto.idCliente)
                return NotFound();

            if (ModelState.IsValid)
            {
                var cliente = await _context.Cliente.Where(x => x.idCliente == id).FirstOrDefaultAsync();
                if(cliente is null)
                    return NotFound();

                cliente.nmCliente = clienteDto.nmCliente;
                cliente.Cidade = clienteDto.Cidade;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(clienteDto);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.idCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente != null)
            {
                _context.Cliente.Remove(cliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.idCliente == id);
        }
    }
}
