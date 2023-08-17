namespace Number_interpreter;

public static class FromElevenToNineteenExtensions
{
    public static IApplicationBuilder UseFromElevenToNineteen(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<FromElevenToNineteenMiddleware>();
    }
}