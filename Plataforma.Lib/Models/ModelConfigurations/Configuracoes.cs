using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Plataforma.Lib.Models.Configurations
{
    #region Entidades Fortes

    class ConfiguracaoMusica : IEntityTypeConfiguration<Musica>
    {
        public void Configure(EntityTypeBuilder<Musica> builder)
        {
        }
    }

    class ConfiguracaoUsuario : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
        }
    }

    class ConfiguracaoEtiqueta : IEntityTypeConfiguration<Etiqueta>
    {
        public void Configure(EntityTypeBuilder<Etiqueta> builder)
        {
        }
    }

    class ConfiguracaoComentario : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> builder)
        {
            builder.HasOne(c => c.Autor)
                .WithMany(a => a.Comentarios)
                .HasForeignKey(c => c.AutorId);

            builder.HasOne(c => c.Musica)
                .WithMany(m => m.Comentarios)
                .HasForeignKey(c => c.MusicaId);
        }
    }

    #endregion

    #region Entidades Fracas

    class ConfiguracaoAutoria : IEntityTypeConfiguration<Autoria>
    {
        public void Configure(EntityTypeBuilder<Autoria> builder)
        {
            builder.HasKey(x => new { x.AutorId, x.MusicaId });

            builder.HasOne(am => am.Autor)
                .WithMany(a => a.Musicas)
                .HasForeignKey(am => am.AutorId);

            builder.HasOne(am => am.Musica)
                .WithMany(m => m.Autores)
                .HasForeignKey(am => am.MusicaId);
        }
    }

    class ConfiguracaoDenuncia : IEntityTypeConfiguration<Denuncia>
    {
        public void Configure(EntityTypeBuilder<Denuncia> builder)
        {
            builder.HasKey(x => new { x.UsuarioId, x.MusicaId });

            builder.HasOne(d => d.Usuario)
                .WithMany(u => u.Denuncias)
                .HasForeignKey(d => d.UsuarioId);

            builder.HasOne(d => d.Musica)
                .WithMany(m => m.Denuncias)
                .HasForeignKey(d => d.MusicaId);
        }
    }

    class ConfiguracaoFavorita : IEntityTypeConfiguration<Favorita>
    {
        public void Configure(EntityTypeBuilder<Favorita> builder)
        {
            builder.HasKey(x => new { x.UsuarioId, x.MusicaId });

            builder.HasOne(d => d.Usuario)
                .WithMany(u => u.Favoritas)
                .HasForeignKey(d => d.UsuarioId);

            builder.HasOne(d => d.Musica)
                .WithMany(m => m.Favoritos)
                .HasForeignKey(d => d.MusicaId);
        }
    }

    class ConfiguracaoPreferencia : IEntityTypeConfiguration<Preferencia>
    {
        public void Configure(EntityTypeBuilder<Preferencia> builder)
        {
            builder.HasKey(x => new { x.UsuarioId, x.EtiquetaId });

            builder.HasOne(p => p.Usuario)
                .WithMany(u => u.Preferencias)
                .HasForeignKey(p => p.UsuarioId);

            builder.HasOne(p => p.Etiqueta)
                .WithMany(e => e.Usuarios)
                .HasForeignKey(p => p.EtiquetaId);
        }
    }

    class ConfiguracaoEtiquetagem : IEntityTypeConfiguration<Etiquetagem>
    {
        public void Configure(EntityTypeBuilder<Etiquetagem> builder)
        {
            builder.HasKey(x => new { x.MusicaId, x.EtiquetaId });

            builder.HasOne(em => em.Musica)
                .WithMany(u => u.Etiquetas)
                .HasForeignKey(em => em.MusicaId);

            builder.HasOne(em => em.Etiqueta)
                .WithMany(e => e.Musicas)
                .HasForeignKey(em => em.EtiquetaId);
        }
    }

    #endregion
}
