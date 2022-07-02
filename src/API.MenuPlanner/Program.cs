using API.MenuPlanner;
using API.MenuPlanner.Database;
using API.MenuPlanner.Entities;
using API.MenuPlanner.Extensions;
using API.MenuPlanner.Helpers;
using API.MenuPlanner.Repositories;
using API.MenuPlanner.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

IConfigurationSection menuPlannerDatabaseSection = builder.Configuration.GetSection(AppSettingsModels.SectionNames.MenuPlannerDatabase);
AppSettingsModels.MenuPlannerDatabase menuPlannerDatabaseSettings = new AppSettingsModels.MenuPlannerDatabase();
menuPlannerDatabaseSection.Bind(menuPlannerDatabaseSettings);

IConfigurationSection authenticationSettingsSection = builder.Configuration.GetSection(AppSettingsModels.SectionNames.AuthenticationSettings);
AppSettingsModels.AuthenticationSettings authenticationSettings = new AppSettingsModels.AuthenticationSettings();
authenticationSettingsSection.Bind(authenticationSettings);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", builder => builder.RequireRole("Admin"));
    options.AddPolicy("Creator", builder => builder.RequireRole("Admin", "Creator"));
    options.AddPolicy("Viewer", builder => builder.RequireRole("Admin", "Creator", "Viewer"));
});
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddLogging(builder => builder.AddSeq(apiKey: "MenuPlannerAPI"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConfiguration(menuPlannerDatabaseSettings);
//builder.Services.Configure<MenuPlannerDatabaseSettings>(menuPlannerDatabaseSection);

//builder.Services.AddAuthorization();
builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();
builder.Services.AddSingleton<IRepository<Dish>, DishRepository>();
builder.Services.AddSingleton<IRepository<Recipe>, RecipeRepository>();
builder.Services.AddSingleton<IRepository<Tag>, TagRepository>();
builder.Services.AddSingleton<IRepository<User>, UserRepository>();
builder.Services.AddSingleton<DishService>();
builder.Services.AddSingleton<RecipeService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<ErrorService>();
builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "localhost",
                      policy =>
                      {
                          //policy.WithOrigins(builder.Configuration.GetSection("AllowedHosts").Value);
                          policy.AllowAnyOrigin();
                          policy.AllowAnyMethod();
                          policy.AllowAnyHeader();
                      });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.UseCors("localhost");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
