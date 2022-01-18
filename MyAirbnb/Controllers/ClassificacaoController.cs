using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyAirbnb.Data;
using MyAirbnb.Models;

namespace MyAirbnb.Controllers
{
    [Authorize]
    public class ClassificacaoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public ClassificacaoController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Classificacao
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Classificacaos.Include(c => c.Imovel).Include(c => c.Utilizador);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Classificacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classificacao = await _context.Classificacaos
                .Include(c => c.Imovel)
                .Include(c => c.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classificacao == null)
            {
                return NotFound();
            }

            return View(classificacao);
        }

        // GET: Classificacao/Create
        public async Task<IActionResult> Create(int id)
        {
            var imovel = await _context.Imoveis.FindAsync(id);

            if (imovel == null)
                return NotFound();

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return NotFound();

            Classificacao classificacao = new Classificacao()
            {
                ImovelId = imovel.Id,
                UtilizadorId = user.Id,
            };
            
            return View(classificacao);
        }

        // POST: Classificacao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Estrelas,Comentario,ImovelId,UtilizadorId")] Classificacao classificacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classificacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ImovelId"] = new SelectList(_context.Imoveis, "Id", "Id", classificacao.ImovelId);
            //ViewData["UtilizadorId"] = new SelectList(_context.Users, "Id", "Id", classificacao.UtilizadorId);
            return View(classificacao);
        }

        // GET: Classificacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classificacao = await _context.Classificacaos.FindAsync(id);
            if (classificacao == null)
            {
                return NotFound();
            }
            ViewData["ImovelId"] = new SelectList(_context.Imoveis, "Id", "Id", classificacao.ImovelId);
            ViewData["UtilizadorId"] = new SelectList(_context.Users, "Id", "Id", classificacao.UtilizadorId);
            return View(classificacao);
        }

        // POST: Classificacao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Estrelas,Comentario,ImovelId,UtilizadorId")] Classificacao classificacao)
        {
            if (id != classificacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classificacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassificacaoExists(classificacao.Id))
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
            ViewData["ImovelId"] = new SelectList(_context.Imoveis, "Id", "Id", classificacao.ImovelId);
            ViewData["UtilizadorId"] = new SelectList(_context.Users, "Id", "Id", classificacao.UtilizadorId);
            return View(classificacao);
        }

        // GET: Classificacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classificacao = await _context.Classificacaos
                .Include(c => c.Imovel)
                .Include(c => c.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classificacao == null)
            {
                return NotFound();
            }

            return View(classificacao);
        }

        // POST: Classificacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classificacao = await _context.Classificacaos.FindAsync(id);
            _context.Classificacaos.Remove(classificacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassificacaoExists(int id)
        {
            return _context.Classificacaos.Any(e => e.Id == id);
        }
    }
}
