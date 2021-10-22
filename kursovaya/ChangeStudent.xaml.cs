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
    /// Логика взаимодействия для ChangeStudent.xaml
    /// </summary>
    public partial class ChangeStudent : Page
    {
        public ChangeStudent()
        {
            InitializeComponent();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditAccounts(((sender as Button).DataContext as students).accounts));
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditAccounts(null));
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
           var studentsForRemove = DGridChangeStudent.SelectedItems.Cast<students>().Select(b => b.accounts).ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {studentsForRemove.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    EntityFrameworkFW.GetContext().accounts.RemoveRange(studentsForRemove);
                    EntityFrameworkFW.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    DGridChangeStudent.ItemsSource = EntityFrameworkFW.GetContext().students.ToList();
                }
                catch
                {

                }
            }

               
        }


        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                EntityFrameworkFW.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridChangeStudent.ItemsSource = EntityFrameworkFW.GetContext().students.ToList();
              
            }
        }
    }
}
