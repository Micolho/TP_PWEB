using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyAirbnb.Data;
using MyAirbnb.Models;

namespace MyAirbnb.Controllers
{
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
            return RedirectToAction("Index");
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

            file.FilePath = file.FilePath.Replace("/Imagens/","");

            var filePath = Path.Combine(canonicalBasePath, file.FilePath);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(file.FilePath);
            }
            _context.Imagens.Remove(file);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Imagens/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Imagens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Path")] Imagens imagens)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(imagens);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(imagens);
        //}

        // GET: Imagens/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var imagens = await _context.Imagens.FindAsync(id);
        //    if (imagens == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(imagens);
        //}

        // POST: Imagens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Path")] Imagens imagens)
        //{
        //    if (id != imagens.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(imagens);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ImagensExists(imagens.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(imagens);
        //}

        // POST: Imagens/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var imagens = await _context.Imagens.FindAsync(id);
        //    _context.Imagens.Remove(imagens);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool ImagensExists(int id)
        {
            return _context.Imagens.Any(e => e.Id == id);
        }
    }
}
