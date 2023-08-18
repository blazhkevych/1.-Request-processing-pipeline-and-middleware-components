using System;

namespace Number_interpreter;

public class ThousandsMiddleware
{
    private readonly RequestDelegate _next;

    public ThousandsMiddleware(RequestDelegate next)
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
            if (number < 101)
            {
                await _next.Invoke(context);
            }
            else if (number > 100000)
            {
                await context.Response.WriteAsync("Number greater than one hundred thousand");
            }
            else
            {
                // Подсчет количества цифр в числе
                int countNumbersInDigit = 0;
                int temp = number; // 99538
                while (temp > 0)
                {
                    temp /= 10; 
                    countNumbersInDigit++; // 5
                }
                // Подсчет количества тысяч
                int thousands = 0;
                if (countNumbersInDigit > 3)
                {
                    thousands = number / 1000; // 99
                    number %= 1000; // 538
                    countNumbersInDigit -= 3; // 2
                }
                // Перевод количества тысяч в слова
                if (thousands > 0)
                {
                    string? result = string.Empty;
                    if (countNumbersInDigit == 1)
                    {
                        string[] Numbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" }; // множина, однина
                        result = Numbers[thousands - 1] + " " + "thousand";
                    }
                    if (countNumbersInDigit == 2)
                    {
                        var num1 = thousands / 10;
                        var num2 = thousands % 10;
                        string[] Numbers = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                        result = Numbers[num1 - 2];

                        if (num2 > 0)
                        {
                            string[] Numbers2 = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
                            result += " " + Numbers2[num2 - 1];
                        }
                        result += " " + "thousand";     
                    }
                    if (countNumbersInDigit == 3)
                    {
                        await context.Response.WriteAsync("Your number is one hundred thousand");
                    }

                    context.Session.SetString("number", result);
                    await _next.Invoke(context);
                }
            }
        }
        catch (Exception)
        {
            await context.Response.WriteAsync("Incorrect parameter");
        }
    }
}