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
using System.Text.RegularExpressions;

namespace PractiFly.MauiApplication.View;

public partial class Admin : ContentPage
{
    private  PractiFlyClient client;
    private readonly HttpClient _httpClient;
    private bool isInitialized = false;
    private int? IDUser;
    private string foto = "https://s3-alpha-sig.figma.com/img/54b1/857d/556a2fbf2e264a5f382c2bd624afade7?Expires=1687132800&Signature=aWu23dhcWYshIHqujpTagx6Sp6xoH171g~gPIQznlA2K0vgmDntGAuAesI6X2m65w9Xi3~AAVBf8MycZI4zkSMc0weqhMGg0Cf7yTXCcacdylV6UuYXYkNNDG2KCQ9AqnbFDj~Q14~~rGbskfJye9wB3x95zAHjVY5rUknu9erf3OMRwJdzThXa8xz7NOcOPTKV2nAf6OUPCsBj-cP2zP~e~XAmwIaa4C85dcj~5cYHAxwtKzjChmd7ONkto2kV7y6ypit8hO6EVej7ufbU-55y~tjTrP61SHnKmjB-6gHS06g7dAmFsU7EWk~FbSnR6NXADV2DN0T2TwII7yJix~A__&Key-Pair-Id=APKAQ4GOSFWCVNEHN3O4";
    public Admin()
	{
        InitializeComponent();

        role.Items.Add(" ");
        role.Items.Add("teacher");
        role.Items.Add("admin");
        role.Items.Add("user");
        role.Items.Add("manager");
        role.SelectedIndex = 0;

        client = new();
        DateFrom.Date = new DateTime(2000, 1, 1);
        DateTo.Date = DateTime.Now;

        GetUsers();
    }

    private async void GetUsers()
    {
        try
        {
            UserFilterInfoDto user = new UserFilterInfoDto();
            var filter = await client.GetFilterUserAsync(user);
            UsersCollectionView.ItemsSource = filter;
            isInitialized = true;
        }
        catch (Exception ex)
        {
            await DisplayAlert(null, ex.Message, "ОК)");
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
        //id.Text = user.Id.ToString();
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
        bool result = await DisplayAlert("Підтвердження дії", "Бажаєте змінити дані користувача?", "Так", "Ні");
        if (result)
        {
            try
            {

                int ID = (int)IDUser;

                string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
                Regex regex = new Regex(emailPattern);

                if (!regex.IsMatch(itemEmail.Text))
                {
                    await DisplayAlert(null, "Невірний формат електронної пошти", "ОК)");
                    return;
                    // 
                }

                string phonePattern = @"^\+\d+$";
                regex = new Regex(phonePattern);

                if (!regex.IsMatch(itemPhoneNumber.Text))
                {
                    await DisplayAlert(null, "Невірний формат номеру телефона", "ОК)");
                    return;
                }
                string datePattern = @"^\d{4}-\d{2}-\d{2}$";
                regex = new Regex(datePattern);
                if (!regex.IsMatch(itemDateBirthday.Text))
                {
                    await DisplayAlert(null, "Невірний формат дати народження", "ОК)");
                    return;
                }
                if (itemfirstName.Text == "")
                {
                    await DisplayAlert(null, "Введіть ім'я", "ОК)");
                    return;
                }
                if (itemlastName.Text == "")
                {
                    await DisplayAlert(null, "Введіть прізвище", "ОК)");
                    return;
                }

                //UpdateUserByAdminAsyncA
                string role = "user";
                if (radioButtonAdmin.IsChecked == true)
                    role = "admin";
                else if (radioButtonManager.IsChecked == true)
                    role = "manager";
                else if (radioButtonTeacher.IsChecked == true)
                    role = "teacher";
                else if (radioButtonUser.IsChecked == true)
                    role = "user";
                UserUpdateInfoDto userUpdate = new UserUpdateInfoDto
                {
                    Id = (int)IDUser,
                    FirstName = itemfirstName.Text,
                    LastName = itemlastName.Text,
                    Email = itemEmail.Text,
                    Phone = itemPhoneNumber.Text,
                    Birthday = DateTime.ParseExact(itemDateBirthday.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    FilePhoto = foto,
                    Role = role,

                };
                var user = await client.UpdateUserByAdminAsync(userUpdate);
                Search_Clicked(sender, e);
                await DisplayAlert(null, "Данні користувача змінено", "ОК)");
            }
        
            catch (Exception ex)
            {
                await DisplayAlert(null, ex.Message, "ОК)");
                return;
            }
        }

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
        bool result = await DisplayAlert("Підтвердження дії", "Бажаєте видалити курс?", "Так", "Ні");
        //Вікно для пітдвердження видалення
        if (result)
        {
            if (IDUser != null)
            {
                try
                {
                    int ID = (int)IDUser;
                    var user = await client.DeleteUserByIdAsAdminAsync(ID);
                    Search_Clicked(sender, e);
                    //id.Text = null;
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
                catch(Exception ex)
                {
                    await DisplayAlert(null, ex.Message, "ОК)");
                }
            }
            else
            {
                await DisplayAlert(null,"Для видалення виберіть користувача зі списку", "ОК)");
            }
        }
    }
    //private async void CreateUser_Clicked(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (string.IsNullOrEmpty(itemfirstName.Text) && string.IsNullOrEmpty(itemlastName.Text)
    //            && string.IsNullOrEmpty(itemPhoneNumber.Text) && string.IsNullOrEmpty(itemEmail.Text)
    //            && string.IsNullOrEmpty(itemDateBirthday.Text))
    //        {
    //            await DisplayAlert(null, "Введіть всі поля для створення нового користувача", "ОК)");
    //            return;
    //        }
    //        //CreateUserByAdminAsync
    //        UserCreateInfoDto user = new UserCreateInfoDto
    //        {
    //            FirstName = itemfirstName.Text,
    //            LastName = itemlastName.Text,
    //            Email = itemEmail.Text,
    //            PhoneNumber = itemPhoneNumber.Text,
    //            Birthday = DateTime.ParseExact(itemDateBirthday.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture),
    //            FilePhoto = foto,
    //        };

    //        if (radioButtonAdmin.IsChecked)
    //            user.Role = "admin";
    //        else if (radioButtonManager.IsChecked)
    //            user.Role = "manager";
    //        else if (radioButtonTeacher.IsChecked)
    //            user.Role = "teacher";
    //        else if (radioButtonUser.IsChecked)
    //            user.Role = "user";


    //        var newUser = await client.CreateUserByAdminAsync(user);
    //        Search_Clicked(sender, e);
    //        await DisplayAlert(null, "Нового користувача створено", "ОК)");
    //    }
    //    catch
    //    {
    //        await DisplayAlert(null, "Помилка створення нового користувача", "ОК)");
    //    }
    //}

}