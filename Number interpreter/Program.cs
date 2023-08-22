using Number_interpreter;

// Creates a new web application builder with the command-line arguments.
var builder = WebApplication.CreateBuilder(args);
// Adds services required for in-memory distributed caching to the DI container.
// All sessions work on top of an IDistributedCache object, and ASP.NET Core provides a built-in implementation of IDistributedCache.
builder.Services.AddDistributedMemoryCache(); // Adds IDistributedMemoryCache
// Adds services required for session state to the DI container.
builder.Services.AddSession(); // Adds session services
// Builds the web application.
var app = builder.Build();
// Adds the session middleware to the request processing pipeline.
app.UseSession(); // Adds the middleware component for working with sessions
// Adds the middleware components to the request processing pipeline.
app.UseThousands();
app.UseHundreds();
app.UseTens();
app.UseOnes();
// Runs the web application.
app.Run();