using System.ComponentModel.DataAnnotations;

namespace LeiloesMonetWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required] // name is not a nullable property
        public int Name { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now; // default value é o now
    }
}
