using PractiFly.Api;
using PractiFly.Api.Api.Login;
using PractiFly.Api.Client;
using PractiFly.Api.Login;


namespace PractiFly.MauiApplication.View;

public partial class Login : ContentPage
{
    private readonly PractiFlyClientWrapper _clientWrapper;
    private PractiFlyClient client;
    public Login()
	{
		InitializeComponent();
        client = new("");
    }

    private async void Input_Clicked(object sender, EventArgs e)
    {
        LoginRequestDto loginRequestDto = new LoginRequestDto
        {
            Email = email.Text,
            Password = password.Text
        };
        try
        {
            var userInfo = await client.GetLoginResponseAsync(loginRequestDto);
            if (userInfo == true)
            {
                await Navigation.PushAsync(new Admin());
            }
            else
            {
                await DisplayAlert(null, "Невірний логін або пароль", "ОК)");
            }
            

        }
        catch (Exception ex)
        {
            await DisplayAlert(null,ex.Message,"Yes");
        }

    }
}