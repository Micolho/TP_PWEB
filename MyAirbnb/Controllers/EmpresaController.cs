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
    [Authorize(Roles = "Admins, Gestor")]
    public class EmpresaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public EmpresaController(RoleManager<IdentityRole> roleManager, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;

        }

        // GET: Empresas
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var applicationDbContext = _context.Empresas.Include(e => e.Dono).Where(u => u.DonoId == user.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> IndexAdmin()
        {
            var applicationDbContext = _context.Empresas.Include(e => e.Dono);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Empresas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .Include(e => e.Dono)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }


            var funcionarios = await _context.Users.Where(f => f.EmpresaId == empresa.Id).ToListAsync();

            empresa.Funcionarios = new List<ApplicationUser>();
            if (funcionarios.Any())
            {
                empresa.Funcionarios = funcionarios;
            }

            return View(empresa);
        }

        // GET: Empresas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmpresaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    return NotFound();
                }

                var temEmpresa = _context.Empresas.Where(d => d.DonoId == user.Id).FirstOrDefault();

                if (temEmpresa != null)
                {   //TODO: alterar o index para possivelmente receber msg erros
                    return (RedirectToAction("Index", new { message = "Can't belong to more than 1 Enterprise" }));
                }

                Empresa empresa = new Empresa
                {
                    DonoId = user.Id,
                    Nome = model.Nome,
                };

                _context.Add(empresa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["DonoId"] = new SelectList(_context.Users, "Id", "Id", empresa.DonoId);
            return View(model);
        }

        // GET: Empresas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .Include(e => e.Dono)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaExists(int id)
        {
            return _context.Empresas.Any(e => e.Id == id);
        }

        public async Task<IActionResult> EditFuncionarios(string id)
        {
            List<UserRoleViewModel> model = new List<UserRoleViewModel>();

            var funcList = await _context.Users.Where(u => u.EmpresaId == int.Parse(id)).ToListAsync();

            foreach (ApplicationUser user in _userManager.Users.ToList())
            {


                UserRoleViewModel userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    Email = user.Email
                };

                if(await _userManager.IsInRoleAsync(user, "Funcionario"))
                {
                    //verificar se esta nesta empresa
                    if (funcList.Contains(user))
                    {
                        userRoleViewModel.IsSelected = true;
                    }
                    //se nao esta nesta empresa e esta desempregado
                    else if(user.EmpresaId == null)
                    {
                        userRoleViewModel.IsSelected = false;

                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditFuncionarios(List<UserRoleViewModel> model, string id)
        {
            for( int i = 0;  i<model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);
                
                if (user == null)
                    continue;

                //IdentityResult result = null;

                //bool IsInRole = await _userManager.IsInRoleAsync(user, "Funcionario");

                if(model[i].IsSelected)
                {

                    user.EmpresaId = int.Parse(id);
                    try
                    {
                        _context.Update(user);
                        await _context.SaveChangesAsync();
                    }catch (DbUpdateConcurrencyException)
                    {
                        return NotFound();
                    }

                    //result = await _userManager.AddToRoleAsync(user, "Funcionario");
                    //result = await _userManager.RemoveFromRoleAsync(user, "Cliente");
                }
                else if (!(model[i].IsSelected))
                {
                    user.EmpresaId = null;
                    try
                    {
                        _context.Update(user);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return NotFound();
                    }
                    //result = await _userManager.RemoveFromRoleAsync(user, "Funcionario");
                    //result = await _userManager.AddToRoleAsync(user, "Cliente");

                }
            }
            return RedirectToAction("EditFuncionarios", new { Id = id });
        }
    }
}
