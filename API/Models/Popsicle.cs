using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public enum PopsicleType
    {
        None = 0,
        Fruit = 1,
        Cream = 2,
        Juice = 3,
        Yogurt = 4,
        Sherbet = 5
    }

    public enum PopsicleSize
    {
        None = 0,
        Mini = 1,
        Regular = 2,
        Large = 3,
        Jumbo = 4
    }
    
    [Table("Popsicles")]
    public class Popsicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Name")]
        public required string Name { get; set; }
        
        [Column("Type")]
        public PopsicleType Type { get; set; }
        
        [Column("Size")]
        public PopsicleSize Size { get; set; }

        [MaxLength(500)]
        [Column("Description")]
        public string? Description { get; set; }
        
        [Column("is_sugar_free")]
        public bool IsSugarFree { get; set; }
        
        [Column("is_organic")]
        public bool IsOrganic { get; set; }

        [Required]
        [Column("Price", TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
    }    
}
