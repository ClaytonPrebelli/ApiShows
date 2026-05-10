using Microsoft.AspNetCore.Mvc;
using ApiShows.Models;
using ApiShows.Repositories;

namespace ApiShows.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContratantesController : ControllerBase
{
    private readonly IContratanteRepository _repository;

    public ContratantesController(IContratanteRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Contratante>>> GetAll()
    {
        var contratantes = await _repository.GetAllAsync();
        return Ok(contratantes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Contratante>> GetById(int id)
    {
        var contratante = await _repository.GetByIdAsync(id);
        if (contratante == null)
            return NotFound();
        return Ok(contratante);
    }

    [HttpPost]
    public async Task<ActionResult<Contratante>> Create([FromBody] Contratante contratante)
    {
        var created = await _repository.CreateAsync(contratante);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Contratante>> Update(int id, [FromBody] Contratante contratante)
    {
        if (id != contratante.Id)
            return BadRequest("ID mismatch");

        var updated = await _repository.UpdateAsync(id, contratante);
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
