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
    /// Логика взаимодействия для AddEditAccounts.xaml
    /// </summary>
    public partial class AddEditAccounts : Page
    {
        private accounts _currentaccounts = new accounts();
        public AddEditAccounts(accounts selectaccounts)
        {
            InitializeComponent();


            if (selectaccounts != null)
                _currentaccounts = selectaccounts;

            DataContext = _currentaccounts;
            _currentaccounts.status = "student";
        
            }
        

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChangeStudent());
        }

        public static accounts currentaccount;
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentaccounts.login))
                errors.AppendLine("Укажите логин ученика!");
            if (string.IsNullOrWhiteSpace(_currentaccounts.password))
                errors.AppendLine("Укажите пароль ученика!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentaccounts.id_user == 0)
            {
                EntityFrameworkFW.GetContext().accounts.Add(_currentaccounts);
            }

            try
            {
                EntityFrameworkFW.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены!");

                using (EntityFrameworkFW db = new EntityFrameworkFW())
                {

                    currentaccount = db.accounts.Where(b => b.login == Login.Text).FirstOrDefault();
                }

                NavigationService.Navigate(new AddEditPageStudent(_currentaccounts.students.FirstOrDefault()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            

        }
        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
