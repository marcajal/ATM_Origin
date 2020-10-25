using ATM_Origin.Core.CustonEntities;
using ATM_Origin.Core.Interfaces;
using ATM_Origin.Core.Services;
using ATM_Origin_Infrastucture;
using ATM_Origin_Infrastucture.Data;
using ATM_Origin_Infrastucture.Filters;
using ATM_Origin_Infrastucture.Interfaces;
using ATM_Origin_Infrastucture.Repositories;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Unicode;

namespace ATM_OriginAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //SE CONFIGURA AddCors POR UN TEMA DE CONFLICTO DE PUERTOS LOCAL HOST CON EL PROYECTO FRONT END
            services.AddCors(option => option.AddPolicy("MyBlogPolicy", builder => {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            }));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            })
            .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                }
            )
            .ConfigureApiBehaviorOptions(options => {
                //options.SuppressModelStateInvalidFilter = true;


            });


            services.Configure<PaginationOptions>(Configuration.GetSection("Pagination")); 
            services.AddDbContext<ATM_OriginDBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ATM_OriginDB"))
                );
            services.AddTransient<ITarjetaService, TarjetaService>();
            services.AddTransient<ITarjetaRepository, TarjetaRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUrl = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUrl);
            });

            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo {Title ="ATM_Origin_API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                doc.IncludeXmlComments(xmlPath);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Authentication:Issuer"],
                    ValidAudience = Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]))
                };
            });

            //FILTRO GLOBAL
            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            })
            .AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors("MyBlogPolicy");
            
            app.UseSwagger();
            app.UseSwaggerUI(options => 
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json","Origin ATM API");
                options.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
