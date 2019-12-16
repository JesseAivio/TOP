using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TOP.Library.API.Models;
using TOP.Library.Data.models;
using System.Windows;
using MaterialDesignThemes.Wpf;

namespace TOP.UI.WPF.Data.Details_PageData
{
    public class Details_Page_Methods
    {
        readonly Details_Functionality details_Functionality = new Details_Functionality();
        string type;
        string ActionType;
        Border ActionPanelBorder;
        StackPanel ActionPanel;
        TextBlock ActionPanelTitle;
        TextBox txtName;

        public void GetDetails(ListView TeachersListView, ListView VocationalQualificationUnitListView)
        {
            SetTeachersToListView(TeachersListView);
            SetVocationalQualificationUnitsToListView(VocationalQualificationUnitListView);
        }

        private void SetObjects(string type, string ActionType, Border ActionPanelBorder, StackPanel ActionPanel, TextBlock ActionPanelTitle, TextBox txtName)
        {
            this.type = type;
            this.ActionType = ActionType;
            this.ActionPanelBorder = ActionPanelBorder;
            this.ActionPanel = ActionPanel;
            this.ActionPanelTitle = ActionPanelTitle;
            this.txtName = txtName;
        }

        private void ClearObjects()
        {
            type = null;
            ActionType = null;
            ActionPanelBorder = null;
            ActionPanel = null;
            ActionPanelTitle = null;
        }

        public void ShowActionPanel(string type, string ActionType, Border ActionPanelBorder, StackPanel ActionPanel, 
            TextBlock ActionPanelTitle, TextBox txtName, ListViewItem selectedTeacher, ListViewItem selectedVocationalQualificationUnit)
        {
            SetObjects(type, ActionType, ActionPanelBorder, ActionPanel, ActionPanelTitle, txtName);
            this.ActionPanelBorder.Visibility = Visibility.Visible;
            this.ActionPanel.Visibility = Visibility.Visible;
            HintAssist.SetHint(txtName, this.type);
            if (this.ActionType == "Add")
            {
                this.ActionPanelTitle.Text = $"Add {this.type}";
                return;
            }
            this.ActionPanelTitle.Text = $"Edit {this.type}";
            if(this.type == "Teacher")
            {
                this.txtName.Text = selectedTeacher.Content.ToString();
                return;
            }
            this.txtName.Text = selectedVocationalQualificationUnit.Content.ToString();
        }

        private void CloseActionPanel()
        {
            ActionPanelBorder.Visibility = Visibility.Collapsed;
            ActionPanel.Visibility = Visibility.Collapsed;
            ClearObjects();
        }

        public void SaveActionPanel(TextBox txtName, ListViewItem selectedTeacher, ListViewItem selectedVocationalQualificationUnit, ListView TeachersListView, ListView VocationalQualificationUnitListView)
        {
            if(ActionType == "Add")
            {
                if(type == "Teacher")
                {
                    AddTeacher(txtName, TeachersListView, VocationalQualificationUnitListView);
                    return;
                }
                AddVocationalQualificationUnit(txtName, TeachersListView, VocationalQualificationUnitListView);
                return;
            }
            if(type == "Teacher")
            {
                UpdateTeacher(txtName, selectedTeacher, TeachersListView, VocationalQualificationUnitListView);
                return;
            }
            UpdateVocationalQualificationUnit(txtName, selectedVocationalQualificationUnit, TeachersListView, VocationalQualificationUnitListView);
        }

