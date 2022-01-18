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
using MyAirbnb.ViewModels;

namespace MyAirbnb.Controllers
{
    public class DoneChecklistController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public DoneChecklistController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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
            ViewData["ReservaId"] = reserva.Id;
            //verificar qual o tipo de imovel e as suas checklists 
            Imovel imovel = await _context.Imoveis.Where(i => i.Id == reserva.ImovelId && 
                                                        (i.ResponsavelId == user.Id || 
                                                         i.DonoId == user.Id))
                                            .FirstOrDefaultAsync();
            if (imovel == null)
                return NotFound();

            //var checklists = await _context.DoneChecklists
            //                        .Where(d => d.MomentoPreparacao &&
            //                               d.CategoriaId == imovel.TipoImovelId)
            //                        .ToListAsync(); 
            var checklists = await _context.DoneChecklists
                                     .Include(d => d.Checklist)
                                     .Where(d => d.Checklist.MomentoPreparacao &&
                                           d.Checklist.CategoriaId == imovel.TipoImovelId)
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

            ViewData["ReservaId"] = reserva.Id;

            //verificar qual o tipo de imovel e as suas checklists 
            Imovel imovel = await _context.Imoveis.Where(i => i.Id == reserva.ImovelId &&
                                                        (i.ResponsavelId == user.Id ||
                                                         i.DonoId == user.Id))
                                            .FirstOrDefaultAsync();
            if (imovel == null)
                return NotFound();

            var checklists = await _context.DoneChecklists
                                     .Include(d => d.Checklist)
                                     .Where(d => d.Checklist.MomentoEntrega &&
                                           d.Checklist.CategoriaId == imovel.TipoImovelId)
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

            doneChecklist.Imagens = new List<Imagens>();

            var imagens = await _context.Imagens
                .Where(m => m.ImovelId == doneChecklist.Id).ToArrayAsync();
            if (imagens != null)
                doneChecklist.Imagens = imagens;

            return View(doneChecklist);
        }

        // GET: DoneChecklists/Create
        public async Task<IActionResult> Create(int reservaId, bool IsPreparacao)
        {
            var reserva = await _context.Reservas.Where(r => r.Id == reservaId)
                                                 .Include(r => r.Imovel)
                                                 .FirstOrDefaultAsync();
            if (reserva == null)
                return NotFound();

            List<Checklist> checklists = null;

            if (IsPreparacao)
                checklists = await _context.Checklists.Where(c => c.CategoriaId == reserva.Imovel.TipoImovelId
                                                             && c.MomentoPreparacao)
                                                       .ToListAsync();
            else
                checklists = await _context.Checklists.Where(c => c.CategoriaId == reserva.Imovel.TipoImovelId
                                                             && c.MomentoEntrega)
                                                       .ToListAsync();

            CreateDoneChecklistViewModel model = new CreateDoneChecklistViewModel
            {
                IsPreparacao = IsPreparacao,
                ReservaId = reservaId,
            };

            ViewData["ChecklistId"] = new SelectList(checklists, "Id", "Descricao");

            return View(model);
        }

        // POST: DoneChecklists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDoneChecklistViewModel model)
        {

            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                DoneChecklist doneChecklist = new DoneChecklist
                {
                    Observacoes = model.Observacoes,
                    ChecklistId = model.ChecklistId,
                    ReservaId = model.ReservaId,
                    ResponsavelId = user.Id,
                };
                _context.Add(doneChecklist);
                await _context.SaveChangesAsync();
                if(model.IsPreparacao)
                    return RedirectToAction(nameof(IndexPreparacao), new {Id = model.ReservaId });

                return RedirectToAction(nameof(IndexEntrega), new { Id = model.ReservaId });
            }

            var reserva = await _context.Reservas.Where(r => r.Id == model.ReservaId)
                                                   .Include(r => r.Imovel)
                                                   .FirstOrDefaultAsync();
            if (reserva == null)
                return NotFound();

            List<Checklist> checklists = null;

            if (model.IsPreparacao)
                checklists = await _context.Checklists.Where(c => c.CategoriaId == reserva.Imovel.TipoImovelId
                                                             && c.MomentoPreparacao)
                                                       .ToListAsync();
            else
                checklists = await _context.Checklists.Where(c => c.CategoriaId == reserva.Imovel.TipoImovelId
                                                             && c.MomentoEntrega)
                                                       .ToListAsync();

            ViewData["ChecklistId"] = new SelectList(checklists, "Id", "Descricao");
            return View();
        }
    }
}
