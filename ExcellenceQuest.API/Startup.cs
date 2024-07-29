namespace ExcellenceQuest.API
{

    using ExcellenceQuest.Business.Implementation;
    using ExcellenceQuest.Business.Contracts;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using ExcellenceQuest.Repository.Models;
    using ExcellenceQuest.Business.Shared;
    using Serilog;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors();
            services.AddDistributedMemoryCache();
            // services.AddMemoryCache();
            services.AddControllers();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Initialize Logger
            Serilog.Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.WithProperty("ApplicationName", "ExcellenceQuest")
                .CreateLogger();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Execellence Quest  API",
                    Description = "Execellence Quest  API in ASP.NET Core",
                    // TermsOfService = "None",
                    Contact = new OpenApiContact
                    {
                        Name = "Innova Solutions",
                        Url = new Uri("https://www.acsicorp.com/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "© 2023 Innova Solutions"
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = Configuration["TokenValidationParameters:IssuerAndAudience"],
                    ValidAudience = Configuration["TokenValidationParameters:IssuerAndAudience"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jkhdlkfjsljdkjfandflkahjkdfncasdfhkjashfksdfhsajkfhas"))
                };
            }).AddCookie("Cookie");

            services.AddDbContext<ExcellenceQuestContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ExcellenceQuestConnection"));

            });

            services.AddSingleton(_ => Configuration);
            services.AddAutoMapper(typeof(MappingProfiles));
            Business.Startup.ConfigureServices(services, Configuration);
            services.AddScoped<IUserService, UserService>();
            services.AddHttpClient<ITokenService, TokenService>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IKeyPerformanceIndexService, KeyPerformanceIndexService>();
            services.AddScoped<IEmployeeAchievementService, EmployeeAchievementService>();
            services.AddScoped<IKPISuccessCriteriaService, KPISuccessCriteriaService>();
            services.AddScoped<IBadgeConfigurationService, BadgeConfigurationService>();
            services.AddScoped<IBadgeService, BadgeService>();
            services.AddCors();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExcellenceQuest.API v1");
                c.RoutePrefix = string.Empty;
            }
            );


            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}