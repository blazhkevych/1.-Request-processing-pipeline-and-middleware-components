namespace Number_interpreter
{
    public static class FromOneHundredOneToHundredThousandExtensions
    {
        public static IApplicationBuilder UseFromOneHundredOneToHundredThousand(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromOneHundredOneToHundredThousandMiddleware>();
        }
    }
}