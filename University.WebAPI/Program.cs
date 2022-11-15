using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PIS.DAL;
using PIS.DAL.Models;
using PIS.DAL.Repositories;
using System.Text;
using University.Application.Services;
using University.Application.Services.Abstract;
using University.WebAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region Jwt Settings
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue("JwtSettings:Secret", string.Empty));

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
#endregion

builder.Services.AddDbContext<ApplicationDbContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddOptions();
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

//Inject services
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IBaseService<Worker>, WorkerService>();
builder.Services.AddScoped<IBaseService<Workplace>, WorkplaceService>();
builder.Services.AddScoped<IBaseService<Job>, JobService>();
builder.Services.AddScoped<IJwtUserService, UserService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();