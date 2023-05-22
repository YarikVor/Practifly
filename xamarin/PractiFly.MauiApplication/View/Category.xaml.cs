
using PractiFly.Api.Api.Admin;
using PractiFly.Api.Api.Heading;
using PractiFly.Api.Client;
using PractiFly.Api.CourseData;
using PractiFly.Api.Heading;
using PractiFly.Api.HeadingCourse;



namespace PractiFly.MauiApplication.View;

public partial class Category : ContentPage
{
    private PractiFlyClient client;
    private bool isInitialized = false;
    private int? IDHead;
    private int? IDStartHead = null;
    public Category()
	{
		InitializeComponent();
        client = new("");
        NextCategory(null);
    }
    public Category(int id)
    {

        InitializeComponent();
        client = new("");
        IDStartHead = id;
        NextCategory(id);
    }
    private async void NextCategory(int? id)
    {
        var head = await client.GetHeadingByBeginHeadCodeAsync(id);
        HeadingCollectionView.ItemsSource = head;
    }
    //protected async override void OnAppearing()
    //{

    //    base.OnAppearing();

    //    //if (!isInitialized)
    //    //{
    //        var heading = await client.GetHeadingByBeginHeadCodeAsync(IDStartHead);
    //        HeadingCollectionView.ItemsSource = heading;

    //        isInitialized = true;
    //    //}

    //}

    private async void HeadingCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection[0] as GetHeadingBeginInfoDto;
        var idHead = selectedItem.Id;
        IDHead = idHead;
        
        var getHeading = await client.GetHeadingByHeadIdAsync(idHead);

        name.Text = getHeading.Name;
        note.Text = getHeading.Note;
        code.Text = getHeading.Code;
        udc.Text = getHeading.Udc;
        description.Text = getHeading.Description;
    }

    private async void NextLevelUdc(object sender, EventArgs e)
    {
        var button = sender as Button;
        var heading = button.BindingContext as GetHeadingBeginInfoDto;


        await Navigation.PushAsync(new Category(heading.Id) );
    }
    private async void Revers_Clicked(object sender, EventArgs e)
    {
        int ID = (int)IDHead;

        if (ID != null)
        {
            //Вікно для пітдвердження 
            

            var getHeading = await client.GetHeadingByHeadIdAsync(ID);

            name.Text = getHeading.Name;
            note.Text = getHeading.Note;
            code.Text = getHeading.Code;
            udc.Text = getHeading.Udc;
            description.Text = getHeading.Description;

        }
    }
    private async void EditHeading(object sender, EventArgs e)
    {
        if (IDHead != null)
        {

            EditHeadingDto createHeading = new EditHeadingDto()
            {
                Id = (int)IDHead,
                Name = name.Text,
                Code = code.Text,
                Udc = udc.Text,
                Note = note.Text,
                Description = description.Text,
            };

            var create = await client.EditHeadingAsync(createHeading);
            NextCategory(IDStartHead);
        }
        else
        {
            await DisplayAlert(null, "Для редагування виберіть категорію зі списку", "ОК)");
        }
       
    }
    private async void CreateHeading(object sender, EventArgs e)
    {
        CreateHeadingDto createHeading = new CreateHeadingDto()
        {
            Name = name.Text,
            Code = code.Text,
            Udc = udc.Text,
            Note = note.Text,
            Description = description.Text,
        };
        name.Text = null;
        note.Text = null;
        code.Text = null;
        udc.Text = null;
        description.Text = null;
        var create = await client.CreateHeadingAsync(createHeading);
        NextCategory(IDStartHead);
    }
    private async void DeleteHeading(object sender, EventArgs e)
    {
        if (IDHead != null)
        {
            
            var create = await client.DeleteHeadingAsync((int)IDHead);
            NextCategory(IDStartHead);
        }
        else
        {
            await DisplayAlert(null, "Для видалення виберіть категорію зі списку", "ОК)");
        }
        
    }

}