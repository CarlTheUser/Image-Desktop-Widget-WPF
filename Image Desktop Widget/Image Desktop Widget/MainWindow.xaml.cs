using Image_Desktop_Widget.ViewModels;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using DataLayer.Client.Data;
using DataLayer.Client.Service;
using Image_Desktop_Widget.Test;
using ViewComponent;

namespace Image_Desktop_Widget
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IApplicationView
    {

        private static MainWindow instance = null;

        public static MainWindow Instance {
            get
            {
                return instance ?? (instance = new MainWindow());
            }
        }

        private MainWindow()
        {
            InitializeComponent();
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            Width = (screenWidth >= 700) ? 700 : Width;
            Height = (screenHeight >= 500) ? 500 : Height;
        }

        public BaseViewModel GetModel()
        {
            return VM;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            instance = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.EnableBlur();
            Button closeButton = (Button)this.Template.FindName("CloseButton", this);
            closeButton.Click += (s, ev) => this.Close();
        }
        
    }
}
