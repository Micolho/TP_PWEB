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
    public class ImovelController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ImovelController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Imovel
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Imoveis.Include(i => i.Dono).Include(i => i.Responsavel).Include(i => i.TipoImovel);
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index([Bind("EspacoM2,PrecoPorNoite,NumeroCamas,TemCozinha,TemJacuzzi,TemPiscina,numeroWC,NumeroPessoas,HoraCheckIn,HoraCheckOut,Localidade,Rua,TipoImovelId,Descricao,DonoId,ResponsavelId")] Imovel imovel)
        {
            var applicationDbContext = _context.Imoveis
                .Include(i => i.Dono)
                .Include(i => i.Responsavel)
                .Include(i => i.TipoImovel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Imovel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imovel = await _context.Imoveis
                .Include(i => i.Dono)
                .Include(i => i.Responsavel)
                .Include(i => i.TipoImovel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imovel == null)
            {
                return NotFound();
            }

            return View(imovel);
        }

        // GET: Imovel/Create
        public async Task<IActionResult> Create()
        {
            ViewData["DonoId"] = new SelectList(_context.Users, "Id", "Id");
            //get user enterprise
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            ApplicationUser Patrao = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

            if (Patrao == null)
            {
                return NotFound();
            }

            Empresa empresa = await _context.Empresas.Where(d => d.DonoId == userId).FirstOrDefaultAsync();

            if (empresa != null)
            {
///                ICollection<ApplicationUser> listFuncionarios = new List<ApplicationUser>();
 //               listFuncionarios = empresa.Funcionarios;
                ViewData["ResponsavelId"] = new SelectList( empresa.Funcionarios, "Id", "Nome");
            }
            else
            {
                ViewData["ResponsavelId"] = user.Nome;
            }
            ViewData["TipoImovelId"] = new SelectList(_context.Categorias, "Id", "Nome");
            return View();
        }

        // POST: Imovel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateImovelViewModel  model)
        {
            if (ModelState.IsValid)
            {
                //create object imovel
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return NotFound();


                Imovel imovel = new Imovel
                {
                    PrecoPorNoite = model.PrecoPorNoite,
                    EspacoM2 = model.EspacoM2,
                    Nome = model.Nome,
                    NumeroCamas = model.NumeroCamas,
                    NumeroPessoas = model.NumeroPessoas,
                    numeroWC = model.numeroWC,
                    TemCozinha = model.TemCozinha,
                    TemJacuzzi = model.TemJacuzzi,
                    TemPiscina = model.TemPiscina,
                    HoraCheckIn = model.HoraCheckIn,
                    HoraCheckOut = model.HoraCheckOut,
                    Localidade = model.Localidade,
                    Rua = model.Rua,
                    TipoImovelId = model.TipoImovelId,
                    Descricao = model.Descricao,
                    DonoId = user.Id,
                    ResponsavelId = user.Id,
                };

                _context.Add(imovel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ///ViewData["DonoId"] = new SelectList(_context.Users, "Id", "Id", imovel.DonoId);
            //ViewData["ResponsavelId"] = new SelectList(_context.Users, "Id", "Nome", imovel.ResponsavelId);
            ViewData["TipoImovelId"] = new SelectList(_context.Categorias, "Id", "Nome", model.TipoImovelId);
            return View(model);
        }

        // GET: Imovel/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["DonoId"] = new SelectList(_context.Users, "Id", "Id", imovel.DonoId);
            ViewData["ResponsavelId"] = new SelectList(_context.Users, "Id", "Id", imovel.ResponsavelId);
            ViewData["TipoImovelId"] = new SelectList(_context.Categorias, "Id", "Nome", imovel.TipoImovelId);
            return View(imovel);
        }

        // POST: Imovel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,EspacoM2,PrecoPorNoite,NumeroCamas,TemCozinha,TemJacuzzi,TemPiscina,numeroWC,NumeroPessoas,HoraCheckIn,HoraCheckOut,Localidade,Rua,TipoImovelId,Descricao,DonoId,ResponsavelId")] Imovel imovel)
        {
            if (id != imovel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imovel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImovelExists(imovel.Id))
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
            ViewData["DonoId"] = new SelectList(_context.Users, "Id", "Id", imovel.DonoId);
            ViewData["ResponsavelId"] = new SelectList(_context.Users, "Id", "Id", imovel.ResponsavelId);
            ViewData["TipoImovelId"] = new SelectList(_context.Categorias, "Id", "Nome", imovel.TipoImovelId);
            return View(imovel);
        }

        // GET: Imovel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imovel = await _context.Imoveis
                .Include(i => i.Dono)
                .Include(i => i.Responsavel)
                .Include(i => i.TipoImovel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imovel == null)
            {
                return NotFound();
            }

            return View(imovel);
        }

        // POST: Imovel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imovel = await _context.Imoveis.FindAsync(id);
            _context.Imoveis.Remove(imovel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImovelExists(int id)
        {
            return _context.Imoveis.Any(e => e.Id == id);
        }
    }
}
