using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kursovaya
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPageStudent : Page
    {
        private students _currentstudents = new students();

        public AddEditPageStudent(students selectstudents = null)
        {
            InitializeComponent();

            if (selectstudents != null)
            {
                _currentstudents = selectstudents;
            }
            else
            {
                _currentstudents.accounts = AddEditAccounts.currentaccount;
            }

            DataContext = _currentstudents;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentstudents.student_name))
                errors.AppendLine("Укажите имя ученика!");
            if (string.IsNullOrWhiteSpace(_currentstudents.student_surname))
                errors.AppendLine("Укажите фамилию ученика!");
            if (string.IsNullOrWhiteSpace(_currentstudents.student_middle_name))
                errors.AppendLine("Укажите Отчетсво ученика!");
            if (string.IsNullOrWhiteSpace(_currentstudents.@class))
                errors.AppendLine("Укажите класс ученика!");
            if (string.IsNullOrWhiteSpace(_currentstudents.sex))
                errors.AppendLine("Укажите пол ученика!");
            if (string.IsNullOrWhiteSpace(_currentstudents.mobile_phone_number))
                errors.AppendLine("Укажите номер телефона ученика!");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentstudents.id_student == 0)
            {
                EntityFrameworkFW.GetContext().students.Add(_currentstudents);
            }

            try
            {
                EntityFrameworkFW.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены!");
                NavigationService.Navigate(new ChangeStudent());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                NavigationService.Navigate(new ChangeStudent());
            }

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditAccounts(null));
        }
    }
}
