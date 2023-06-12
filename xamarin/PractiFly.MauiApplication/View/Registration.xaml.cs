namespace PractiFly.MauiApplication.View;

public partial class Registration : ContentPage
{
	public Registration()
	{
		InitializeComponent();
        login.GestureRecognizers.Add(tapGesture);

        tapGesture.Tapped += async (_, e) =>
        {
            await Navigation.PopAsync();
        };
    }

    TapGestureRecognizer tapGesture = new TapGestureRecognizer
    {
        NumberOfTapsRequired = 1
    };
}