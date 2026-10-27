using HousingPortfolio.Api.Data;
using HousingPortfolio.Api.Models;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HousingDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("HousingDb")));
builder.Services.AddEndpointsApiExplorer();builder.Services.AddSwaggerGen();
var app = builder.Build();
if(app.Environment.IsDevelopment()){app.UseSwagger();app.UseSwaggerUI();}
app.MapGet("/api/applications", async (HousingDbContext db, string? status, string? search, CancellationToken ct) => {
 var q=db.Applications.AsNoTracking().AsQueryable();
 if(!string.IsNullOrWhiteSpace(status))q=q.Where(a=>a.Status==status);
 if(!string.IsNullOrWhiteSpace(search))q=q.Where(a=>a.Applicant.Contains(search)||a.Property.Contains(search)||a.Program.Contains(search));
 return await q.OrderByDescending(a=>a.SubmittedAt).ThenByDescending(a=>a.Id).Take(200).ToListAsync(ct);
});
app.MapGet("/api/applications/{id:int}", async (int id, HousingDbContext db, CancellationToken ct) => await db.Applications.AsNoTracking().FirstOrDefaultAsync(a=>a.Id==id,ct) is { } item ? Results.Ok(item) : Results.NotFound());
app.MapPost("/api/applications", async (Application input, HousingDbContext db, CancellationToken ct) => {
 if(string.IsNullOrWhiteSpace(input.Applicant)||string.IsNullOrWhiteSpace(input.Property)||input.Units<1||input.Units>1000) return Results.BadRequest(new {message="Valid applicant, property and unit count are required."});
 input.Id=0;input.Status="Pending";input.SubmittedAt=DateOnly.FromDateTime(DateTime.UtcNow);
 db.Applications.Add(input);await db.SaveChangesAsync(ct);return Results.Created($"/api/applications/{input.Id}",input);
});
app.MapGet("/api/summary", async (HousingDbContext db, CancellationToken ct) => new {total=await db.Applications.CountAsync(ct),approved=await db.Applications.CountAsync(a=>a.Status=="Approved",ct),pending=await db.Applications.CountAsync(a=>a.Status=="Pending",ct),underReview=await db.Applications.CountAsync(a=>a.Status=="Under Review",ct)});
app.Run();
