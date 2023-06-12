﻿using PractiFly.MauiApplication.View;

namespace PractiFly.MauiApplication;

public partial class App : Application
{

    public App()
	{
		InitializeComponent();

        MainPage = new NavigationPage(new Login());
        //MainPage = new AppShell();
    }
    public App(bool login)
    {
        InitializeComponent();
        MainPage = new AppShell();

    }
   
}
