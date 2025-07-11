using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalizationNamespace.Data;
using LocalizationNamespace.DTOs;
using LocalizationNamespace.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalizationNamespace.Services
{
	public class LanguageService
	{
		private readonly AppDbContext _context;
		public LanguageService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<List<LanguageDto>> GetAllAsync()
		{
			return await _context.Languages
				.Select(l => new LanguageDto { Code = l.Code, Name = l.Name, InUse = l.InUse })
				.ToListAsync();
		}

		public async Task<LanguageDto> GetByCodeAsync(string code)
		{
			var lang = await _context.Languages.FirstOrDefaultAsync(l => l.Code == code);
			if (lang == null) return null;
			return new LanguageDto { Code = lang.Code, Name = lang.Name, InUse = lang.InUse };
		}

		public async Task<LanguageDto> CreateAsync(LanguageDto dto)
		{
			var entity = new Language { Code = dto.Code, Name = dto.Name, InUse = dto.InUse };
			_context.Languages.Add(entity);
			await _context.SaveChangesAsync();
			return dto;
		}

		public async Task<bool> UpdateAsync(string code, LanguageDto dto)
		{
			var entity = await _context.Languages.FirstOrDefaultAsync(l => l.Code == code);
			if (entity == null) return false;
			entity.Name = dto.Name;
			entity.InUse = dto.InUse;
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteAsync(string code)
		{
			var entity = await _context.Languages.FirstOrDefaultAsync(l => l.Code == code);
			if (entity == null) return false;
			_context.Languages.Remove(entity);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> SetInUseAsync(string code)
		{
			var entity = await _context.Languages.FirstOrDefaultAsync(l => l.Code == code);
			if (entity == null || entity.InUse) return false;
			entity.InUse = true;
			await _context.SaveChangesAsync();
			return true;
		}
	}
}