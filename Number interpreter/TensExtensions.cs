namespace Number_interpreter;

public static class TensExtensions
{
    public static IApplicationBuilder UseTens(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TensMiddleware>();
    }
}