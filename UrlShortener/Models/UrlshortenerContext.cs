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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=URLShortener;Username=postgres;Password=jrc1234;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Url>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("urls_pkey");

            entity.ToTable("urls");

            entity.HasIndex(e => e.Code, "urls_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Creationdate).HasColumnName("creationdate");
            entity.Property(e => e.Shorturl)
                .HasMaxLength(255)
                .HasColumnName("shorturl");
            entity.Property(e => e.Url1).HasColumnName("url");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
