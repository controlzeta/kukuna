using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Models;

public partial class SqlServerDbContext : DbContext
{
    public SqlServerDbContext()
    {
    }

    public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<MealPlan> MealPlans { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; }

    public virtual DbSet<ShoppingList> ShoppingLists { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=172.17.0.1;Initial Catalog=Kukuna;Persist Security Info=True;User ID=sa;Password=kukuna@1;Trust Server Certificate=True");
    //=> optionsBuilder.UseSqlServer("Data Source=172.18.112.1;Initial Catalog=Kukuna;Persist Security Info=True;User ID=sa;Password=kukuna@1;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.IngredientId).HasName("PK__Ingredie__BEAEB27A2FC3A819");

            entity.Property(e => e.IngredientId).HasColumnName("IngredientID");
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.IngredientName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MealPlan>(entity =>
        {
            entity.HasKey(e => e.MealPlanId).HasName("PK__MealPlan__0620DB56CAE9CA86");

            entity.Property(e => e.MealPlanId).HasColumnName("MealPlanID");
            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");

            entity.HasOne(d => d.Recipe).WithMany(p => p.MealPlans)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__MealPlans__Recip__2F10007B");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__Recipes__FDD988D026B20D1F");

            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.Instructions).HasColumnType("text");
            entity.Property(e => e.RecipeName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RecipeIngredient>(entity =>
        {
            entity.HasKey(e => e.RecipeIngredientId).HasName("PK__RecipeIn__A2C34276EB6D5603");

            entity.Property(e => e.RecipeIngredientId).HasColumnName("RecipeIngredientID");
            entity.Property(e => e.IngredientId).HasColumnName("IngredientID");
            entity.Property(e => e.Quantity).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.RecipeIngredients)
                .HasForeignKey(d => d.IngredientId)
                .HasConstraintName("FK__RecipeIng__Ingre__2B3F6F97");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeIngredients)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__RecipeIng__Recip__2A4B4B5E");

            entity.HasOne(d => d.Unit).WithMany(p => p.RecipeIngredients)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK__RecipeIng__UnitI__2C3393D0");
        });

        modelBuilder.Entity<ShoppingList>(entity =>
        {
            entity.HasKey(e => e.ShoppingListId).HasName("PK__Shopping__6CBBDD7499AF5F1F");

            entity.Property(e => e.ShoppingListId).HasColumnName("ShoppingListID");
            entity.Property(e => e.IngredientId).HasColumnName("IngredientID");
            entity.Property(e => e.TotalQuantity).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.ShoppingLists)
                .HasForeignKey(d => d.IngredientId)
                .HasConstraintName("FK__ShoppingL__Ingre__31EC6D26");

            entity.HasOne(d => d.Unit).WithMany(p => p.ShoppingLists)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK__ShoppingL__UnitI__32E0915F");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("PK__Units__44F5EC95CE27F3CE");

            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.UnitName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
