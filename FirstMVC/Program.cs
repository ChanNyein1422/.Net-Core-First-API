using Data.Model;
using Infra.Helper;
using Infra.Helper.DeptApiRequest;
using Infra.Helper.OrderApiRequest;
using Infra.Helper.UserApiRequest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();


builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation()
                 .AddJsonOptions(options =>
                 {
                     options.JsonSerializerOptions.PropertyNamingPolicy = null;
                 });

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("") });

builder.Services.AddHttpClient();
builder.Services.AddScoped<IUserApiRequest, UserApiRequest>();
builder.Services.AddScoped<IDeptApiRequest, DeptApiRequest>();
builder.Services.AddScoped<IOrderApiRequest, OrderApiRequest>();

var app = builder.Build();

//var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Order}/{action=Index}/{id?}");

app.Run();
