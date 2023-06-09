﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PractiFly.XamarinApplication.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registration : ContentPage
    {
        public Registration()
        {
            InitializeComponent();
            login.GestureRecognizers.Add(tapGesture);

            tapGesture.Tapped += async (_, e) =>
            {
                await Navigation.PopAsync();
            };
        }
        TapGestureRecognizer tapGesture = new TapGestureRecognizer
        {
            NumberOfTapsRequired = 1
        };
    }
}