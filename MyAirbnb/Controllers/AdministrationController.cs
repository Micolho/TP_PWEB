using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyAirbnb.Models;
using MyAirbnb.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAirbnb.Controllers
{
    [Authorize(Roles = "Admins")]
    public class AdministrationController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
    
        //criar roles, listar utilizadores na role, adicionar ou remover da roles

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    //TODO:CHANGE THIS
                    return RedirectToAction("Index");
                }

                foreach (IdentityError erro in result.Errors)
                {
                    ModelState.AddModelError("", erro.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> DetalhesRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if(role == null)
            {
                return NotFound();
            }

            var model = new EditRoleViewModel
            {
                Id = id,
                RoleName = role.Name,
            };

            //Users no Role
            foreach(ApplicationUser user in _userManager.Users.ToList())
            {
                if( await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.Nome);
                }
            }
            

            return View(model);
        }

        public async Task<IActionResult> EditUsersInRole(string id)
        {

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            ViewBag.RoleName = role.Name;
            ViewBag.RoleId = role.Id;

            List<UserRoleViewModel> model = new List<UserRoleViewModel>();

            foreach (ApplicationUser user in _userManager.Users.ToList())
            {
                UserRoleViewModel userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    Email = user.Email
                };

                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    //esta no role
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    // nao esta no role
                    userRoleViewModel.IsSelected= false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            for(int i=0; i<model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);

                if(user == null)
                {
                    continue;
                }

                IdentityResult result = null;

                bool IsInRole = await _userManager.IsInRoleAsync(user, role.Name);

                if(model[i].IsSelected && !(IsInRole))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!(model[i].IsSelected) && (IsInRole))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
            }

            return RedirectToAction("DetalhesRole", new { Id = role.Id });
        }
    }
}
