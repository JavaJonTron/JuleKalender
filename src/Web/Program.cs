using MudBlazor.Services;
using Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

// Add HttpClient for API calls
builder.Services.AddHttpClient<ApiService>(client =>
{
    var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7001";
    if (!apiBaseUrl.StartsWith("http://") && !apiBaseUrl.StartsWith("https://"))
    {
        // Default to http and port 8080 for internal Render communication
        apiBaseUrl = $"http://{apiBaseUrl}:8080";
    }
    client.BaseAddress = new Uri(apiBaseUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline
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
