using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using QuestionLairBE.Infrastructure.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using QuestionLairBE.Services.AWS;
using QuestionLairBE.Services.CourseService;
using QuestionLairBE.Services.MaterialService;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// JWT config
var jwtKey = builder.Configuration["Jwt:Key"] ?? "this-is-your-super-secret-key";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "QuestionLairAPI";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration["Jwt:Key"]!))
    };
});


builder.Services.AddAuthorization();
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy
                .WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

// Register Service
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddScoped<S3Service>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
