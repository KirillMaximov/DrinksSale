var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.MapControllerRoute(name: "default", pattern: "{controller}/{action=Index}");

app.UseHttpsRedirection();

app.UseRouting();

app.MapRazorPages();

app.Run();
