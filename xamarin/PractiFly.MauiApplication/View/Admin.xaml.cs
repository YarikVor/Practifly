using Cysharp.Web;
using PractiFly.Api;
using PractiFly.Api.Admin;
using PractiFly.Api.Api.Admin;
using PractiFly.Api.Api.Login;
using PractiFly.Api.Client;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Net.NetworkInformation;

namespace PractiFly.MauiApplication.View;

public partial class Admin : ContentPage
{
    private  PractiFlyClient client;
    private readonly HttpClient _httpClient;
    private bool isInitialized = false;
    private int? IDUser;
    private string foto = "https://s3-alpha-sig.figma.com/img/54b1/857d/556a2fbf2e264a5f382c2bd624afade7?Expires=1685318400&Signature=LnOMmo2yFVosZS6WNqzGOtEEdNU2oqGoThKoIEBe8mOdoKdIM3YPpHtwKtSX~-9T6dJwDWqOXK-TVwatPuaNjLI5lmWceHWXLyXkkTJl-eLcS6XXqNGkytEyzVCboEmfddYI-xgjVXQmL2p51-rQ8zGkHK4XArEl7hS3-Otb5VsApJwU1yR0-t~BeZx4VKmVbxS~Ln4q17Rhl-dnyVSJDzGaP-SRrkoHKAQoIPNJ4wwxmncxuZIkrmiiCutwRgtBtDsONkSIICOgPTXGtcLvFVMfGcNdUddSSPh2v5VTIo8FiNX5URFXcnq2xVUqifck7CcH1Vql1jEc~76WTB7NRg__&Key-Pair-Id=APKAQ4GOSFWCVNEHN3O4";
    public Admin()
	{
        InitializeComponent();
     
        role.Items.Add(" ");
        role.Items.Add("teacher");
        role.Items.Add("admin");
        role.Items.Add("user");
        role.Items.Add("manager");
        role.SelectedIndex = 0;

        client = new("");
        DateFrom.Date = new DateTime(2000, 1, 1);
        DateTo.Date = DateTime.Now;
    }
    
    protected async override void OnAppearing()
    {
        
        base.OnAppearing();

        if (!isInitialized)
        {
            UserFilterInfoDto user = new UserFilterInfoDto();
            var filter = await client.GetFilterUserAsync(user);
            UsersCollectionView.ItemsSource = filter;
            //date.Text = DateFrom.Date.ToString();
            isInitialized = true;
        }
    }

    private async void Search_Clicked(object sender, EventArgs e)
    {

        UserFilterInfoDto user = new UserFilterInfoDto
        {
            FirstName = firstName.Text,
            LastName = lastName.Text,
            Phone = phone.Text,
            //RegistrationDateFrom = DateOnly.FromDateTime(DateFrom.Date),
            //RegistrationDateTo = DateOnly.FromDateTime(DateTo.Date),
            Email = email.Text,
            Role = role.Items[role.SelectedIndex],
        };
        //Debug.WriteLine(DateFrom.Date);
        
        var filter = await client.GetFilterUserAsync(user);
        UsersCollectionView.ItemsSource = filter;

    }

    private async void UsersCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection[0] as UserItemInfoDto;
        var idUser = selectedItem.Id;
        var user = await client.GetUserByIdAsAdminAsync(idUser);
        IDUser = user.Id;
        id.Text = user.Id.ToString();
        itemlastName.Text = user.LastName;
        itemfirstName.Text = user.FirstName;
        itemEmail.Text = user.Email;
        itemPhoneNumber.Text = user.PhoneNumber;
        itemDate.Text = user.RegistrationDate;
        itemDateBirthday.Text = user.Birthday;

        fotoProfile.Source = ImageSource.FromUri(
            //new Uri($"{user.FilePhoto}"));
        new Uri(foto));

