using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionAvanzadaWeb.Data;
using ProyectoProgramacionAvanzadaWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ProyectoProgramacionAvanzadaWebContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProyectoProgramacionAvanzadaWebContext") ?? throw new InvalidOperationException("Connection string 'ProyectoProgramacionAvanzadaWebContext' not found.")));

builder.Services.AddScoped<CarrosApiService>();
builder.Services.AddScoped<CarrosImagenesApiService>();
builder.Services.AddScoped<CategoriaApiService>();
builder.Services.AddScoped<EstadosApiService>();
builder.Services.AddScoped<FacturacionApiService>();
builder.Services.AddScoped<GenerosApiService>();
builder.Services.AddScoped<HistorialCompraCarroApiService>();
builder.Services.AddScoped<ImagenesProductosApiService>();
builder.Services.AddScoped<MarcasCarrosApiService>();
builder.Services.AddScoped<MetodosDePagoApiService>();
builder.Services.AddScoped<ModelosCarrosApiService>();
builder.Services.AddScoped<ProductosApiService>();
builder.Services.AddScoped<ProveedoresApiService>();
builder.Services.AddScoped<RolesApiService>();
builder.Services.AddScoped<TipoCombustiblesApiService>();
builder.Services.AddScoped<TipoFinanciamientosApiService>();
builder.Services.AddScoped<TipoIdentificacionesApiService>();
builder.Services.AddScoped<TipoTransmisionesApiService>();
builder.Services.AddScoped<UsuarioApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
