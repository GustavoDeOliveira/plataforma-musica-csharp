using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plataforma.Admin.Models;
using Plataforma.Lib.Domain;
using Plataforma.Lib.Models;
using Plataforma.Lib.Models.ViewModels;
using Plataforma.Lib.ViewModels;

namespace Plataforma.Admin.Controllers
{
    public class MusicasController : MestreController
    {
        private readonly MusicaDomain _domain;

        public MusicasController(ApplicationContext db)
        {
            _domain = new MusicaDomain(db);
        }

        // GET: Musicas
        public ActionResult Index(MensagemViewModel mensagem)
        {
            return View(mensagem);
        }

        // GET: Musicas/Listar?skip=10&take=20&_=123456
        public async Task<ActionResult> Listar(DataTablesParamsModel parametros)
        {
            var retorno = new RetornoDataTablesModel<ListaMusicaItemModel>();
            try
            {
                retorno.Data = (await _domain.ListarAsync(parametros.Skip, parametros.Take, apenasAtivos: false))
                    .Select(m => new ListaMusicaItemModel
                    {
                        Nome = m.Nome,
                        Autores = m.Autores.Select(a => new ListaAutorItemModel
                        {
                            Id = a.Autor.Id,
                            Nome = a.Autor.Nome
                        })
                    });
                retorno.Draw = parametros.Draw;
                retorno.RecordsTotal = await _domain.ContarAsync();
                retorno.RecordsFiltered = retorno.Data.Count();
                retorno.Error = "";
            }
            catch
            {
                retorno.Error = "Ocorreu um erro ao carregar as músicas.";
            }
            return Json(retorno);
        }

        // GET: Musicas/5
        //[Route("/Musicas/{id}")]
        public async Task<ActionResult> Detalhes(int id)
        {
            Musica m;
            try
            {
                m = await _domain.CarregarAsync(id);
            }
            catch
            {
                return RedirectToAction(nameof(Index), new MensagemViewModel
                {
                    Mensagem = "Ocorreu um erro ao carregar a música.", Tipo = TipoMensagem.Erro
                });
            }
            return View(m);
        }

        // POST: Musicas/Excluir/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Excluir(int id)
        {
            try
            {
                await _domain.ExcluirAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index), new MensagemViewModel
                {
                    Mensagem = "Ocorreu um erro ao excluir a música.",
                    Tipo = TipoMensagem.Erro
                });
            }
        }
    }
}