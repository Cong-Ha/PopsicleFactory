using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public enum PopsicleType
    {
        Fruit = 0,
        Cream = 1,
        Juice = 2,
        Yogurt = 3,
        Sherbet = 4,
        IceCream = 5
    }

    public enum PopsicleSize
    {
        Mini = 0,
        Regular = 1,
        Large = 2,
        Jumbo = 3
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
        public PopsicleType Type { get; set; } = PopsicleType.Fruit;
        
        [Column("Size")]
        public PopsicleSize Size { get; set; } = PopsicleSize.Regular;

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
