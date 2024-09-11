using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetWBlazor.Share.Entities;

public class Team
{
    public int Id { get; set; }

    [MaxLength(100)]
    [Required]
    public string Name { get; set; } = null!;

    public string? Image { get; set; }

    public Country Country { get; set; } = null!;

    public int CountryId { get; set; }
}