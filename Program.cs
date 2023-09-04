using System.Text;
using CostMSWebAPI;
using CostMSWebAPI.DALs;
using CostMSWebAPI.DALs.Impls;
using CostMSWebAPI.Data;
using CostMSWebAPI.Exceptions;
using CostMSWebAPI.Services;
using CostMSWebAPI.Services.Impls;
using CostMSWebAPI.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(
        builder.Configuration.GetConnectionString("ApplicationDefault") ?? "",
        opt =>opt.EnableRetryOnFailure()
        ));
builder.Services.AddDbContext<AuthenticationDbContext>(options =>
    options.UseMySQL(
        builder.Configuration.GetConnectionString("AuthenticationDefault") ?? "",
        opt => opt.EnableRetryOnFailure()
    ));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthenticationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"] ?? ""))
        };
    });

// builder.Services.AddTransient(_ =>
//     new MySqlConnection(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevPolicy", (corsBuilder) =>
    {
        corsBuilder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Authorization", "Test")
            .AllowCredentials();
    });
    options.AddPolicy("ProdPolicy", (corsBuilder) =>
    {
        corsBuilder.WithOrigins("https://costms.github.io")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Authorization")
            .AllowCredentials();
    });
});

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IFundsRepository, FundsRepository>();
builder.Services.AddScoped<IIndicatorRepository, IndicatorRepository>();
builder.Services.AddScoped<IMeterRepository, MeterRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ITariffRepository, TariffRepository>();
builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFundsService, FundsService>();
builder.Services.AddScoped<IIndicatorService, IndicatorService>();
builder.Services.AddScoped<IMeterService, MeterService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ITariffService, TariffService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevPolicy");
}
else
{
    app.UseCors("ProdPolicy");
    app.UseHttpsRedirection();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.UseJwtPropagation();
app.UseErrorHandler();

app.MapControllers();

app.Run();

