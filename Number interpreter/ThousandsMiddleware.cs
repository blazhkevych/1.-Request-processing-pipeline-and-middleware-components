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
                var countNumbersInDigit = 0;
                var temp = number;
                while (temp > 0)
                {
                    temp /= 10;
                    countNumbersInDigit++;
                }

                // Подсчет количества тысяч
                var thousands = 0;
                if (countNumbersInDigit > 3)
                {
                    thousands = number / 1000;
                    number %= 1000;
                    countNumbersInDigit -= 3;
                }

                // Перевод количества тысяч в слова
                if (thousands > 0)
                {
                    var result = string.Empty;
                    if (countNumbersInDigit == 1)
                    {
                        string[] Numbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
                        result = Numbers[thousands - 1] + " " + "thousand";
                        context.Session.SetString("number", result);
                        await _next.Invoke(context);
                    }
                    else if (countNumbersInDigit == 2)
                    {
                        var num1 = thousands / 10;
                        if (num1 == 1)
                        {
                            string[] Numbers =
                            {
                                "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen",
                                "seventeen", "eighteen", "nineteen"
                            };
                            result = Numbers[thousands % 10];
                        }
                        else
                        {
                            var num2 = thousands % 10;
                            string[] Numbers =
                                { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                            result = Numbers[num1 - 2];

                            if (num2 > 0)
                            {
                                string[] Numbers2 =
                                    { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
                                result += " " + Numbers2[num2 - 1];
                            }
                        }

                        result += " " + "thousand";
                        context.Session.SetString("number", result);
                        await _next.Invoke(context);
                    }
                    else
                    {
                        await context.Response.WriteAsync("Your number is one hundred thousand");
                    }
                }
            }
        }
        catch (Exception)
        {
            await context.Response.WriteAsync("Incorrect parameter");
        }
    }
}