using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EFRecordAndPlay_Use_MakingDemos.DatabaseRelated
{
    [Table("Employee")]
    public partial class Employee: IValidatableObject
    {
        public bool ValidateEmployee;

        public Employee()
        {
            ValidateEmployee = false;
        }
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstNameEmployee { get; set; }

        [Required]
        [StringLength(50)]
        public string LastNameEmployee { get; set; }

        public int IDDepartment { get; set; }

        public virtual Department Department { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!ValidateEmployee)
                yield break ;

            var c = new ContextForDatabase();
            if(c.Department.FirstOrDefault(it=>it.Id == this.IDDepartment) == null)
                yield return new ValidationResult("department must exists");



        }
    }
}
