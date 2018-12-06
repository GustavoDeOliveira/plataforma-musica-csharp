using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plataforma.Lib.Models;

namespace Plataforma.Lib.Domain
{
    public class MusicaDomain : EntidadeDomain<Musica>
    {
        public MusicaDomain(ApplicationContext db) : base(db)
        {
        }

        public new async Task<IEnumerable<Musica>> ListarAsync(int pag = 0, int qtd = 0, bool apenasAtivos = true)
        {
            var query = _db.Set<Musica>()
                .Include(m => m.Autores).ThenInclude(a => a.Autor)
                .AsNoTracking();

            return await _listarAsync(query, pag, qtd, apenasAtivos);
        }

    }
}
