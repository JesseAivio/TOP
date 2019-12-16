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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TOP.UI.WPF.Data.Pages_Data;

namespace TOP.UI.WPF.UI.Pages.Accounts
{
    /// <summary>
    /// Interaction logic for AccountsPage.xaml
    /// </summary>
    public partial class AccountsPage : UserControl
    {
        readonly Accounts_Page_Methods accounts_Page_Methods = new Accounts_Page_Methods();
        public AccountsPage()
        {
            InitializeComponent();
            accounts_Page_Methods.GetAccounts(AccountsListView);
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            accounts_Page_Methods.SaveAccount(btnOk, FormTitle, txtUsername, txtPassword, txtPasswordConfirm, cboRole, AccountsListView,
                AccountsListView.SelectedItem as ListViewItem);
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            accounts_Page_Methods.SetDetailsToForm(btnOk, FormTitle, txtUsername, txtPassword, txtPasswordConfirm,
                cboRole, AccountsListView.SelectedItem as ListViewItem);
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            accounts_Page_Methods.DeleteAccount(AccountsListView,
                   AccountsListView.SelectedItem as ListViewItem);
        }
    }
}
