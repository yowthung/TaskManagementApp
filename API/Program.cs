using MediatR;
using System.Reflection;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("http://localhost:5066") // BlazorWasm dev server origin
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("Application")));

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlite("Data Source=taskapp.db"));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? "Data Source=taskapp.db"; // fallback to SQLite
builder.Services.AddInfrastructure(connectionString);

var app = builder.Build();

// 🔹 Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorClient");
app.UseAuthorization();

app.MapControllers();

app.Run();