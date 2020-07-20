using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e.mix.repository;
using e.mix.repository.Models;
using e.mix.Services.Interfaces;
using System.Text.RegularExpressions;
using System.Threading;
using e.mix.Models;

namespace e.mix.Controllers
{
    public class CepController : Controller
    {
        private readonly CEPContext _context;
        private readonly IBuscaCEPClient _client;

        public CepController(CEPContext context, IBuscaCEPClient client)
        {
            _context = context;
            _client = client;
        }

        // GET: Ceps
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cep.ToListAsync());
        }

        [HttpGet]
        public async Task<BuscaCEPResult> GetCep(string cep)
        {
            //Busca as informacoes pelo banco, caso nao exista vai ao site VIACEP
            var db = await _context.Cep.FirstOrDefaultAsync(m => m.Cep1 == cep);
            if (db != null)
                return await _client.GetAsync(cep, CancellationToken.None);
            else
                return new BuscaCEPResult
                {
                    Bairro = db.Bairro,
                    Cep = db.Cep1,
                    Complemento = db.Complemento,
                    GIACode = int.Parse(db.Gia),
                    IBGECode = (int)db.Ibge,
                    Logradouro = db.Logradouro,
                    UF = db.Uf,
                    Cidade = db.Localidade,
                    Unidade = db.Unidade?.ToString()
                };
        }

        // GET: Ceps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cep = await _context.Cep
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cep == null)
            {
                return NotFound();
            }

            return View(cep);
        }

        // GET: Ceps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ceps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cep1,Logradouro,Complemento,Bairro,Localidade,Uf,Unidade,Ibge,Gia")] Cep cep)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cep);
        }

        // GET: Ceps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cep = await _context.Cep.FindAsync(id);
            if (cep == null)
            {
                return NotFound();
            }
            return View(cep);
        }

        // POST: Ceps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cep1,Logradouro,Complemento,Bairro,Localidade,Uf,Unidade,Ibge,Gia")] Cep cep)
        {
            if (id != cep.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cep);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CepExists(cep.Id))
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
            return View(cep);
        }

        // GET: Ceps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cep = await _context.Cep
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cep == null)
            {
                return NotFound();
            }

            return View(cep);
        }

        // POST: Ceps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cep = await _context.Cep.FindAsync(id);
            _context.Cep.Remove(cep);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CepExists(int id)
        {
            return _context.Cep.Any(e => e.Id == id);
        }
    }
}
