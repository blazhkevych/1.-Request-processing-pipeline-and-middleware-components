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
            //if (number < 20)
            //{
            //    await _next.Invoke(context);
            //}
            //else if (number > 100)
            //{
            //    await context.Response.WriteAsync("Number greater than one hundred");
            //}
            //else if (number == 100)
            //{
            //    await context.Response.WriteAsync("Your number is one hundred");
            //}
            //else
            //{
                // Получение сотен
                int hundreds = number % 1000 / 100;
                
                // Перевод сотен в слова
                if (hundreds > 0)
                {
                    string[] Numbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
                    await context.Response.WriteAsync("Your number is " + Numbers[hundreds - 1] + " hundred");
                    string result = context.Session.GetString("number") + " " + Numbers[hundreds - 1] + " " + "hundred";

                    context.Session.SetString( "number", result);
                    await _next.Invoke(context);
                }
            //}
        }
        catch (Exception)
        {
            await context.Response.WriteAsync("Incorrect parameter");
        }
    }
}