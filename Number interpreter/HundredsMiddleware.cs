namespace Number_interpreter;

// Declares a public class named `HundredsMiddleware`. This class is a middleware in the ASP.NET Core pipeline.
public class HundredsMiddleware
{
    // Declares a private readonly field of type `RequestDelegate`. This field will hold the next middleware in the pipeline.
    private readonly RequestDelegate _next;

    // Declares a public constructor that takes a `RequestDelegate` as a parameter. This constructor is used to inject the next middleware in the pipeline.
    public HundredsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // Declares an asynchronous method named `Invoke` that takes an `HttpContext` as a parameter. This method is called for each HTTP request.
    public async Task Invoke(HttpContext context)
    {
        // Retrieves the "number" query parameter from the HTTP request.
        string? token = context.Request.Query["number"];
        try
        {
            // Retrieves the "number" session variable.
            var s = context.Session.GetString("number"); // test
            // Converts the "number" query parameter to an integer and takes its absolute value.
            var number = Convert.ToInt32(token);
            number = Math.Abs(number);
            // Retrieves the hundreds digit of the number.
            var hundreds = number % 1000 / 100;
            // If the number is less than 100, calls the next middleware in the pipeline.
            if (number < 100)
            {
                await _next.Invoke(context);
            }
            // Otherwise, translates the hundreds digit to words and adds it to the "number" session variable.
            else
            {
                // If the hundreds digit is greater than 0, translates it to words.
                if (hundreds > 0)
                {
                    string[] Numbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
                    var result = context.Session.GetString("number") + " " + Numbers[hundreds - 1] + " " + "hundred";
                    // Sets the "number" session variable to the translated number.
                    context.Session.SetString("number", result);
                    // Calls the next middleware in the pipeline.
                    await _next.Invoke(context);
                }
                else
                {
                    // If the hundreds digit is 0, calls the next middleware in the pipeline.
                    await _next.Invoke(context);
                }
            }
        }
        // If an exception occurs, writes an error message to the HTTP response.
        catch (Exception)
        {
            await context.Response.WriteAsync("Incorrect parameter");
        }
    }
}