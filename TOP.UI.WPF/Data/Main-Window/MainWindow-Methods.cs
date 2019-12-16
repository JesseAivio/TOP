using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TOP.UI.WPF.Data.Models;
using TOP.UI.WPF.UI.Pages;
using TOP.UI.WPF.UI.Pages.Accounts;
using TOP.UI.WPF.UI.Pages.Details;
using TOP.UI.WPF.UI.Pages.Home;
using TOP.UI.WPF.UI.Windows.AuthenticationWindow;

namespace TOP.UI.WPF.Data.Main_Window
{
    public class MainWindow_Methods
    {
        public void CheckAuthentication(Window window, ListViewItem AccountsItem, ListViewItem DetailsItem)
        {
            if (Authenticate(window) == true)
            {
                CheckRole(AccountsItem, DetailsItem);
            }
        }
        private bool Authenticate(Window window)
        {
            window.Hide();
            AuthenticationWindow authentication = new AuthenticationWindow();
            if (authentication.ShowDialog() == true)
            {
                window.Show();
                return true;
            }
            return false;
        }

        private void CheckRole(ListViewItem AccountsItem, ListViewItem DetailsItem)
        {
            if (Globals.AuthenticatedAccount.Role != "Teacher")
            {
                AccountsItem.Visibility = Visibility.Collapsed;
                DetailsItem.Visibility = Visibility.Collapsed;
            }
        }

        public void MoveWindow(MouseButtonEventArgs e, Window window)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                window.DragMove();
            }
        }

        public void MinimizeWindow(Window window)
        {
            window.WindowState = WindowState.Minimized;
        }

        public void CloseApplication()
        {
            Application.Current.Shutdown();
        }

        public void ChangePage(ListView ListViewMenu, Grid MainGird, TransitioningContent TrainsitionigContentSlide, Grid GridCursor)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index, TrainsitionigContentSlide, GridCursor);
            MainGird.Children.Clear();
            switch (index)
            {
                case 0:
                    MainGird.Children.Add(new HomePage());
                    break;
                case 1:
                    MainGird.Children.Add(new TOP_Page());
                    break;
                case 2:
                    MainGird.Children.Add(new AccountsPage());
                    break;
                case 3:
                    MainGird.Children.Add(new DetailsPage());
                    break;
                default:
                    break;
            }
        }

        private void MoveCursorMenu(int index, TransitioningContent TrainsitionigContentSlide, Grid GridCursor)
        {
            TrainsitionigContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, (100 + (60 * index)), 0, 0);
        }
    }
}
