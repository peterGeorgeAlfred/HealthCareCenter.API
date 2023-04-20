using HelCareCenter.Context;
using HelCareCenter.Repositery.IManger;
using HelCareCenter.Repositery.Manger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;

namespace HelCareCenter.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string MyAllowSpecificOrigins = "m";
            // Add services to the container.


            builder.Services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            builder.Services.AddDbContext<DBContext>(option => option./*UseLazyLoadingProxies().*/UseSqlServer(builder.Configuration.GetConnectionString($"DefaultConnection")));
            builder.Services.AddCors(options => { options.AddPolicy(MyAllowSpecificOrigins, builder => { builder.AllowAnyOrigin(); builder.AllowAnyMethod(); builder.AllowAnyHeader(); }); });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Add Identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false; //@,$,_
                options.Password.RequiredLength = 8;

            }
                ).AddEntityFrameworkStores<DBContext>()
                .AddDefaultTokenProviders();
            #endregion

            #region Tooken

            var key = Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:Key"].ToString());


            builder.Services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            #endregion


            #region Add Scoped

            builder.Services.AddScoped<IHelpCenterCare, HelpCareCenrterSQL>();
            //builder.Services.AddScoped<, ShopSQL>();
            //builder.Services.AddScoped<ICategorey, CategorySQL>();
            
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}