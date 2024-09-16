using BetWBlazor.Backend.UnitsOfWork.Interfaces;
using BetWBlazor.Share.DTOs;
using BetWBlazor.Share.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BetWBlazor.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : GenericController<Team>
{
    private readonly ITeamsUnitOfWork _teamsUnitOfWork;

    public TeamsController(IGenericUnitOfWork<Team> unitOfWork, ITeamsUnitOfWork teamsUnitOfWork) : base(unitOfWork)
    {
        _teamsUnitOfWork = teamsUnitOfWork;
    }

    [HttpGet]
    public override async Task<IActionResult> GetAsync()
    {
        var response = await _teamsUnitOfWork.GetAsync();
        if (response.WasSuccess)
            return Ok(response.Result);

        return BadRequest();
    }

    [HttpGet("{id}")]
    public override async Task<IActionResult> GetAsync(int id)
    {
        var response = await _teamsUnitOfWork.GetAsync(id);
        if (response.WasSuccess)
            return Ok(response.Result);

        return NotFound(response.Message);
    }

    [HttpGet("combo/{id:int}")]
    public async Task<IActionResult> GetComboAsync(int id)
    {
        return Ok(await _teamsUnitOfWork.GetComboAsync(id));
    }

    [HttpPost("full")]
    public async Task<IActionResult> PostAsync(TeamDTO model)
    {
        var action = await _teamsUnitOfWork.AddAsync(model);
        if (action.WasSuccess)
            return Ok(action.Result);

        return BadRequest(action.Message);
    }

    [HttpPut("full")]
    public async Task<IActionResult> PutAsync(TeamDTO model)
    {
        var action = await _teamsUnitOfWork.UpdateAsync(model);
        if (action.WasSuccess)
            return Ok(action.Result);

        return BadRequest(action.Message);
    }
}