namespace Number_interpreter;

public static class HundredsExtensions
{
    // Declares an extension method named `UseHundreds` for the `IApplicationBuilder` interface.
    // The `this` keyword before the first parameter indicates that this is an extension method.
    // The method returns an instance of `IApplicationBuilder`, allowing for fluent configuration.
    public static IApplicationBuilder UseHundreds(this IApplicationBuilder builder)
    {
        // Calls the `UseMiddleware` method on the `IApplicationBuilder` instance, adding the `HundredsMiddleware` to the application's pipeline.
        // The `HundredsMiddleware` is presumably a class that handles some specific logic in the HTTP request processing pipeline.
        return builder.UseMiddleware<HundredsMiddleware>();
    }
}