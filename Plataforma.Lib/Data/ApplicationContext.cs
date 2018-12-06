using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plataforma.Lib.Models.Configurations;

namespace Plataforma.Lib.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext (DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            if (Usuarios.Any()) return;

            Database.EnsureCreated();

            var u1 = new Usuario
            {
                Email = "login",
                Senha = "senha",
                Nome = "Usuário",
                CriadoEm = DateTime.Now
            };
            Usuarios.AddRange(new Usuario[]
            {
                u1
            });

            if (Musicas.Any()) return;

            var m1 = new Musica
            {
                Nome = "Música 1",
                Descricao = "Primeira música do banco."
            };
            m1.Autores = new Autoria[]
            {
                new Autoria
                {
                    Autor = u1,
                    Musica = m1
                }
            };
            Musicas.AddRange(new Musica[]
            {
                m1,
                new Musica
                {
                    Nome = "Música 2",
                    Descricao = "Lorem ipsum dolor sit amet"
                },
                new Musica
                {
                    Nome = "Música 3",
                    Descricao = "Lorem ipsum dolor sit amet"
                },
                new Musica
                {
                    Nome = "Música 4",
                    Descricao = "Lorem ipsum dolor sit amet"
                }
            });

            SaveChanges();
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Musica> Musicas { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Etiqueta> Etiquetas { get; set; }

        public DbSet<Autoria> Autorias { get; set; }
        public DbSet<Denuncia> Denuncias { get; set; }
        public DbSet<Favorita> Favoritas { get; set; }
        public DbSet<Preferencia> Preferencias { get; set; }
        public DbSet<Etiquetagem> Etiquetagens { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.ApplyConfiguration(new ConfiguracaoMusica());
            mb.ApplyConfiguration(new ConfiguracaoComentario());
            mb.ApplyConfiguration(new ConfiguracaoUsuario());
            mb.ApplyConfiguration(new ConfiguracaoEtiqueta());

            mb.ApplyConfiguration(new ConfiguracaoAutoria());
            mb.ApplyConfiguration(new ConfiguracaoDenuncia());
            mb.ApplyConfiguration(new ConfiguracaoFavorita());
            mb.ApplyConfiguration(new ConfiguracaoPreferencia());
            mb.ApplyConfiguration(new ConfiguracaoEtiquetagem());
        }
    }
}
