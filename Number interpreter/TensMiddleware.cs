namespace Number_interpreter;

public class TensMiddleware
{
    // Declare a private read-only field of type RequestDelegate
    private readonly RequestDelegate _next;

    // Declare a public constructor that takes a RequestDelegate as a parameter
    public TensMiddleware(RequestDelegate next)
    {
        // Assign the passed in RequestDelegate to the private field _next
        _next = next;
    }

    // Declare an asynchronous public method named Invoke that takes an HttpContext as a parameter
    public async Task Invoke(HttpContext context)
    {
        // Get the "number" query parameter from the request
        string? token = context.Request.Query["number"];
        try
        {
            // Get the "number" from the session
            var s = context.Session.GetString("number"); // test
            // Convert the "number" query parameter to an integer
            var number = Convert.ToInt32(token);
            // Get the absolute value of the number
            number = Math.Abs(number);
            // If the number is less than 10, call the next middleware in the pipeline
            if (number < 10)
            {
                await _next.Invoke(context);
            }
            else
            {
                // Get the tens digit of the number
                number %= 100;
                // If the number is greater than or equal to 20
                if (number >= 20)
                {
                    // Divide the number by 10 to get the tens digit
                    number /= 10;
                    // Declare an array of strings for the words of the tens digits
                    string[] Numbers = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
                    // Add the word of the tens digit to the "number" in the session
                    context.Session.SetString("number",
                        context.Session.GetString("number") + " " + Numbers[number - 2]);
                    // Call the next middleware in the pipeline
                    await _next.Invoke(context);
                }
                // If the number is between 11 and 19
                else if (number >= 11 && number <= 19)
                {
                    // Declare an array of strings for the words of the numbers 11 to 19
                    string[] Numbers =
                    {
                        "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen",
                        "nineteen"
                    };
                    // Write the word of the number to the response
                    await context.Response.WriteAsync("Your number is " + context.Session.GetString("number") +
                                                      Numbers[number - 11]);
                }
                // If the number is 10
                else if (number == 10)
                {
                    // Write "ten" to the response
                    await context.Response.WriteAsync("Your number is " + context.Session.GetString("number") + " " +
                                                      "ten");
                }
                else
                {
                    // Call the next middleware in the pipeline
                    await _next.Invoke(context);
                }
            }
        }
        // If an exception is thrown
        catch (Exception)
        {
            // Write "Incorrect parameter" to the response
            await context.Response.WriteAsync("Incorrect parameter");
        }
    }
}