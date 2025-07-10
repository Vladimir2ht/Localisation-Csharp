using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LocalizationNamespace.Services;
using LocalizationNamespace.DTOs;

namespace LocalizationNamespace.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TranslationsTableController : ControllerBase
	{
		private readonly TranslationsTableService _service;

		public TranslationsTableController(TranslationsTableService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<ActionResult<List<TranslationsTableItemDto>>> Get()
		{
			var data = await _service.GetTranslationsTableAsync();
			return Ok(data);
		}
	}
}