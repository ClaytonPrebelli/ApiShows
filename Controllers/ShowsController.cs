using Microsoft.AspNetCore.Mvc;
using ApiShows.Models;
using ApiShows.Repositories;

namespace ApiShows.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShowsController : ControllerBase
{
    private readonly IShowRepository _repository;

    public ShowsController(IShowRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Show>>> GetAll()
    {
        var shows = await _repository.GetAllAsync();
        return Ok(shows);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Show>> GetById(int id)
    {
        var show = await _repository.GetByIdAsync(id);
        if (show == null)
            return NotFound();
        return Ok(show);
    }

    [HttpPost]
    public async Task<ActionResult<Show>> Create([FromBody] Show show)
    {
        var created = await _repository.CreateAsync(show);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Show>> Update(int id, [FromBody] Show show)
    {
        if (id != show.Id)
            return BadRequest("ID mismatch");

        var updated = await _repository.UpdateAsync(id, show);
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

    [HttpPatch("{id}/toggle-pago")]
    public async Task<ActionResult<Show>> TogglePago(int id)
    {
        var show = await _repository.TogglePagoAsync(id);
        if (show == null)
            return NotFound();
        return Ok(show);
    }
}
