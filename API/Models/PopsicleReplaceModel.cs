using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class PopsicleReplaceModel
{
    [Required]
    [MaxLength(50, ErrorMessage = "Name must be less than 50 characters.")]
    public string Name { get; set; }
    
    public PopsicleType Type { get; set; }
    
    public PopsicleSize Size { get; set; }

    [MaxLength(500,  ErrorMessage = "Description must be less than 500 characters.")]
    public string? Description { get; set; }
    
    public bool IsSugarFree { get; set; }
    
    public bool IsOrganic { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, 1000.00, ErrorMessage = "Price must be greater than 0.")]
    public decimal Price { get; set; }
}