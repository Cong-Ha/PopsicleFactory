using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class PopsicleSearchModel
{
    [MaxLength(50, ErrorMessage = "Name must be less than 50 characters.")]
    public string? Name { get; set; }
    
    [Range(1, 5, ErrorMessage = "Type is required: (Fruit, Cream, Juice, Yogurt, Sherbet, IceCream)")]
    public PopsicleType? Type { get; set; }
    
    [Range(1, 4, ErrorMessage = "Size is required: (Mini, Regular, Large, Jumbo)")]
    public PopsicleSize? Size { get; set; }
    
    public bool? IsSugarFree { get; set; }
    
    public bool? IsOrganic { get; set; }
    
    [Range(1, 1000.00, ErrorMessage = "Price must be greater than 0.99")]
    public decimal? MinPrice { get; set; }
    
    [Range(1, 1000.00, ErrorMessage = "Price must be greater than 0.99")]
    public decimal? MaxPrice { get; set; }
}