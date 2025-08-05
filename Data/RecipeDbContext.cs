using Microsoft.EntityFrameworkCore;
using RecipeApi.Models;

namespace RecipeApi.Data;

// DbContext is a core class in Entity Framework Core that represents a session with the database.
// It provides the main API for interacting with the database and managing entities.
public class RecipeDbContext : DbContext
{
    // DbContextOptions are configured in the Program.cs file
    public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options)
    {
    }

    // DbSet<TEntity> represents a collection of entities in the database.
    // By default, DbSet<Recipe> maps to a table named "Recipes" in the database.
    public DbSet<Recipe> Recipes { get; set; }

    // OnModelCreating is used to configure the entity mappings and relationships.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Recipe entity
        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Ingredients).IsRequired();
            entity.Property(e => e.Instructions).IsRequired();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        });
    }
} 