using System.ComponentModel.DataAnnotations.Schema;

namespace EFRecordAndPlayTest
{
    public class PersonWithBlog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
    }
}
