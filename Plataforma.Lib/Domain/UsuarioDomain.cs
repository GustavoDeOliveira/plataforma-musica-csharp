using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plataforma.Lib.Models;

namespace Plataforma.Lib.Domain
{
    public class UsuarioDomain : EntidadeDomain<Usuario>
    {
        public UsuarioDomain(ApplicationContext db) : base(db)
        {
        }

        public new async Task<Usuario> CarregarAsync(int id)
        {
            return await _db.Set<Usuario>()
                .Include(x => x.Preferencias)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Usuario> EncontrarAsync(Usuario u)
        {
            return await _db.Query<Usuario>().AsNoTracking()
                .FirstOrDefaultAsync(x => x.Ativo && x.Email == u.Email && x.Senha == u.Senha && x.Admin == u.Admin);
        }
    }
}
