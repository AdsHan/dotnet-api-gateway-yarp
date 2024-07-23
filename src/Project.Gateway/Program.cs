using Project.Gateway.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddAuthenticationConfiguration();
builder.Services.AddRateLimitConfiguration();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.UseApiConfiguration();
app.UseAuthenticationConfiguration();
app.UseRateLimitConfiguration();
app.UseSwaggerConfiguration();

app.Run();