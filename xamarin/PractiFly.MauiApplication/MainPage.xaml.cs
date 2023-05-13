using PractiFly.Api.Client;

namespace PractiFly.MauiApplication;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
		
		
	}

    protected override async void OnAppearing()
    {
        PractiFlyClient client = new PractiFlyClient("");
        label.Text =  ( await client.GetLoginResponseAsync(new Api.Api.Login.LoginRequestDto() { Email = "svvaleron@gmail.com", Password = "Qwerty@2003" })).Token;
    }

    //private void OnCounterClicked(object sender, EventArgs e)
    //{
    //	count++;

    //	if (count == 1)
    //		CounterBtn.Text = $"Clicked {count} time";
    //	else
    //		CounterBtn.Text = $"Clicked {count} times";

    //	SemanticScreenReader.Announce(CounterBtn.Text);
    //}
}

