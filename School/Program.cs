using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using School.MappingConfigs;
using School.Models;
using School.Services;
using School.UnitOfWorks;



namespace School
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());
            });
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDbContext<SchoolDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("CS")));


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidIssuer = "SchoolSystem",
                            ValidAudience = "SchoolAppClient",
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("welcome dear to my website hope that it will be useful and helpful"))
                        };
                   });


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<SchoolDbContext>()
                    .AddDefaultTokenProviders();

            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddScoped<UnitOfWork>();

            builder.Services.AddAutoMapper(typeof(MappingConfig));


            var app = builder.Build();

            app.UseCors("AllowAll");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json", "v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
