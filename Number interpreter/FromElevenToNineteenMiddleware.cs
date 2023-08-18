namespace Number_interpreter;

public class FromElevenToNineteenMiddleware
{
    private readonly RequestDelegate _next;

    public FromElevenToNineteenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        string? token = context.Request.Query["number"];
        try
        {
            var number = Convert.ToInt32(token);
            number = Math.Abs(number) % 100;
            if (number < 11 || number > 19)
            {
                await _next.Invoke(context);
            }
            else
            {
                string[] Numbers =
                {
                    "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen",
                    "nineteen"
                };
                await context.Response.WriteAsync("Your number is " + Numbers[number - 11]);
            }
        }
        catch (Exception)
        {
            await context.Response.WriteAsync("Incorrect parameter");
        }
    }
}