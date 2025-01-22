namespace CleanArchitecture.APIs.Middleware
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IConfiguration configuration)
        {
            if (!context.Request.Path.StartsWithSegments("/api"))
            {
                await _next(context);
                return;
            }

            var localizationConfig = configuration.GetSection("Localization");

            if(!localizationConfig.Exists())
            {
                await _next(context);
                return;
            }

            var languages = localizationConfig.GetSection("Languages").Get<List<string>>() ?? new List<string>();
            var headerName = localizationConfig["HeaderParams"] ?? "Accept-Language";

            if (!context.Request.Headers.ContainsKey(headerName))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync($"Missing '{headerName}' header.");
                return;
            }

            var language = context.Request.Headers[headerName].FirstOrDefault();

            if (!languages.Contains(language))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync($"Invalid '{headerName}' header value. Allowed values: {string.Join(", ", languages)}.");
                return;
            }

            var culture = new System.Globalization.CultureInfo(language);
            System.Globalization.CultureInfo.CurrentCulture = culture;
            System.Globalization.CultureInfo.CurrentUICulture = culture;

            context.Response.Headers.Add("X-Culture", culture.EnglishName);
            context.Response.Headers.Add("X-Language", language);

            await _next(context);
        }
    }
}
