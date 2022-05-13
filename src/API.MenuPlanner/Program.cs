using API.MenuPlanner.Models;
using API.MenuPlanner.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MenuPlannerDatabaseSettings>(
    builder.Configuration.GetSection("MenuPlannerDatabase"));


builder.Services.AddSingleton<DishService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "localhost",
                      policy =>
                      {
                          policy.WithOrigins(builder.Configuration.GetSection("AllowedHosts").Value);
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors("localhost");
app.UseAuthorization();

app.MapControllers();

app.Run();
