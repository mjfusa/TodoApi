using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("https://patient-magpie-destined.ngrok-free.app");
                      });
});

// Add services to the container.

builder.Services.AddControllers();
  builder.Services.AddDbContext<TodoContext>(opt =>
      opt.UseInMemoryDatabase("TodoList"));
//  builder.Services.AddDbContext<TodoContext>(opt =>
//      opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint("v1/swagger.yaml", "TodoApi v1"));
}

app.UseHttpsRedirection();

app.UseRouting(); // Add this line
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
