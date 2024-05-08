using Asp.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentsManagerMW.EFCore;
using StudentsManagerMW.Extensions;
using StudentsManagerMW.Interfaces;
using StudentsManagerMW.Middlewares;
using StudentsManagerMW.Repositories;
using StudentsManagerMW.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


// test workflow

// Add services to the container.
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<UserService>(); // Registers UserService with scoped lifetime

// For adding IDentity to DI container
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<AppDBContext>();

// Add EF Core services to the container
builder.Services.AddDbContext<AppDBContext>(options =>
        options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Define API versions
//builder.Services.AddApiVersioning(options =>
//{
//    options.ReportApiVersions = true;
//    options.AssumeDefaultVersionWhenUnspecified = true;
//    options.DefaultApiVersion = new ApiVersion(1);
//    options.ApiVersionReader = ApiVersionReader.Combine(
//        new UrlSegmentApiVersionReader(),
//        new HeaderApiVersionReader("X-Api-Version"));
//});
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
}); ;
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "StudentsManagerAPI v1", Version = "v1" });
    c.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "StudentsManagerAPI v2", Version = "v2" });
});
builder.Services.AddLog4net();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Explore v1
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentsManagerAPI v1");

        // Explore v2
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "StudentsManagerAPI v2");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapIdentityApi<IdentityUser>();
app.MapControllers();
//  .RequireAuthorization();
app.UseMiddleware<ApiResponseMiddleware>();

app.Run();
