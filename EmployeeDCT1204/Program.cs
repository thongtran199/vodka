using VodkaDataAccess;
using VodkaServices;
using VodkaServices.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using VodkaEntities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

//builder.Services.AddDbContext<ApplicationDbContext>
//    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
//);

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnectiondb4free2"))
//);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnectiondb4free2"))
);

builder.Services.AddIdentity<VodkaUser, IdentityRole>()
         .AddEntityFrameworkStores<ApplicationDbContext>()
         .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "MyIssuer",
        ValidAudience = "MyAudience",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("@VanthongSGU19092002"))
    };
});


builder.Services.Configure<IdentityOptions>(options =>
{
    // Set password requirements if needed
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    

    // Disable email confirmation requirement
    options.SignIn.RequireConfirmedEmail = false;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddScoped<IProductService, ProductServices>();
builder.Services.AddScoped<ICategoryService, CategoryServices>();
builder.Services.AddScoped<IVodkaUserService, VodkaUserService>();
builder.Services.AddScoped<IPaymentmethodService, PaymentmethodServices>();
builder.Services.AddScoped<ITransactheaderService, TransactheaderServices>();
builder.Services.AddScoped<ITransactdetailService, TransactdetailServices>();
builder.Services.AddScoped<ITaxinfoService, TaxinfoServices>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});



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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
app.UseCors("AllowAnyOrigin");
app.Run();
