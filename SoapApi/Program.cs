using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using SoapApi.Infrastructure;
using SoapCore;
using SoapApi.Repositories;
using SoapApi.Contracts;
using SoapApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSoapCore();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserContract, UserService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookContract, BookService>();

//Ponerle Contraseña para pasarsela al Deployment.yml
builder.Services.AddDbContext<RelationalDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

var app = builder.Build();
app.UseSoapEndpoint<IUserContract>("/UserService.svc", new SoapEncoderOptions());
app.UseSoapEndpoint<IBookContract>("/BookService.svc", new SoapEncoderOptions());


app.Run();