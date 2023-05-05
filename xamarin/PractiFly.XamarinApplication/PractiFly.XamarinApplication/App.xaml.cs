﻿using PractiFly.XamarinApplication.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PractiFly.XamarinApplication
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Login());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
