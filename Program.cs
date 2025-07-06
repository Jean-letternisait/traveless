using TravelessSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Register the backend services as singletons
builder.Services.AddSingleton<FlightManager>();
builder.Services.AddSingleton<ReservationManager>();

// Add Razor Pages and Blazor Server services
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure middleware and endpoints
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
