using BetWBlazor.Share.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BetWBlazor.Frontend.Pages;

public partial class Home
{
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
}