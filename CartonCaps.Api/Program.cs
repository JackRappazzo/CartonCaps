using CartonCaps.Core.Services.DeferredDeepLinking;
using CartonCaps.Core.Services.DeferredLinking;
using CartonCaps.Core.Services.Referrals;
using CartonCaps.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IDeferredLinkService, DeferredLinkService>();
builder.Services.AddScoped<IShortCodeGenerator, ShortCodeGenerator>();
builder.Services.AddScoped<IDeepLinkClient, MockDeepLinkClient>();
builder.Services.AddScoped<IReferralRepository, ReferralRepository>();
builder.Services.AddScoped<IReferredUserService, ReferredUserService>();

var app = builder.Build();



// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
