﻿using Automasipp.Desktop.Pages;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Automasipp.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PageManager pageManager;

        public MainWindow()
        {
            InitializeComponent();

            pageManager= new PageManager();
            DataContext = pageManager;
            
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await pageManager.OpenPageAsync(new ScenariosPage(pageManager)); 
        }
    }
}