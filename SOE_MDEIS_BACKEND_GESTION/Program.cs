using Microsoft.EntityFrameworkCore;
using SOE_MDEIS_BACKEND_GESTION.Data;
using SOE_MDEIS_BACKEND_GESTION.Repositories.Interfaces;
using SOE_MDEIS_BACKEND_GESTION.Repositories.Implementations;
using SOE_MDEIS_BACKEND_GESTION.Services.Interfaces;
using SOE_MDEIS_BACKEND_GESTION.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DBCONTEXT
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();

// Services
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
