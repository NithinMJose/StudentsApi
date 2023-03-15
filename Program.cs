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



app.MapGet("/Students/{id}", async (DataContext context, int id) => {
    var student = await context.Students.FindAsync(id);
    if (student == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(student);
});

app.MapPost("/Students", async (DataContext context, Student student) => {
    context.Students.Add(student);
    await context.SaveChangesAsync();
    return Results.Created($"/Students/{student.Id}", student);
});


app.MapPut("/Students/{id}", async (DataContext context, int id, Student student) => {
    if (id != student.Id)
    {
        return Results.BadRequest();
    }
    context.Entry(student).State = EntityState.Modified;
    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/Students/{id}", async (DataContext context, int id) => {
    var student = await context.Students.FindAsync(id);
    if (student == null)
    {
        return Results.NotFound();
    }
    context.Students.Remove(student);
    await context.SaveChangesAsync();
    return Results.NoContent();
});




app.Run();
