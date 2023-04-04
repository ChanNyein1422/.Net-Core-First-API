using Data.Model;
using Microsoft.EntityFrameworkCore;
using FirstAPI.Services.UserService;
using FirstAPI.Services.DeptService;
using FirstAPI.Services.OrderService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:7049" //mvc local route
                            )
                          .AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                      });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers()
                     .AddJsonOptions(options =>
                     {
                         // Use the default property (Pascal) casing.
                         options.JsonSerializerOptions.PropertyNamingPolicy = null;
                     });


var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseSqlServer("Data Source=DESKTOP-ADBOKI3;Initial Catalog=TestingDB;User ID=root;Password=root;MultipleActiveResultSets=True;TrustServerCertificate=True")
             .Options;

builder.Services.AddScoped<ApplicationDbContext>(s => new ApplicationDbContext(contextOptions));


builder.Services.AddScoped<IUser>(s => new UserBase(
                                        s.GetService<ApplicationDbContext>()

                                    ));


builder.Services.AddScoped<IDept>(s => new DeptBase(
                                        s.GetService<ApplicationDbContext>()

                                    ));

builder.Services.AddScoped<IOrder>(s => new OrderBase(
                                        s.GetService<ApplicationDbContext>()

                                    ));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
