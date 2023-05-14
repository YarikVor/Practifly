using PractiFly.Api.Client;
using PractiFly.MauiApplication.View;

namespace PractiFly.MauiApplication;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
		
		
	}

    protected override async void OnAppearing()
    {
        //PractiFlyClient client = new PractiFlyClient("");
        //label.Text =  ( await client.GetLoginResponseAsync
        //    (new Api.Api.Login.LoginRequestDto() 
        //    { Email = "svvaleron@gmail.com", Password = "Qwerty@2003" })).Token;
    }
    private async void Login(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Login());
    }

    private async void Registration(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Registration());
    }
    private async void Admin(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Admin());
    }
    private async void Category(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Category());
    }
    private async void Course(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Course());
    }
    private async void CourseMaterial(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CourseMaterial());
    }
    private async void CourseTheme(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CourseTheme());
    }
    private async void MaterialBlocks(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MaterialBlocks());
    }
    private async void RubricsCourse(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RubricsCourse());
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

