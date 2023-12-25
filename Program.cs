using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<UserService>();

var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options=>
    options.UseMySql(connectionString,ServerVersion.Parse("8.0.31-mysql")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options=>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sjwBearerdddddddddddd"))
    };
});

bool IsPublicEndpoint(AuthorizationFilterContext context){
    var endpoint = context?.HttpContext.GetEndpoint();
    if(endpoint?.Metadata?.GetMetadata<IAllowAnonymous>()!=null){
        return true;
    }
    return false;
}

builder.Services.AddAuthorization(options=>
{
    options.AddPolicy("TokenRequired",policy=>
        policy.RequireAuthenticatedUser());
    options.AddPolicy("PublicEndpoint",
        policy => policy.RequireAssertion(
            context=> context.Resource is AuthorizationFilterContext && IsPublicEndpoint(context.Resource as AuthorizationFilterContext)
    ));
});



builder.Services.AddCors(options=>{
    options.AddDefaultPolicy(builder=>{
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
