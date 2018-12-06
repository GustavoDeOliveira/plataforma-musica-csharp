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
    public class UsuariosController : MestreController
    {
        private readonly UsuarioDomain _domain;
        private readonly ApplicationContext _db;

        public UsuariosController(ApplicationContext db)
        {
            _db = db;
            _domain = new UsuarioDomain(db);
        }

        // GET: Usuarios
        public ActionResult Index(MensagemViewModel mensagem)
        {
            return View(mensagem);
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
            UsuarioEditarViewModel model = new UsuarioEditarViewModel();
            try
            {
                model.Usuario = await _domain.CarregarAsync(id);
                model.Preferencias = model.Usuario?.Preferencias != null ? string.Join(',', model.Usuario.Preferencias) : "";
                var etiquetas = await new EntidadeDomain<Etiqueta>(_db).ListarAsync();
                model.Etiquetas = etiquetas != null ? string.Join(',', etiquetas) : "";
            }
            catch
            {
                return RedirectToAction(nameof(Index), new MensagemViewModel
                {
                    Mensagem = "Ocorreu um erro ao carregar o usuário.", Tipo = TipoMensagem.Erro
                });
            }
            return View(model);
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