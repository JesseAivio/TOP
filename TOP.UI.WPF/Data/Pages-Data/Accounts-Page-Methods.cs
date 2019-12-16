using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using TOP.Library.API.Models;
using TOP.Library.Data.models;

namespace TOP.UI.WPF.Data.Pages_Data
{
    public class Accounts_Page_Methods
    {
        readonly Accounts_Functionality accounts_Functionality = new Accounts_Functionality();
        private string type = "register";
        public async void GetAccounts(ListView AccountsListView)
        {
            AccountsListView.Items.Clear();
            foreach (var account in await accounts_Functionality.GetAccountsAsync())
            {
                ListViewItem accountItem = new ListViewItem()
                {
                    Content = account.Username,
                    Name = account.Role,
                    Tag = account.Id
                };
                AccountsListView.Items.Add(accountItem);
            }
        }

        public void SetDetailsToForm(Button btnOk, TextBlock FormTitle, TextBox txtUsername, PasswordBox txtPassword,
            PasswordBox txtPasswordConfirm, ComboBox cboRole, ListViewItem selectedAccount)
        {
            FormTitle.Text = "Edit account";
            btnOk.Content = "Save";
            txtUsername.Text = selectedAccount.Content.ToString();
            txtPassword.Password = "";
            txtPasswordConfirm.Password = "";
            cboRole.Text = selectedAccount.Name;
            type = "edit";
        }

        public async void SaveAccount(Button btnOk, TextBlock FormTitle, TextBox txtUsername, PasswordBox txtPassword, 
            PasswordBox txtPasswordConfirm, ComboBox cboRole, ListView AccountsListView, ListViewItem selectedAccount)
        {
            if(type == "register")
            {
                Account account = new Account
                {
                    Username = txtUsername.Text,
                    Password = txtPassword.Password,
                    Role = cboRole.Text
                };
                if(await accounts_Functionality.RegisterAccountAsync(account) != "{\"message\":\"Your account is registered!\"}")
                {
                    MessageBox.Show("Error while registering");
                    return;
                }
                MessageBox.Show("Registered", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                GetAccounts(AccountsListView);
                return;
            }
            else if(type == "edit")
            {
                Account account = new Account
                {
                    Id = Guid.Parse(selectedAccount.Tag.ToString()),
                    Username = txtUsername.Text,
                    Password = txtPassword.Password,
                    Role = cboRole.Text
                };
                if(await accounts_Functionality.UpdateAccountAsync(account) != "{\"message\":\"Your account is updated!\"}")
                {
                    MessageBox.Show("Error while updating");
                    return;
                }
                FormTitle.Text = "Register new account";
                btnOk.Content = "Register";
                txtUsername.Text = "";
                txtPassword.Password = "";
                txtPasswordConfirm.Password = "";
                cboRole.SelectedIndex = -1;
                type = "register";
                MessageBox.Show("Updated", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                GetAccounts(AccountsListView);
                return;
            }
        }

        public async void DeleteAccount(ListView AccountsListView, ListViewItem selectedAccount)
        {
            if(await accounts_Functionality.DeleteAccountAsync(selectedAccount.Content.ToString()) != "{\"message\":\"Account deleted\"}")
            {
                MessageBox.Show("Error while deleting");
                return;
            }
            MessageBox.Show("Deleted", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            GetAccounts(AccountsListView);
            return;
        }
    }
}
