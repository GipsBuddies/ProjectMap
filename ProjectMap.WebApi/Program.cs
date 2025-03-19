using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ProjectMap.WebApi;


//authorization services means its used for registraion and logging in.



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();



builder.Services.Configure<RouteOptions>(o => o.LowercaseUrls = true);

var sqlConnectionString = builder.Configuration["SqlConnectionString"];

if (string.IsNullOrWhiteSpace(sqlConnectionString))
    throw new InvalidProgramException("Configuration variable SqlConnectionString not found");

//place to add builder.services.addtransient.

//authorization services

builder.Services.AddAuthorization();
builder.Services
    .AddIdentityApiEndpoints<IdentityUser>()
    .AddDapperStores(options => {
        options.ConnectionString = sqlConnectionString;
    });



builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ProjectMap.WebApi.Interfaces.IAuthenticationService, AspNetIdentityAuthenticationService>();



var app = builder.Build();

//authorization services



app.MapGroup(prefix: "/account")
   .MapIdentityApi<IdentityUser>();



app.MapPost(pattern: "/account/logout",
    async (SignInManager<IdentityUser> signInManager,
    [FromBody] object empty) => {
        if (empty != null)
        {
            await signInManager.SignOutAsync();
            return Results.Ok();
        }
        return Results.Unauthorized();
    })
    .RequireAuthorization();

//results that all endpoints in controllers are required by login.
//commented for now because no controllers
//app.MapControllers().RequireAuthorization();



// Configure the HTTP request pipeline.|
app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();