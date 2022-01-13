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
            var applicationDbContext = _context.Imoveis.Include(i => i.Dono)
                                                .Include(i => i.Responsavel)
                                                .Include(i => i.TipoImovel)
                                                .Where(i => i.Listado);
            return View(await applicationDbContext.OrderBy(c => c.Localidade).ToListAsync());
        }

        //TODO: CRIAR VISTA PARA ESTA ACTION
        public async Task<IActionResult> MyIndex()
        {
            var user = await _userManager.GetUserAsync(User);
            var applicationDbContext = _context.Imoveis.Include(i => i.Dono)
                                                .Include(i => i.Responsavel)
                                                .Include(i => i.TipoImovel)
                                                .Where(i => i.DonoId == user.Id || i.ResponsavelId == user.Id);
            return View(await applicationDbContext.OrderBy(c => c.Localidade).ToListAsync());
        }

        //[HttpPost]
        //public async Task<IActionResult> Index([Bind("EspacoM2,PrecoPorNoite,NumeroCamas,TemCozinha,TemJacuzzi,TemPiscina,numeroWC,NumeroPessoas,HoraCheckIn,HoraCheckOut,Localidade,Rua,TipoImovelId,Descricao,DonoId,ResponsavelId")] Imovel imovel)
        //{
        //    var applicationDbContext = _context.Imoveis
        //        .Include(i => i.Dono)
        //        .Include(i => i.Responsavel)
        //        .Include(i => i.TipoImovel);
        //    return View(await applicationDbContext.ToListAsync());
        //}

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


            imovel.Classificacao = new List<Classificacao>();
            var reviews = await _context.Classificacaos
                .Where(m => m.ImovelId == imovel.Id).ToArrayAsync();

            if (reviews != null)
            {
                imovel.Classificacao = reviews;
            }

            return View(imovel);
        }

        // GET: Imovel/Create
        public async Task<IActionResult> Create()
        {
            //get user enterprise
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            ApplicationUser Patrao = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

            if (Patrao == null)
            {
                return NotFound();
            }

            Empresa empresa = await _context.Empresas.Where(d => d.DonoId == userId).FirstOrDefaultAsync();
            List<ApplicationUser> userList = new List<ApplicationUser>();
            if (empresa != null)
            {
                userList = await _context.Users.Where(u => u.EmpresaId == empresa.Id).ToListAsync();
            }
            userList.Add( await _context.Users.Where(u => u.Id == user.Id).FirstAsync());
            ViewData["ResponsavelId"] = new SelectList(userList, "Id", "Nome");
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
                    ResponsavelId = model.ResponsavelId,
                    DonoId = user.Id,
                    Listado = model.Listado
                };

                _context.Add(imovel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var user2 = await _userManager.GetUserAsync(User);
            var userId = user2.Id;
            ApplicationUser Patrao = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

            if (Patrao == null)
            {
                return NotFound();
            }

            Empresa empresa = await _context.Empresas.Where(d => d.DonoId == userId).FirstOrDefaultAsync();
            List<ApplicationUser> userList = new List<ApplicationUser>();
            if (empresa != null)
            {
                userList = await _context.Users.Where(u => u.EmpresaId == empresa.Id).ToListAsync();
            }
            userList.Add(await _context.Users.Where(u => u.Id == userId).FirstAsync());
            ViewData["ResponsavelId"] = new SelectList(userList, "Id", "Nome");

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
            var user = await _userManager.GetUserAsync(User);
            //reutilizar o viewmodel uma vez que a informacao e a mesma necessaria!

            if (user.Id != imovel.DonoId)
            {
                return NotFound();
            }

            CreateImovelViewModel model = new CreateImovelViewModel
            {
                Id = imovel.Id,    
                PrecoPorNoite = imovel.PrecoPorNoite,
                EspacoM2 = imovel.EspacoM2,
                Nome = imovel.Nome,
                NumeroCamas = imovel.NumeroCamas,
                NumeroPessoas = imovel.NumeroPessoas,
                numeroWC = imovel.numeroWC,
                TemCozinha = imovel.TemCozinha,
                TemJacuzzi = imovel.TemJacuzzi,
                TemPiscina = imovel.TemPiscina,
                HoraCheckIn = imovel.HoraCheckIn,
                HoraCheckOut = imovel.HoraCheckOut,
                Localidade = imovel.Localidade,
                Rua = imovel.Rua,
                TipoImovelId = imovel.TipoImovelId,
                Descricao = imovel.Descricao,
                ResponsavelId = imovel.ResponsavelId,
                DonoId = user.Id,
                Listado = imovel.Listado
            };

            var userId = user.Id;
            ApplicationUser Patrao = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

            if (Patrao == null)
            {
                return NotFound();
            }

            Empresa empresa = await _context.Empresas.Where(d => d.DonoId == userId).FirstOrDefaultAsync();
            List<ApplicationUser> userList = new List<ApplicationUser>();
            if (empresa != null)
            {
                userList = await _context.Users.Where(u => u.EmpresaId == empresa.Id).ToListAsync();
            }
            userList.Add(await _context.Users.Where(u => u.Id == user.Id).FirstAsync());
            ViewData["ResponsavelId"] = new SelectList(userList, "Id", "Nome");
            ViewData["TipoImovelId"] = new SelectList(_context.Categorias, "Id", "Nome", model.TipoImovelId);
            return View(model);
        }

        // POST: Imovel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CreateImovelViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            var oldImovel = await _context.Imoveis.FindAsync(id);

            if (user.Id != oldImovel.DonoId)
            {
                return NotFound();
            }

            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Imovel imovel = new Imovel
                {
                    Id = model.Id,
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
                    ResponsavelId = model.ResponsavelId,
                    DonoId = user.Id,
                    Listado = model.Listado
                };

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

            //var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            ApplicationUser Patrao = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

            if (Patrao == null)
            {
                return NotFound();
            }

            Empresa empresa = await _context.Empresas.Where(d => d.DonoId == userId).FirstOrDefaultAsync();

            if (empresa != null)
            {
                empresa.Funcionarios = new List<ApplicationUser>();
                empresa.Funcionarios = await _context.Users.Where(u => u.EmpresaId == empresa.Id).ToArrayAsync();

                ViewData["ResponsavelId"] = new SelectList(empresa.Funcionarios, "Id", "Nome", model.ResponsavelId);
            }
            else
            {
                List<ApplicationUser> userList = await _context.Users.Where(u => u.Id == user.Id).ToListAsync();
                ViewData["ResponsavelId"] = new SelectList(userList, "Id", "Nome", model.ResponsavelId);
            }
            ViewData["TipoImovelId"] = new SelectList(_context.Categorias, "Id", "Nome", model.TipoImovelId);
            return View(model);
        }

        private bool ImovelExists(int id)
        {
            return _context.Imoveis.Any(e => e.Id == id);
        }
    }
}
