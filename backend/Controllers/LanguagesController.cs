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
    [Route("api/[controller]")]
    public class LanguagesController : ControllerBase
    {
        private readonly LanguageService _service;
        private readonly IValidator<LanguageDto> _validator;

        public LanguagesController(LanguageService service, IValidator<LanguageDto> validator)
        {
            _service = service;
            _validator = validator;
        }

        // GET: api/Languages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAll()
        {
            var langs = await _service.GetAllAsync();
            // Для фронта возвращаем массив только имён языков (Name), если нужен массив объектов — поменять на return langs;
            return Ok(langs.ConvertAll(l => l.Name));
        }

        // GET: api/Languages/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LanguageDto>> GetById(int id)
        {
            var lang = await _service.GetByIdAsync(id);
            if (lang == null) return NotFound();
            return Ok(lang);
        }

        // POST: api/Languages
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
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/Languages/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LanguageDto dto)
        {
            ValidationResult result = await _validator.ValidateAsync(dto);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var updated = await _service.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        // DELETE: api/Languages/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}