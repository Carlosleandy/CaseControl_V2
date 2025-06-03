using CaseControl.Api.Helpers;
using CaseControl.Api.Interfaces;
using CaseControl.Api.Services;
using CaseControl.Api.TOKEN.Interfaces;
using CaseControl.Api.TOKEN.Services;
using CaseControl.DATA;
using CaseControl.Domain.DTOs;
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

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Agregar servicios al contenedor
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin", policy =>
            {
                policy.SetIsOriginAllowed(origin => true)  // Allow any origin
                      .AllowAnyMethod()                    // Permite cualquier método (GET, POST, etc.)
                      .AllowAnyHeader()                    // Permite cualquier encabezado
                      .AllowCredentials();                 // Permite enviar credenciales si es necesario (cookies, headers, etc.)
            });
        });

        // Add services to the container.
        builder.Services.AddControllers()
            .AddJsonOptions(options => {
                // Ignorar referencias cíclicas
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                
                // Ignorar propiedades nulas para reducir el tamaño de la respuesta JSON
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                
                // Usar camelCase para los nombres de propiedades (estándar de JSON)
                options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            });

        builder.Services.AddSwaggerGen();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=DefaultConnectionString"));
        builder.Services.AddTransient<SeedDB>();
        builder.Services.AddHttpContextAccessor();

        // Configuración de JWT
        var jwtSettingsSection = builder.Configuration.GetSection("JWTSettings");
        builder.Services.Configure<JWTSettings>(jwtSettingsSection);

        // Configurar autenticación JWT
        var jwtSettings = jwtSettingsSection.Get<JWTSettings>();
        var key = Encoding.ASCII.GetBytes(jwtSettings.Key);

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                ClockSkew = TimeSpan.Zero
            };
        });

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
        builder.Services.AddScoped<IFault, FaultService>();
        builder.Services.AddScoped<ILinked, LinkedService>();
        builder.Services.AddScoped<ILinkType, LinkTypeService>();
        builder.Services.AddScoped<IInterview, InterviewService>();
        builder.Services.AddScoped<IEvidenceClassification, EvidenceClassificationService>();
        builder.Services.AddScoped<IEvidence, EvidenceService>();
        builder.Services.AddScoped<IFaultLinked, FaultLinkedService>();
        builder.Services.AddScoped<IGerencia, GerenciaService>();

        builder.Services.AddResponseCompression(o =>
        {
            o.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
               new[] { "application/octet-stream" }
               );
        });

        // Configura Kestrel para usar HTTP/2 (esto es generalmente automático si usas HTTPS)
        builder.WebHost.ConfigureKestrel(options =>
        {
            // Se puede desactivar HTTP/2 aquí si lo necesitas
            options.ListenAnyIP(7188, listenOptions =>
            {
                listenOptions.UseHttps();
                listenOptions.Protocols = HttpProtocols.Http2; // Asegurarse de que HTTP/2 está habilitado
            });
        });

        var app = builder.Build();

        // Crear usuario administrador por defecto
       

        void SeedData(WebApplication app)
        {
            IServiceScopeFactory? scopeFactory = app.Services.GetService<IServiceScopeFactory>();

            using (IServiceScope scope = scopeFactory!.CreateScope())
            {
                SeedDB? service = scope.ServiceProvider.GetService<SeedDB>();
                service!.SeedAsync().Wait();
            }
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        //else
        //{
        //    app.UseSwagger();
        //    app.UseSwaggerUI();
        //    app.Run("http://s460-aud06:5004");
        //}

        // Configurar la solicitud HTTP.
        app.UseCors("AllowSpecificOrigin");  // Habilitar CORS usando la política configurada

        // Permite explícitamente las solicitudes OPTIONS en tu aplicación
        app.Use(async (context, next) =>
        {
            if (context.Request.Method == "OPTIONS")
            {
                context.Response.StatusCode = 200;  // Responde con código 200 OK para OPTIONS
                return;  // No continúa con el pipeline, ya que no es necesario seguir procesando OPTIONS
            }

            await next();  // Continúa con el siguiente middleware si no es una solicitud OPTIONS
        });

        //app.UseResponseCompression();

        //app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseRouting();

        app.MapControllers();

        app.UseExceptionHandler("/Error");
        app.UseStatusCodePages();

        await app.RunAsync();
    }
}
