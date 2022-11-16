using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using University.DAL;
using University.DAL.Repositories;
using System.Text;
using University.Application.MappingProfiles;
using University.Application.Services;
using University.Application.Services.Abstract;
using University.Application.Configurations;
using University.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging(config => 
{
    config.ClearProviders();
    config.AddConsole();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

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

//Inject services
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IJwtSettings, JwtSettings>();
builder.Services.AddScoped<IWorkerService, WorkerService>();
builder.Services.AddScoped<IJwtUserService, UserService>();

builder.Services.AddAutoMapper(typeof(Program), typeof(MapperProfile));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();