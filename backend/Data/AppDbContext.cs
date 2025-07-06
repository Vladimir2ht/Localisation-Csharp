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

			// Уникальный индекс на ключ локализации
			modelBuilder.Entity<LocalizationKey>()
				.HasIndex(k => k.Key)
				.IsUnique();

			// Уникальный индекс на код языка (например, "en", "ru")
			modelBuilder.Entity<Language>()
				.HasIndex(l => l.Code)
				.IsUnique();

			// Конфигурация связи "Перевод" с ключом и языком
			modelBuilder.Entity<Translation>()
				.HasKey(t => t.Id);

			modelBuilder.Entity<Translation>()
				.HasOne(t => t.LocalizationKey)
				.WithMany(k => k.Translations)
				.HasForeignKey(t => t.LocalizationKeyId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Translation>()
				.HasOne(t => t.Language)
				.WithMany(l => l.Translations)
				.HasForeignKey(t => t.LanguageId)
				.OnDelete(DeleteBehavior.Cascade);

			// Уникальный составной индекс для пары (LocalizationKeyId, LanguageId),
			// чтобы для каждого ключа и языка был только один перевод
			modelBuilder.Entity<Translation>()
				.HasIndex(t => new { t.LocalizationKeyId, t.LanguageId })
				.IsUnique();
		}
	}
}