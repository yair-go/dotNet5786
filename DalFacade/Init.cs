using System;
using System.Collections.Generic;
using System.Linq;
using DO;

namespace DalFacade;

public class Init
{
    public static List<Order> GetOrders()
    {
        var orders = new List<Order>
        {
            new Order(1, 101, 5, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-5), "Tel Aviv", 120.5, true),
            new Order(2, 102, null, DateTime.Now.AddDays(-9), null, "Jerusalem", 80.0, false),
            new Order(3, 103, 7, DateTime.Now.AddDays(-8), DateTime.Now.AddDays(-3), "Haifa", 95.75, true),
            new Order(4, 104, 3, DateTime.Now.AddDays(-7), null, "Eilat", 150.0, false),
            new Order(5, 105, 2, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-1), "Beer Sheva", 60.0, true),
            new Order(6, 106, null, DateTime.Now.AddDays(-5), null, "Ashdod", 45.9, false),
            new Order(7, 107, 4, DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-2), "Rishon Lezion", 110.0, true),
            new Order(8, 108, 6, DateTime.Now.AddDays(-3), null, "Netanya", 72.3, false),
            new Order(9, 109, 1, DateTime.Now.AddDays(-2), DateTime.Now, "Holon", 130.8, true),
            new Order(10, 110, null, DateTime.Now.AddDays(-1), null, "Kfar Saba", 50.0, false)
        };
        return orders;
    }

    public static List<Courier> GetCouriers()
    {
        const int count = 24; // at least 20 couriers
        const double companyMaxKm = 50.0;
        var rnd = new Random(42); // fixed seed for reproducible sample

        string[] firstNames = { "Adi", "Eden", "Noam", "Yael", "Itai", "Shira", "Amit", "Lior", "Tal", "Neta", "Ori", "Gal" };
        string[] lastNames = { "Levi", "Cohen", "Peretz", "Mizrahi", "BenDavid", "Goldberg", "Klein", "Shapira", "Avraham", "Barak" };

        // prepare uniform distribution of shipment types
        var types = Enum.GetValues(typeof(CourierShipmentType)).Cast<CourierShipmentType>().ToArray();
        var typePool = new List<CourierShipmentType>();
        for (int i = 0; i < count; i++) typePool.Add(types[i % types.Length]);
        Shuffle(typePool, rnd);

        var couriers = new List<Courier>(count);

        for (int i = 0; i < count; i++)
        {
            var first = firstNames[rnd.Next(firstNames.Length)];
            var last = lastNames[rnd.Next(lastNames.Length)];
            var id = 10000000 + i + 1;
            var fullName = $"{first} {last}";
            var email = $"{first}.{last}.{i}@example.com".ToLowerInvariant();

            // Israeli-like mobile number: 10 digits starting with '05'
            var phone = $"05{rnd.Next(0, 10)}{rnd.Next(0, 10000000):D7}";

            var shipmentType = typePool[i];

            // most active (~80%)
            var isActive = rnd.NextDouble() < 0.80;

            // optional personal max distance (~60% have a limit), sensible per-type ranges and not above company max
            double? maxDistance = null;
            if (rnd.NextDouble() < 0.60)
            {
                (double min, double max) = shipmentType switch
                {
                    CourierShipmentType.Car => (10.0, Math.Min(80.0, companyMaxKm)),
                    CourierShipmentType.Motorbike => (5.0, Math.Min(40.0, companyMaxKm)),
                    CourierShipmentType.Bicycle => (2.0, Math.Min(20.0, companyMaxKm)),
                    CourierShipmentType.OnFoot => (0.5, Math.Min(5.0, companyMaxKm)),
                    _ => (1.0, Math.Min(20.0, companyMaxKm))
                };
                if (max < min) max = min;
                var value = min + rnd.NextDouble() * (max - min);
                maxDistance = Math.Round(value, 1);
            }

            // StartWorkAt between 30 days and 5 years ago
            var daysAgo = rnd.Next(30, 5 * 365);
            var startWorkAt = DateTime.Now.AddDays(-daysAgo).AddHours(-rnd.Next(0, 24)).AddMinutes(-rnd.Next(0, 60));

            // initial password for some
            string? password = (rnd.NextDouble() < 0.2) ? $"Init{rnd.Next(1000, 9999)}" : null;

            var courier = new Courier(
                id,
                fullName,
                phone,
                email,
                startWorkAt,
                password,
                isActive,
                maxDistance,
                shipmentType
            );

            couriers.Add(courier);
        }

        return couriers;
    }

    private static void Shuffle<T>(IList<T> list, Random rnd)
    {
        for (int n = list.Count; n > 1; n--)
        {
            int k = rnd.Next(n);
            (list[k], list[n - 1]) = (list[n - 1], list[k]);
        }
    }
}