
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

        if (IDHead != null)
        {
            //Вікно для пітдвердження 
            var getHeading = await client.GetHeadingByHeadIdAsync((int)IDHead);

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

            EditHeadingDto editHeading = new EditHeadingDto()
            {
                Id = (int)IDHead,
                Name = name.Text,
                Code = code.Text,
                Udc = udc.Text,
                Note = note.Text,
                Description = description.Text,
            };

            var edit = await client.EditHeadingAsync(editHeading);
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
    private async void AdminPanel(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new Admin());
    }
    private async void CategoryPanel(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new Category());
    }
    private async void CoursePanel(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new Course());
    }
    private async void CourseThemePanel(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new CourseTheme());
    }
    private async void MaterialBlocksPanel(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new MaterialBlocks());
    }
    private async void RubricsCoursePanel(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new RubricsCourse());
    }
}