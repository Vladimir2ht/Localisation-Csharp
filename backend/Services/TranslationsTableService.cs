using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LocalizationNamespace.Data;
using LocalizationNamespace.DTOs;

namespace LocalizationNamespace.Services
{
	public class TranslationsTableService
	{
		private readonly AppDbContext _context;

		public TranslationsTableService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<List<TranslationsTableItemDto>> GetTranslationsTableAsync()
		{
			var keys = await _context.LocalizationKeys
				.Include(k => k.Translations)
				.ThenInclude(t => t.Language)
				.ToListAsync();

			return keys.Select(k => new TranslationsTableItemDto
			{
				Key = k.Key,
				Translations = k.Translations.ToDictionary(
						t => t.Language.Name,
						t => t.Value
					)
			}).ToList();
		}
	}
}