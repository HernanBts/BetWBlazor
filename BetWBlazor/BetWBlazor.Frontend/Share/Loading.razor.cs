using BetWBlazor.Share.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BetWBlazor.Frontend.Share;

public partial class Loading
{
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
}