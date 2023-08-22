namespace Number_interpreter;

public static class OnesExtensions
{
    // Declares an extension method named `UseOnes` for the `IApplicationBuilder` interface.
    // The `this` keyword before the first parameter indicates that this is an extension method.
    // The method returns an instance of `IApplicationBuilder`, allowing for fluent configuration.
    public static IApplicationBuilder UseOnes(this IApplicationBuilder builder)
    {
        // Calls the `UseMiddleware` method on the `IApplicationBuilder` instance, adding the `OnesMiddleware` to the application's pipeline.
        // The `OnesMiddleware` is presumably a class that handles some specific logic in the HTTP request processing pipeline.
        return builder.UseMiddleware<OnesMiddleware>();
    }
}