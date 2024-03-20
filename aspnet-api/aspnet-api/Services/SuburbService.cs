using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using aspnet_api.Models;
using Microsoft.Extensions.Logging;

namespace aspnet_api.Services
{
    public class SuburbService
    {
        private static List<Suburb>? Suburbs { get; set; }
        private static ILogger<SuburbService>? logger;

        public SuburbService(ILogger<SuburbService> logger)
        {
            SuburbService.logger = logger;
            InitializeSuburbs();
        }

        private static void InitializeSuburbs()
        {
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "suburb.json");
                var json = File.ReadAllText(filePath);
                Suburbs = JsonSerializer.Deserialize<List<Suburb>>(json);
                logger?.LogInformation("Successfully loaded suburbs.");
                Console.WriteLine(Directory.GetCurrentDirectory());
            }
            catch (FileNotFoundException ex)
            {
                logger?.LogError(ex, "Suburb data file not found.");
            }
            catch (JsonException ex)
            {
                logger?.LogError(ex, "Error deserializing suburb data.");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Unexpected error loading suburb data.");
            }
        }

        public static List<Suburb>? GetAllSuburbs() => Suburbs;

        public Suburb FindClosestSuburb(double latitude, double longitude)
        {
            var closestSuburb = Suburbs?.OrderBy(suburb => GetDistance(suburb.Latitude, suburb.Longitude, latitude, longitude))
                            .FirstOrDefault();

            if (closestSuburb == null)
            {
                throw new Exception("No suburb found");
            }

            return closestSuburb;
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