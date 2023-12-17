using GestaoDeEstoque.WebApp.Data;
using GestaoEstoque.Plugins.EFCoreSql;
using GestaoEstoque.Plugins.InMemory;
using GestaoEstoque.UseCase.Activities;
using GestaoEstoque.UseCase.Activities.InterfacesUseCase;
using GestaoEstoque.UseCase.Interfaces;
using GestaoEstoque.UseCase.Inventories;
using GestaoEstoque.UseCase.Inventories.InterfacesUseCase;
using GestaoEstoque.UseCase.Products;
using GestaoEstoque.UseCase.Products.InterfacesUseCase;
using GestaoEstoque.UseCase.Reports;
using GestaoEstoque.UseCase.Reports.InterfacesUseCase;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;




var builder = WebApplication.CreateBuilder(args);

//configuracao authorizations

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("Department", "Administrator"));
    options.AddPolicy("Inventory", policy => policy.RequireClaim("Department", "InventoryManagement"));
    options.AddPolicy("Sales", policy => policy.RequireClaim("Department", "Sales"));
    options.AddPolicy("Purchasers", policy => policy.RequireClaim("Department", "Purchasing"));
    options.AddPolicy("Productions", policy => policy.RequireClaim("Department", "ProductionManagement"));
});

var connectionString = builder.Configuration.GetConnectionString("InventoryManagement");

//Configura��o EF Core for Identity(database)
builder.Services.AddDbContext<AccountDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

//Configura��o Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<AccountDbContext>();

builder.Services.AddDbContextFactory<GestaoEstoqueContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

if (builder.Environment.IsEnvironment("TESTING"))
{
    StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

    builder.Services.AddSingleton<IInventoryRepository, InventoryRepository>();
    builder.Services.AddSingleton<IProductRepository, ProductRepository>();
    builder.Services.AddSingleton<IInventoryTransactionRepository, InventoryTransactionRepository>();
    builder.Services.AddSingleton<IProductTransactionRepository, ProductTransactionRepository>();
}
else
{
    builder.Services.AddTransient<IInventoryRepository, InventoryEFCoreRepository>();
    builder.Services.AddTransient<IProductRepository, ProductEFCoreRepository>();
    builder.Services.AddTransient<IInventoryTransactionRepository, InventoryTransactionEFCoreRepository>();
    builder.Services.AddTransient<IProductTransactionRepository, ProductTransactionEFCoreRepository>();
}


builder.Services.AddTransient<IViewInventoryByNameUseCase, ViewInventoryByNameUseCase>();
builder.Services.AddTransient<IAddInventoryUseCase, AddInventoryUseCase>();
builder.Services.AddTransient<IEditInventoryUseCase, EditInventoryUseCase>();
builder.Services.AddTransient<IViewInventoryByIdUseCase, ViewInventoryByIdUseCase>();


builder.Services.AddTransient<IPurchaseInventoryUseCase, PurchaseInventoryUseCase>();
builder.Services.AddTransient<IProduceProductUseCase, ProduceProductUseCase>();
builder.Services.AddTransient<ISellProductUseCase, SellProductUseCase>();


builder.Services.AddTransient<ISearchInventoryTransactionUseCase, SearchInventoryTransactionUseCase>();
builder.Services.AddTransient<ISearchProductTransactionUseCase, SearchProductTransactionUseCase>();



builder.Services.AddTransient<IViewProductByNameUseCase,ViewProductByNameUseCase>();
builder.Services.AddTransient<IAddProductUseCase,AddProducctUseCase>();
builder.Services.AddTransient<IViewProductByIdUseCase,ViewProductByIdUseCase>();
builder.Services.AddTransient<IEditProductUseCase,EditProductUseCase>();



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


app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
