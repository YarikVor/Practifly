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
        client = new();
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
            bool useBool = await client.GetLoginResponseAsync(loginRequestDto);
            LoginResponseDto userInfo = await client.GetLoginUsersDataAsync(loginRequestDto);
            PractiFlyClient.token = userInfo.Token;

            if (useBool == true)
            {
                App.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert(null, "Невірний логін або пароль", "ОК)");
            }


        }
        catch (Exception ex)
        {
            await DisplayAlert(null, ex.Message, "Yes");
        }

    }


    private async void Login_Clicked(object sender, EventArgs e)
    {
        LoginRequestDto loginRequestDto = new LoginRequestDto
        {
            Email = email.Text,
            Password = password.Text
        };
        try
        {
            bool useBool = await client.GetLoginResponseAsync(loginRequestDto);
            LoginResponseDto userInfo = await client.GetLoginUsersDataAsync(loginRequestDto);
            PractiFlyClient.token = userInfo.Token;

            if (useBool == true)
            {

                var userDataInfo = await client.GetLoginUsersDataAsync(loginRequestDto);
                var idUser = userDataInfo.User.Id;
                //App.Current.MainPage = new AppShell();

                var user = await client.GetUserByIdAsAdminAsync(idUser);
                if (user.Role == "admin")
                {
                    App.Current.MainPage = new AppShell();
                }
                else
                {
                    await DisplayAlert("Помилка", "Вхід дозволено тільки адміністраторам", "ОК)");
                }
            }
            else
            {
                await DisplayAlert(null, "Невірний логін або пароль", "ОК)");
            }


        }
        catch (Exception ex)
        {
            await DisplayAlert(null, ex.Message, "Yes");
        }
    }
}