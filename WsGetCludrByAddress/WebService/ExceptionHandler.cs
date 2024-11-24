using Microsoft.AspNetCore.Diagnostics;

namespace WsGetCludrByAddress.WebService {
    public class ExceptionHandler : IExceptionHandler {
        private ILogger<ExceptionHandler> _logger = null;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)=>
            _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
            CancellationToken cancellationToken) {
            _logger.LogError(exception, exception.StackTrace);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Internal server error", cancellationToken);
            return true;
        }
    }
}
