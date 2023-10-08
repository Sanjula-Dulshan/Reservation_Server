using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Reservation_Server.Database;
using Reservation_Server.Services.Inquiries;
using Reservation_Server.Services.Reservations;
using Reservation_Server.Services.Routes;
using Reservation_Server.Services.TrainService;
using Reservation_Server.Services.Users;

var builder = WebApplication.CreateBuilder(args);


// Add user services to the container.
builder.Services.Configure<DatabaseSettings>(
        builder.Configuration.GetSection(nameof(DatabaseSettings)));

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
        sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(sp =>
        new MongoClient(builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITrainService, TrainService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ITrainRouteService, TrainRouteService>();
builder.Services.AddScoped<IInquiryService, InquiryService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Train Reservation API",
        
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
