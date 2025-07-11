using Microsoft.AspNetCore.Mvc;
using LocalizationNamespace.DTOs;
using LocalizationNamespace.Services;
using LocalizationNamespace.Validators;

namespace LocalizationNamespace.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TranslationsController : ControllerBase
	{
		private readonly TranslationService _service;

		public TranslationsController(TranslationService service)
		{
			_service = service;
		}

		[HttpPut]
		public IActionResult UpdateTranslation([FromBody] UpdateTranslationDto dto)
		{
			var success = _service.UpdateTranslation(dto.key, dto.langCode, dto.value);
			if (!success) return StatusCode(500, "Ошибка сохранения перевода");

			return Ok();
		}
	}

	public class UpdateTranslationDto
	{
		public string key { get; set; }
		public string langCode { get; set; }
		public string? value { get; set; }
	}
}