
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
options.AddDefaultPolicy(policy =>
{
	policy.AllowAnyHeader();
	policy.AllowAnyMethod();
	policy.AllowCredentials();
}));

builder.Services.AddSignalR();

var app = builder.Build();

app.UseCors();
app.UseRouting();

// ############## ROUTES #############
app.MapHub<SnakHub>("/snakhub");
// ###################################

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

app.Run();