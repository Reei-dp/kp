//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace kursovaya
{
    using System;
    using System.Collections.ObjectModel;
    
    public partial class teachers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public teachers()
        {
            this.subjects = new ObservableCollection<subjects>();
        }
    
        public int id_teacher { get; set; }
        public string teacher_name { get; set; }
        public string teacher_surname { get; set; }
        public string teacher_middle_name { get; set; }
        public string speciality { get; set; }
        public Nullable<int> id_user { get; set; }
        public string sex { get; set; }
        public string mobile_phone_number { get; set; }
    
        public virtual accounts accounts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<subjects> subjects { get; set; }
    }
}
