
using PractiFly.Api.Api.HeadingCourse;
using PractiFly.Api.Client;
using PractiFly.Api.CourseData;
using PractiFly.Api.HeadingCourse;

namespace PractiFly.MauiApplication.View;

public partial class RubricsCourse : ContentPage
{
    private PractiFlyClient client;
    private int? IDHead;
    private int? IDStartHead;
    private int? IDCourse;
    private string CodeHead;
    private int? AddHead;
    private int? DellHead;

    public RubricsCourse()
	{
		InitializeComponent();
        client = new();
        GetCourse();
        NextCategory(null);
    }
    public RubricsCourse(int id)
    {
        InitializeComponent();
        client = new();
        GetCourse();
        IDStartHead = id;
        NextCategory(id);
    }
    public RubricsCourse(int id,string code)
    {
        InitializeComponent();
        client = new();
        GetCourse();
        NextCategoryHeading(id, code);
        NextCategory(null);
    }
    private async void NextLevelUdc(object sender, EventArgs e)
    {
        var button = sender as Button;
        var heading = button.BindingContext as GetHeadingBeginInfoDto;


        await Navigation.PushAsync(new RubricsCourse(heading.Id));
    }
    private async void NextLevelHeadingCourse(object sender, EventArgs e)
    {
        var button = sender as Button;
        var heading = button.BindingContext as GetHeadingBeginInfoDto;
        var code = heading.Code;

        await Navigation.PushAsync(new RubricsCourse((int)IDCourse, code) { IDCourse = this.IDCourse });
    }
    private async void GetCourse()
	{
        var course = await client.GetAllCourseAsync(null);
        CourseCollectionView.ItemsSource = course;
    }
    private async void NextCategoryHeading(int id,string code)
    {
        HeadingsCodeAndCorseIdDto corseIdDto = new HeadingsCodeAndCorseIdDto();
        corseIdDto.courseId = id;
        corseIdDto.beginCode = code;
        var head = await client.GetHeadingCourseAsync(corseIdDto.courseId, corseIdDto.beginCode);
        HeadingCourseCollectionView.ItemsSource = head;
    }
    private async void NextCategory(int? id)
    {
        var head = await client.GetHeadingByBeginHeadCodeAsync(id);
        HeadingCollectionView.ItemsSource = head;
    }
    private async void UpdateCategory(int? id)
    {
        var head = await client.GetHeadingByBeginHeadCodeAsync(id);
        HeadingCourseCollectionView.ItemsSource = head;
    }
    private async void CourseCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection[0] as CourseItemInfoDto;
        var idCourse = selectedItem.Id;
        IDCourse = selectedItem.Id;
        var coursehead = await client.GetHeadingCourseAsync(idCourse);
        //var count = coursehead.Length;
        //course.Text = selectedItem.Name;
        //numberRubric.Text = count.ToString();

        HeadingCourseCollectionView.ItemsSource = coursehead;
    }
    private async void RubricCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection[0] as GetHeadingBeginInfoDto;
        var idHead = selectedItem.Id;
        AddHead = idHead;
    }
    private async void RubricCourse_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection[0] as GetHeadingBeginInfoDto;
        var idHead = selectedItem.Id;
        DellHead = idHead;
        
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

    private async void DeleteHeadingInCourse_Clicked(object sender, EventArgs e)
    {
        if (IDCourse != null && DellHead != null)
        {
            ChangeHeadingInCourseDto changeHeadingIn = new ChangeHeadingInCourseDto();

            changeHeadingIn.CourseId = (int)IDCourse;
            changeHeadingIn.HeadingId = (int)DellHead;
            changeHeadingIn.IsIncluded = false;
            var add = await client.ChangeHeadingInCourseAsync(changeHeadingIn);
            UpdateCategory((int)IDCourse);
            await DisplayAlert(null, "Рубрику видалено з курсу", "ОК)");
            IDCourse = null;
            DellHead = null;
        }
        else
        {
            await DisplayAlert(null, "Виберіть рубрику яку бажаєте видалити з курсу", "ОК)");
        }
        
    }
    private async void AddHeadingInCourse_Clicked(object sender, EventArgs e)
    {
        

        if (IDCourse != null && AddHead != null)
        {
            ChangeHeadingInCourseDto changeHeadingIn = new ChangeHeadingInCourseDto();

            changeHeadingIn.CourseId = (int)IDCourse;
            changeHeadingIn.HeadingId = (int)AddHead;
            changeHeadingIn.IsIncluded = true;
            var add = await client.ChangeHeadingInCourseAsync(changeHeadingIn);
            UpdateCategory((int)IDCourse);
            await DisplayAlert(null, "Рубрику додано до курсу", "ОК)");
            IDCourse = null;
            AddHead = null;
        }
        else
        {
            await DisplayAlert(null, "Виберіть рубрику яку бажаєте додати та відповідний їй курс", "ОК)");
        }
    }
}