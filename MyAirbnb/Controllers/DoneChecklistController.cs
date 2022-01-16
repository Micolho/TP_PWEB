using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyAirbnb.Data;
using MyAirbnb.Models;

namespace MyAirbnb.Controllers
{
    public class DoneChecklistsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public DoneChecklistsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> IndexPreparacao(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            //procurar a reserva
            Reserva reserva = await _context.Reservas.Where(i => i.Id == id)
                                            .FirstOrDefaultAsync();
            if (reserva == null)
                return NotFound();

            //verificar qual o tipo de imovel e as suas checklists 
            Imovel imovel = await _context.Imoveis.Where(i => i.Id == reserva.ImovelId && 
                                                        (i.ResponsavelId == user.Id || 
                                                         i.DonoId == user.Id))
                                            .FirstOrDefaultAsync();
            if (imovel == null)
                return NotFound();

            var checklists = await _context.Checklists
                                    .Where(d => d.MomentoPreparacao && 
                                           d.CategoriaId == imovel.TipoImovelId)
                                    .ToListAsync();
            return View(checklists);
        }


        public async Task<IActionResult> IndexEntrega(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            //procurar a reserva
            Reserva reserva = await _context.Reservas.Where(i => i.Id == id)
                                            .FirstOrDefaultAsync();
            if (reserva == null)
                return NotFound();

            //verificar qual o tipo de imovel e as suas checklists 
            Imovel imovel = await _context.Imoveis.Where(i => i.Id == reserva.ImovelId &&
                                                        (i.ResponsavelId == user.Id ||
                                                         i.DonoId == user.Id))
                                            .FirstOrDefaultAsync();
            if (imovel == null)
                return NotFound();

            var checklists = await _context.Checklists
                                    .Where(d => d.MomentoEntrega &&
                                           d.CategoriaId == imovel.TipoImovelId)
                                    .ToListAsync();
            return View(checklists);
        }

        // GET: DoneChecklists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doneChecklist = await _context.DoneChecklists
                .Include(d => d.Checklist)
                .Include(d => d.Reserva)
                .Include(d => d.Responsavel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doneChecklist == null)
            {
                return NotFound();
            }

            return View(doneChecklist);
        }

        // GET: DoneChecklists/Create
        public IActionResult Create()
        {
            ViewData["ChecklistId"] = new SelectList(_context.Checklists, "Id", "Descricao");
            ViewData["ReservaId"] = new SelectList(_context.Reservas, "Id", "Id");
            ViewData["ResponsavelId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: DoneChecklists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Observacoes,ChecklistId,ReservaId,ResponsavelId")] DoneChecklist doneChecklist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doneChecklist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChecklistId"] = new SelectList(_context.Checklists, "Id", "Descricao", doneChecklist.ChecklistId);
            ViewData["ReservaId"] = new SelectList(_context.Reservas, "Id", "Id", doneChecklist.ReservaId);
            ViewData["ResponsavelId"] = new SelectList(_context.Users, "Id", "Id", doneChecklist.ResponsavelId);
            return View(doneChecklist);
        }

        // GET: DoneChecklists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doneChecklist = await _context.DoneChecklists.FindAsync(id);
            if (doneChecklist == null)
            {
                return NotFound();
            }
            ViewData["ChecklistId"] = new SelectList(_context.Checklists, "Id", "Descricao", doneChecklist.ChecklistId);
            ViewData["ReservaId"] = new SelectList(_context.Reservas, "Id", "Id", doneChecklist.ReservaId);
            ViewData["ResponsavelId"] = new SelectList(_context.Users, "Id", "Id", doneChecklist.ResponsavelId);
            return View(doneChecklist);
        }

        // POST: DoneChecklists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Observacoes,ChecklistId,ReservaId,ResponsavelId")] DoneChecklist doneChecklist)
        //{
        //    if (id != doneChecklist.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(doneChecklist);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!DoneChecklistExists(doneChecklist.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ChecklistId"] = new SelectList(_context.Checklists, "Id", "Descricao", doneChecklist.ChecklistId);
        //    ViewData["ReservaId"] = new SelectList(_context.Reservas, "Id", "Id", doneChecklist.ReservaId);
        //    ViewData["ResponsavelId"] = new SelectList(_context.Users, "Id", "Id", doneChecklist.ResponsavelId);
        //    return View(doneChecklist);
        //}

        // POST: DoneChecklists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doneChecklist = await _context.DoneChecklists.FindAsync(id);
            _context.DoneChecklists.Remove(doneChecklist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoneChecklistExists(int id)
        {
            return _context.DoneChecklists.Any(e => e.Id == id);
        }
    }
}
