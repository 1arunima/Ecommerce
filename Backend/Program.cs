using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using dotnet_practice.Migrations;
using FluentMigrator.Runner.Conventions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(Options=>{
    Options.AddPolicy("AllowAll", builder=>{
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

// register services
// builder.Services.AddScoped<DbService>();


// FluentMigrator and Database Migration Runner

builder.Services
        .AddSingleton<IConventionSet>(new DefaultConventionSet("arunima", null))
        .AddFluentMigratorCore()
        .ConfigureRunner(runner=>     
            runner
            .AddPostgres11_0()
            .WithGlobalConnectionString("Host=192.168.1.10;Port=5432;Database=test;User Id=postgres;Password=PostgresDB@dm1n;SearchPath=arunima;")
            .ScanIn(typeof(CreateCustomerProductSakesTable ).Assembly).For.Migrations())
            .AddLogging(logging=>logging.AddFluentMigratorConsole());
 
var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();



// Run Migrations at startup

using (var scope =app.Services.CreateScope())
{
    var runner =scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

app.Run();