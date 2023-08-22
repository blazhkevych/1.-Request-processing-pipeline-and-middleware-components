namespace Number_interpreter;

// Declares a public class named `OnesMiddleware`. This class is a middleware in the ASP.NET Core pipeline.
public class OnesMiddleware
{
    // Declares a private readonly field of type `RequestDelegate`. This field will hold the next middleware in the pipeline.
    private readonly RequestDelegate _next;

    // Declares a public constructor that takes a `RequestDelegate` as a parameter. This constructor is used to inject the next middleware in the pipeline.
    public OnesMiddleware(RequestDelegate next)
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
            // Converts the "number" query parameter to an integer and takes its absolute value.
            var number = Convert.ToInt32(token);
            number = Math.Abs(number);
            // If the number is 10, writes a response with the translated number.
            if (number == 10)
            {
                await context.Response.WriteAsync("Your number is " + context.Session.GetString("number") + " " +
                                                  "ten");
            }
            // Otherwise, translates the ones digit to words and adds it to the "number" session variable.
            else
            {
                // Retrieves the ones digit of the number.
                number %= 10;
                // If the ones digit is greater than 0, translates it to words.
                if (number > 0)
                {
                    string[] Numbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
                    var ses = context.Session.GetString("number");
                    context.Session.SetString("number", ses + " " + Numbers[number - 1]);
                    var newses = context.Session.GetString("number"); // test
                    // Writes a response with the translated number.
                    await context.Response.WriteAsync("Your number is " + context.Session.GetString("number"));
                }
                else
                {
                    // Writes a response with the translated number.
                    await context.Response.WriteAsync("Your number is " + context.Session.GetString("number"));
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