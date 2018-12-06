using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plataforma.Lib.Domain;
using Plataforma.Lib.Models;
using Plataforma.Lib.ViewModels;

namespace Plataforma.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioDomain _domain;

        public UsuarioController(ApplicationContext context)
        {
            _domain = new UsuarioDomain(context);
        }

        // GET: Usuario
        public async Task<JsonResult> Index()
        {
            var json = new JsonViewModel<IEnumerable<Usuario>>
            {
                Sucesso = true,
                Mensagem = "ok"
            };

            try
            {
                json.Dados = await _domain.ListarAsync();
            }
            catch(Exception ex)
            {
                json.Sucesso = false;
                json.Mensagem = ex.Message;
            }

            return Json(json);
        }

        // GET: Usuario/5
        public async Task<IActionResult> Index(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var usuario = await _domain.CarregarAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuario/Cadastro
        public IActionResult Cadastro()
        {
            return View(new Usuario());
        }

        // POST: Usuario/Cadastro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro([Bind("Nome,Email,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                await _domain.SalvarAsync(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuario/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var usuario = await _domain.CarregarAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar([Bind("Nome,Email,Senha,ID")] Usuario usuario)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await _domain.SalvarAsync(usuario);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _domain.EncontrarAsync(usuario.Id))
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
            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Desativar(int id)
        {
            if (id == 0) return NotFound();
            try
            {
                await _domain.DesativarAsync(id);
                return RedirectToAction(nameof(HomeController.Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _domain.EncontrarAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
