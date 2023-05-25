using PractiFly.Api.Api.Admin;
using PractiFly.Api.Api.CourseData;
using PractiFly.Api.Api.CourseThemes;
using PractiFly.Api.Client;
using PractiFly.Api.CourseData;
using PractiFly.Api.CourseThemes;
using System.Text.RegularExpressions;

namespace PractiFly.MauiApplication.View;

public partial class CourseTheme : ContentPage
{
    private PractiFlyClient client;
    private bool isInitialized = false;
    private int? IDCourse;
    private int? IDTheme;
    public CourseTheme()
	{
		InitializeComponent();
        client = new("");
    }

    protected async override void OnAppearing()
    {

        base.OnAppearing();

        if (!isInitialized)
        {
            var course = await client.GetAllCourseAsync(null);
            CourseCollectionView.ItemsSource = course;
            isInitialized = true;
        }

    }

    private async void CourseCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection[0] as CourseItemInfoDto;
        var idCourse = selectedItem.Id;
        IDCourse = idCourse;

        var themes = await client.GetListThemesCourseByIdAsync(idCourse);
        ThemesCourseCollectionView.ItemsSource = themes;

        var materials = await client.GetListMaterialsCourseByIdAsync(idCourse);
        MaterialsCourseCollectionView.ItemsSource = materials;
    }

    private async void ThemeCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection[0] as ListThemesInfoDto;
        var idTheme = selectedItem.Id;
        IDTheme = idTheme;
        var themesInfo = await client.GetInformationThemeByIdAsync(idTheme);

        name.Text = themesInfo.Name;
        note.Text = themesInfo.Note;
        description.Text = themesInfo.Description;
        levelId.Text = themesInfo.LevelId.ToString();
    }
    private async void ReversTheme_Clicked(object sender, EventArgs e)
    {
        if (IDTheme != null)
        {
            //³��� ��� ������������ 
            int ID = (int)IDTheme;

            var theme = await client.GetInformationThemeByIdAsync(ID);

            name.Text = theme.Name;
            note.Text = theme.Note;
            description.Text = theme.Description;
            levelId.Text = theme.LevelId.ToString();
            
        }
    }
    private async void CreateTheme_Clicked(object sender, EventArgs e)
    {
        string codePattern = @"^\d+$";
        if (name.Text == "")
        {
            await DisplayAlert(null, "������ ��'�", "��)");
            return;
        }

        Regex regex = new Regex(codePattern);
        if (!regex.IsMatch(levelId.Text))
        {
            await DisplayAlert(null, "������������ �� ���������� ��������� �������", "��)");
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
        bool result = await DisplayAlert("ϳ����������� 䳿", "������ ������ ���� ����?", "���", "ͳ");
        if (result)
        {
            
            try
            {
                CreateThemesOfCourseDto themes = new CreateThemesOfCourseDto()
                {
                    Name = name.Text,
                    Note = note.Text,
                    Description = description.Text,
                    Number = 1,
                    LevelId = Int32.Parse(levelId.Text),
                    CourseId = (int)IDCourse,
                };
                var createCourse = await client.CreateThemesOfCourseAsync(themes);
                var updatethemes = await client.GetListThemesCourseByIdAsync(themes.CourseId);
                ThemesCourseCollectionView.ItemsSource = updatethemes;
                await DisplayAlert(null, "���� ������", "��)");
            }
            catch (Exception ex)
            {
                await DisplayAlert(null, ex.Message, "��)");
            }
        }

    }
    private async void Delete_Clicked(object sender, EventArgs e)
    {

        if (IDTheme != null)
        {
            bool result = await DisplayAlert("ϳ����������� 䳿", "������ �������� ����?", "���", "ͳ");
            //³��� ��� ������������ ���������
            if (result)
            {
                try
                {
                    int ID = (int)IDTheme;
                    var user = await client.DeleteThemesAsync(ID);

                    name.Text = null;
                    note.Text = null;
                    description.Text = null;
                    levelId.Text = null;

                    await DisplayAlert(null, "���� ��������", "��)");

                    var updatethemes = await client.GetListThemesCourseByIdAsync((int)IDCourse);
                    ThemesCourseCollectionView.ItemsSource = updatethemes;
                }
                catch (Exception ex)
                {
                    await DisplayAlert(null, ex.Message, "��)");
                }
            }
        }
        else
        {
            await DisplayAlert(null, "��� ��������� ������� ���� � ������", "��)");
        }
    }
    private async void EditTheme_Clicked(object sender, EventArgs e)
    {
        string codePattern = @"^\d+$";
        if (name.Text == "")
        {
            await DisplayAlert(null, "������ ��'�", "��)");
            return;
        }

        Regex regex = new Regex(codePattern);
        if (!regex.IsMatch(levelId.Text))
        {
            await DisplayAlert(null, "������������ �� ���������� ��������� �������", "��)");
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
        if (IDTheme != null)
        {
            bool result = await DisplayAlert("ϳ����������� 䳿", "������ ������ ��� ����?", "���", "ͳ");
            if (result)
            {
                
                try
                {

                    UpdateThemesDto themes = new UpdateThemesDto()
                    {
                        Id = (int)IDTheme,
                        Name = name.Text,
                        Note = note.Text,
                        Description = description.Text,
                        Number = 1,
                        LevelId = Int32.Parse(levelId.Text),

                    };
                    var createCourse = await client.UpdateThemesAsync(themes);
                    var updatethemes = await client.GetListThemesCourseByIdAsync((int)IDCourse);
                    ThemesCourseCollectionView.ItemsSource = updatethemes;
                    await DisplayAlert(null, "��� ���� ������", "��)");
                }
                catch (Exception ex)
                {
                    await DisplayAlert(null, ex.Message, "��)");
                }
            }
            
        }
        else
        {
            await DisplayAlert(null, "��� ����������� ������� ����", "��)");
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