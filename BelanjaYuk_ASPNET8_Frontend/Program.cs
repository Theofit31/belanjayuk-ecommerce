using BelanjaYuk_ASPNET8_Frontend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// register ApiService dan ambil base URL dari appsettings.json
builder.Services.AddHttpClient<ApiService>((sp, client) =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var baseUrl = config["ApiSettings:BaseUrl"] ?? "http://localhost:5283/api/v1/";
    client.BaseAddress = new Uri(baseUrl);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapGet("/", context =>
{
    context.Response.Redirect("/Auth/Login");
    return Task.CompletedTask;
});

app.MapRazorPages();

app.Run();
