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
    /// Логика взаимодействия для Adminstration.xaml
    /// </summary>
    public partial class Adminstration : Page
    {
        public Adminstration()
        {
            InitializeComponent();
        }

        private void Change_Teacher(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChangeTeacher());
        }

        private void Change_Student(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChangeStudent());
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
