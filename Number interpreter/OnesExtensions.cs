namespace Number_interpreter;

public static class OnesExtensions
{
    public static IApplicationBuilder UseOnes(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<OnesMiddleware>();
    }
}