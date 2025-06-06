// revisado por Carlos Leandy Moreno Reyes, el Varon.
using CaseControl.Api.Helpers;
using CaseControl.Api.Interfaces;
using CaseControl.Api.Services;
using CaseControl.Api.TOKEN.Interfaces;
using CaseControl.Api.TOKEN.Services;
using CaseControl.DATA;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using CaseControl.Domain.Entities;
using Microsoft.AspNetCore.Http.Features;
using CaseControl.Api.TOKEN.DTOs; // Mantenemos este para TokenService, pero especificaremos JWTSettings

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configurar servicios al contenedor
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin", policy =>
            {
                policy.SetIsOriginAllowed(origin => true) // Allow any origin
                      .AllowAnyMethod()                    // Permite cualquier método (GET, POST, etc.)
                      .AllowAnyHeader()                    // Permite cualquier encabezado
                      .AllowCredentials();                 // Permite enviar credenciales si es necesario
            });
        });

        // Agregar servicios al contenedor
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            });

        builder.Services.AddSwaggerGen();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
        builder.Services.AddTransient<SeedDB>();
        builder.Services.AddHttpContextAccessor();

        // Configurar JWTSettings con depuración
        var jwtSettingsSection = builder.Configuration.GetSection("JWTSettings");
        var jwtSettings = jwtSettingsSection.Get<CaseControl.Api.TOKEN.DTOs.JWTSettings>(); // Especificamos el namespace completo
        if (jwtSettings == null || string.IsNullOrEmpty(jwtSettings.Key))
        {
            throw new ArgumentException("Error: No se pudo cargar la configuración JWTSettings desde appsettings.json o la clave está vacía. Verifica el archivo de configuración.");
        }
        Console.WriteLine($"JWTSettings cargado correctamente: Key={jwtSettings.Key}, Issuer={jwtSettings.Issuer}, Audience={jwtSettings.Audience}, ExpirationInMinutes={jwtSettings.ExpirationInMinutes}");
        builder.Services.Configure<CaseControl.Api.TOKEN.DTOs.JWTSettings>(jwtSettingsSection); // Especificamos el namespace completo

        // Configurar autenticación JWT
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var loadedJwtSettings = jwtSettingsSection.Get<CaseControl.Api.TOKEN.DTOs.JWTSettings>(); // Especificamos el namespace completo
                if (loadedJwtSettings == null || string.IsNullOrEmpty(loadedJwtSettings.Key))
                {
                    throw new ArgumentException("La configuración JWT no está definida o la clave secreta está vacía.");
                }

                var key = Encoding.UTF8.GetBytes(loadedJwtSettings.Key);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = loadedJwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = loadedJwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                options.RequireHttpsMetadata = false; // Para desarrollo; cámbialo a true en producción
                options.SaveToken = true;
            });

        // Registrar servicios
        builder.Services.AddScoped<ICaseStatus, CaseStatusService>();
        builder.Services.AddScoped<ICaseType, CaseTypeService>();
        builder.Services.AddScoped<IReceptionMedium, ReceptionMediumService>();
        builder.Services.AddScoped<IRecommendationStatus, RecommendationStatusService>();
        builder.Services.AddScoped<IRecommendation, RecommendationService>();
        builder.Services.AddScoped<IBinnacle, BinnacleService>();
        builder.Services.AddScoped<ICaseAssignment, CaseAssignmentService>();
        builder.Services.AddScoped<IUserLevel, UserLevelService>();
        builder.Services.AddScoped<IWorkingGroup, WorkingGroupService>();
        builder.Services.AddScoped<IRecommendationType, RecommendationTypeService>();
        builder.Services.AddScoped<ICase, CaseService>();
        builder.Services.AddScoped<IKeyExistsServices, KeyExistsServices>();
        builder.Services.AddScoped<IUser, UserService>();
        builder.Services.AddScoped<IToken, TokenService>();
        builder.Services.AddScoped<IRole, RoleService>();
        builder.Services.AddScoped<IUtil, UtilService>();
        builder.Services.AddScoped<IDbUtil, UtilService>();
        builder.Services.AddScoped<IFault, FaultService>();
        builder.Services.AddScoped<ILinked, LinkedService>();
        builder.Services.AddScoped<ILinkType, LinkTypeService>();
        builder.Services.AddScoped<IInterview, InterviewService>();
        builder.Services.AddScoped<IEvidenceClassification, EvidenceClassificationService>();
        builder.Services.AddScoped<IEvidence, EvidenceService>();
        builder.Services.AddScoped<IFaultLinked, FaultLinkedService>();
        builder.Services.AddScoped<IGerencia, GerenciaService>();

        builder.Services.AddResponseCompression(options =>
        {
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                new[] { "application/octet-stream" });
        });

        // Configurar Kestrel para usar HTTP/2
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(7188, listenOptions =>
            {
                listenOptions.UseHttps();
                listenOptions.Protocols = HttpProtocols.Http2;
            });
        });

        var app = builder.Build();

        // Método para sembrar datos
        void SeedData(WebApplication app)
        {
            IServiceScopeFactory? scopeFactory = app.Services.GetService<IServiceScopeFactory>();
            using (IServiceScope scope = scopeFactory!.CreateScope())
            {
                SeedDB? service = scope.ServiceProvider.GetService<SeedDB>();
                service!.SeedAsync().Wait();
            }
        }

        // Configurar el pipeline de HTTP
        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("AllowSpecificOrigin");

        app.Use(async (context, next) =>
        {
            if (context.Request.Method == "OPTIONS")
            {
                context.Response.StatusCode = 200;
                return;
            }
            await next();
        });

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseRouting();
        app.MapControllers();

        app.UseExceptionHandler("/Error");
        app.UseStatusCodePages();

        await app.RunAsync();
    }
}