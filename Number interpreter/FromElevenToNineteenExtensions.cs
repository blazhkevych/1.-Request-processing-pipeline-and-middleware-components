namespace Number_interpreter;

public static class FromElevenToNineteenExtensions
{
    public static IApplicationBuilder UseTens(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<FromElevenToNineteenMiddleware>();
    }
}