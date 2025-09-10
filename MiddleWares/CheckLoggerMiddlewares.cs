namespace LibraryManagementSystemMVC_Project.MiddleWares
{
    public class CheckLoggerMiddlewares
    {
        private readonly RequestDelegate _requestDelegate;

        public CheckLoggerMiddlewares(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            //request pipline:
            Console.WriteLine($"Request Pipline : Method Name {httpContext.Request.Method} and Path {httpContext.Request.Path}"); ;

            //for next middleware
            await _requestDelegate(httpContext);

            //Response pipline:
            Console.WriteLine($"Response Pipline : {httpContext.Response.StatusCode}");


        }
    }
}
