using kanbanRespository;
using tl2_tp10_2023_vaninaze.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var CadenaDeConexion =
builder.Configuration.GetConnectionString("SqliteConexion")!.ToString();
builder.Services.AddSingleton<string>(CadenaDeConexion);

//Insercion de dependencias
builder.Services.AddScoped<ITableroRepository, TableroRepository>();
builder.Services.AddScoped<ITareaRepository, TareaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache(); //cuando agrego variables de sesion
builder.Services.AddSession(options => //cuando agrego variables de sesion
{
    options.IdleTimeout = TimeSpan.FromSeconds(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); //cuando agrego variables de sesion

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();