using Microsoft.OpenApi.Models;
using System.Reflection;
using thehayk.secureapi.Configuration;
using thehayk.secureapi.Middlewares;
using thehayk.secureapi.security.Dice;

string Version = Assembly.GetEntryAssembly().GetName().Version.ToString();

var builder = WebApplication.CreateBuilder(args);

// config
ISecureApiConfiguration config = new SecureApiConfiguration();
config.Init();
builder.Services.AddSingleton(config);

// dice dictionary init
IDiceDictionary dictionary = new DiceDictionary();
dictionary.Init(config.DictionaryFilePath, '\r', '\n', ' ', '\t');

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(Version, new OpenApiInfo()
    {
        Version = Version,
        Title = "thehayk's security API",
        Description = "",
        License = new OpenApiLicense()
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/license/mit")
        }
    });

    if (config.BasicAuthIsOn)
    {
        options.AddSecurityDefinition("basic", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "basic",
            In = ParameterLocation.Header,
            Description = "Basic Authorization header using the Bearer scheme."
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                      new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basic"
                            }
                        },
                        new string[] {}
                }
            });
    }

    // include xml documentation
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"), true);
});

builder.Services.AddSingleton(dictionary);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

// CORS
app.UseCors(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
});

app.UseMiddleware<AuthenticationMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint($"/swagger/{Version}/swagger.json", "thehayk.secureapi");
});

app.Run();