        #region Teacher
        private async void AddTeacher(TextBox txtName, ListView TeachersListView, ListView VocationalQualificationUnitListView)
        {
            Teacher teacher = new Teacher
            {
                teacher = txtName.Text
            };
            if (await details_Functionality.AddTeacherAsync(teacher) != null)
            {
                MessageBox.Show("Teacher added!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                CloseActionPanel();
                txtName.Text = "";
                GetDetails(TeachersListView, VocationalQualificationUnitListView);
                return;
            }
            MessageBox.Show("Teacher is not added!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private async void UpdateTeacher(TextBox txtName, ListViewItem selectedTeacher, ListView TeachersListView, ListView VocationalQualificationUnitListView)
        {
            Teacher teacher = new Teacher
            {
                Id = Guid.Parse(selectedTeacher.Tag.ToString()),
                teacher = txtName.Text
            };
            if (await details_Functionality.UpdateTeacherAsync(teacher) != null)
            {
                MessageBox.Show("Teacher updated!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                CloseActionPanel();
                txtName.Text = "";
                GetDetails(TeachersListView, VocationalQualificationUnitListView);
                return;
            }
            MessageBox.Show("Teacher is not updated!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public async Task<IEnumerable<Teacher>> GetTeachersAsync()
        {
            return await details_Functionality.GetTeachersAsync();
        }

        public async void DeleteTeacher(ListViewItem selectedTeacher, ListView TeachersListView, ListView VocationalQualificationUnitListView)
        {
            if (selectedTeacher == null)
            {
                MessageBox.Show("Please select teacher you want to delete!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Teacher teacher = new Teacher
            {
                Id = Guid.Parse(selectedTeacher.Tag.ToString()),
                teacher = selectedTeacher.Content.ToString()
            };
            if (await details_Functionality.DeleteTeacherAsync(teacher) != "{\"message\":\"Teacher deleted from database\"}")
            {
                MessageBox.Show("Unable to delete teacher", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Teacher is deleted", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            GetDetails(TeachersListView, VocationalQualificationUnitListView);
        }

        private async void SetTeachersToListView(ListView TeachersListView)
        {
            TeachersListView.Items.Clear();
            foreach (var teacher in await GetTeachersAsync())
            {
                ListViewItem item = new ListViewItem
                {
                    Content = teacher.teacher,
                    Tag = teacher.Id.ToString()
                };
                TeachersListView.Items.Add(item);
            }
        } 
        #endregion

        private async void SetVocationalQualificationUnitsToListView(ListView VocationalQualificationUnitListView)
        {
            VocationalQualificationUnitListView.Items.Clear();
            foreach (var vocationalQualificationUnit in await GetVocationalQualificationUnitsAsync())
            {
                ListViewItem item = new ListViewItem
                {
                    Content = vocationalQualificationUnit.vocationalQualificationUnit,
                    Tag = vocationalQualificationUnit.Id.ToString()
                };
                VocationalQualificationUnitListView.Items.Add(item);
            }
        }

        public async Task<IEnumerable<VocationalQualificationUnit>> GetVocationalQualificationUnitsAsync()
        {
            return await details_Functionality.GetVocationalQualificationUnitsAsync();
        }

        private async void AddVocationalQualificationUnit(TextBox txtName, ListView TeachersListView, ListView VocationalQualificationUnitListView)
        {
            VocationalQualificationUnit vocationalQualificationUnit = new VocationalQualificationUnit
            {
                vocationalQualificationUnit = txtName.Text
            };
            if (await details_Functionality.AddVocationalQualificationUnitAsync(vocationalQualificationUnit) != null)
            {
                MessageBox.Show("VocationalQualificationUnit added!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                CloseActionPanel();
                txtName.Text = "";
                GetDetails(TeachersListView, VocationalQualificationUnitListView);
                return;
            }
            MessageBox.Show("VocationalQualificationUnit is not added!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private async void UpdateVocationalQualificationUnit(TextBox txtName, ListViewItem selectedVocationalQualificationUnit, ListView TeachersListView, ListView VocationalQualificationUnitListView)
        {
            VocationalQualificationUnit vocationalQualificationUnit = new VocationalQualificationUnit
            {
                Id = Guid.Parse(selectedVocationalQualificationUnit.Tag.ToString()),
                vocationalQualificationUnit = txtName.Text
            };
            if (await details_Functionality.UpdateVocationalQualificationUnitAsync(vocationalQualificationUnit) != null)
            {
                MessageBox.Show("VocationalQualificationUnit updated!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                CloseActionPanel();
                txtName.Text = "";
                GetDetails(TeachersListView, VocationalQualificationUnitListView);
                return;
            }
            MessageBox.Show("VocationalQualificationUnit is not updated!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public async void DeleteVocationalQualificationUnit(ListViewItem selectedVocationalQualificationUnit, ListView TeachersListView, ListView VocationalQualificationUnitListView)
        {
            if (selectedVocationalQualificationUnit == null)
            {
                MessageBox.Show("Please select vocationalQualificationUnit you want to delete!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            VocationalQualificationUnit vocationalQualificationUnit = new VocationalQualificationUnit
            {
                Id = Guid.Parse(selectedVocationalQualificationUnit.Tag.ToString()),
                vocationalQualificationUnit = selectedVocationalQualificationUnit.Content.ToString()
            };
            if (await details_Functionality.DeleteVocationalQualificationUnitAsync(vocationalQualificationUnit) != "{\"message\":\"VocationalQualificationUnit deleted from database\"}")
            {
                MessageBox.Show("Unable to delete vocationalQualificationUnit", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("VocationalQualificationUnit is deleted", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            GetDetails(TeachersListView, VocationalQualificationUnitListView);
        }
    }
}
