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

	// GET: LocalizationKeys/5
	[HttpGet("{id}")]
	public async Task<IActionResult> Get(int id)
	{
		var key = await _dbContext.LocalizationKeys.FindAsync(id);
		if (key == null) return NotFound();
		return Ok(key);
	}

	public class CreateLocalizationKeyRequest
	{
		public string key { get; set; }
	}

	// Put: LocalizationKeys
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
		return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
	}

	// PUT: LocalizationKeys/5
	[HttpPut("{id}")]
	public async Task<IActionResult> Update(int id, [FromBody] LocalizationKey model)
	{
		if (id != model.Id) return BadRequest();

		var existing = await _dbContext.LocalizationKeys.FindAsync(id);
		if (existing == null) return NotFound();

		existing.Key = model.Key;
		// обновить другие поля, если есть

		await _dbContext.SaveChangesAsync();
		return NoContent();
	}

	// DELETE: LocalizationKeys/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		var existing = await _dbContext.LocalizationKeys.FindAsync(id);
		if (existing == null) return NotFound();

		_dbContext.LocalizationKeys.Remove(existing);
		await _dbContext.SaveChangesAsync();
		return NoContent();
	}
}