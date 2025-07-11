using Microsoft.EntityFrameworkCore;
using LocalizationNamespace.Models;

namespace LocalizationNamespace.Data
{
	public class AppDbContext : DbContext
	{
		// Конструктор, принимающий опции, передаваемые из Program.cs
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		// DbSet для ключей локализаций (Localization Keys)
		public DbSet<LocalizationKey> LocalizationKeys { get; set; }

		// DbSet для языков (Languages)
		public DbSet<Language> Languages { get; set; }

		// DbSet для переводов (Translations)
		public DbSet<Translation> Translations { get; set; }

		// Конфигурация модели и связей между сущностями
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<LocalizationKey>()
				.HasKey(k => k.Key);

			modelBuilder.Entity<LocalizationKey>()
				.HasIndex(k => k.Key)
				.IsUnique();

			modelBuilder.Entity<Language>()
				.HasKey(l => l.Code);

			modelBuilder.Entity<Language>()
				.HasIndex(l => l.Code)
				.IsUnique();

			modelBuilder.Entity<Translation>()
				.HasKey(t => new { t.LocalizationKey, t.Language });

			modelBuilder.Entity<Translation>()
				.HasOne(t => t.LocalizationKeyNavigation)
				.WithMany(k => k.Translations)
				.HasForeignKey(t => t.LocalizationKey)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Translation>()
				.HasOne(t => t.LanguageNavigation)
				.WithMany(l => l.Translations)
				.HasForeignKey(t => t.Language)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Translation>()
				.HasIndex(t => new { t.LocalizationKey, t.Language })
				.IsUnique();
		}
	}
}