using System.ComponentModel.DataAnnotations;

namespace RecipeApi.DTOs;

/// <summary>
/// Data transfer object for creating a new recipe
/// </summary>
public class CreateRecipeDto
{
    /// <summary>
    /// The title of the recipe (required, max 200 characters)
    /// </summary>
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Optional description of the recipe (max 1000 characters)
    /// </summary>
    [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string? Description { get; set; }
    
    /// <summary>
    /// List of ingredients needed for the recipe (required)
    /// </summary>
    [Required(ErrorMessage = "Ingredients are required")]
    public string Ingredients { get; set; } = string.Empty;
    
    /// <summary>
    /// Step-by-step cooking instructions (required)
    /// </summary>
    [Required(ErrorMessage = "Instructions are required")]
    public string Instructions { get; set; } = string.Empty;
    
    /// <summary>
    /// Cooking time in minutes (optional)
    /// </summary>
    [Range(1, 1440, ErrorMessage = "Cooking time must be between 1 and 1440 minutes")]
    public int? CookingTimeMinutes { get; set; }
    
    /// <summary>
    /// Number of servings the recipe makes (optional)
    /// </summary>
    [Range(1, 100, ErrorMessage = "Servings must be between 1 and 100")]
    public int? Servings { get; set; }
    
    /// <summary>
    /// Difficulty level (optional): Easy, Medium, Hard
    /// </summary>
    [RegularExpression("^(Easy|Medium|Hard)$", ErrorMessage = "Difficulty must be Easy, Medium, or Hard")]
    public string? Difficulty { get; set; }
    
    /// <summary>
    /// Type of cuisine (optional): Italian, Mexican, Chinese, etc.
    /// </summary>
    [MaxLength(50, ErrorMessage = "Cuisine cannot exceed 50 characters")]
    public string? Cuisine { get; set; }
}

/// <summary>
/// Data transfer object for updating an existing recipe
/// All fields are optional - only provided fields will be updated
/// </summary>
public class UpdateRecipeDto
{
    /// <summary>
    /// The title of the recipe (optional, max 200 characters)
    /// </summary>
    [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
    public string? Title { get; set; }
    
    /// <summary>
    /// Optional description of the recipe (max 1000 characters)
    /// </summary>
    [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string? Description { get; set; }
    
    /// <summary>
    /// List of ingredients needed for the recipe (optional)
    /// </summary>
    public string? Ingredients { get; set; }
    
    /// <summary>
    /// Step-by-step cooking instructions (optional)
    /// </summary>
    public string? Instructions { get; set; }
    
    /// <summary>
    /// Cooking time in minutes (optional)
    /// </summary>
    [Range(1, 1440, ErrorMessage = "Cooking time must be between 1 and 1440 minutes")]
    public int? CookingTimeMinutes { get; set; }
    
    /// <summary>
    /// Number of servings the recipe makes (optional)
    /// </summary>
    [Range(1, 100, ErrorMessage = "Servings must be between 1 and 100")]
    public int? Servings { get; set; }
    
    /// <summary>
    /// Difficulty level (optional): Easy, Medium, Hard
    /// </summary>
    [RegularExpression("^(Easy|Medium|Hard)$", ErrorMessage = "Difficulty must be Easy, Medium, or Hard")]
    public string? Difficulty { get; set; }
    
    /// <summary>
    /// Type of cuisine (optional): Italian, Mexican, Chinese, etc.
    /// </summary>
    [MaxLength(50, ErrorMessage = "Cuisine cannot exceed 50 characters")]
    public string? Cuisine { get; set; }
}

/// <summary>
/// Data transfer object for recipe responses
/// Contains all recipe information including system-generated fields
/// </summary>
public class RecipeResponseDto
{
    /// <summary>
    /// Unique identifier for the recipe
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The title of the recipe
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Optional description of the recipe
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// List of ingredients needed for the recipe
    /// </summary>
    public string Ingredients { get; set; } = string.Empty;
    
    /// <summary>
    /// Step-by-step cooking instructions
    /// </summary>
    public string Instructions { get; set; } = string.Empty;
    
    /// <summary>
    /// Cooking time in minutes
    /// </summary>
    public int? CookingTimeMinutes { get; set; }
    
    /// <summary>
    /// Number of servings the recipe makes
    /// </summary>
    public int? Servings { get; set; }
    
    /// <summary>
    /// Difficulty level: Easy, Medium, or Hard
    /// </summary>
    public string? Difficulty { get; set; }
    
    /// <summary>
    /// Type of cuisine
    /// </summary>
    public string? Cuisine { get; set; }
    
    /// <summary>
    /// When the recipe was created
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// When the recipe was last updated (null if never updated)
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
} 