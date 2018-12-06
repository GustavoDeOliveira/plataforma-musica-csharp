using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plataforma.Lib.Models;

namespace Plataforma.Lib.Domain
{
    public class EntidadeDomain<TEntidade> where TEntidade : Entidade
    {
        protected readonly ApplicationContext _db;

        public EntidadeDomain(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<TEntidade>> ListarAsync(int pag = 0, int qtd = 0, bool apenasAtivos = true)
        {
            var query = _db.Set<TEntidade>().AsNoTracking();

            return await _listarAsync(query, pag, qtd, apenasAtivos);
        }

        public async Task<TEntidade> CarregarAsync(int id)
        {
            return await _db.FindAsync<TEntidade>(id);
        }

        public async Task ExcluirAsync(int id)
        {
            await ExcluirAsync(_db.Find<TEntidade>(id));
        }

        public async Task ExcluirAsync(TEntidade entidade)
        {
            _db.Remove(entidade);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> EncontrarAsync(int id, bool apenasAtivos = true)
        {
            var query = _db.Set<TEntidade>().AsNoTracking();
            return await (apenasAtivos ? query.AnyAsync(x => x.Id == id && x.Ativo)
                                       : query.AnyAsync(x => x.Id == id));
        }

        public async Task<int> ContarAsync(bool apenasAtivos = true)
        {
            var query = _db.Set<TEntidade>().AsNoTracking();
            return await (apenasAtivos ? query.CountAsync(x => x.Ativo) 
                                       : query.CountAsync());
        }

        public async Task SalvarAsync(TEntidade entidade)
        {
            if (entidade.Id > 0)
            {
                entidade.ModificadoEm = DateTime.Now;
                _db.Update(entidade);
            }
            else
            {
                entidade.CriadoEm = DateTime.Now;
                entidade.Ativo = true;
                _db.Add(entidade);
            }
            await _db.SaveChangesAsync();
        }

        public async Task DesativarAsync(int id)
        {
            await _AlterarAtivacaoAsync(id, false);
        }

        public async Task AtivarAsync(int id)
        {
            await _AlterarAtivacaoAsync(id, true);
        }

        private async Task _AlterarAtivacaoAsync(int id, bool ativo)
        {
            TEntidade entidade = await _db.FindAsync<TEntidade>(id);
            entidade.Ativo = ativo;
            await SalvarAsync(entidade);
        }

        protected async Task<IEnumerable<TEntidade>> _listarAsync(IQueryable<TEntidade> query, int pag, int qtd, bool atv)
        {
            if (atv) query = query.Where(x => x.Ativo);
            if (pag > 0) query = query.Skip(pag);
            if (qtd > 0) query = query.Take(qtd);

            return await query.ToListAsync();
        }
    }
}
