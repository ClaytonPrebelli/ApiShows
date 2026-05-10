using Microsoft.AspNetCore.Mvc;
using ApiShows.Models;
using ApiShows.Repositories;

namespace ApiShows.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocaisController : ControllerBase
{
    private readonly ILocalRepository _repository;

    public LocaisController(ILocalRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Local>>> GetAll()
    {
        var locais = await _repository.GetAllAsync();
        return Ok(locais);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Local>> GetById(int id)
    {
        var local = await _repository.GetByIdAsync(id);
        if (local == null)
            return NotFound();
        return Ok(local);
    }

    [HttpPost]
    public async Task<ActionResult<Local>> Create([FromBody] Local local)
    {
        var created = await _repository.CreateAsync(local);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Local>> Update(int id, [FromBody] Local local)
    {
        if (id != local.Id)
            return BadRequest("ID mismatch");

        var updated = await _repository.UpdateAsync(id, local);
        if (updated == null)
            return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await _repository.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
