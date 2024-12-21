﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LB4_AnimeTitlesApp.Models;

public partial class AppContext : DbContext
{
    // конструкторы класса AppContext
    public AppContext()
    {
    }

    public AppContext(DbContextOptions<AppContext> options)
        : base(options)
    {
    }

    // свойства класса для связи с таблицами базы данных
    public virtual DbSet<AnimeTitle> AnimeTitles { get; set; }

    public virtual DbSet<AnimeType> AnimeTypes { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<User> Users { get; set; }

    // переопределение методов
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=animeTitles;Username=postgres;Password=1111");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnimeTitle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("animeTitles_pkey");

            entity.ToTable("animeTitles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountOfSeries).HasColumnName("countOfSeries");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.IdAnimeType).HasColumnName("idAnimeType");
            entity.Property(e => e.IdGenre).HasColumnName("idGenre");
            entity.Property(e => e.IdModerator).HasColumnName("idModerator");
            entity.Property(e => e.IsComplete).HasColumnName("isComplete");
            entity.Property(e => e.OriginalName).HasColumnName("originalName");
            entity.Property(e => e.Poster).HasColumnName("poster");
            entity.Property(e => e.Studio).HasColumnName("studio");
            entity.Property(e => e.TitleName).HasColumnName("titleName");
            entity.Property(e => e.YearOfRelese).HasColumnName("yearOfRelese");

            entity.HasOne(d => d.IdAnimeTypeNavigation).WithMany(p => p.AnimeTitles)
                .HasForeignKey(d => d.IdAnimeType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_titles_types");

            entity.HasOne(d => d.IdGenreNavigation).WithMany(p => p.AnimeTitles)
                .HasForeignKey(d => d.IdGenre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_titles_genres");

            entity.HasOne(d => d.IdModeratorNavigation).WithMany(p => p.AnimeTitles)
                .HasForeignKey(d => d.IdModerator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_titles_users");
        });

        modelBuilder.Entity<AnimeType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("animeTypes_pkey");

            entity.ToTable("animeTypes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnimeOfType).HasColumnName("animeOfType");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("genres_pkey");

            entity.ToTable("genres");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.GenreName).HasColumnName("genreName");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateOfRegistration).HasColumnName("dateOfRegistration");
            entity.Property(e => e.FirstName).HasColumnName("firstName");
            entity.Property(e => e.LastName).HasColumnName("lastName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
