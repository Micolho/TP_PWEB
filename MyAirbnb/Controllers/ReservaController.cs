using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAirbnb.Data;
using MyAirbnb.Models;
using MyAirbnb.ViewModels;

namespace MyAirbnb.Controllers
{
    [Authorize]
    public class ReservaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservaController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reserva
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            List<Reserva> ListaReservas;

            if (User.IsInRole("Cliente"))
            {
                user = await _userManager.GetUserAsync(User);
                ListaReservas = await _context.Reservas
                                            .Include(r => r.Cliente)
                                            .Include(r => r.Imovel)
                                            .Where(r => r.ClienteId == user.Id)
                                            .OrderBy(d => d.DataCheckin)
                                            .ToListAsync();
                return View(ListaReservas);
            }

            ListaReservas = await _context.Reservas
                                        .Include(r => r.Cliente)
                                        .Include(r => r.Imovel)
                                        .Where(r => r.Imovel.DonoId == user.Id || r.Imovel.ResponsavelId == user.Id)
                                        .OrderBy(d => d.DataCheckin)
                                        .ToListAsync();
            return View(ListaReservas);
        }

        // GET: Reserva/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Imovel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reserva/Create/<ImovelId>
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var imovel = await _context.Imoveis.FindAsync(id);
            if (imovel == null)
            {
                return NotFound();
            }

            CreateReservaViewModel model = new CreateReservaViewModel
            {
                ImovelId = imovel.Id,
                ImovelNome = imovel.Nome,
                DataCheckin = DateTime.Today,
                DataCheckout = DateTime.Today,
            };

            return View(model);
        }

        // POST: Reserva/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateReservaViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                if(model.DataCheckin <= DateTime.Today || model.DataCheckout <= DateTime.Today)
                {
                    ViewData["Erro"] = "Sadly, you can't time travel to the past to enjoy this amazing reserve...";
                    return View(model);
                }

                if(model.DataCheckin >= model.DataCheckout)
                {
                    ViewData["Erro"] = "Data check-in can't be posterior to Data check-out";
                    return View(model);
                }

                var reservasAt = _context.Reservas
                                        .Where(r => r.ImovelId == model.ImovelId);

                Reserva availableCheckinAt = await reservasAt
                                        .Where(r => r.DataCheckin >= model.DataCheckin && 
                                                     r.DataCheckin <= model.DataCheckout)
                                        .Where(r => r.Confirmado)
                                        .FirstOrDefaultAsync();

                if(availableCheckinAt != null)
                {
                    ViewData["Erro"] = "The Check-in Date is unavailable! \n" +
                        "Check-in Date is available before " + availableCheckinAt.DataCheckin.ToShortDateString() + 
                        " or after " + availableCheckinAt.DataCheckout.ToShortDateString();
                    return View(model);
                }

                Reserva availableCheckoutAt = await reservasAt
                                        .Where(r => r.DataCheckout >= model.DataCheckin &&
                                                    r.DataCheckout <= model.DataCheckout)
                                        .Where(r => r.Confirmado)
                                        .FirstOrDefaultAsync();

                if (availableCheckoutAt != null)
                {
                    ViewData["Erro"] = "The Check-out Date is unavailable! \n" +
                        "Check-out Date is available before " + availableCheckoutAt.DataCheckin.ToShortDateString() + 
                        " or after " + availableCheckoutAt.DataCheckout.ToShortDateString();
                    return View(model);
                }

                Reserva reserva = new Reserva
                {
                    ClienteId = user.Id,
                    DataCheckin = model.DataCheckin,
                    DataCheckout = model.DataCheckout,
                    Confirmado = model.Confirmado,
                    ImovelId = model.ImovelId,
                };

                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [Authorize(Roles = "Funcionario, Gestor ,Admins")]
        // GET: Reserva/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                                .Include(r => r.Cliente)
                                .Include(r => r.Imovel)
                                .Where(r => r.Id == id)
                                .FirstAsync();
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reserva/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Funcionario, Gestor ,Admins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataCheckin,DataCheckout,Confirmado,ImovelId,ClienteId")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = id });
            }
            return View(reserva);
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
