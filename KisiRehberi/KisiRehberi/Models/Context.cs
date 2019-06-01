using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using KisiRehberi.Services;
using Microsoft.EntityFrameworkCore;

namespace KisiRehberi
{
    public class Context : DbContext, IDataStore<Kisi>
    {
        /// <summary>
        /// Creates our repo.
        /// </summary>
        /// <param name="dbPath">the platform specific path to the database</param>
        public Context(string dbPath) : base()
        {
            _dbPath = dbPath;
            // Create database if not there. This will also ensure the data seeding will happen.
            var IsDbCreated = Database.EnsureCreated();
            Debug.WriteLine("**** OnConfiguring" + dbPath);
        }

        private readonly string _dbPath;
        public DbSet<Kisi> Kisi { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Debug.WriteLine("**** OnConfiguring");
            optionsBuilder.UseSqlite($"Filename={_dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Debug.WriteLine("**** OnModelCreating");

            // Make ID property the primary key.
            modelBuilder.Entity<Kisi>()
                .HasKey(p => p.Id);

            // Require text to be set.
            modelBuilder.Entity<Kisi>()
                .Property(p => p.Ad)
                .IsRequired();
            modelBuilder.Entity<Kisi>()
              .Property(p => p.Soyad)
              .IsRequired();
            modelBuilder.Entity<Kisi>()
              .Property(p => p.TCKimlikNo)
              .IsRequired();
            modelBuilder.Entity<Kisi>()
              .Property(p => p.AnneAd)
              .IsRequired();
            modelBuilder.Entity<Kisi>()
            .Property(p => p.BabaAd)
            .IsRequired();

            // Add a converter to store category enum as string instead of int.
            //modelBuilder.Entity<BinaInformation>()
            //    .Property(p => p.Category)
            //    .HasConversion(new EnumToStringConverter<ItemCategory>());

        }

        #region IDataStore<Kisi> start
        public async Task<Kisi> GetKisiAsync(string id)
        {
            Debug.WriteLine("**** GetIKisiAsync");
            return await Kisi.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Kisi>> GetKisisAsync(bool forceRefresh = false)
        {
            Debug.WriteLine("**** GetKisisAsync");
            // Ignore forceRefresh for now.
            return await Kisi.ToListAsync().ConfigureAwait(false);

        }

        public async Task<bool> AddKisiAsync(Kisi item)
        {
            try
            {
                Debug.WriteLine("**** AddKisiAsync");
                await Kisi.AddAsync(item).ConfigureAwait(false);
                await SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> UpdateKisiAsync(Kisi item)
        {
            try
            {
                Debug.WriteLine("**** UpdateKisiAsync");
                Kisi.Update(item);
                await SaveChangesAsync().ConfigureAwait(false);
                // No error handling. Homework :-)
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteKisiAsync(string id)
        {
            try
            {
                Debug.WriteLine("**** DeleteKisiAsync");
                var itemToRemove = Kisi.FirstOrDefault(x => x.Id == id);
                if (itemToRemove != null)
                {
                    Kisi.Remove(itemToRemove);
                    await SaveChangesAsync().ConfigureAwait(false);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
