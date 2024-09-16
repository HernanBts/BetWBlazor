using BetWBlazor.Frontend.Pages.Teams;
using BetWBlazor.Frontend.Repositories;
using BetWBlazor.Share.DTOs;
using BetWBlazor.Share.Entities;
using BetWBlazor.Share.Resources;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BetWBlazor.Frontend.Pages.Teams;

public partial class TeamEdit
{
    private TeamForm? form;
    private TeamDTO? teamDTO;

    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
    }

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await Repository.GetAsync<Team>($"api/teams/{Id}");

        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                NavigationManager.NavigateTo("teams");
            else
            {
                var messageError = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync(Localizer["Error"], Localizer[messageError!], SweetAlertIcon.Error);
            }
        }
        else
        {
            var team = responseHttp.Response;

            teamDTO = new TeamDTO()
            {
                Id = team!.Id,
                Name = team!.Name,
                Image = team!.Image,
                CountryId = team!.CountryId
            };
        }
    }

    private async Task EditAsync()
    {
        var responseHttp = await Repository.PutAsync("api/teams/full", teamDTO);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            await SweetAlertService.FireAsync(Localizer["Error"], Localizer[message!], SweetAlertIcon.Error);
            return;
        }

        Return();

        var toast = SweetAlertService.Mixin(new SweetAlertOptions
        {
            Toast = true,
            Position = SweetAlertPosition.BottomEnd,
            ShowConfirmButton = true,
            Timer = 3000
        });
        toast.FireAsync(icon: SweetAlertIcon.Success, message: Localizer["RecordUpdateOk"]);
    }

    private void Return()
    {
        form!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/teams");
    }
}