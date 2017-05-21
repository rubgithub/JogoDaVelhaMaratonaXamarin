﻿using JogoDaVelhaMaratona.ViewModel;
using Xamarin.Forms;

namespace JogoDaVelhaMaratona.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new MainViewModel();
        }
    }
}
