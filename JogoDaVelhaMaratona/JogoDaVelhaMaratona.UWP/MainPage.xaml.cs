﻿using ImageCircle.Forms.Plugin.UWP;
using Windows.Foundation;
using Windows.UI.ViewManagement;

namespace JogoDaVelhaMaratona.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(200, 100));
            ImageCircleRenderer.Init();
            LoadApplication(new JogoDaVelhaMaratona.App());
        }
    }
}
