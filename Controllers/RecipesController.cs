using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApi.Data;
using RecipeApi.DTOs;
using RecipeApi.Models;

namespace RecipeApi.Controllers;

/// <summary>
/// Controller for managing recipe operations (CRUD)
/// Handles HTTP requests for creating, reading, updating, and deleting recipes
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly RecipeDbContext _context;

    /// <summary>
    /// Constructor that receives the database context via dependency injection
    /// </summary>
    /// <param name="context">The database context for recipe operations</param>
    public RecipesController(RecipeDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// GET: api/recipes
    /// Retrieves all recipes from the database
    /// </summary>
    /// <returns>List of all recipes converted to DTOs</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecipeResponseDto>>> GetRecipes()
    {
        // Query all recipes from the database asynchronously
        var recipes = await _context.Recipes.ToListAsync();
        
        // Convert Recipe entities to RecipeResponseDto objects
        var recipeDtos = recipes.Select(recipe => new RecipeResponseDto
        {
            Id = recipe.Id,
            Title = recipe.Title,
            Description = recipe.Description,
            Ingredients = recipe.Ingredients,
            Instructions = recipe.Instructions,
            CookingTimeMinutes = recipe.CookingTimeMinutes,
            Servings = recipe.Servings,
            Difficulty = recipe.Difficulty,
            Cuisine = recipe.Cuisine,
            CreatedAt = recipe.CreatedAt,
            UpdatedAt = recipe.UpdatedAt
        });

        return Ok(recipeDtos);
    }

    /// <summary>
    /// GET: api/recipes/{id}
    /// Retrieves a specific recipe by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the recipe</param>
    /// <returns>The recipe if found, or NotFound (404) if not found</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<RecipeResponseDto>> GetRecipe(int id)
    {
        // Find the recipe by ID asynchronously
        var recipe = await _context.Recipes.FindAsync(id);
        
        // Return 404 Not Found if recipe doesn't exist
        if (recipe == null)
        {
            return NotFound($"Recipe with ID {id} not found");
        }

        // Convert Recipe entity to RecipeResponseDto
        var recipeDto = new RecipeResponseDto
        {
            Id = recipe.Id,
            Title = recipe.Title,
            Description = recipe.Description,
            Ingredients = recipe.Ingredients,
            Instructions = recipe.Instructions,
            CookingTimeMinutes = recipe.CookingTimeMinutes,
            Servings = recipe.Servings,
            Difficulty = recipe.Difficulty,
            Cuisine = recipe.Cuisine,
            CreatedAt = recipe.CreatedAt,
            UpdatedAt = recipe.UpdatedAt
        };

        return Ok(recipeDto);
    }

    /// <summary>
    /// POST: api/recipes
    /// Creates a new recipe in the database
    /// </summary>
    /// <param name="createRecipeDto">The recipe data to create</param>
    /// <returns>The created recipe with generated ID and timestamps</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/recipes
    ///     {
    ///       "title": "Authentic Street Tacos",
    ///       "description": "Classic Mexican street tacos with marinated carne asada",
    ///       "ingredients": "1 lb flank steak, 1/4 cup olive oil, 2 limes, 3 cloves garlic",
    ///       "instructions": "1. Mix marinade ingredients\n2. Marinate steak for 2-4 hours\n3. Grill steak",
    ///       "cookingTimeMinutes": 30,
    ///       "servings": 4,
    ///       "difficulty": "Medium",
    ///       "cuisine": "Mexican"
    ///     }
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(RecipeResponseDto), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<RecipeResponseDto>> CreateRecipe(CreateRecipeDto createRecipeDto)
    {
        // Create a new Recipe entity from the DTO
        var recipe = new Recipe
        {
            Title = createRecipeDto.Title,
            Description = createRecipeDto.Description,
            Ingredients = createRecipeDto.Ingredients,
            Instructions = createRecipeDto.Instructions,
            CookingTimeMinutes = createRecipeDto.CookingTimeMinutes,
            Servings = createRecipeDto.Servings,
            Difficulty = createRecipeDto.Difficulty,
            Cuisine = createRecipeDto.Cuisine,
            CreatedAt = DateTime.UtcNow // Set creation timestamp
        };

        // Add the recipe to the database context
        _context.Recipes.Add(recipe);
        
        // Save changes to the database asynchronously
        await _context.SaveChangesAsync();

        // Convert the created recipe back to DTO for response
        var recipeDto = new RecipeResponseDto
        {
            Id = recipe.Id,
            Title = recipe.Title,
            Description = recipe.Description,
            Ingredients = recipe.Ingredients,
            Instructions = recipe.Instructions,
            CookingTimeMinutes = recipe.CookingTimeMinutes,
            Servings = recipe.Servings,
            Difficulty = recipe.Difficulty,
            Cuisine = recipe.Cuisine,
            CreatedAt = recipe.CreatedAt,
            UpdatedAt = recipe.UpdatedAt
        };

        // Return 201 Created with the new recipe data
        return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipeDto);
    }

    /// <summary>
    /// PUT: api/recipes/{id}
    /// Updates an existing recipe in the database
    /// </summary>
    /// <param name="id">The unique identifier of the recipe to update</param>
    /// <param name="updateRecipeDto">The updated recipe data</param>
    /// <returns>No content (204) on success, or NotFound (404) if recipe doesn't exist</returns>
    /// <remarks>
    /// Sample request (partial update - only provided fields will be updated):
    /// 
    ///     PUT /api/recipes/1
    ///     {
    ///       "title": "Updated Recipe Title",
    ///       "servings": 6,
    ///       "difficulty": "Easy"
    ///     }
    ///     
    /// All fields are optional. Only the fields you want to update need to be included.
    /// </remarks>
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateRecipe(int id, UpdateRecipeDto updateRecipeDto)
    {
        // Find the recipe by ID asynchronously
        var recipe = await _context.Recipes.FindAsync(id);
        
        // Return 404 Not Found if recipe doesn't exist
        if (recipe == null)
        {
            return NotFound($"Recipe with ID {id} not found");
        }

        // Update only the properties that are provided in the DTO
        // This allows partial updates (only update fields that are sent)
        if (updateRecipeDto.Title != null)
            recipe.Title = updateRecipeDto.Title;
        
        if (updateRecipeDto.Description != null)
            recipe.Description = updateRecipeDto.Description;
        
        if (updateRecipeDto.Ingredients != null)
            recipe.Ingredients = updateRecipeDto.Ingredients;
        
        if (updateRecipeDto.Instructions != null)
            recipe.Instructions = updateRecipeDto.Instructions;
        
        if (updateRecipeDto.CookingTimeMinutes.HasValue)
            recipe.CookingTimeMinutes = updateRecipeDto.CookingTimeMinutes;
        
        if (updateRecipeDto.Servings.HasValue)
            recipe.Servings = updateRecipeDto.Servings;
        
        if (updateRecipeDto.Difficulty != null)
            recipe.Difficulty = updateRecipeDto.Difficulty;
        
        if (updateRecipeDto.Cuisine != null)
            recipe.Cuisine = updateRecipeDto.Cuisine;

        // Set the update timestamp
        recipe.UpdatedAt = DateTime.UtcNow;

        // Mark the entity as modified and save changes
        _context.Entry(recipe).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        // Return 204 No Content (successful update, no response body needed)
        return NoContent();
    }

    /// <summary>
    /// DELETE: api/recipes/{id}
    /// Deletes a recipe from the database
    /// </summary>
    /// <param name="id">The unique identifier of the recipe to delete</param>
    /// <returns>No content (204) on success, or NotFound (404) if recipe doesn't exist</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecipe(int id)
    {
        // Find the recipe by ID asynchronously
        var recipe = await _context.Recipes.FindAsync(id);
        
        // Return 404 Not Found if recipe doesn't exist
        if (recipe == null)
        {
            return NotFound($"Recipe with ID {id} not found");
        }

        // Remove the recipe from the database context
        _context.Recipes.Remove(recipe);
        
        // Save changes to the database asynchronously
        await _context.SaveChangesAsync();

        // Return 204 No Content (successful deletion, no response body needed)
        return NoContent();
    }
}