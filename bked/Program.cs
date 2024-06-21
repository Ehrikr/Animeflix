using Microsoft.EntityFrameworkCore;
using Animeflix.Data;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure the DbContext with a connection string
builder.Services.AddDbContext<AnimeflixContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 21))));

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(
       options => options.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader()
   );
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
