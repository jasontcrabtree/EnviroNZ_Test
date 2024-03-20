using aspnet_api.Models;

namespace aspnet_api.Services
{
    public class SuburbService
    {
        private readonly ILogger<SuburbService> _logger;

        public SuburbService(ILogger<SuburbService> logger)
        {
            _logger = logger;
        }

        public static List<Suburb> GetAllSuburbs() => Suburbs;
    }
}
