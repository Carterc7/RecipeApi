using System.ComponentModel.DataAnnotations;

namespace RecipeApi.Models;

public class Recipe
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [MaxLength(1000)]
    public string? Description { get; set; }
    
    [Required]
    public string Ingredients { get; set; } = string.Empty;
    
    [Required]
    public string Instructions { get; set; } = string.Empty;
    
    public int? CookingTimeMinutes { get; set; }
    
    public int? Servings { get; set; }
    
    public string? Difficulty { get; set; }
    
    public string? Cuisine { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
} 