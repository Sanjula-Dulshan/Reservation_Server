using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Reservation_Server.Models.Users;
using Reservation_Server.Services.Users;

var builder = WebApplication.CreateBuilder(args);

// Add user services to the container.
builder.Services.Configure<UserStoreDatabaseSettings>(
        builder.Configuration.GetSection(nameof(UserStoreDatabaseSettings)));

builder.Services.AddSingleton<IUserStoreDatabaseSettings>(sp =>
        sp.GetRequiredService<IOptions<UserStoreDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(sp =>
        new MongoClient(builder.Configuration.GetValue<string>("UserStoreDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IUserService, UserService>();

 



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "User API",
        
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
