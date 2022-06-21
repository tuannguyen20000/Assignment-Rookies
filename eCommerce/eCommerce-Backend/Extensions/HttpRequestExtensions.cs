namespace eCommerce_Backend.Extensions
{
    public static class HttpRequestExtensions
    {
        public static bool TryGetBearerToken(this HttpRequest httpRequest, out string token)
        {
            string authorization = httpRequest.Headers.ContainsKey("Authorization") ? httpRequest.Headers["Authorization"] : string.Empty;

            if (!string.IsNullOrWhiteSpace(authorization) && authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = authorization.Substring("Bearer ".Length).Trim();
                return true;
            }
            token = null;
            return false;
        }
    }
}
