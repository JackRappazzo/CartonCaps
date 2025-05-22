using CartonCaps.Api.Exceptions;
using CartonCaps.Core.Services.DeferredDeepLinking;
using CartonCaps.Core.Services.DeferredLinking;
using CartonCaps.Core.Services.Referrals;
using CartonCaps.Persistence.Repositories;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(config =>
{
    config.SerializerSettings.Converters.Add(new StringEnumConverter());
});

builder.Services.AddScoped<IShortCodeGenerator, ShortCodeGenerator>();
builder.Services.AddScoped<IDeepLinkClient, MockDeepLinkClient>();

builder.Services.AddScoped<IDeferredLinkService, DeferredLinkService>();
builder.Services.AddScoped<IReferredUserService, ReferredUserService>();
builder.Services.AddScoped<IReferralLinkService, ReferralLinkService>();

builder.Services.AddScoped<IUserRepository, MockUserRepository>();
builder.Services.AddScoped<IReferralLinkRepository, MockReferralLinkRepository>();
builder.Services.AddScoped<IReferralRepository, MockReferralRepository>();

var app = builder.Build();

//Middleware
app.UseMiddleware<GlobalErrorLogger>();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//Gives the integration test a valid entry point
public partial class Program { };