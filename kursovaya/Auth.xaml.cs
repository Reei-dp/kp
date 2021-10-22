using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace kursovaya
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        public Auth()
        {
            InitializeComponent();
         }

        public static int StudentId;
        public static int TeacherId;
        private void Button_toTeacher(object sender, RoutedEventArgs e)
        {

            object authTeacher = null;
            object authStudent = null;
            object authAdmin = null;
            using (EntityFrameworkFW db = new EntityFrameworkFW())
            {
                authTeacher = db.accounts.Where(b => b.login == LoginAuth.Text && b.password == PasswordAuth.Password && b.status == "teacher").FirstOrDefault();
                TeacherId = db.accounts.Where(b => b.login == LoginAuth.Text).Select(x => x.id_user).FirstOrDefault();
            }
                
            if (authTeacher != null)
            {
                NavigationService.Navigate(new TeacherList());
            }
            else
            {
                using (EntityFrameworkFW db = new EntityFrameworkFW())
                {
                    authStudent = db.accounts.Where(b => b.login == LoginAuth.Text && b.password == PasswordAuth.Password && b.status == "student").FirstOrDefault();
                    StudentId = db.accounts.Where(b => b.login == LoginAuth.Text).Select(x => x.id_user).FirstOrDefault();
                  
                }
                if (authStudent != null)
                {
                    NavigationService.Navigate(new StudentList());
                }
                else
                {
                    using (EntityFrameworkFW db = new EntityFrameworkFW())
                    {
                        authAdmin = db.accounts.Where(b => b.login == LoginAuth.Text && b.password == PasswordAuth.Password && b.status == "admin").FirstOrDefault();
                    }
                    if (authAdmin != null)
                    {
                        NavigationService.Navigate(new Adminstration());
                    }
                    else
                    {
                        MessageBox.Show("Неправельный логин или пароль!");
                    }

                }
            }

        }

    }
}

