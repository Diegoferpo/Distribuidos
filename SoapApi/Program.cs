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

builder.Services.AddDbContext<RelationalDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
app.UseSoapEndpoint<IUserContract>("/UserService.svc", new SoapEncoderOptions());

app.Run();