using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<NoteDb>(
    opt =>
    {
        opt.UseSqlite("Data Source=myDB.db");
        opt.UseSqlite(
           builder.Configuration.GetConnectionString("DefaultConnection")
          );
    });
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllers();

var app = builder.Build();
var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(
             Path.Combine(Directory.GetCurrentDirectory(), "assets")),
    RequestPath = "/static",
    EnableDefaultFiles = true
});

app.MapGet("/", () => "Hello World!");


app.MapControllerRoute(
name: "note",
pattern: "{controller=Home}/{action=Index}/{id?}"
);
app.Run();
