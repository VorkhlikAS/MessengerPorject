using MessengerNetSix.Hubs;
using MessengerNetSix.Models;
using MessengerNetSix.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

/*services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>();*/
builder.Services.AddIdentity<User, IdentityRole>(opts => {
    opts.Password.RequiredLength = 5;   // минимальная длина
    opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
    opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
    opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
    opts.Password.RequireDigit = false; // требуются ли цифры
}).AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddTransient<IContacts, ContactsCollections>();
builder.Services.AddTransient<IChatMember, ChatMemberCollections>();
builder.Services.AddTransient<IMessages, MessagesCollection>();

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddServerSideBlazor();

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

app.UseAuthentication();    // подключение аутентификации
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapBlazorHub();
app.MapHub<MessengerHub>("/messengerhub");
app.Run();
