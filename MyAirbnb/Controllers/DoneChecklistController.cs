using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public DoneChecklistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DoneChecklists
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DoneChecklists.Include(d => d.Checklist).Include(d => d.Reserva).Include(d => d.Responsavel);
            return View(await applicationDbContext.ToListAsync());
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Observacoes,ChecklistId,ReservaId,ResponsavelId")] DoneChecklist doneChecklist)
        {
            if (id != doneChecklist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doneChecklist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoneChecklistExists(doneChecklist.Id))
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
            ViewData["ChecklistId"] = new SelectList(_context.Checklists, "Id", "Descricao", doneChecklist.ChecklistId);
            ViewData["ReservaId"] = new SelectList(_context.Reservas, "Id", "Id", doneChecklist.ReservaId);
            ViewData["ResponsavelId"] = new SelectList(_context.Users, "Id", "Id", doneChecklist.ResponsavelId);
            return View(doneChecklist);
        }

        // GET: DoneChecklists/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
