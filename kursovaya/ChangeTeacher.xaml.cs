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
    /// Логика взаимодействия для ChangeTeacher.xaml
    /// </summary>
    public partial class ChangeTeacher : Page
    {
        public ChangeTeacher()
        {
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditAccountsTeacher(null));
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var teacherForRemove = DGridChangeTeacher.SelectedItems.Cast<teachers>().Select(b => b.accounts).ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {teacherForRemove.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    EntityFrameworkFW.GetContext().accounts.RemoveRange(teacherForRemove);
                    EntityFrameworkFW.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    DGridChangeTeacher.ItemsSource = EntityFrameworkFW.GetContext().teachers.ToList();
                }
                catch
                {

                }
            }


        }


        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditAccountsTeacher(((sender as Button).DataContext as teachers).accounts));
        }



        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                EntityFrameworkFW.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridChangeTeacher.ItemsSource = EntityFrameworkFW.GetContext().teachers.ToList();

            }
        }
    }
}
