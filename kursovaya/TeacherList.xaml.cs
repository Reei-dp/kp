using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace kursovaya
{
    /// <summary>
    /// Логика взаимодействия для TeacherList.xaml
    /// </summary>
    public partial class TeacherList : Page
    {


        int currentSemester = 1;
        int subject_id;
        public TeacherList()
        {
            InitializeComponent();
            var m1 = EntityFrameworkFW.GetContext().marks.GroupBy(x => x.semester);
            var m2 = m1.Select(x => x.Key).ToList();
            ComboSemester.ItemsSource = m2;

            ComboClass.ItemsSource = EntityFrameworkFW.GetContext().students.Select(x => x.@class).Distinct().ToList();
           
            ComboClass.SelectedIndex = 0;
            ComboStudent.ItemsSource = EntityFrameworkFW.GetContext().students.Where(x => x.@class == ComboClass.Text).Select(c => string.Concat(c.student_surname, " ", c.student_name, " ", c.student_middle_name)).ToList();
            ComboSemester.SelectedIndex = 0;
            ComboStudent.SelectedIndex = 0;

            Name.Text = EntityFrameworkFW.GetContext().teachers.Where(x => x.id_user == Auth.TeacherId).Select(c => string.Concat(c.teacher_surname, " ", c.teacher_name, " ", c.teacher_middle_name)).First();
            subject_id = EntityFrameworkFW.GetContext().subjects.Where(x => x.id_teacher == Auth.TeacherId).Select(c => c.id_subject).First();
            string[] fio = ComboStudent.Text.Split(' ');
            var surname = fio[0];
            var name = fio[1];
            var middle_name = fio[2];
            var student_id = EntityFrameworkFW.GetContext().students.Where(x => x.student_surname == surname && x.student_name == name && x.student_middle_name == middle_name).First();

            DGridList.ItemsSource = EntityFrameworkFW.GetContext().marks.Where(x => x.id_student == student_id.id_student && x.id_subject == subject_id).ToList();
        }




        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SetMark((sender as Button).DataContext as marks));
        }

        
        private void ComboClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s = (string)e.AddedItems[0];
            ComboStudent.ItemsSource = EntityFrameworkFW.GetContext().students.Where(x => x.@class == s).Select(c => string.Concat(c.student_surname, " ", c.student_name, " ", c.student_middle_name)).ToList();
            ComboStudent.SelectedIndex = 0;

        }

        private void ComboStudent_SelectionChanged(object sender, SelectionChangedEventArgs z)
        {
            if (z.AddedItems.Count == 0)
                return;
            
            var s = (string)z.AddedItems[0];
            
            string stud_class = (string)ComboClass.SelectedItem;
            string[] fio = s.Split(' ');
            string surname = fio[0];
            int student = EntityFrameworkFW.GetContext().students.Where(c => c.student_surname == surname && c.@class == stud_class).Select(x => x.id_student).First();
            DGridList.ItemsSource = EntityFrameworkFW.GetContext().marks.Where(c => c.id_student == student && c.id_subject == subject_id && c.semester == currentSemester).ToList();
        }

        private void ComboSemester_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentSemester = (int)e.AddedItems[0];
            string stud_class = (string)ComboClass.SelectedItem;
            string st = (string)ComboStudent.SelectedItem;
            string[] fio = st.Split(' ');
            string surname = fio[0];
            int student = EntityFrameworkFW.GetContext().students.Where(c => c.student_surname == surname && c.@class == stud_class).Select(x => x.id_student).First();
            DGridList.ItemsSource = EntityFrameworkFW.GetContext().marks.Where(c => c.id_student == student && c.id_subject == subject_id && c.semester == currentSemester).ToList();
        }
    }
}
