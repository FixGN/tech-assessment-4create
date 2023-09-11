using _4Create.Application.Bootstrap;
using _4Create.Domain.Bootstrap;
using _4Create.Infrastructure.Bootstrap;
using _4Create.WebApi.Bootstrap;
using _4Create.WebApi.Configuration.Options;
using _4Create.WebApi.Configuration.Settings;
using _4Create.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(ExceptionFilter));
});
builder.Services.ConfigureExceptionHandlers();
builder.Services.ConfigureSwagger();

builder.Services.ConfigureMediatr();
builder.Services.ConfigureDomain();

var mySqlOptions = builder.Configuration
    .GetSection(MySqlOptions.OptionsName)
    .Get<MySqlOptions>();
var mySqlSettings = MySqlSettings.FromOptions(mySqlOptions!);
builder.Services.ConfigureMySql(mySqlSettings.ConnectionString);

var authenticationOptions = builder.Configuration
    .GetSection(AuthenticationOptions.OptionsName)
    .Get<AuthenticationOptions>();
var authenticationSettings = AuthenticationSettings.FromOptions(authenticationOptions!);
builder.Services.ConfigureAuthentication(
    authenticationSettings.Audience,
    authenticationSettings.Issuer,
    authenticationSettings.SigningKey,
    authenticationSettings.ExpirationTimeInMinutes);

builder.Services.ConfigurePasswordHashing();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
