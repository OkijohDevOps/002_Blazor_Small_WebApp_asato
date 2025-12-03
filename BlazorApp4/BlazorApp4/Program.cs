using BlazorApp4.Components;
using BlazorApp4.Data;
using Microsoft.EntityFrameworkCore;

// ★★★ using の後、builder の前に書く ★★★
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//7-3ここから
builder.Services.AddDbContextFactory<TestDbContext>(opt => {
    if (builder.Environment.IsDevelopment())
    {
        opt = opt.EnableSensitiveDataLogging().EnableDetailedErrors();
    }


    opt.UseNpgsql(
        builder.Configuration.GetConnectionString("PubsDbContext"),
        providerOptions =>
        {
            providerOptions.EnableRetryOnFailure();
        });
});
//7-3ここまで

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//物理サーバWeb化の後に、ここを修正したよ
//元はapp.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

