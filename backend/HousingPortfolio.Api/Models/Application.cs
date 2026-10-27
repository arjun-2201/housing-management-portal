using System.ComponentModel.DataAnnotations;
namespace HousingPortfolio.Api.Models;
public class Application {
 public int Id { get; set; }
 [Required, StringLength(100)] public string Applicant { get; set; } = "";
 [Required, StringLength(120)] public string Property { get; set; } = "";
 [Required, StringLength(60)] public string Program { get; set; } = "";
 [Required, StringLength(30)] public string Status { get; set; } = "Pending";
 [Range(1, 1000)] public int Units { get; set; } = 1;
 public DateOnly SubmittedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
}
