using PractiFly.Api;
using PractiFly.Api.Api.Login;
using PractiFly.Api.Login;


namespace PractiFly.MauiApplication.View;

public partial class Login : ContentPage
{
    private readonly PractiFlyClientWrapper _clientWrapper;

    public Login(PractiFlyClientWrapper clientWrapper)
	{
		InitializeComponent();

        _clientWrapper = clientWrapper;
 
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
            UserInfoDto userInfo = await _clientWrapper.LoginAsync(loginRequestDto);
            //await DisplayAlert(null,"Ok", "Yes");
            await Navigation.PushModalAsync(new Admin());

        }
        catch (Exception ex)
        {
            await DisplayAlert(null,ex.Message,"Yes");
        }

    }
}