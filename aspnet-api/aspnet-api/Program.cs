using aspnet_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure CORS to allow specific origins
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials());
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<SuburbService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();