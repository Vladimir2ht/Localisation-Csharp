using Microsoft.AspNetCore.Mvc;
// using FluentResults;
using LocalizationNamespace.Data;
using LocalizationNamespace.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class LocalizationKeysController : ControllerBase
{
	private readonly AppDbContext _context;

	public LocalizationKeysController(AppDbContext context)
	{
		_context = context;
	}

	// GET: api/LocalizationKeys?page=1&pageSize=20&search=key
	[HttpGet]
	public async Task<IActionResult> GetAll(int page = 1, int pageSize = 20, string? search = null)
	{
		var query = _context.LocalizationKeys.AsQueryable();

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

	// GET: api/LocalizationKeys/5
	[HttpGet("{id}")]
	public async Task<IActionResult> Get(int id)
	{
		var key = await _context.LocalizationKeys.FindAsync(id);
		if (key == null) return NotFound();
		return Ok(key);
	}

	// POST: api/LocalizationKeys
	[HttpPost]
	public async Task<IActionResult> Create([FromBody] LocalizationKey model)
	{
		if (await _context.LocalizationKeys.AnyAsync(k => k.Key == model.Key))
			return Conflict("Key already exists.");

		_context.LocalizationKeys.Add(model);
		await _context.SaveChangesAsync();
		return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
	}

	// PUT: api/LocalizationKeys/5
	[HttpPut("{id}")]
	public async Task<IActionResult> Update(int id, [FromBody] LocalizationKey model)
	{
		if (id != model.Id) return BadRequest();

		var existing = await _context.LocalizationKeys.FindAsync(id);
		if (existing == null) return NotFound();

		existing.Key = model.Key;
		// обновить другие поля, если есть

		await _context.SaveChangesAsync();
		return NoContent();
	}

	// DELETE: api/LocalizationKeys/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		var existing = await _context.LocalizationKeys.FindAsync(id);
		if (existing == null) return NotFound();

		_context.LocalizationKeys.Remove(existing);
		await _context.SaveChangesAsync();
		return NoContent();
	}
}