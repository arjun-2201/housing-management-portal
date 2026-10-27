using HousingPortfolio.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace HousingPortfolio.Api.Data;
public class HousingDbContext(DbContextOptions<HousingDbContext> options) : DbContext(options) {
 public DbSet<Application> Applications => Set<Application>();
 protected override void OnModelCreating(ModelBuilder builder) { builder.Entity<Application>().HasIndex(a => a.Status); builder.Entity<Application>().HasIndex(a => a.SubmittedAt); }
}
