
using PractiFly.Api.Api.Admin;
using PractiFly.Api.Api.Heading;
using PractiFly.Api.Client;
using PractiFly.Api.CourseData;
using PractiFly.Api.Heading;
using PractiFly.Api.HeadingCourse;
using System.Text.RegularExpressions;

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
            //³��� ��� ������������ 
            try
            {
                var getHeading = await client.GetHeadingByHeadIdAsync((int)IDHead);
                name.Text = getHeading.Name;
                note.Text = getHeading.Note;
                code.Text = getHeading.Code;
                udc.Text = getHeading.Udc;
                description.Text = getHeading.Description;
            }
            catch(Exception ex)
            {
                await DisplayAlert(null, ex.Message, "��)");
            }
           

           

        }
    }
    private async void EditHeading(object sender, EventArgs e)
    {
        if (IDHead != null)
        {
            string codePattern = @"^(?:\d{2}(?:.\d{2}){0,2})$";
            if (name.Text == "")
            {
                await DisplayAlert(null, "������ ��'�", "��)");
                return;
            }
           
            Regex regex = new Regex(codePattern);
            if (!regex.IsMatch(code.Text))
            {
                await DisplayAlert(null, "������� ������ ���� (01.01.01.01)", "��)");
                return;
            }
            if (udc.Text == "")
            {
                await DisplayAlert(null, "������ ���", "��)");
                return;
            }
            if (note.Text == "")
            {
                await DisplayAlert(null, "������ ����", "��)");
                return;
            }
            if (description.Text == "")
            {
                await DisplayAlert(null, "������ �������", "��)");
                return;
            }
            bool result = await DisplayAlert("ϳ����������� 䳿", "������ ������ ��� �������?", "���", "ͳ");
            if (result)
            {
                try
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
                    await DisplayAlert(null, "������� ��������", "��)");
                }
                catch (Exception ex)
                {
                    await DisplayAlert(null, ex.Message, "��)");
                }
            }
        }
        else
        {
            await DisplayAlert(null, "��� ����������� ������� �������� � ������", "��)");
        }
       
    }
    private async void CreateHeading(object sender, EventArgs e)
    {
        string codePattern = @"^(?:\d{2}(?:.\d{2}){0,2})$";
        if (name.Text == "")
        {
            await DisplayAlert(null, "������ ��'�", "��)");
            return;
        }

        Regex regex = new Regex(codePattern);
        if (!regex.IsMatch(code.Text))
        {
            await DisplayAlert(null, "������� ������ ���� (01.01.01.01)", "��)");
            return;
        }
        if (udc.Text == "")
        {
            await DisplayAlert(null, "������ ���", "��)");
            return;
        }
        if (note.Text == "")
        {
            await DisplayAlert(null, "������ ����", "��)");
            return;
        }
        if (description.Text == "")
        {
            await DisplayAlert(null, "������ �������", "��)");
            return;
        }
        bool result = await DisplayAlert("ϳ����������� 䳿", "������ �������� ���� �������?", "���", "ͳ");
        if (result)
        {
            
            try
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
                await DisplayAlert(null, "������� ��������", "��)");
            }
            catch (Exception ex)
            {
                await DisplayAlert(null, ex.Message, "��)");
            }
        }
        
    }
    private async void DeleteHeading(object sender, EventArgs e)
    {
        if (IDHead != null)
        {
            try
            {
                var create = await client.DeleteHeadingAsync((int)IDHead);
                NextCategory(IDStartHead);
            }
            catch(Exception ex)
            {
                await DisplayAlert(null, ex.Message, "��)");
            }
           
        }
        else
        {
            await DisplayAlert(null, "��� ��������� ������� �������� � ������", "��)");
        }
        
    }
   
}