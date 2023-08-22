namespace Number_interpreter;

public static class ThousandsExtensions
{
    // Declare a public static method named UseThousands that extends the IApplicationBuilder interface
    public static IApplicationBuilder UseThousands(this IApplicationBuilder builder)
    {
        // Use the UseMiddleware method of the IApplicationBuilder to add the ThousandsMiddleware to the pipeline
        // and return the IApplicationBuilder
        return builder.UseMiddleware<ThousandsMiddleware>();
    }
}