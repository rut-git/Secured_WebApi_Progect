using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyFirstWebApiSite;
using NLog.Web;
using PresidentsApp.Middlewares;
using Repositories;
using Repository;
using Services;
//sing PresidentsApp.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddTransient<IUsersRepository, UsersRepository>();
builder.Services.AddTransient<IUsersServices, UsersServices>();
builder.Services.AddTransient<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddTransient<ICategoriesServices, CategoriesServices>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductsServices,ProductsServices >();
builder.Services.AddTransient<IOrdersRepository, OrdersRepository>();
builder.Services.AddTransient<IOrdersService, OrdersService>();
builder.Services.AddTransient<IRatingRepository, RatingRepository>();
builder.Services.AddTransient<IRatingService, RatingService>();


builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Host.UseNLog();
builder.Services.AddDbContext<AdoNetUsers326077351Context>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings"]));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.UseRatingMiddleware();

app.UseErrorHandlingMiddleware();

app.MapControllers();

app.Run();
