using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;
using UrlShortener.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UrlshortenerContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUrlShortenerServices, UrlShortenerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Fix
app.MapPost("api/shorten", async (Url request,
    UrlShortenerService urlShortenerService,
    UrlshortenerContext dbContext) => {

    if (!Uri.TryCreate(request.Url1, UriKind.Absolute, out _)) 
    {
        return Results.BadRequest("Invalid Url");
    
    }     

});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
