using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TOP.Library.API;
using TOP.Library.API.Auth;
using TOP.Library.Data.models;
using TOP.UI.WPF.Data.Authentication_Window;
using TOP.UI.WPF.Data.Models;

namespace TOP.UI.WPF.UI.Windows.AuthenticationWindow
{
    /// <summary>
    /// Interaction logic for AuthenticationWindow.xaml
    /// </summary>
    public partial class AuthenticationWindow : Window
    {
        AuthenticationWindow_Methods authenticationWindow_Methods = new AuthenticationWindow_Methods();
        public AuthenticationWindow()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            authenticationWindow_Methods.LogIn(txtUsername, txtPassword, this);
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            authenticationWindow_Methods.IfEnterPresses(e, txtUsername, txtPassword, this);
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            authenticationWindow_Methods.IfEnterPresses(e, txtUsername, txtPassword, this);
        }
    }
}
