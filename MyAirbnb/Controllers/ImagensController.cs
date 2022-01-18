using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyAirbnb.Data;
using MyAirbnb.Models;

namespace MyAirbnb.Controllers
{
    [Authorize(Roles = "Funcionario ,Gestor, Admin")]
    public class ImagensController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string relativeBasePath = Path.Combine(
                    Path.DirectorySeparatorChar + "Imagens" + Path.DirectorySeparatorChar
            );  //  => /Imagens/
        private readonly string canonicalBasePath;

        public ImagensController(ApplicationDbContext context)
        {
            _context = context;
            canonicalBasePath = Path.Combine(
                Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "wwwroot" + relativeBasePath
                );  //  => ~/wwwroot/Imagens/
            if (!Directory.Exists(canonicalBasePath))
                Directory.CreateDirectory(canonicalBasePath);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int id, bool isImovel, List<IFormFile> files)
        {
            //if (id == null)
            //{   // the id of the imovel must be specified
            //    return NotFound();
            //}
            
            foreach (var file in files)
            {
                var filename = DateTime.Now.Ticks/TimeSpan.TicksPerMillisecond + "_" + file.FileName;
                var filePath = Path.Combine(canonicalBasePath, filename);
                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var model = new Imagens()
                    {
                        FilePath = Path.Combine(relativeBasePath, filename),   //=> /Imagens/file.png
                    };
                    if (isImovel)
                        model.ImovelId = id;
                    else
                        model.DoneChecklistId = id;

                    _context.Imagens.Add(model);
                    _context.SaveChanges();
                }
            }
            var thisId = id;
            //return RedirectToAction("Index");
            if(isImovel)
                return RedirectToAction("Details", "Imovel", new {id = thisId });
            else
                return RedirectToAction("Details", "DoneChecklist", new { id = thisId });
        }

        // GET: Imagens
        public async Task<IActionResult> Index()
        {
            return View(await _context.Imagens.ToListAsync());
        }

        // GET: Imagens/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var imagens = await _context.Imagens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imagens == null)
            {
                return NotFound();
            }

            return View(imagens);
        }

        // GET: Imagens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagens = await _context.Imagens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imagens == null)
            {
                return NotFound();
            }

            return View(imagens);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var file = await _context.Imagens.FirstOrDefaultAsync(x => x.Id == id);
            
            if (file == null)
                return NotFound();

            var filePath = file.FilePath.Replace("\\Imagens\\","");

            var filePathv2 = Path.Combine(canonicalBasePath, filePath);

            if (System.IO.File.Exists(filePathv2))
            {
                System.IO.File.Delete(filePathv2);
            }
            _context.Imagens.Remove(file);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
