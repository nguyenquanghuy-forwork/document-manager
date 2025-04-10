using DocumentManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentDetail> DocumentDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>()
                .HasKey(x => x.DocumentNumber);

            modelBuilder.Entity<DocumentDetail>()
                .HasOne(d => d.Document)
                .WithMany(p => p.Details)
                .HasForeignKey(d => d.DocumentNumber)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
