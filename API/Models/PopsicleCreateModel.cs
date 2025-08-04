using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace API.Models;

public class PopsicleCreateModel
{
    [Required(ErrorMessage = "Name is required: atleast 3 characters, maximum 50 characters.")]
    [MaxLength(50, ErrorMessage = "Name must be less than 50 characters.")]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters.")]
    public string Name { get; set; }
    
    [Required]
    [Range(1, 5, ErrorMessage = "Type is required: (Fruit, Cream, Juice, Yogurt, Sherbet, IceCream)")]
    public PopsicleType Type { get; set; }
    
    [Required]
    [Range(1, 4, ErrorMessage = "Size is required: (Mini, Regular, Large, Jumbo)")]
    public PopsicleSize Size { get; set; }

    [MaxLength(500,  ErrorMessage = "Description must be less than 500 characters.")]
    public string? Description { get; set; }
    
    public bool IsSugarFree { get; set; }
    
    public bool IsOrganic { get; set; }

    [Required(ErrorMessage = "Price is required: Must be greater than 0.99")]
    [Range(1, 1000.00, ErrorMessage = "Price must be greater than 0.99")]
    public decimal Price { get; set; }
}