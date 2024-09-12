using BetWBlazor.Backend.Data;
using BetWBlazor.Backend.UnitsOfWork.Implementations;
using BetWBlazor.Backend.UnitsOfWork.Interfaces;
using BetWBlazor.Share.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetWBlazor.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : GenericController<Country>
{
    public CountriesController(IGenericUnitOfWork<Country> unitOfWork) : base(unitOfWork)
    {
    }
}