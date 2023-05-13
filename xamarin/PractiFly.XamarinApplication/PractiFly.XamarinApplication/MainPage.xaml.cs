using PractiFly.XamarinApplication.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PractiFly.XamarinApplication
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Login(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }
    
        private async void Registration(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registration());
        }
        private async void Admin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Admin());
        }
        private async void Category(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Category());
        }
        private async void Course(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Course());
        }
        private async void CourseMaterial(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CourseMaterial());
        }
        private async void CourseTheme(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CourseTheme());
        }
        private async void MaterialBlocks(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MaterialBlocks());
        }
        private async void RubricsCourse(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RubricsCourse());
        }
    }
}


