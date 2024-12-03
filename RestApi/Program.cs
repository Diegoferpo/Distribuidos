using MongoDB.Driver;
using RespApi.Repositories;
using RestApi.Repositories;
using RestApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMongoClient, MongoClient>(s => new MongoClient(builder.Configuration.GetValue<string>("MongoDB:Groups:ConnectionString")));


builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IMongoClient, MongoClient>(s => 
{
    var connectionString = Environment.GetEnvironmentVariable("MongoDb_Groups_ConnectionString");
    var settings = MongoClientSettings.FromConnectionString(connectionString);
    var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

    if (!string.IsNullOrEmpty(password))
    {
        settings.Credential = MongoCredential.CreateCredential(
            password
        );
    }

    return new MongoClient(settings);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
