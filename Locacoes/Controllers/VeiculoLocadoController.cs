using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Locacoes.Data;
using Locacoes.Models;

namespace Locacoes.Controllers
{
    public class VeiculoLocadoController : Controller
    {
        private readonly LocacoesContext _context;

        public VeiculoLocadoController(LocacoesContext context)
        {
            _context = context;
        }

        // GET: VeiculoLocado
        public async Task<IActionResult> Index()
        {
            var locacoesContext = _context.VeiculosLocados.Include(v => v.Locacao).Include(v => v.Veiculo);
            return View(await locacoesContext.ToListAsync());
        }

        // GET: VeiculoLocado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculoLocado = await _context.VeiculosLocados
                .Include(v => v.Locacao)
                .Include(v => v.Veiculo)
                .FirstOrDefaultAsync(m => m.LocacaoId == id);
            if (veiculoLocado == null)
            {
                return NotFound();
            }

            return View(veiculoLocado);
        }

        // GET: VeiculoLocado/Create
        public IActionResult Create()
        {
            ViewData["LocacaoId"] = new SelectList(_context.Locacoes, "Id", "Id");
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Id");
            return View();
        }

        // POST: VeiculoLocado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VeiculoId,LocacaoId,DataDevolucao,KilometragemInicial,KilometragemFinal,ValorDiaria")] VeiculoLocado veiculoLocado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(veiculoLocado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocacaoId"] = new SelectList(_context.Locacoes, "Id", "Id", veiculoLocado.LocacaoId);
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Id", veiculoLocado.VeiculoId);
            return View(veiculoLocado);
        }

        // GET: VeiculoLocado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculoLocado = await _context.VeiculosLocados.FindAsync(id);
            if (veiculoLocado == null)
            {
                return NotFound();
            }
            ViewData["LocacaoId"] = new SelectList(_context.Locacoes, "Id", "Id", veiculoLocado.LocacaoId);
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Id", veiculoLocado.VeiculoId);
            return View(veiculoLocado);
        }

        // POST: VeiculoLocado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VeiculoId,LocacaoId,DataDevolucao,KilometragemInicial,KilometragemFinal,ValorDiaria")] VeiculoLocado veiculoLocado)
        {
            if (id != veiculoLocado.LocacaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veiculoLocado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeiculoLocadoExists(veiculoLocado.LocacaoId))
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
            ViewData["LocacaoId"] = new SelectList(_context.Locacoes, "Id", "Id", veiculoLocado.LocacaoId);
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Id", veiculoLocado.VeiculoId);
            return View(veiculoLocado);
        }

        // GET: VeiculoLocado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculoLocado = await _context.VeiculosLocados
                .Include(v => v.Locacao)
                .Include(v => v.Veiculo)
                .FirstOrDefaultAsync(m => m.LocacaoId == id);
            if (veiculoLocado == null)
            {
                return NotFound();
            }

            return View(veiculoLocado);
        }

        // POST: VeiculoLocado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veiculoLocado = await _context.VeiculosLocados.FindAsync(id);
            if (veiculoLocado != null)
            {
                _context.VeiculosLocados.Remove(veiculoLocado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeiculoLocadoExists(int id)
        {
            return _context.VeiculosLocados.Any(e => e.LocacaoId == id);
        }
    }
}
