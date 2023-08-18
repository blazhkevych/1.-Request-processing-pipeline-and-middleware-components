namespace Number_interpreter
{
    public static class ThousandsExtensions
    {
        public static IApplicationBuilder UseThousands(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ThousandsMiddleware>();
        }
    }
}