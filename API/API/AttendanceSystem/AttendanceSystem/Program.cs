using System.Text;
using AttendanceSystem.Models;
using AttendanceSystem.Utility;          // JwtHelper, JWTMiddleWare, getTokenDataFromJWT
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

//D:\MTSU SEM2\Web Tech\ref\API\AttendanceSystem\AttendanceSystem\appsettings.json
//D:\MTSU SEM2\Web Tech\ref\API\AttendanceSystem\AttendanceSystem
// Load configuration
var configuration = builder.Configuration;

#region AP code: JWT Configuration
JwtHelper.SecretKey = configuration.GetValue<string>("Jwt:Key")!;
JwtHelper.Key = Encoding.ASCII.GetBytes(JwtHelper.SecretKey);
JwtHelper.strIssuer = configuration.GetValue<string>("Jwt:Issuer")!;
JwtHelper.strAudince = configuration.GetValue<string>("Jwt:Audience")!;
#endregion

//  MySQL DbContext
builder.Services.AddDbContext<AttendanceSystemContext>(options =>
    options.UseMySql(
        configuration.GetConnectionString("DefaultConnection")!,
        ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")!)
    )
);

//  Session (if you ever use HttpContext.Session)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(opts =>
{
    opts.IdleTimeout = TimeSpan.FromMinutes(30);
    opts.Cookie.HttpOnly = true;
    opts.Cookie.IsEssential = true;
});

#region AP code: JWT Authentication & Authorization
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IgetTokenData, getTokenDataFromJWT>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = JwtHelper.strIssuer,
        ValidAudience = JwtHelper.strAudince,
        IssuerSigningKey = new SymmetricSecurityKey(JwtHelper.Key),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();
#endregion

#region AP code: Swagger + JWT in Swagger UI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AttendanceSystem API", Version = "v1" });

    // Add JWT bearer definition
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
#endregion

// MVC controllers
builder.Services.AddControllers();

// CORS for React front-end
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("AllowReactApp", p =>
        p.AllowAnyOrigin()
         .AllowAnyHeader()
         .AllowAnyMethod()
    );
});

var app = builder.Build();

// Enable Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AttendanceSystem API v1");
        // c.RoutePrefix = ""; // if you want Swagger at root "/"
    });
}

app.UseCors("AllowReactApp");

app.UseSession();

app.UseAuthentication();

// Apply your JWT middleware to *all* non-Swagger requests
app.UseWhen(
    ctx => !ctx.Request.Path.StartsWithSegments("/swagger"),
    appBuilder => appBuilder.UseMiddleware<JWTMiddleWare>()
);

app.UseAuthorization();

app.MapControllers();

app.Run();










































































//using AttendanceSystem.Models;
//using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.OpenApi.Models;
//using QuestPDF.Drawing;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<AttendanceSystemContext>(options =>
//    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),new MySqlServerVersion(new Version(8,0,41)))
//);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "Your API Title",
//        Version = "v1",
//        Description = "API Documentation for Your Application",
//    });

//    c.MapType<IFormFile>(() => new OpenApiSchema
//    {
//        Type = "string",
//        Format = "binary"
//    });

//});
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowReactApp", policy =>
//    {
//        policy.AllowAnyOrigin() // Allows requests from any origin
//              .AllowAnyHeader()
//              .AllowAnyMethod();
//    });
//});



//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//app.UseCors("AllowReactApp");
//app.UseAuthorization();

//app.MapControllers();

//app.Run();
