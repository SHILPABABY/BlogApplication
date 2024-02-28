using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;//added
using Microsoft.AspNetCore.Authentication.JwtBearer;//added
using System.Text;
using BlogApplication.Models;
using BlogApplication.Repositories.Interfaces;
using BlogApplication.Repositories.Implementation;
using Microsoft.OpenApi.Models;//added


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ConnectionString");

// Add services to the container.//Register the services in container

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters //setting the parameters
        {
            ValidateIssuer = true, // validate the server and generate the token
            ValidateAudience = true, // validate the receipient of the token
            ValidateLifetime = true, // check the token is not expired
            ValidIssuer = builder.Configuration["Jwt:Issuer"], //taking from appsettings
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // validate the signature of the token
        };
    }
    );

builder.Services.AddControllers();
//builder.Services.AddDbContext<BlogContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Services.AddDbContext<BlogContext>(options =>
    //options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));
builder.Services.AddDbContext<BlogContext>(options => options.UseSqlServer(connectionString)); //inject(register) the dbcontext.AddDbContext: Registers the BlogContext DbContext in the dependency injection container. This allows the DbContext to be injected into other components.

builder.Services.AddScoped<IBlogRepository,BlogRepository>(); //inject the services after the rpository eg of dependency injection

builder.Services.AddCors();// Registers CORS services in the application. CORS allows cross-origin requests from specified origins.
builder.Services.AddCors(options => options.AddDefaultPolicy(
    builder =>
    {
        builder.WithOrigins("https://localhost:44351", "https://localhost:4200").AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod();
          // .AllowAnyHeader());

    }));
builder.Services.AddSwaggerGen(option => { option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" }); option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { In = ParameterLocation.Header, Description = "Please enter a valid token", Name = "Authorization", Type = SecuritySchemeType.Http, BearerFormat = "JWT", Scheme = "Bearer" }); option.AddSecurityRequirement(new OpenApiSecurityRequirement { { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, new string[] { } } }); });
var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// called that Cors below
app.UseCors(x => x  
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()); //Adds CORS middleware to the request pipeline. It configures CORS policies to allow requests from specified origins, methods, and headers.
app.UseAuthentication(); //should be add before authentication
app.UseAuthorization(); //Adds authorization middleware to the request pipeline

app.MapControllers(); //Maps controllers to the request pipeline, allowing them to handle incoming HTTP requests.

app.Run();
