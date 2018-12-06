using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plataforma.Admin.Models;
using Plataforma.Lib.Domain;
using Plataforma.Lib.Models;
using Plataforma.Lib.Models.ViewModels;
using Plataforma.Lib.ViewModels;

namespace Plataforma.Admin.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UsuarioDomain _domain;

        public UsuariosController(ApplicationContext db)
        {
            _domain = new UsuarioDomain(db);
        }

        // GET: Usuarios
        public ActionResult Index(MensagemViewModel mensagem)
        {
            return View(mensagem);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Autenticar(Usuario credenciais)
        {
            var usuario = await _domain.EncontrarAsync(credenciais);

            if (usuario == null)
                return BadRequest(new { message = "Login ou senha incorretos." });

            return RedirectToAction(nameof(Index));
        }

        // GET: Usuarios/Listar?start=10&length=20
        public async Task<ActionResult> Listar(DataTablesParamsModel parametros)
        {
            var retorno = new RetornoDataTablesModel<ListaUsuarioItemModel>();
            try
            {
                retorno.Data = (await _domain.ListarAsync(parametros.Skip, parametros.Take, apenasAtivos: false))
                    .Select(u => new ListaUsuarioItemModel
                    {
                        Id = u.Id,
                        Nome = u.Nome,
                        Email = u.Email,
                        Ativo = u.Ativo
                    });
                retorno.Draw = parametros.Draw;
                retorno.RecordsTotal = await _domain.ContarAsync(apenasAtivos: false);
                retorno.RecordsFiltered = retorno.Data.Count();
                retorno.Error = "";
            }
            catch
            {
                retorno.Error = "Ocorreu um erro ao carregar os usuários.";
            }
            return Json(retorno);
        }

        // GET: Usuarios/Editar/5
        public async Task<ActionResult> Editar(int id)
        {
            Usuario u;
            try
            {
                u = await _domain.CarregarAsync(id);
            }
            catch
            {
                return RedirectToAction(nameof(Index), new MensagemViewModel
                {
                    Mensagem = "Ocorreu um erro ao carregar o usuário.", Tipo = TipoMensagem.Erro
                });
            }
            return View(u);
        }

        // POST: Usuarios/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Usuario usuario)
        {
            try
            {
                await _domain.SalvarAsync(usuario);
            }
            catch
            {
                return RedirectToAction(nameof(Index), new MensagemViewModel
                {
                    Mensagem = "Ocorreu um erro ao salvar o usuário.",
                    Tipo = TipoMensagem.Erro
                });
            }
            return RedirectToAction(nameof(Index), new MensagemViewModel
            {
                Mensagem = "Usuário salvo com sucesso.",
                Tipo = TipoMensagem.Sucesso
            });
        }

        // POST: Usuarios/Excluir/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Excluir(int id)
        //{
        //    try
        //    {
        //        await _domain.ExcluirAsync(id);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return RedirectToAction(nameof(Index), new MensagemViewModel
        //        {
        //            Mensagem = "Ocorreu um erro ao excluir o usuário.",
        //            Tipo = TipoMensagem.Erro
        //        });
        //    }
        //}

        // POST: Usuarios/Ativacao/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Ativacao([Bind("Id,Ativo")]Usuario usuario)
        {
            var model = new JsonViewModel<bool>();
            try
            {
                if (usuario.Ativo)
                    await _domain.AtivarAsync(usuario.Id);
                else await _domain.DesativarAsync(usuario.Id);
                model.Sucesso = true;
                model.Mensagem = "ok";
                model.Dados = usuario.Ativo;
            }
            catch
            {
                model.Sucesso = false;
                model.Mensagem = "Ocorreu um erro ao alterar ativação do usuário.";
            }
            return Json(model);
        }
    }
}