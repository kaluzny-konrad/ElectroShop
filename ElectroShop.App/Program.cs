using ElectroShop.App.Helpers;
using ElectroShop.App.Services;
using static ElectroShop.gRPC.WishlistService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient<IProductService, ProductService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7208/");
});

builder.Services.AddHttpClient<IManufacturerService, ManufacturerService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7208/");
});

builder.Services.AddHttpClient<IProductDescriptionService, ProductDescriptionService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7208/");
});

builder.Services.AddHttpClient<IBasketService, BasketService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7294/");
});

builder.Services.AddTransient<IWishlistService, WishlistService>();

builder.Services.AddGrpcClient<WishlistServiceClient>(client =>
{
    client.Address = new Uri("https://localhost:7036");
});

builder.Services.AddSingleton<IJsonSerializeHelper, JsonSerializeHelper>();

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

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
