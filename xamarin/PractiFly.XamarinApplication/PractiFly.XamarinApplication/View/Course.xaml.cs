using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PractiFly.XamarinApplication.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Course : ContentPage
    {
        public Course()
        {
            InitializeComponent();
            fotoProfile.Source = new UriImageSource
            {
                CachingEnabled = true,
                CacheValidity = new System.TimeSpan(2, 0, 0, 0),
                Uri = new System.Uri("https://s3-alpha-sig.figma.com/img/54b1/857d/556a2fbf2e264a5f382c2bd624afade7?Expires=1684108800&Signature=cx~sAsYLsINMPYiDFPSWwOvMdl3OerGsCEKgCYsvaiU-fZc-ZKTgWgjoQMC1ubc9gWYX7zpEF6FYcHUzESFE4cilOyGbKGVCIiqT6I9WiWrc7Vzi3pvW9KokTbaYxNQec3ylr5Q5oDWjXbY5T81evVedb3602bRjlEO9zFjZsnbZJrAtULsAwhVjedijr3a~EA3Gf4XmAnuycPs7ILSmPcXOhIi6sXt3i997~CPpjXVYwkvd3cjwD0IfqTFklBG776WIcdQo1nT~tCCXW7YNKnMbyleFSidy9Bw1wraa30ahr~6B6VPE7A5EPgE7Tb4lqedH2xWzOOWUcoD4l4lK1A__&Key-Pair-Id=APKAQ4GOSFWCVNEHN3O4")
            };
        }
    }
}