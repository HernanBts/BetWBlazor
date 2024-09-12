using BetWBlazor.Frontend.Repositories;
using BetWBlazor.Share.Entities;
using BetWBlazor.Share.Resources;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Diagnostics.Metrics;

namespace BetWBlazor.Frontend.Pages.Countries;

public partial class CountriesIndex
{
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

    private List<Country>? Countries { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadAsync();
    }

    private async Task LoadAsync()
    {
        var responseHttp = await Repository.GetAsync<List<Country>>("api/Countries");
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            await SweetAlertService.FireAsync(Localizer["Error"], message, SweetAlertIcon.Error);
            return;
        }
        Countries = responseHttp.Response;
    }

    private async Task DeleteAsync(Country model)
    {
        var result = await SweetAlertService.FireAsync(new SweetAlertOptions
        {
            Title = Localizer["Confirmation"],
            Text = string.Format(Localizer["DeleteConfirm"], Localizer["Country"], model.Name),
            Icon = SweetAlertIcon.Question,
            ShowCancelButton = true,
            CancelButtonText = Localizer["Cancel"]
        });

        var confirm = string.IsNullOrEmpty(result.Value);

        if (confirm)
        {
            return;
        }

        var responseHttp = await Repository.DeleteAsync($"api/countries/{model.Id}");
        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync(Localizer["Error"], Localizer[message!], SweetAlertIcon.Error);
            }
            return;
        }

        await LoadAsync();

        var toast = SweetAlertService.Mixin(new SweetAlertOptions
        {
            Toast = true,
            Position = SweetAlertPosition.BottomEnd,
            ShowConfirmButton = true,
            Timer = 3000,
            ConfirmButtonText = Localizer["Yes"]
        });
        toast.FireAsync(icon: SweetAlertIcon.Success, message: Localizer["RecordDeletedOk"]);
    }
}