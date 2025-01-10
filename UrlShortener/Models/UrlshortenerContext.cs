using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Models;

public partial class UrlshortenerContext : DbContext
{
    public UrlshortenerContext()
    {
    }

    public UrlshortenerContext(DbContextOptions<UrlshortenerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Url> Urls { get; set; }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Url>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("urls_pkey");

            entity.ToTable("urls");

            entity.HasIndex(e => e.Code, "urls_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Creationdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creationdate");
            entity.Property(e => e.Shorturl)
                .HasMaxLength(255)
                .HasColumnName("shorturl");
            entity.Property(e => e.Url1).HasColumnName("url");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
