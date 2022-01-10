using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyAirbnb.Models;
using MyAirbnb.ViewModels;
using System.Threading.Tasks;

namespace MyAirbnb.Controllers
{
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
                    RedirectToAction("Index");
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
                NotFound();
            }

            var model = new EditRoleViewModel
            {
                Id = id,
                RoleName = role.Name,
            };

            //Users no Role

            

            return View(model);
        }

        public IActionResult EditUsersInRole(string id)
        {
            return View();
        }
    }
}
