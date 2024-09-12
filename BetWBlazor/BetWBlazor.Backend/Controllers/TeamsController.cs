using BetWBlazor.Backend.UnitsOfWork.Interfaces;
using BetWBlazor.Share.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BetWBlazor.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : GenericController<Team>
{
    public TeamsController(IGenericUnitOfWork<Team> unitOfWork) : base(unitOfWork)
    {
    }
}