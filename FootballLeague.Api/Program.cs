using FootballLeague.Infrastructure.Data;
using FootballLeague.Infrastructure.ServiceRegistration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FootballLeagueDbContext>(options =>
   options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddInfrastructure();

builder.Services.AddControllers().AddJsonOptions(options =>
{
   options.JsonSerializerOptions.PropertyNamingPolicy = null;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.MapOpenApi();
   app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

