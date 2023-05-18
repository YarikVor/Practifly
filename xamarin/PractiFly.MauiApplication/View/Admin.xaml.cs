using Cysharp.Web;
using PractiFly.Api;
using PractiFly.Api.Admin;
using PractiFly.Api.Api.Admin;
using PractiFly.Api.Api.Login;
using PractiFly.Api.Client;
using System.Net.Http.Json;

namespace PractiFly.MauiApplication.View;

public partial class Admin : ContentPage
{
    private  PractiFlyClient client;
    private readonly HttpClient _httpClient;
    public Admin()
	{
        InitializeComponent();
        client = new("");
        DateFrom.Date = new DateTime(2000, 1, 1);
        DateTo.Date = DateTime.Now;
    }

    protected async override void OnAppearing()
    {
        UserFilterInfoDto user = new UserFilterInfoDto() ;
        var filter = await client.GetFilterUserAsync(user);
        UsersCollectionView.ItemsSource = filter;
    }

    private async void Search_Clicked(object sender, EventArgs e)
    {

        UserFilterInfoDto user = new UserFilterInfoDto()
        {
            FirstName = firstName.Text,
            LastName = lastName.Text,
            Phone = phone.Text,
            //RegistrationDateFrom = DateOnly.Parse(registrationDateFrom.Text),
            //RegistrationDateTo = DateOnly.Parse(registrationDateTo.Text),
            Email = email.Text,
            Role = role.Text,
        };
        
        var filter = await client.GetFilterUserAsync(user);
        UsersCollectionView.ItemsSource = filter;

    }
    

   
}