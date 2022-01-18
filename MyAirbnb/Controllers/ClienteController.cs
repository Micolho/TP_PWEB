using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAirbnb.Data;
using MyAirbnb.Models;
using MyAirbnb.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAirbnb.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public ClienteController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<ApplicationUser> listOfClients = new List<ApplicationUser>();

            foreach(ApplicationUser cliente in _userManager.Users.ToList())
            {
                if (cliente == null)
                    continue;

                if(await _userManager.IsInRoleAsync(cliente, "Cliente"))
                {
                    listOfClients.Add(cliente);
                }
            }

            return View(listOfClients);
        }

        public async Task<IActionResult> Details(string id)
        {
            var cliente = await _context.Users.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            //retornar os comentarios do user
            var classificacoes = await _context.Classificacaos
                                        .Where(c => c.UtilizadorId == cliente.Id)
                                        .ToListAsync();

            DetailsClienteViewModel model = new DetailsClienteViewModel
            {
                cliente = cliente,
                classificacoes = classificacoes
            };

            return View(model);
        }
    }
}
