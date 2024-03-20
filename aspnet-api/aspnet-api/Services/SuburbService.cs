using System.Text.Json;
using System.Text.Json.Serialization;
using aspnet_api.Models;

namespace aspnet_api.Services
{
    public class SuburbService
    {

        static List<Suburb>? Suburbs = new List<Suburb>();
        static SuburbService()
        {
            var json = File.ReadAllText("Data/Suburb.json");
            Suburbs = JsonSerializer.Deserialize<List<Suburb>>(json);
        }
        public static List<Suburb>? GetAllSuburbs() => Suburbs;

        public Suburb FindClosestSuburb(double latitude, double longitude)
        {

            var OrderedSuburbs = Suburbs?.OrderBy(suburb => GetDistance(suburb.Latitude, suburb.Longitude, latitude, longitude))
                            .FirstOrDefault();

            if (OrderedSuburbs == null)
            {
                throw new Exception("No suburb found");
            }

            return OrderedSuburbs;
        }

        private double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            lat1 = ToRadians(lat1);
            lat2 = ToRadians(lat2);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            const double earthRadius = 6371; // Kilometers
            return earthRadius * c;
        }

        private static double ToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
}