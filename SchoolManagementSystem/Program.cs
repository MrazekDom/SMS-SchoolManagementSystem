using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SMS.Models.Models;
using SMS.Data.Services;
using SMS.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => {        //napojeni na databazi
    options.UseSqlServer(builder.Configuration.GetConnectionString("SMSdb"));
});
builder.Services.AddIdentity<AppUser,
IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options => {        //jak dlouho vydrzi prihlaseni aktivni
    options.Cookie.Name = ".AspNetCore.Identity.Application";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    options.SlidingExpiration = true;  //reset cookie(doba vydrze prihlaseni) pri akci uzivatele na webu
});

builder.Services.ConfigureApplicationCookie(opts => opts.AccessDeniedPath = "/Account/AccessDenied");

builder.Services.AddScoped<StudentsService>();      //pridavam service pro praci s daty z databaze
builder.Services.AddScoped<SubjectsService>();
builder.Services.AddScoped<GradesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();        //autentikace, prihlaseni
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");     //NEmenim na defaultni login view, pro pripad, kdy by uzivatel zustal prihlasen z minule

app.Run();
