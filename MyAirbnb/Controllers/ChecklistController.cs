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
using MyAirbnb.ViewModels;

namespace MyAirbnb.Controllers
{
    [Authorize(Roles = "Gestor")]
    public class ChecklistController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public ChecklistController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Checklists
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            //mostra apenas as checklists do utilizador em questao
            var applicationDbContext = _context.Checklists
                .Include(c => c.Categoria)
                .Where(c => c.DonoId == user.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Checklists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklist = await _context.Checklists
                .Include(c => c.Categoria)
                .Include(c => c.Dono)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checklist == null)
            {
                return NotFound();
            }

            return View(checklist);
        }

        // GET: Checklists/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome");    
            return View();
        }

        // POST: Checklists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Descricao,MomentoPreparacao,MomentoEntrega,DonoId,CategoriaId")] Checklist checklist)
        public async Task<IActionResult> Create(CreateChecklistViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(!model.MomentoPreparacao && !model.MomentoEntrega)
                {
                    ViewData["Erro"] = "You need to choose at least one of the moments!";
                    ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome");
                    return View();
                }
                    
                //get user
                var user = await _userManager.GetUserAsync(User);
                var userID = user.Id;
                //criar objeto checklist
                Checklist checklist = new Checklist
                {
                    CategoriaId = model.CategoriaId,
                    Descricao = model.Descricao,
                    MomentoEntrega = model.MomentoEntrega,
                    MomentoPreparacao = model.MomentoPreparacao,
                    DonoId = userID,
                };

                //adicionar
                _context.Add(checklist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", model.CategoriaId);
            //ViewData["DonoId"] = new SelectList(_context.Users, "Id", "Id", checklist.DonoId);
            return View(model);
        }

        // GET: Checklists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklist = await _context.Checklists
                .Include(c => c.Categoria)
                .Include(c => c.Dono)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checklist == null)
            {
                return NotFound();
            }

            return View(checklist);
        }

        // POST: Checklists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checklist = await _context.Checklists.FindAsync(id);
            _context.Checklists.Remove(checklist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
