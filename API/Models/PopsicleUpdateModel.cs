using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class PopsicleUpdateModel
{
    [MaxLength(50, ErrorMessage = "Name must be less than 50 characters.")]
    [MinLength(3, ErrorMessage = "Name must have at least 3 characters.")]
    public string? Name { get; set; }
    
    public PopsicleType? Type { get; set; }
    
    public PopsicleSize? Size { get; set; }

    [MaxLength(500,  ErrorMessage = "Description must be less than 500 characters.")]
    public string? Description { get; set; }
    
    public bool? IsSugarFree { get; set; }
    
    public bool? IsOrganic { get; set; }
    
    [Range(1, 1000.00, ErrorMessage = "Price must be greater than 0.99")]
    public decimal? Price { get; set; }
}