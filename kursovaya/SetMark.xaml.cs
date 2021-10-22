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
    /// Логика взаимодействия для SetMark.xaml
    /// </summary>
    public partial class SetMark : Page
    {
        private marks _currentmark = new marks();
        public SetMark(marks selectmarks)
        {
            InitializeComponent();
            if (selectmarks != null)
                _currentmark = selectmarks;
            DataContext = _currentmark;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

            int mark = (int)_currentmark.mark;
            string m1 = mark.ToString();
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(m1))
                errors.AppendLine("Укажите логин ученика!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentmark.id_mark == 0)
            {
                EntityFrameworkFW.GetContext().marks.Add(_currentmark);
            }
            try
            {
                EntityFrameworkFW.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены!");

                NavigationService.Navigate(new TeacherList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
    }
}
