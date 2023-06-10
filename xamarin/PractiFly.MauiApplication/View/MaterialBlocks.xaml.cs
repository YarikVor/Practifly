
using PractiFly.Api.Api.Heading;
using PractiFly.Api.Api.MaterialBlocks;
using PractiFly.Api.Client;
using PractiFly.Api.HeadingCourse;
using PractiFly.Api.MaterialBlocks;
using System.Text.RegularExpressions;

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
    
    private async void DeleteHeading(object sender, EventArgs e)
    {

        if (IDMaterial != null)
        {
            bool result = await DisplayAlert("Підтвердження дії", "Бажаєте видалити матеріал?", "Так", "Ні");
            if (result)
            {
                try
                {
                    var delete = await client.DeleteHeadingAsync((int)IDMaterial);
                    AllMaterial();
                    name.Text = null;
                    priority.Text = null;
                    note.Text = null;
                    url.Text = null;
                    await DisplayAlert(null, "Матеріал видалено", "ОК)");
                }
                catch(Exception ex)
                {
                    await DisplayAlert(null, ex.Message, "ОК)");
                }
                
            }
        }
        else
        {
            await DisplayAlert(null, "Для видалення виберіть матеріал зі списку", "ОК)");
        }

    }
    private async void CreateMaterial(object sender, EventArgs e)
    {
        string codePattern = @"^\d+$";
        string UrlPattern = @"(https?|ftp):\/\/[^\s/$.?#].[^\s]*";

        if (name.Text == "")
        {
            await DisplayAlert(null, "Введіть назву матеріалу", "ОК)");
            return;
        }

        Regex regex = new Regex(codePattern);
        if (!regex.IsMatch(priority.Text))
        {
            await DisplayAlert(null, "Приорітетність має дорівнювати числовому значеню", "ОК)");
            return;
        }
        regex = new Regex(UrlPattern);
        if (!regex.IsMatch(url.Text))
        {
            await DisplayAlert(null, "Невірно введенне посилання", "ОК)");
            return;
        }
        if (note.Text == "")
        {
            await DisplayAlert(null, "Введіть примітку", "ОК)");
            return;
        }
        bool result = await DisplayAlert("Підтвердження дії", "Бажаєте створити новий матеріал?", "Так", "Ні");
        if (result)
        {
            try
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
            catch (Exception ex)
            {
                await DisplayAlert(null, ex.Message, "ОК)");
            }

        }
    }
    private void MaterialCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection[0] as ListAllMaterialInfoDto;
        var idmaterial = selectedItem.Id;
        IDMaterial = idmaterial;
    }

    #region
    private async void EditMaterial(object sender, EventArgs e)
    {
        if (IDMaterial != null)
        {
            bool result = await DisplayAlert("Підтвердження дії", "Бажаєте змінити дані матеріалу?", "Так", "Ні");
            if (result)
            {
                try
                {
                    EditMaterialBlockDto editMaterial = new EditMaterialBlockDto()
                    {
                        Name = name.Text,
                        Priority = Int32.Parse(priority.Text),
                        Note = note.Text,
                        Url = url.Text,
                        IsPractical = isPractical.IsChecked,
                        Id = (int)IDMaterial,
                    };

                    var edit = await client.EditMaterialAsync(editMaterial);
                    AllMaterial();
                }
                catch (Exception ex)
                {
                    await DisplayAlert(null, ex.Message, "ОК)");
                }

            }
        }
        else
        {
            await DisplayAlert(null, "Для редагування виберіть матеріал зі списку", "ОК)");
        }

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

    #endregion


}