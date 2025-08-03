using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class PopsicleSearchModel
{
    [MaxLength(50, ErrorMessage = "Name must be less than 50 characters.")]
    public string? Name { get; set; }
    
    public PopsicleType? Type { get; set; }
    
    public PopsicleSize? Size { get; set; }
    
    public bool? IsSugarFree { get; set; }
    
    public bool? IsOrganic { get; set; }
    
    [Range(0.01, 1000.00, ErrorMessage = "Price must be greater than 0.")]
    public decimal? MinPrice { get; set; }
    
    [Range(0.01, 1000.00, ErrorMessage = "Price must be greater than 0.")]
    public decimal? MaxPrice { get; set; }
}