using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

//-----Para agregar una sesion-------
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache(); // Usar memoria para la sesión
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
//------------------------------------------------------------------------------------

//Para la autenticacion de google
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    options.ClientId = builder.Configuration.GetSection("GoogleKeys:ClientId").Value;
    options.ClientSecret = builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;
    // Solicitar acceso al perfil del usuario, incluida la imagen
    options.Scope.Add("https://www.googleapis.com/auth/userinfo.profile");

    // Guardar los tokens obtenidos
    options.SaveTokens = true;

    // Mapear los claims adicionales (opcional, si no se obtienen automáticamente)
    options.ClaimActions.MapJsonKey("picture", "picture");
});

// Add services to the container.
builder.Services.AddControllersWithViews();

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

//Para usar la sesion
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Inicio}/{id?}");

app.Run();
