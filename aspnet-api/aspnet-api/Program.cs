using aspnet_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure CORS to allow specific origins
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000/")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<SuburbService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("AllowSpecificOrigin"); // This should come before UseRouting/MapControllers and UseAuthorization

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();