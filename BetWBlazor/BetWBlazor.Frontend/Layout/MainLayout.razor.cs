using BetWBlazor.Share.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BetWBlazor.Frontend.Layout;

public partial class MainLayout
{
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
}