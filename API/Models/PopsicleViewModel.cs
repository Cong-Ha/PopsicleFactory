namespace API.Models;

public class PopsicleViewModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    
    public PopsicleType Type { get; set; }
    
    public PopsicleSize Size { get; set; }
    
    public string? Description { get; set; }
    
    public bool IsSugarFree { get; set; }
    
    public bool IsOrganic { get; set; }
    
    public decimal Price { get; set; }
}