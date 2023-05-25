using PractiFly.Api.Api.Heading;
using PractiFly.Api.Api.MaterialBlocks;
using PractiFly.Api.Client;
using PractiFly.Api.HeadingCourse;
using PractiFly.Api.MaterialBlocks;

namespace PractiFly.MauiApplication.View;

public partial class MaterialBlocks : ContentPage
{
    private PractiFlyClient client;
    private bool isInitialized = false;
    private int? IDMaterial;
    private int? IDStartHead = null;
    public MaterialBlocks()
    {
        InitializeComponent();
        client = new("");
        //NextCategory(null);
        AllMaterial();
    }
    

    private async void AllMaterial()
    {
        var materials = await client.GetAllListMaterialsAsync();
        MaterialsCollectionView.ItemsSource = materials;
    }
    
    private async void EditMaterial(object sender, EventArgs e)
    {
        if (IDMaterial != null)
        {

            EditMaterialBlockDto editMaterial = new EditMaterialBlockDto()
            {
                Name = name.Text,
                Priority = Int32.Parse(priority.Text),
                Note = note.Text,
                Url = url.Text,
                IsPractical = isPractical.IsChecked,
                Id =(int)IDMaterial,
            };

            var edit = await client.EditMaterialAsync(editMaterial);
            AllMaterial();
        }
        else
        {
            await DisplayAlert(null, "Для редагування виберіть матеріал зі списку", "ОК)");
        }

    }
    private async void DeleteHeading(object sender, EventArgs e)
    {
        if (IDMaterial != null)
        {
            var delete = await client.DeleteHeadingAsync((int)IDMaterial);
            AllMaterial();
            name.Text = null;
            priority.Text = null;
            note.Text = null;
            url.Text = null;
            await DisplayAlert(null, "Матеріал видалено", "ОК)");
        }
        else
        {
            await DisplayAlert(null, "Для видалення виберіть матеріал зі списку", "ОК)");
        }

    }
    private async void CreateMaterial(object sender, EventArgs e)
    {

        CreateMaterialBlockDto createMaterial = new CreateMaterialBlockDto()
        {
            Name = name.Text,
            Priority = Int32.Parse(priority.Text),
            Note = note.Text,
            Url = url.Text,
            IsPractical = isPractical.IsChecked,
        };

        var create = await client.CreateMaterialAsync(createMaterial);
        AllMaterial();

    }
    private void MaterialCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection[0] as ListAllMaterialInfoDto;
        var idmaterial = selectedItem.Id;
        IDMaterial = idmaterial;


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

    //private async void HeadingCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //{
    //    var selectedItem = e.CurrentSelection[0] as GetHeadingBeginInfoDto;
    //    var idHead = selectedItem.Id;
    //    IDHead = idHead;

    //    var getMaterials = await client.GetListMaterialsByIdAsync(idHead);

    //    MaterialsCollectionView.ItemsSource = getMaterials;

    //}
    //private async void NextCategory(int? id)
    //{
    //    var head = await client.GetHeadingByBeginHeadCodeAsync(id);
    //    HeadingCollectionView.ItemsSource = head;
    //}
    //private async void NextLevelUdc(object sender, EventArgs e)
    //{
    //    var button = sender as Button;
    //    var heading = button.BindingContext as GetHeadingBeginInfoDto;


    //await Navigation.PushAsync(new MaterialBlocks(heading.Id));
    //}
    //   public MaterialBlocks(int id)
    //   {

    //       InitializeComponent();
    //       client = new("");
    //       IDStartHead = id;
    //       NextCategory(id);
    //       AllMaterial();
    //   }
}