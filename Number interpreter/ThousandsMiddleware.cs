using System;

namespace Number_interpreter;

public class ThousandsMiddleware
{
    // Declare a private read-only field of type RequestDelegate
    private readonly RequestDelegate _next;

    // Declare a public constructor that takes a RequestDelegate as a parameter
    public ThousandsMiddleware(RequestDelegate next)
    {
        // Assign the passed in RequestDelegate to the private field _next
        _next = next;
    }

    // Declare an asynchronous public method named Invoke that takes an HttpContext as a parameter
    public async Task Invoke(HttpContext context)
    {
        // Clear the session
        context.Session.Clear();
        // Get the "number" query parameter from the request
        string? token = context.Request.Query["number"];
        try
        {
            // Convert the "number" query parameter to an integer
            var number = Convert.ToInt32(token);
            // Get the absolute value of the number
            number = Math.Abs(number);
            // If the number is less than 1000, call the next middleware in the pipeline
            if (number < 1000)
            {
                await _next.Invoke(context);
            }
            // If the number is greater than 100000, write "Number greater than one hundred thousand" to the response
            else if (number > 100000)
            {
                await context.Response.WriteAsync("Number greater than one hundred thousand");
            }
            else
            {
                // Calculate the number of digits in the number
                var countNumbersInDigit = 0;
                var temp = number;
                while (temp > 0)
                {
                    temp /= 10;
                    countNumbersInDigit++;
                }

                // Calculate the number of thousands
                var thousands = 0;
                if (countNumbersInDigit > 3)
                {
                    thousands = number / 1000;
                    number %= 1000;
                    countNumbersInDigit -= 3;
                }

                // Convert the number of thousands to words
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
                        // Remove double spaces in the string
                        context.Session.SetString("number", context.Session.GetString("number").Replace("  ", " "));
                    }
                    else
                    {
                        await context.Response.WriteAsync("Your number is one hundred thousand");
                    }
                }
            }
        }
        // If an exception is thrown, write "Incorrect parameter" to the response
        catch (Exception)
        {
            await context.Response.WriteAsync("Incorrect parameter");
        }
    }
}