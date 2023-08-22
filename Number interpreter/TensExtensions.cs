namespace Number_interpreter;

public static class TensExtensions
{
    // Declares an extension method named `UseTens` for the `IApplicationBuilder` interface.
    // The `this` keyword before the first parameter indicates that this is an extension method.
    // The method returns an instance of `IApplicationBuilder`, allowing for fluent configuration.
    public static IApplicationBuilder UseTens(this IApplicationBuilder builder)
    {
        // Calls the `UseMiddleware` method on the `IApplicationBuilder` instance, adding the `TensMiddleware` to the application's pipeline.
        // The `TensMiddleware` is presumably a class that handles some specific logic in the HTTP request processing pipeline.
        return builder.UseMiddleware<TensMiddleware>();
    }
}