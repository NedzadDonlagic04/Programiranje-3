﻿using FIT.Data;
using FIT.Data.IspitBrojIndeksa;
using Microsoft.EntityFrameworkCore;

using System.Configuration;

namespace FIT.Infrastructure
{
    public class DLWMSDbContext : DbContext
    {
        private readonly string dbPutanja;

        public DLWMSDbContext()
        {
            dbPutanja = ConfigurationManager.
                ConnectionStrings["DLWMSBaza"].ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(dbPutanja);
        }
    
        public DbSet<Student> Studenti { get; set; }

        // Stuff I added below

        public DbSet<PredmetiBrojIndeksa> Predmeti => Set<PredmetiBrojIndeksa>();
        public DbSet<PolozeniPredmetiBrojIndeksa> PolozeniPredmeti => Set<PolozeniPredmetiBrojIndeksa>();
        public DbSet<StudentiUvjerenjaBrojIndeksa> Uvjerenja => Set<StudentiUvjerenjaBrojIndeksa>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                        .HasMany(student => student.PolozeniPredmeti)
                        .WithOne(polozeniPredmet => polozeniPredmet.Student);
        }

    }
}