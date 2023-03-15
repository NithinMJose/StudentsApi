using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DataContext>(options => 
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/Students", async (DataContext context) => await context.Students.ToListAsync());

app.MapGet("/", () => "Hello World!");

app.Run();
