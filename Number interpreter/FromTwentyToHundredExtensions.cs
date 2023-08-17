namespace Number_interpreter;

public static class FromTwentyToHundredExtensions
{
    public static IApplicationBuilder UseFromTwentyToHundred(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<FromTwentyToHundredMiddleware>();
    }
}