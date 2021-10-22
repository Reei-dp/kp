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
    /// Логика взаимодействия для AddEditPageTeacher.xaml
    /// </summary>
    public partial class AddEditPageTeacher : Page
    {
        private teachers _currentteachers = new teachers();

        public AddEditPageTeacher(teachers selectteachers = null)
        {
            InitializeComponent();

            if (selectteachers != null)
            {
                _currentteachers = selectteachers;
            }
            else
            {
                _currentteachers.accounts = AddEditAccountsTeacher.currentaccount;
            }

            DataContext = _currentteachers;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentteachers.teacher_name))
                errors.AppendLine("Укажите имя преподавателя!");
            if (string.IsNullOrWhiteSpace(_currentteachers.teacher_surname))
                errors.AppendLine("Укажите фамилию преподавателя!");
            if (string.IsNullOrWhiteSpace(_currentteachers.teacher_middle_name))
                errors.AppendLine("Укажите Отчетсво преподавателя!");
            if (string.IsNullOrWhiteSpace(_currentteachers.speciality))
                errors.AppendLine("Укажите специальность преподавателя!");
            if (string.IsNullOrWhiteSpace(_currentteachers.sex))
                errors.AppendLine("Укажите пол преподавателя!");
            if (string.IsNullOrWhiteSpace(_currentteachers.mobile_phone_number))
                errors.AppendLine("Укажите номер телефона преподавателя!");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentteachers.id_teacher == 0)
            {
                EntityFrameworkFW.GetContext().teachers.Add(_currentteachers);
            }

            try
            {
                EntityFrameworkFW.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены!");
                NavigationService.Navigate(new ChangeTeacher());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                NavigationService.Navigate(new ChangeTeacher());
            }

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditAccountsTeacher(null));
        }
    }
}
