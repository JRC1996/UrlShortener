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

builder.Services.AddScoped<IUrlShortenerServices,UrlShortenerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.MapPost("api/shorten", async (Url request,
    UrlShortenerService urlShortenerService,
    UrlshortenerContext dbContext,
    HttpContext httpContext) => {

    if (!Uri.TryCreate(request.Url1, UriKind.Absolute, out _)) 
    {
        return Results.BadRequest("Invalid Url");
    
    }

    var code = await urlShortenerService.UniqueCodeGenerator();
        var shortUrl = new Url
        {
            Id = request.Id,
            Url1 = request.Url1,
            Code = code,
            Shorturl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/api/{code}",
            Creationdate = DateTime.Now

        };

        dbContext.Urls.Add(shortUrl);
        await dbContext.SaveChangesAsync();

        return Results.Ok(shortUrl.Shorturl);
});

//app.MapGet(){ }


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
