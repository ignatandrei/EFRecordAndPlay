using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFRecordAndPlay_Use_MakingDemos.DatabaseRelated
{
    [Table("Department")]
    public partial class Department
    {
        private int _id;

        public Department()
        {
            Employee = new HashSet<Employee>();
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [Required]
        [StringLength(50)]
        public string NameDepartment { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
