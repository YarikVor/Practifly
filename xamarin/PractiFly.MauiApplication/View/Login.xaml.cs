namespace PractiFly.MauiApplication.View;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
        registration.GestureRecognizers.Add(tapGesture);

        tapGesture.Tapped += async (s, e) =>
        {
            await Navigation.PushAsync(new Registration());
        };
    }
    TapGestureRecognizer tapGesture = new TapGestureRecognizer
    {
        NumberOfTapsRequired = 1
    };
}