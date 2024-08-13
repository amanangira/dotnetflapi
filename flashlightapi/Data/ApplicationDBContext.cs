
using System.Text.RegularExpressions;
using flashlightapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace flashlightapi.Data;

public class ApplicationDBContext : DbContext 
{
    public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        
    }
    
    // private string ConvertToSnakeCase(string name)
    // {
    //     if (string.IsNullOrEmpty(name)) return name;
    //
    //     // Use regex to convert PascalCase to snake_case
    //     return Regex.Replace(name, @"([a-z])([A-Z])", "$1_$2").ToLower();
    // }
    //
    // // TODO - Fix me 
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     foreach (var entity in modelBuilder.Model.GetEntityTypes())
    //     {
    //         var tableName = ConvertToSnakeCase(entity.ClrType.Name);
    //         entity.SetTableName(tableName);
    //
    //         foreach (var property in entity.GetProperties())
    //         {
    //             var columnName = ConvertToSnakeCase(property.Name);
    //             property.SetColumnName(columnName);
    //         }
    //     }
    //
    //     base.OnModelCreating(modelBuilder);
    // }
    
    public DbSet<User> User { get; set; }
    
    public DbSet<Assignment> Assignment { get; set; }
}