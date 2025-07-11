using Microsoft.AspNetCore.Mvc;
// using FluentResults;
using LocalizationNamespace.Data;
using LocalizationNamespace.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class LocalizationKeysController : ControllerBase
{
	private readonly AppDbContext _dbContext;

	public LocalizationKeysController(AppDbContext context)
	{
		_dbContext = context;
	}

	// GET: LocalizationKeys?page=1&pageSize=20&search=key
	[HttpGet]
	public async Task<IActionResult> GetAll(int page = 1, int pageSize = 20, string? search = null)
	{
		var query = _dbContext.LocalizationKeys.AsQueryable();

		if (!string.IsNullOrWhiteSpace(search))
		{
			query = query.Where(k => k.Key.Contains(search));
		}

		var totalItems = await query.CountAsync();
		var items = await query
				.OrderBy(k => k.Key)
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

		return Ok(new { totalItems, items });
	}

	// GET: LocalizationKeys/{key}
	[HttpGet("{key}")]
	public async Task<IActionResult> Get(string key)
	{
		var locKey = await _dbContext.LocalizationKeys.FirstOrDefaultAsync(k => k.Key == key);
		if (locKey == null) return NotFound();
		return Ok(locKey);
	}

	public class CreateLocalizationKeyRequest
	{
		public string key { get; set; }
	}

	// PUT: LocalizationKeys
	[HttpPut]
	public async Task<IActionResult> Create([FromBody] CreateLocalizationKeyRequest request)
	{
		if (string.IsNullOrWhiteSpace(request.key))
			return BadRequest("Key is required.");

		if (await _dbContext.LocalizationKeys.AnyAsync(k => k.Key == request.key))
			return Conflict("Key already exists.");

		var model = new LocalizationKey { Key = request.key };
		_dbContext.LocalizationKeys.Add(model);
		await _dbContext.SaveChangesAsync();
		return CreatedAtAction(nameof(Get), new { key = model.Key }, model);
	}

	// PUT: LocalizationKeys/{key}
	[HttpPut("{key}")]
	public async Task<IActionResult> Update(string key, [FromBody] LocalizationKey model)
	{
		if (key != model.Key) return BadRequest();

		var existing = await _dbContext.LocalizationKeys.FirstOrDefaultAsync(k => k.Key == key);
		if (existing == null) return NotFound();

		existing.Key = model.Key;
		// обновить другие поля, если есть

		await _dbContext.SaveChangesAsync();
		return NoContent();
	}

	// DELETE: LocalizationKeys/{key}
	[HttpDelete("{key}")]
	public async Task<IActionResult> Delete(string key)
	{
		var existing = await _dbContext.LocalizationKeys.FirstOrDefaultAsync(k => k.Key == key);
		if (existing == null) return NotFound();

		_dbContext.LocalizationKeys.Remove(existing);
		await _dbContext.SaveChangesAsync();
		return NoContent();
	}
}