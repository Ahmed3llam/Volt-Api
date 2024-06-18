using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using Shipping.Models;
using Shipping.UnitOfWork;
internal class Program
{
    private static void Main(string[] args)
    {
        string txt = "";

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        });

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(
                o =>
                {
                    o.SwaggerDoc("v1", new OpenApiInfo()
                    {
                        Title = "Shipping Api",
                        Description = " Api To Manage Shipping System For Pioneer",
                        Version = "v1",
                        TermsOfService = new Uri("http://tempuri.org/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "",
                            Email = "",

                        },
                    });
                    //o.IncludeXmlComments("");
                    o.EnableAnnotations();
                }
            );

        builder.Services.AddDbContext<ShippingContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("Db"));
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        //builder.Services.AddIdentity<User, IdentityRole>(
        //            options =>
        //            {
        //                options.User.RequireUniqueEmail = true;
        //                options.Password.RequireNonAlphanumeric = false;
        //                options.Password.RequireDigit = false;
        //                options.Password.RequireUppercase = false;
        //                options.Password.RequireLowercase = false;
        //                options.Password.RequiredLength = 8;
        //            }).AddEntityFrameworkStores<ShippingContext>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(txt,
            builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });
        });

        builder.Services.AddScoped<Unit>();

        builder.Services.AddAuthentication(option => option.DefaultAuthenticateScheme = "schema")
                    .AddJwtBearer("schema",
                    op =>
                    {
                        string key = "Iti Pd And Bi 44 Menoufia Shipping System For GP";
                        var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

                        op.TokenValidationParameters = new TokenValidationParameters()
                        {
                            IssuerSigningKey = secertkey,
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    }
                    );

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(txt);

        app.UseAuthorization();

       
        app.MapControllers();

        app.Run();
    }
}