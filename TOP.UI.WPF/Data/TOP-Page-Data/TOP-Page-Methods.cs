using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using TOP.Library.API.Enteties;
using TOP.Library.API.TOPs;
using TOP.Library.Data.models;
using TOP.UI.WPF.Data.Details_PageData;
using TOP.UI.WPF.Data.Models;
using TOP.UI.WPF.UI.Windows.Reservation;

namespace TOP.UI.WPF.Data.TOP_Page_Data
{
    public class TOP_Page_Methods
    {

        #region Objects
        private readonly TOP_Functionality top_Functionality = new TOP_Functionality();
        readonly Details_Page_Methods details_Page_Methods = new Details_Page_Methods();
        private readonly List<TOPModel> TOPs = new List<TOPModel>();
        readonly List<string> teachers = new List<string>();
        readonly List<string> vocationalQualificationUnits = new List<string>();

        bool isTeacher = false;
        string Type = "";
        Guid UpdateId = Guid.Empty;
        #endregion

        public void LoadDetails(MenuItem AcceptItem, MenuItem EditItem, MenuItem DeleteItem, 
            DataGrid TOPsDataGrid, ComboBox cboTeacherAdd, ComboBox cboVocationQualificationUnitAdd)
        {
            CheckAccountRole(AcceptItem, EditItem, DeleteItem);
            GetTOPs(TOPsDataGrid);
            GetTeachers(cboTeacherAdd);
            GetVocationalQualificationUnits(cboVocationQualificationUnitAdd);
        }

        private void CheckAccountRole(MenuItem AcceptItem, MenuItem EditItem, MenuItem DeleteItem)
        {
            if (Globals.AuthenticatedAccount.Role != "Teacher")
            {
                AcceptItem.Visibility = Visibility.Collapsed;
                EditItem.Visibility = Visibility.Collapsed;
                DeleteItem.Visibility = Visibility.Collapsed;
                return;
            }
            isTeacher = true;
        }

        #region TeachersAndUnits
        private async void SetTeachersToList()
        {
            teachers.Clear();
            foreach (var teacher in await details_Page_Methods.GetTeachersAsync())
            {
                teachers.Add(teacher.teacher);
            }
        }
        private void GetTeachers(ComboBox cboTeacherAdd)
        {
            SetTeachersToList();
            cboTeacherAdd.ItemsSource = null;
            cboTeacherAdd.ItemsSource = teachers;
        }

        private async void SetVocationalQualificationUnitsToList()
        {
            vocationalQualificationUnits.Clear();
            foreach (var vocationalQualificationUnit in await details_Page_Methods.GetVocationalQualificationUnitsAsync())
            {
                vocationalQualificationUnits.Add(vocationalQualificationUnit.vocationalQualificationUnit);
            }
        }
        private void GetVocationalQualificationUnits(ComboBox cboVocationQualificationUnitAdd)
        {
            SetVocationalQualificationUnitsToList();
            cboVocationQualificationUnitAdd.ItemsSource = null;
            cboVocationQualificationUnitAdd.ItemsSource = vocationalQualificationUnits;
        } 
        #endregion

        #region GetTOPs
        public async void GetTOPs(DataGrid TOPsDataGrid)
        {
            TOPs.Clear();
            
            if(isTeacher == false)
            {
                foreach (var top in await top_Functionality.GetTOPSAsync())
                {
                    if (top.Accepted == false)
                    {
                        TOPs.Add(top);
                    }
                }
            }
            TOPs.AddRange(await top_Functionality.GetTOPSAsync());
            UpdateTOPsDataGrid(TOPsDataGrid);
        }


        private void UpdateTOPsDataGrid(DataGrid TOPsDataGrid)
        {
            RefreshDataGrid(TOPsDataGrid);
        }

        public void RefreshDataGrid(DataGrid TOPsDataGrid)
        {
            TOPsDataGrid.ItemsSource = null;
            TOPsDataGrid.ItemsSource = TOPs;
        }
        public void TOPsDataGridColumns(DataGrid TOPsDataGrid)
        {
            TOPsDataGrid.Columns[0].Visibility = Visibility.Collapsed;//Id
            TOPsDataGrid.Columns[1].Visibility = Visibility.Collapsed;//PhoneNumber
            TOPsDataGrid.Columns[2].Visibility = Visibility.Collapsed;//EmailAddress
            TOPsDataGrid.Columns[6].Visibility = Visibility.Collapsed;//Info
            if (isTeacher == false)
            {
                TOPsDataGrid.Columns[5].Visibility = Visibility.Collapsed;//Accepted
            }
        } 
        #endregion

