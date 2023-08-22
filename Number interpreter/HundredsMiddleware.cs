namespace Number_interpreter;

public class HundredsMiddleware
{
    private readonly RequestDelegate _next;

    public HundredsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        string? token = context.Request.Query["number"];
        try
        {
            var number = Convert.ToInt32(token);
            number = Math.Abs(number);
            // Получение сотен
            var hundreds = number % 1000 / 100;
            if (number < 100)
            {
                await _next.Invoke(context);
            }
            // Перевод сотен в слова
            else
            {
                string[] Numbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
                var result = context.Session.GetString("number") + " " + Numbers[hundreds - 1] + " " + "hundred";

                context.Session.SetString("number", result);
                await _next.Invoke(context);
            }
        }
        catch (Exception)
        {
            await context.Response.WriteAsync("Incorrect parameter");
        }
    }
}