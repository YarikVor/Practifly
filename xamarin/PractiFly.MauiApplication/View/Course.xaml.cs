using PractiFly.Api.Admin;
using PractiFly.Api.Api.Admin;
using PractiFly.Api.Api.CourseData;
using PractiFly.Api.Client;
using PractiFly.Api.CourseData;
using System.Globalization;

namespace PractiFly.MauiApplication.View;

public partial class Course : ContentPage
{
    private PractiFlyClient client;
    private string foto = "https://s3-alpha-sig.figma.com/img/54b1/857d/556a2fbf2e264a5f" +
        "382c2bd624afade7?Expires=1685318400&Signature=LnOMmo2yFVosZS6WNqzGOtEEdNU2oqGoThKoIE" +
        "Be8mOdoKdIM3YPpHtwKtSX~-9T6dJwDWqOXK-TVwatPuaNjLI5lmWceHWXLyXkkTJl-eLcS6XXqNGkytEyzVCboE" +
        "mfddYI-xgjVXQmL2p51-rQ8zGkHK4XArEl7hS3-Otb5VsApJwU1yR0-t~BeZx4VKmVbxS~Ln4q17Rhl-dnyVSJDzGaP" +
        "-SRrkoHKAQoIPNJ4wwxmncxuZIkrmiiCutwRgtBtDsONkSIICOgPTXGtcLvFVMfGcNdUddSSPh2v5VTIo8FiNX5URFXcnq2x" +
        "VUqifck7CcH1Vql1jEc~76WTB7NRg__&Key-Pair-Id=APKAQ4GOSFWCVNEHN3O4";

    private int? IDCourse;
    public Course()
	{

		InitializeComponent();
        client = new("");
    }
    private bool isInitialized = false;
    protected async override void OnAppearing()
    {

        base.OnAppearing();

        if (!isInitialized)
        {

            UserFilterInfoDto user = new UserFilterInfoDto();
            var course = await client.GetAllCourseAsync(null);
            CourseCollectionView.ItemsSource = course;
            isInitialized = true;
        }
        
    }
    
    private async void UsersCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection[0] as CourseItemInfoDto;
        var idCourse = selectedItem.Id;
        var course = await client.GetCourseById(idCourse);
        IDCourse = course.Course.Id;
        id.Text = course.Course.Id.ToString();
        name.Text = course.Course.Name;
        note.Text = course.Course.Note;
        description.Text = course.Course.Description;

        var users = await client.GetUserCourseAsync(course.Course.Id);
        UsersCollectionView.ItemsSource = users;
        var owner = await client.GetOwnerCourseAsync(course.Course.Id);

        ownerImage.Source = ImageSource.FromUri(
        //new Uri($"{user.FilePhoto}"));
        new Uri(foto));
        ownerName.Text = owner.FullName;
    }
    private async void ReversDataCourse_Clicked(object sender, EventArgs e)
    {
        if (IDCourse != null)
        {
            //Вікно для пітдвердження 
            int ID = (int)IDCourse;
            var course = await client.GetCourseById(ID);
            name.Text = course.Course.Name;
            note.Text = course.Course.Note;
            description.Text = course.Course.Description;
        }
    }
    private async void CreateCourse_Clicked(object sender, EventArgs e)
    {
        //Вікно для пітдвердження 
        try
        {
            CreateCourseDto cours = new CreateCourseDto()
            {
                OwnerId = 8,//Отримати свій ID
                Name = name.Text,
                Note = note.Text,
                Description = description.Text,
                Language = "ua",
            };

            var createCourse = await client.CreateCourseAsync(cours);

        }
        catch (Exception ex)
        {
            await DisplayAlert(null, ex.Message, "ОК)");
            return;
        }
        var course = await client.GetAllCourseAsync(null);
        CourseCollectionView.ItemsSource = course;
        await DisplayAlert(null, "Курс додано", "ОК)");
        
    }
    private async void Edit_Clicked(object sender, EventArgs e)
    {
        //Вікно для пітдвердження 
        if (IDCourse != null)
        {
            int ID = (int)IDCourse;
            //UpdateUserByAdminAsync

            try
            {
                UpdateCourseDto cours = new UpdateCourseDto()
                {
                    Id = ID,
                    Name = name.Text,
                    Note = note.Text,
                    Description = description.Text,
                    Language = "ua",
                };

                var updatecourse = await client.UpdateCourseAsync(cours);

            }
            catch (Exception ex)
            {
                await DisplayAlert(null, ex.Message, "ОК)");
                return;
            }
            var course = await client.GetAllCourseAsync(null);
            CourseCollectionView.ItemsSource = course;
            await DisplayAlert(null, "Данні курсу змінено", "ОК)");
        }
        else
        {
            await DisplayAlert(null, "Для редагування виберіть курс зі списку", "ОК)");
        }
    }
    private async void Delete_Clicked(object sender, EventArgs e)
    {

        if (IDCourse != null)
        {
            //Вікно для пітдвердження видалення
            int ID = (int)IDCourse;
            var user = await client.DeleteCourseAsync(ID);

            IDCourse = null;
            id.Text = null;
            name.Text = null;
            note.Text = null;
            description.Text = null;
            await DisplayAlert(null, "Курс видалено", "ОК)");

            var course = await client.GetAllCourseAsync(null);
            CourseCollectionView.ItemsSource = course;
        }
        else
        {
            await DisplayAlert(null, "Для видалення виберіть курс зі списку", "ОК)");
        }
    }

    private async void AdminPanel(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Admin());
    }
    private async void CategoryPanel(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Category());
    }
    private async void CoursePanel(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Course());
    }
    private async void CourseThemePanel(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CourseTheme());
    }
    private async void MaterialBlocksPanel(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MaterialBlocks());
    }
    private async void RubricsCoursePanel(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RubricsCourse());
    }
}