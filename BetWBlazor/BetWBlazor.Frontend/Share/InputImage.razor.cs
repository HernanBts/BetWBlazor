using BetWBlazor.Share.Resources;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BetWBlazor.Frontend.Share;

public partial class InputImage
{
    private string? imageBase64;
    private string? fileName;

    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

    [Parameter] public string? Label { get; set; }
    [Parameter] public string? ImageURL { get; set; }
    [Parameter] public EventCallback<string> ImageSelected { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (string.IsNullOrWhiteSpace(Label))
        {
            Label = Localizer["Image"];
        }
    }

    private async Task OnChange(InputFileChangeEventArgs e)
    {
        var file = e.File;
        if (file != null)
        {
            fileName = file.Name;

            var arrBytes = new byte[file.Size];
            await file.OpenReadStream().ReadAsync(arrBytes);
            imageBase64 = Convert.ToBase64String(arrBytes);
            ImageURL = null;
            await ImageSelected.InvokeAsync(imageBase64);
            StateHasChanged();
        }
    }
}