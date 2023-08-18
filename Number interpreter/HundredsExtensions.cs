namespace Number_interpreter;

public static class HundredsExtensions
{
    public static IApplicationBuilder UseHundreds(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HundredsMiddleware>();
    }
}