using BlazorAppU.Components;
using BlazorAppU.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Añadir el servicio HttpClient al contenedor de dependencias
builder.Services.AddHttpClient();

builder.Services.AddControllers();
//builder.Services.AddOpenApi();

// Añadir tu servicio personalizado de usuario
builder.Services.AddScoped<UserService>();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://apiventas20251104155920-e6fbexc8dnahbbat.mexicocentral-01.azurewebsites.net/") });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

//app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

app.Run();
