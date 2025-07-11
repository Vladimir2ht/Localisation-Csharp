using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LocalizationNamespace.DTOs;
using LocalizationNamespace.Services;
using LocalizationNamespace.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace LocalizationNamespace.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class LanguagesController : ControllerBase
	{
		private readonly LanguageService _service;
		private readonly IValidator<LanguageDto> _validator;

		public LanguagesController(LanguageService service, IValidator<LanguageDto> validator)
		{
			_service = service;
			_validator = validator;
		}

		// GET: Languages
		[HttpGet]
		public async Task<ActionResult<IEnumerable<LanguageDto>>> GetAll()
		{
			var langs = await _service.GetAllAsync();
			return Ok(langs);
		}

		// GET: Languages/{code}
		[HttpGet("{code}")]
		public async Task<ActionResult<LanguageDto>> GetByCode(string code)
		{
			var lang = await _service.GetByCodeAsync(code);
			if (lang == null) return NotFound();
			return Ok(lang);
		}

		// POST: Languages
		[HttpPost]
		public async Task<ActionResult<LanguageDto>> Create([FromBody] LanguageDto dto)
		{
			// Если Code не передан, генерируем из Name (например, латиницей, нижний регистр)
			if (string.IsNullOrWhiteSpace(dto.Code) && !string.IsNullOrWhiteSpace(dto.Name))
				dto.Code = dto.Name.ToLower().Replace(" ", "_");

			ValidationResult result = await _validator.ValidateAsync(dto);
			if (!result.IsValid)
				return BadRequest(result.Errors);

			var created = await _service.CreateAsync(dto);
			return CreatedAtAction(nameof(GetByCode), new { code = created.Code }, created);
		}

		// PUT: Languages/{code}
		[HttpPut("{code}")]
		public async Task<IActionResult> Update(string code, [FromBody] LanguageDto dto)
		{
			ValidationResult result = await _validator.ValidateAsync(dto);
			if (!result.IsValid)
				return BadRequest(result.Errors);

			var updated = await _service.UpdateAsync(code, dto);
			if (!updated) return NotFound();
			return NoContent();
		}

		// PATCH: Languages/Enable
		[HttpPatch("Enable")]
		public async Task<IActionResult> Enable([FromBody] string[] codes)
		{
			foreach (var code in codes)
			{
				await _service.SetInUseAsync(code);
			}
			return NoContent();
		}

		// DELETE: Languages/{code}
		[HttpDelete("{code}")]
		public async Task<IActionResult> Delete(string code)
		{
			var deleted = await _service.DeleteAsync(code);
			if (!deleted) return NotFound();
			return NoContent();
		}
	}
}