


var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});

builder.Services.AddMarten(config =>
{
    config.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();
