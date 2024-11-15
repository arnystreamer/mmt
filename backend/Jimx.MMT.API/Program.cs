using Jimx.MMT.API;
using Jimx.MMT.API.App;
using Jimx.MMT.API.Context;
using Jimx.MMT.API.Models.Options;
using Jimx.MMT.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAuthentication(opt =>
{
	opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer();
builder.Services.ConfigureOptions<ConfigureJwtBearerOptions>();

builder.Services.AddCors();
builder.Services.ConfigureOptions<ConfigureCorsOptions>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<ConfigureSwaggerGenOptions>();

var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApiDbContext>(options =>
options.UseNpgsql(conn));

builder.Services.Configure<GeneralOptions>(
	builder.Configuration.GetSection(GeneralOptions.OptionName));

builder.Services.RegisterAllDbActionsWrappers();

builder.Services.AddSingleton<SettingsProvider>();
builder.Services.AddSingleton<KeysProvider>();

var app = builder.Build();

app.UseMiddleware<StatusCodeExceptionHandler>();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("Frontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
