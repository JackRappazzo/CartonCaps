namespace CartonCaps.Api.Exceptions
{
    public class GlobalErrorLogger
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalErrorLogger> logger;

        public GlobalErrorLogger(RequestDelegate next, ILogger<GlobalErrorLogger> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context); // Proceed to the next middleware
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while processing the request.");
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var response = new { message = "Internal Server Error" };
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