        //admin or manager or teacher or user
        var role = user.Role;
        if (role == "admin")
            radioButtonAdmin.IsChecked = true;
        else if (role == "manager")
            radioButtonManager.IsChecked = true;
        else if (role == "teacher")
            radioButtonTeacher.IsChecked = true;
        else if (role == "user")
            radioButtonUser.IsChecked = true;

    }

    private async void Edit_Clicked(object sender, EventArgs e)
    {

        int ID = (int)IDUser;
        //UpdateUserByAdminAsync
        UserUpdateInfoDto userUpdate = new UserUpdateInfoDto
        {
            Id = (int)IDUser,
            FirstName = itemfirstName.Text,
            LastName = itemlastName.Text,
            Email = itemEmail.Text,
            Phone = itemPhoneNumber.Text,
            Birthday = DateTime.ParseExact(itemDateBirthday.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture),
            FilePhoto = foto,
            Role = "user",

        };
        try
        {
            var user = await client.UpdateUserByAdminAsync(userUpdate);
            
        }
        catch (Exception ex)
        {
            await DisplayAlert(null, ex.Message, "ОК)");
            return;
        }
        Search_Clicked(sender, e);
        await DisplayAlert(null, "Данні користувача змінено", "ОК)");
    }
    private async void ReversDataUser_Clicked(object sender, EventArgs e)
    {
        if (IDUser != null)
        {
            //Вікно для пітдвердження 
            int ID = (int)IDUser;

            var user = await client.GetUserByIdAsAdminAsync(ID);
            itemlastName.Text = user.LastName;
            itemfirstName.Text = user.FirstName;
            itemEmail.Text = user.Email;
            itemPhoneNumber.Text = user.PhoneNumber;
            itemDate.Text = user.RegistrationDate;
            itemDateBirthday.Text = user.Birthday;
        }
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        
        if(IDUser != null)
        {
            //Вікно для пітдвердження видалення
            int ID = (int)IDUser;
            var user = await client.DeleteUserByIdAsAdminAsync(ID);
            Search_Clicked( sender, e);
            id.Text = null;
            IDUser = null;
            itemlastName.Text = null;
            itemfirstName.Text = null;
            itemEmail.Text = null;
            itemPhoneNumber.Text = null;
            itemDate.Text = null;
            itemDateBirthday.Text = null;
            fotoProfile.Source = null;
            await DisplayAlert(null, "Користувача видалено", "ОК)");
        }
        else
        {
            await DisplayAlert(null,"Для видалення виберіть користувача зі списку", "ОК)");
        }
    }
    private async void CreateUser_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(itemfirstName.Text) && string.IsNullOrEmpty(itemlastName.Text)
                && string.IsNullOrEmpty(itemPhoneNumber.Text) && string.IsNullOrEmpty(itemEmail.Text)
                && string.IsNullOrEmpty(itemDateBirthday.Text))
            {
                await DisplayAlert(null, "Введіть всі поля для створення нового користувача", "ОК)");
                return;
            }
            //CreateUserByAdminAsync
            UserCreateInfoDto user = new UserCreateInfoDto
            {
                FirstName = itemfirstName.Text,
                LastName = itemlastName.Text,
                Email = itemEmail.Text,
                PhoneNumber = itemPhoneNumber.Text,
                Birthday = DateTime.ParseExact(itemDateBirthday.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                FilePhoto = foto,
            };

            if (radioButtonAdmin.IsChecked)
                user.Role = "admin";
            else if (radioButtonManager.IsChecked)
                user.Role = "manager";
            else if (radioButtonTeacher.IsChecked)
                user.Role = "teacher";
            else if (radioButtonUser.IsChecked)
                user.Role = "user";


            var newUser = await client.CreateUserByAdminAsync(user);
            Search_Clicked(sender, e);
            await DisplayAlert(null, "Нового користувача створено", "ОК)");
        }
        catch
        {
            await DisplayAlert(null, "Помилка створення нового користувача", "ОК)");
        }

    }
}