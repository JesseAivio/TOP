using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TOP.Library.API;
using TOP.Library.API.Auth;
using TOP.Library.Data.models;
using TOP.UI.WPF.Data.Models;

namespace TOP.UI.WPF.Data.Authentication_Window
{
    public class AuthenticationWindow_Methods
    {
        Authentication authentication = new Authentication();


        public async void LogIn(TextBox txtUsername, PasswordBox txtPassword, Window window)
        {
            if (txtUsername.Text == "" || txtPassword.Password == "")
            {
                MessageBox.Show("Error");
                return;
            }
            Account account = new Account
            {
                Username = txtUsername.Text,
                Password = txtPassword.Password
            };
            account = await authentication.LogIn(account);
            if (account == null)
            {
                MessageBox.Show("Error");
                return;
            }
            MessageBox.Show($"Hello, {txtUsername.Text}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            HttpClientSettings.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", account.Token);
            account.Token = null;
            Globals.AuthenticatedAccount = account;
            window.DialogResult = true;
        }

        public void IfEnterPresses(KeyEventArgs e, TextBox txtUsername, PasswordBox txtPassword, Window window)
        {
            if (e.Key == Key.Enter)
            {
                LogIn(txtUsername, txtPassword, window);
            }
        }
    }
}
