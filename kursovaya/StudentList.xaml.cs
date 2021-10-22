using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace kursovaya
{
    /// <summary>
    /// Логика взаимодействия для StudentList.xaml
    /// </summary>
    public partial class StudentList : Page
    {
        subjects currentSubject;
        int currentSemester = 1;


        public StudentList()
        {
            InitializeComponent();
            string content;
            string student_class;
           
            ComboSubject.ItemsSource = EntityFrameworkFW.GetContext().subjects.ToList();

             var m1 = EntityFrameworkFW.GetContext().marks.GroupBy(x => x.semester);
             var m2 = m1.Select(x => x.Key).ToList();
            ComboSemester.ItemsSource = m2;

            ComboSubject.SelectedIndex = 0;
            ComboSemester.SelectedIndex = 0;
            using (EntityFrameworkFW db = new EntityFrameworkFW())
            {
                var stud = db.students.Where(x => x.id_user == Auth.StudentId).First();
                content = $"{stud.student_surname} {stud.student_name} {stud.student_middle_name}";
                student_class = stud.@class;
               
                Name.Text = content;
                Class.Text = student_class;

                currentSubject = (ComboSubject.SelectedItem as subjects);
                DGridStudentList.ItemsSource = currentSubject.marks.Where(x => x.semester == currentSemester && x.id_student == stud.id_student).ToList();
            }

           
        }

        private void ComboSubject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var stud = EntityFrameworkFW.GetContext().students.Where(x => x.id_user == Auth.StudentId).First();
            currentSubject = (subjects)e.AddedItems[0];
            DGridStudentList.ItemsSource = currentSubject.marks.Where(x => x.semester == currentSemester && x.id_student == stud.id_student).ToList();
        }

        private void ComboSemester_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var stud = EntityFrameworkFW.GetContext().students.Where(x => x.id_user == Auth.StudentId).First();
            var s = (int)e.AddedItems[0];
            DGridStudentList.ItemsSource = currentSubject.marks.Where(x => x.semester == s && x.id_student == stud.id_student).ToList();
        }



    }
}
