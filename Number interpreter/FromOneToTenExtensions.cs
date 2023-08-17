namespace Number_interpreter;

public static class FromOneToTenExtensions
{
    public static IApplicationBuilder UseFromOneToTen(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<FromOneToTenMiddleware>();
    }
}