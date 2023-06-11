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
    private string foto = "https://s3-alpha-sig.figma.com/img/54b1/857d/556a2fbf2e264a5f382c2bd624afade7?Expires=1687132800&Signature=aWu23dhcWYshIHqujpTagx6Sp6xoH171g~gPIQznlA2K0vgmDntGAuAesI6X2m65w9Xi3~AAVBf8MycZI4zkSMc0weqhMGg0Cf7yTXCcacdylV6UuYXYkNNDG2KCQ9AqnbFDj~Q14~~rGbskfJye9wB3x95zAHjVY5rUknu9erf3OMRwJdzThXa8xz7NOcOPTKV2nAf6OUPCsBj-cP2zP~e~XAmwIaa4C85dcj~5cYHAxwtKzjChmd7ONkto2kV7y6ypit8hO6EVej7ufbU-55y~tjTrP61SHnKmjB-6gHS06g7dAmFsU7EWk~FbSnR6NXADV2DN0T2TwII7yJix~A__&Key-Pair-Id=APKAQ4GOSFWCVNEHN3O4";
    private int? IDCourse;
    public Course()
	{

		InitializeComponent();
        client = new("");
        GetCourse();

    }
    private bool isInitialized = false;
    private async void GetCourse()
    {
        try
        {
            var course = await client.GetAllCourseAsync(null);
            CourseCollectionView.ItemsSource = course;
        }
        catch (Exception ex)
        {
            await DisplayAlert(null, ex.Message, "ОК)");
        }
    }

    
    private async void UsersCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection[0] as CourseItemInfoDto;
        var idCourse = selectedItem.Id;
        var course = await client.GetCourseById(idCourse);

        IDCourse = course.Course.Id;
        //id.Text = course.Course.Id.ToString();
        name.Text = course.Course.Name;
        note.Text = course.Course.Note;
        description.Text = course.Course.Description;

        var users = await client.GetUserCourseAsync(course.Course.Id);
        UsersCollectionView.ItemsSource = users;
        var owner = await client.GetOwnerCourseAsync(course.Course.Id);

        ownerImage.Source = ImageSource.FromUri(
        new Uri(foto));
        ownerName.Text = owner.FullName;
    }
    private async void ReversDataCourse_Clicked(object sender, EventArgs e)
    {
        if (IDCourse != null)
        {
            try
            {
                int ID = (int)IDCourse;
                var course = await client.GetCourseById(ID);
                name.Text = course.Course.Name;
                note.Text = course.Course.Note;
                description.Text = course.Course.Description;
            }
            catch(Exception ex)
            {
                await DisplayAlert(null, ex.Message, "OK)");
            }
            
            
        }
    }
    private async void CreateCourse_Clicked(object sender, EventArgs e)
    {
        if (name.Text == "")
        {
            await DisplayAlert(null, "Введіть ім'я", "ОК)");
            return;
        }
        if (note.Text == "")
        {
            await DisplayAlert(null, "Введіть опис", "ОК)");
            return;
        }
        if (description.Text == "")
        {
            await DisplayAlert(null, "Введіть примітку", "ОК)");
            return;
        }
        bool result = await DisplayAlert("Підтвердження дії", "Бажаєте додати новий курсу?", "Так", "Ні");
        if (result)
        {
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
                var course = await client.GetAllCourseAsync(null);
                CourseCollectionView.ItemsSource = course;
                await DisplayAlert(null, "Курс додано", "ОК)");
            }
            catch (Exception ex)
            {
                await DisplayAlert(null, ex.Message, "ОК)");
            }
        }
        
    }
    private async void Edit_Clicked(object sender, EventArgs e)
    {
        if (IDCourse != null)
        {
            if (name.Text == "")
            {
                await DisplayAlert(null, "Введіть назву", "ОК)");
                return;
            }
            if (note.Text == "")
            {
                await DisplayAlert(null, "Введіть опис", "ОК)");
                return;
            }
            if (description.Text == "")
            {
                await DisplayAlert(null, "Введіть примітку", "ОК)");
                return;
            }
            bool result = await DisplayAlert("Підтвердження дії", "Бажаєте змінити дані курсу?", "Так", "Ні");
            if (result)
            {
                int ID = (int)IDCourse;
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
                    var course = await client.GetAllCourseAsync(null);
                    CourseCollectionView.ItemsSource = course;
                    await DisplayAlert(null, "Данні курсу змінено", "ОК)");
                }
                catch (Exception ex)
                {
                    await DisplayAlert(null, ex.Message, "ОК)");
                    return;
                }
            }
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
            bool result = await DisplayAlert("Підтвердження дії", "Бажаєте видалити курс?", "Так", "Ні");
            if(result) 
            {
                try
                {
                    int ID = (int)IDCourse;
                    var user = await client.DeleteCourseAsync(ID);

                    IDCourse = null;
                    //id.Text = null;
                    name.Text = null;
                    note.Text = null;
                    description.Text = null;
                    await DisplayAlert(null, "Курс видалено", "ОК)");

                    var course = await client.GetAllCourseAsync(null);
                    CourseCollectionView.ItemsSource = course;
                }
                catch (Exception ex)
                {
                    await DisplayAlert(null, ex.Message, "ОК)");
                }
                
            }
            
        }
        else
        {
            await DisplayAlert(null, "Для видалення виберіть курс зі списку", "ОК)");
        }
    }

}