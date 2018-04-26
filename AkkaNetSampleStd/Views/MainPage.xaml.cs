using System;
using System.Collections.Generic;
using AkkaNetSampleStd.ViewModels;
using Xamarin.Forms;

namespace AkkaNetSampleStd.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}
