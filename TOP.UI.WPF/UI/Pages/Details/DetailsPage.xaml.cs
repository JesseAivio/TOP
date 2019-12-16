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
using TOP.UI.WPF.Data.Details_PageData;

namespace TOP.UI.WPF.UI.Pages.Details
{
    /// <summary>
    /// Interaction logic for DetailsPage.xaml
    /// </summary>
    public partial class DetailsPage : UserControl
    {
        readonly Details_Page_Methods details_Page_Methods = new Details_Page_Methods();
        public DetailsPage()
        {
            InitializeComponent();
            details_Page_Methods.GetDetails(TeachersListView, VocationalQualificationUnitListView);
        }

        private void AddTeacher_Click(object sender, RoutedEventArgs e)
        {
            details_Page_Methods.ShowActionPanel("Teacher", "Add", ActionPanelBorder, ActionPanel, ActionPanelTitle,
                txtName, TeachersListView.SelectedItem as ListViewItem, VocationalQualificationUnitListView.SelectedItem as ListViewItem);
        }

        private void EditTeacher_Click(object sender, RoutedEventArgs e)
        {
            details_Page_Methods.ShowActionPanel("Teacher", "Edit", ActionPanelBorder, ActionPanel, ActionPanelTitle,
                 txtName, TeachersListView.SelectedItem as ListViewItem, VocationalQualificationUnitListView.SelectedItem as ListViewItem);
        }

        private void DeleteTeacher_Click(object sender, RoutedEventArgs e)
        {
            details_Page_Methods.DeleteTeacher(TeachersListView.SelectedItem as ListViewItem, TeachersListView, VocationalQualificationUnitListView);
        }

        private void AddVocationalQualificationUnit_Click(object sender, RoutedEventArgs e)
        {
            details_Page_Methods.ShowActionPanel("VocationalQualificationUnit", "Add", ActionPanelBorder, ActionPanel, ActionPanelTitle,
                txtName, VocationalQualificationUnitListView.SelectedItem as ListViewItem, VocationalQualificationUnitListView.SelectedItem as ListViewItem);
        }

        private void EditVocationalQualificationUnit_Click(object sender, RoutedEventArgs e)
        {
            details_Page_Methods.ShowActionPanel("VocationalQualificationUnit", "Edit", ActionPanelBorder, ActionPanel, ActionPanelTitle,
                txtName, VocationalQualificationUnitListView.SelectedItem as ListViewItem, VocationalQualificationUnitListView.SelectedItem as ListViewItem);
        }

        private void DeleteVocationalQualificationUnit_Click(object sender, RoutedEventArgs e)
        {
            details_Page_Methods.DeleteVocationalQualificationUnit(VocationalQualificationUnitListView.SelectedItem as ListViewItem, TeachersListView, VocationalQualificationUnitListView);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            details_Page_Methods.SaveActionPanel(txtName, TeachersListView.SelectedItem as ListViewItem, VocationalQualificationUnitListView.SelectedItem as ListViewItem, TeachersListView, VocationalQualificationUnitListView);
        }
    }
}
