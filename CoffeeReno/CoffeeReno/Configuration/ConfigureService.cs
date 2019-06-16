using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using AutoMapper;
using Data.Context;
using Facebook;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Services.Mappings;
using Swashbuckle.AspNetCore.Swagger;

namespace CoffeeReno.Configuration
{
    public class ConfigureService
    {
        public static void InitServices(IServiceCollection services, IConfiguration configuration)
        {
            InitAutoMapper(services);
            InitSwagger(services);
            InitMySQL(services, configuration);
            IninitAuth(services);
        }

        private static void IninitAuth(IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<CoffeeRenoContext>();

            services.AddHttpContextAccessor();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddFacebook(options =>
                {
                    options.AppId = "400769287316836";
                    options.AppSecret = "896d529984a688a55b472eba65a967d2";
                    options.Scope.Add("user_birthday");
                    options.Scope.Add("public_profile");
                    options.Fields.Add("picture");
                    options.Fields.Add("birthday");
                    options.Fields.Add("name");
                    options.Fields.Add("email");
                    options.Fields.Add("gender");
                    options.Fields.Add("picture");
                    options.UserInformationEndpoint = "https://graph.facebook.com/v2.4/me?fields=email";
                    options.Events = new OAuthEvents
                    {
                        OnCreatingTicket = context => {
                            var client = new FacebookClient(context.AccessToken);
                            dynamic info = client.Get("me", new { fields = "name, id, email, picture, gender, birthday, address, first_name, age_range" });
                            return Task.FromResult(0);
                        }
                    };
                })
                //.AddTwitter(options =>
                //{
                //    options.ConsumerKey = "";
                //    options.ConsumerSecret = "";
                //})
                .AddGoogle(options =>
                {
                    options.ClientId = "996326863156-enla0cmr75m9i74p5ubstj2tqklpmh7a.apps.googleusercontent.com";
                    options.ClientSecret = "Yuj_3Lq5KjJhgQ6tZ2UmuU52";
                    options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
                    options.ClaimActions.MapJsonKey("urn:google:locale", "locale", "string");
                    options.SaveTokens = true;
                    options.Events.OnCreatingTicket = ctx =>
                    {
                        List<AuthenticationToken> tokens = ctx.Properties.GetTokens().ToList();

                        tokens.Add(new AuthenticationToken()
                        {
                            Name = "TicketCreated",
                            Value = DateTime.UtcNow.ToString()
                        });

                        ctx.Properties.StoreTokens(tokens);

                        return Task.CompletedTask;
                    };
                })
                .AddCookie(options =>
                {
                    options.LoginPath = "/signin";
                });
        }

        // ReSharper disable once InconsistentNaming
        private static void InitMySQL(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddAuthentication();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<CoffeeRenoContext>(options =>
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                    mySqlOptionsAction => mySqlOptionsAction.ServerVersion(new Version(), ServerType.MySql)
                ));
        }

        private static void InitSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Test API",
                    Description = "ASP.NET Core Web API"
                });
            });
        }

        public static void InitConfig(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMvc();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
                });
            }
            else
            {
                app.UseHsts();
            }

#pragma warning disable 618
            app.UseIdentity();
#pragma warning restore 618
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseJwtTokenMiddleware();
            //Enable authentication
            app.UseAuthentication();
            //Enable support for default files (eg. default.htm, default.html, index.htm, index.html)
            app.UseDefaultFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            loggerFactory.AddLog4Net(configuration.GetValue<string>("Log4NetConfigFile:Name"));
            
        }

        private static void InitAutoMapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
#pragma warning disable 618
            services.AddAutoMapper();
#pragma warning restore 618

        }
    }
}
