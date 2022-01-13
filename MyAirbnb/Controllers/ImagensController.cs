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

        public ImagensController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int? id, List<IFormFile> files)
        {
            if (id == null)
            {
                return BadRequest();
            }
            foreach (var file in files)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
                bool basePathExists = System.IO.Directory.Exists(basePath);
                if (!basePathExists) Directory.CreateDirectory(basePath);

                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var filePath = Path.Combine(basePath, file.FileName);
                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var fileModel = new Imagens()
                    {
                        FilePath = filePath
                    };
                    _context.Imagens.Add(fileModel);
                    _context.SaveChanges();
                }
            }
            TempData["Message"] = "File successfully uploaded to File System.";
            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> Delete(int id)
        //{
        //    var file = await _context.Imagens.Where(x => x.Id == id).FirstOrDefaultAsync();
        //    if (file == null) return null;
        //    if (System.IO.File.Exists(file.FilePath))
        //    {
        //        System.IO.File.Delete(file.FilePath);
        //    }
        //    _context.Imagens.Remove(file);
        //    _context.SaveChanges();
        //    TempData["Message"] = $"Removed {file.Name + file.Extension} successfully from File System.";
        //    return RedirectToAction("Index");
        //}

        //// GET: Imagens
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Imagens.ToListAsync());
        //}

        // GET: Imagens/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Imagens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Imagens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Path")] Imagens imagens)
        {
            if (ModelState.IsValid)
            {
                _context.Add(imagens);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(imagens);
        }

        // GET: Imagens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagens = await _context.Imagens.FindAsync(id);
            if (imagens == null)
            {
                return NotFound();
            }
            return View(imagens);
        }

        // POST: Imagens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Path")] Imagens imagens)
        {
            if (id != imagens.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imagens);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImagensExists(imagens.Id))
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

        // POST: Imagens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imagens = await _context.Imagens.FindAsync(id);
            _context.Imagens.Remove(imagens);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImagensExists(int id)
        {
            return _context.Imagens.Any(e => e.Id == id);
        }
    }
}
