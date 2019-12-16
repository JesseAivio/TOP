using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TOP.UI.WPF.UI.Pages;
using TOP.UI.WPF.UI.Pages.Accounts;
using TOP.UI.WPF.UI.Pages.Details;
using TOP.UI.WPF.UI.Pages.Home;
using TOP.UI.WPF.Data.Main_Window;
using TOP.UI.WPF.Data.Models;

namespace TOP.UI.WPF.UI.Windows.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly MainWindow_Methods MainWindow_Methods = new MainWindow_Methods();
        public MainWindow()
        {
            InitializeComponent();
            MainWindow_Methods.CheckAuthentication(this, AccountsItem, DetailsItem);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow_Methods.MoveWindow(e, this);
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            MainWindow_Methods.MinimizeWindow(this);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow_Methods.CloseApplication();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow_Methods.ChangePage(ListViewMenu, MainGird, TrainsitionigContentSlide, GridCursor);
        }
    }
}
