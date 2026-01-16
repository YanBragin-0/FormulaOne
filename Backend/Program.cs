using Elastic.Clients.Elasticsearch;
using FluentValidation;
using FluentValidation.AspNetCore;
using FormulaOne.Application.Identitiy;
using FormulaOne.Application.Identity;
using FormulaOne.Application.RepoAbstractions.CarsRepo;
using FormulaOne.Application.RepoAbstractions.CircuitRepo;
using FormulaOne.Application.RepoAbstractions.ConstructorsCShRepo;
using FormulaOne.Application.RepoAbstractions.DriverCShRepo;
using FormulaOne.Application.RepoAbstractions.DriversRepo;
using FormulaOne.Application.RepoAbstractions.RaceRepo;
using FormulaOne.Application.RepoAbstractions.SeasonRepo;
using FormulaOne.Application.RepoAbstractions.TeamsRepo;
using FormulaOne.Application.Services.Abstractions.ReadInterfaces;
using FormulaOne.Application.Services.Abstractions.WriteInterfaces;
using FormulaOne.Application.Services.ElasticSearch;
using FormulaOne.Application.Services.ReadServices;
using FormulaOne.Application.Services.WriteServices;
using FormulaOne.Application.SharedAbstractions;
using FormulaOne.Application.Validators;
using FormulaOne.Entities;
using FormulaOne.Enums;
using FormulaOne.Infrastructure.Repositories.Read;
using FormulaOne.Infrastructure.Repositories.Write;
using FormulaOne.Infrastructure.Storage;
using FormulaOne.Infrastructure.Storage.Redis;
using FormulaOne.Infrastructure.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using StackExchange.Redis;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            bool log = builder.Environment.IsDevelopment();
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                 .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                                 .AddEnvironmentVariables();

            //seq 
            var seqUrl = builder.Configuration["Serilog:SeqServerUrl"];
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Seq(seqUrl!)
                .CreateLogger();
            builder.Host.UseSerilog();
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy => 
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
            var elasticUrl = builder.Configuration["Elastic:Url"];
            builder.Services.AddSingleton<ElasticsearchClient>(e =>
            {
                var settings = new ElasticsearchClientSettings(new Uri(elasticUrl!)).DefaultIndex("global");
                return new ElasticsearchClient(settings);
            });
            builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options => 
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;

                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/auth/login";
                options.AccessDeniedPath = "/auth/access-denied";

                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.SlidingExpiration = true;
 
                options.Events.OnRedirectToLogin = context =>
                {
                    if (context.Request.Path.StartsWithSegments("/api"))
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    }
                    else
                    {
                        context.Response.Redirect(context.RedirectUri);
                    }
                    return Task.CompletedTask;
                };
            });

            //redis
            builder.Services.AddSingleton<IConnectionMultiplexer>(r =>
            {
                var rc = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(rc!);
            });



            builder.Services.AddControllers();
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<CarRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CircuitRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<ConstrCShRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<DriverCShRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<DriverRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<RaceRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<RegistrationValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<SeasonRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<TeamRequestValidator>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FormulaOne",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "¬ведите: Bearer {токен}"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            builder.Configuration.AddUserSecrets<Program>();
            if (!Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER")?.Equals("true") ?? true)
            {
                builder.Configuration.AddUserSecrets<Program>();
            }

            //auth
            builder.Services.AddScoped<IAuthService,AuthService>();
            builder.Services.AddScoped<IPermissionProvider,PermissionProvider>();
            builder.Services.AddScoped<IClaimsTransformation,PermissionClaimsTransformation>();
            //elastic search
            builder.Services.AddScoped<ISearchService, SearchService>();
            builder.Services.AddScoped<AdminSearchService>();
            //read repo
            builder.Services.AddScoped<ICarsReadRepository,CarsReadRepository>();
            builder.Services.AddScoped<ICircuitReadRepository,CircuitReadRepository>();
            builder.Services.AddScoped<IConstructorsCShReadRepository,ConstructorsReadRepository>();
            builder.Services.AddScoped<IDriverCShReadRepository,DriverCShReadRepository>();
            builder.Services.AddScoped<IDriverReadRepository,DriversReadRepository>();
            builder.Services.AddScoped<IRaceReadRepository,RaceReadRepository>();
            builder.Services.AddScoped<ISeasonReadRepository,SeasonReadRepository>();
            builder.Services.AddScoped<ITeamReadRepository,TeamsReadRepository>();

            //write repo
            builder.Services.AddScoped<ICarsWriteRepository,CarsWriteRepository>();
            builder.Services.AddScoped<ICircuitWriteRepository,CircuitWriteRepository>();
            builder.Services.AddScoped<IConstructorsCShWriteRepository,ConstructorsWriteRepository>();
            builder.Services.AddScoped<IDriverCShWriteRepository,DriverCShWriteRepository>();
            builder.Services.AddScoped<IDriverWriteRepository,DriversWriteRepository>();
            builder.Services.AddScoped<IRaceWriteRepository,RaceWriteRepository>();
            builder.Services.AddScoped<ISeasonWriteRepository,SeasonWriteRepository>();
            builder.Services.AddScoped<ITeamWriteRepository,TeamsWriteRepository>();

            //write services
            builder.Services.AddScoped<IWriteCarService,WriteCarService>();
            builder.Services.AddScoped<IWriteCircuitService,WriteCircuitService>();
            builder.Services.AddScoped<IWriteConstrCShService,WriteConstrCshSrevice>();
            builder.Services.AddScoped<IWriteDriverCShService,WriteDriverCShService>();
            builder.Services.AddScoped<IWriteDriverService,WriteDriverService>();
            builder.Services.AddScoped<IWriteRaceService,WriteRaceService>();
            builder.Services.AddScoped<IWriteSeasonService,WriteSeasonService>();
            builder.Services.AddScoped<IWriteTeamService,WriteTeamService>();

            builder.Services.AddScoped<IManageRoleService,ManageRoleService>();
            //read services
            builder.Services.AddScoped<IReadCarService,ReadCarService>();
            builder.Services.AddScoped<IReadCircuitService,ReadCircuitService>();
            builder.Services.AddScoped<IReadConstrCShService,ReadConstrCShSerivice>();
            builder.Services.AddScoped<IReadDriverCShService,ReadDriversCShService>();
            builder.Services.AddScoped<IReadDriverService,ReadDriverService>();
            builder.Services.AddScoped<IReadRaceService,ReadRaceService>();
            builder.Services.AddScoped<IReadTeamService,ReadTeamService>();
            //redis
            builder.Services.AddScoped<IRedisCache,RedisCache>();



            

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ICurrentUser, CurrentUser>();


            builder.Services.AddAuthentication(options => 
            { 
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie(options => 
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

            }).AddGoogle(GoogleDefaults.AuthenticationScheme,options => 
            {
                options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
                options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
                options.CallbackPath = "/auth/google-callback";
                options.SaveTokens = true;
            });
            builder.Services.AddAuthorization(options => 
            {
                options.AddPermissionPolicies();
            });



            var app = builder.Build();
            
            using (var s = app.Services.CreateScope())
            {
                var db = s.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
            }
            var allowedOrigins = builder.Configuration.GetSection("Angular:BaseUrl").Get<string[]>()!;
            app.UseCors(policy =>
                policy.WithOrigins(allowedOrigins)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials()
            );
            using var scope = app.Services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            foreach(var role in Enum.GetValues<Roles>())
            {
                if(!await roleManager.RoleExistsAsync(role.ToString()))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role.ToString()));
                }
            }
            if (!userManager.Users.Any())
            {
                var email = "lawzmih@gmail.com";
                var admin = new AppUser {UserName = email,Email = email };
                await userManager.CreateAsync(admin,"Admin123");
                await userManager.AddToRoleAsync(admin,"Admin");
            }

            

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "F1Proj V1");
                x.RoutePrefix = string.Empty;
            }
            );

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
           
            app.Run();
        }
    }
}
