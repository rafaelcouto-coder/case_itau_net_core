using CaseItau.Web.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//builder.Services.AddHttpClient("FundosAPI", client =>
//{
//    client.BaseAddress = new Uri("http://localhost:57252/api/fundo/");
//});

builder.Services.AddHttpClient<IFundosClientService, FundosClientService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:44378/api/"); // Ajuste para a URL da sua API
});

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