        public void ShowDetailsOfTOP(TextBox txtCompanyAdd, TextBox txtAddressAdd, ComboBox cboTeacherAdd,
            ComboBox cboVocationQualificationUnitAdd, TextBox txtPhoneAdd, TextBox txtEmailAdd,
            RichTextBox txtInfoAdd, DataGrid TOPsDataGrid, StackPanel TOP_Panel, Button btnOk)
        {
            TOP_Panel_Action(TOP_Panel, 300, "show", txtCompanyAdd, txtAddressAdd, cboTeacherAdd, cboVocationQualificationUnitAdd, txtPhoneAdd,
                txtEmailAdd, txtInfoAdd, TOPsDataGrid, btnOk);
        }

        #region Reservation
        public void ReserveTOP(DataGrid TOPsDataGrid)
        {
            TOPModel top = TOPsDataGrid.SelectedItem as TOPModel;
            if (isReserved(top))
            {
                MessageBox.Show($"This top is already reserved to someone it's reservations ends {top.ReservationEnds}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Reservation(top, TOPsDataGrid);
        }
        private void Reservation(TOPModel top, DataGrid TOPsDataGrid)
        {
            ReservationWindow reservation = new ReservationWindow();
            if (reservation.ShowDialog() == true)
            {
                top.Reserved = true;
                top.ReservationEnds = reservation.ReservationEndDate.SelectedDate.Value;
                UpdateTOP(top, TOPsDataGrid);
                return;
            }
            MessageBox.Show("TOP is not reserved!", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private bool isReserved(TOPModel top)
        {
            if (top.Reserved)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Acception
        public void AcceptTOP(DataGrid TOPsDataGrid)
        {
            TOPModel top = TOPsDataGrid.SelectedItem as TOPModel;
            if (isAccepted(top))
            {
                MessageBox.Show($"This top is already accpeted", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Acception(top, TOPsDataGrid);
        }

        public void Acception(TOPModel top, DataGrid TOPsDataGrid)
        {
            top.Accepted = true;
            UpdateTOP(top, TOPsDataGrid);
        }

        private bool isAccepted(TOPModel top)
        {
            if (top.Accepted)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region DeleteTOP
        public async void DeleteTOP(DataGrid TOPsDataGrid)
        {
            TOPModel top = TOPsDataGrid.SelectedItem as TOPModel;
            if (await top_Functionality.DeleteTOPAsync(top) != "{\"message\":\"TOP is deleted from database\"}")
            {
                MessageBox.Show("TOP is not deleted", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("TOP is deleted", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            GetTOPs(TOPsDataGrid);
        }
        #endregion

        #region SaveTOP
        private async void UpdateTOP(TOPModel top, DataGrid TOPsDataGrid)
        {
            if (await top_Functionality.UpdateTOPAsync(top) != "{\"message\":\"TOP is updated to database\"}")
            {
                MessageBox.Show("TOP is not updated", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("TOP is updated", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            GetTOPs(TOPsDataGrid);
        }
        
        private async void AddTOP(TOPModel top, DataGrid TOPsDataGrid)
        {
            if (await top_Functionality.AddTOPAsync(top) != "{\"message\":\"TOP is added to database\"}")
            {
                MessageBox.Show("TOP is not added", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("TOP is added", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            GetTOPs(TOPsDataGrid);
        }

        public void SaveTOP(TextBox txtCompanyAdd, TextBox txtAddressAdd, ComboBox cboTeacherAdd,
            ComboBox cboVocationQualificationUnitAdd, TextBox txtPhoneAdd, TextBox txtEmailAdd,
            RichTextBox txtInfoAdd, DataGrid TOPsDataGrid, StackPanel TOP_Panel, Button btnOk)
        {
            TOPModel top = new TOPModel
            {
                Id = UpdateId,
                PhoneNumber = txtPhoneAdd.Text,
                EmailAddress = txtEmailAdd.Text,
                Reserved = false,
                ReservationEnds = DateTime.MinValue,
                Accepted = false,
                Info = new TextRange(txtInfoAdd.Document.ContentStart, txtInfoAdd.Document.ContentEnd).Text,
                Company = txtCompanyAdd.Text,
                Address = txtAddressAdd.Text,
                Teacher = cboTeacherAdd.Text,
                VocationalQualificationUnit = cboVocationQualificationUnitAdd.Text
            };
            if (Type == "new")
            {
                NewTOP(top, TOPsDataGrid, TOP_Panel);
                TOP_Panel_Action(TOP_Panel, 0, "", txtCompanyAdd, txtAddressAdd, cboTeacherAdd, cboVocationQualificationUnitAdd, txtPhoneAdd,
                txtEmailAdd, txtInfoAdd, TOPsDataGrid, btnOk);
                return;
            }
            UpdateTOP(top, TOPsDataGrid);
            TOP_Panel_Action(TOP_Panel, 0, "", txtCompanyAdd, txtAddressAdd, cboTeacherAdd, cboVocationQualificationUnitAdd, txtPhoneAdd,
                txtEmailAdd, txtInfoAdd, TOPsDataGrid, btnOk);
        }

        public void NewTOP(TOPModel top, DataGrid TOPsDataGrid, StackPanel TOP_Panel)
        {
            if (top.Company == "" || top.Address == "" || top.Teacher == "" || top.VocationalQualificationUnit == "")
            {
                MessageBox.Show("Missing information!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            AddTOP(top, TOPsDataGrid);
        } 
        #endregion

        public void TOP_Panel_Action(StackPanel TOP_Panel, int width, string type, TextBox txtCompanyAdd, TextBox txtAddressAdd, ComboBox cboTeacherAdd,
            ComboBox cboVocationQualificationUnitAdd, TextBox txtPhoneAdd, TextBox txtEmailAdd,
            RichTextBox txtInfoAdd, DataGrid TOPsDataGrid, Button btnOk)
        {
            Type = type;
            TOP_Panel.Width = width;
            if(type == "")
            {
                txtCompanyAdd.IsEnabled = true;
                txtAddressAdd.IsEnabled = true;
                txtEmailAdd.IsEnabled = true;
                txtInfoAdd.IsEnabled = true;
                txtPhoneAdd.IsEnabled = true;
                cboTeacherAdd.IsEnabled = true;
                cboVocationQualificationUnitAdd.IsEnabled = true;
                btnOk.IsEnabled = true;
                txtCompanyAdd.Text = "";
                txtAddressAdd.Text = "";
                txtEmailAdd.Text = "";
                txtInfoAdd.Document.Blocks.Clear();
                txtInfoAdd.Document.Blocks.Add(new Paragraph(new Run("")));
                txtPhoneAdd.Text = "";
                cboTeacherAdd.SelectedIndex = -1;
                cboVocationQualificationUnitAdd.SelectedIndex = -1;
            }
            else if(type == "edit" || type == "show")
            {
                TOPModel top = TOPsDataGrid.SelectedItem as TOPModel;
                UpdateId = top.Id;
                txtCompanyAdd.Text = top.Company;
                txtAddressAdd.Text = top.Address;
                txtEmailAdd.Text = top.EmailAddress;
                txtInfoAdd.Document.Blocks.Clear();
                txtInfoAdd.Document.Blocks.Add(new Paragraph(new Run(top.Info)));
                txtPhoneAdd.Text = top.PhoneNumber;
                cboTeacherAdd.Text = top.Teacher;
                cboVocationQualificationUnitAdd.Text = top.VocationalQualificationUnit;
                if(type == "show")
                {
                    txtCompanyAdd.IsEnabled = false;
                    txtAddressAdd.IsEnabled = false;
                    txtEmailAdd.IsEnabled = false;
                    txtInfoAdd.IsEnabled = false;
                    txtPhoneAdd.IsEnabled = false;
                    cboTeacherAdd.IsEnabled = false;
                    cboVocationQualificationUnitAdd.IsEnabled = false;
                    btnOk.IsEnabled = false;
                }
            }
        }
    }
}
