using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolves.AutoFac;
using Business.Extensions;
using Core.DependencyResolves;
using Core.Extensions;
using Core.Middlewares.ExceptionMiddleware;
using Core.Utilities.IoC;
using DataAccess.Concrete.EntityFramework.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//connectionString
builder.Services.AddDbContext<PortalContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DevConnectionStrings"));
});

//Autofac modülü
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutoFacBusinessModule()));

//JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,//uygulamada izin verilecek sitelerin denetlenmesini kontrol eder(kimlerin eriþebileceðini kontrol eder)
        ValidateIssuer = true,//oluþturulan token deðerinin kimin daðýttýðýný kontrol eder
        ValidateLifetime = false,//token deðerinin süresinin kontrol eder.
        ValidateIssuerSigningKey = true,//tokenin uygulamamýza ait olup olmadýðýný kontrol eder
        //yapýlan doðrulamalarýn deðerleri
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddDependencyResolvers(new ICoreModule[]{
    new CoreModule()
});


// Define the URL and port to listen on
//builder.WebHost.UseUrls("http://0.0.0.0:8080");

//builder.Services.AddHostedService<RepatingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors();


// File server
app.MapFileServer("/task-attachments", "task-attachments");
app.MapFileServer("/profile-images", "profile-images");
app.MapFileServer("/user-pdf-attachments", "user-pdf-attachments");
app.MapFileServer("/ticket-attachments", "ticket-attachments");


app.Run();
