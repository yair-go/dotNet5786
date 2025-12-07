using System;
using System.Collections.Generic;
using System.Linq;
using Dal;
using DO;

namespace DalTest
{
    /// <summary>
    /// Populate Dal.DataSource.Couriers with a fixed, reproducible collection of couriers.
    /// - >=20 couriers
    /// - most active, some inactive
    /// - all shipment types uniformly distributed
    /// - some couriers have a randomized MaxDeliveryDistanceKm within company max and sensible per-type ranges
    /// - StartWorkAt values are in the past (reasonable range)
    /// </summary>
    internal static class Initialization
    {
        public static void Initialize()
        {
            if (DataSource.Couriers.Count > 0) return; // already initialized

            const int count = 24;
            const double companyMaxKm = 50.0; // company-wide maximum distance (used as upper bound)
            var rnd = new Random(42); // fixed seed -> fixed collection (reproducible)

            string[] firstNames = { "Adi", "Eden", "Noam", "Yael", "Itai", "Shira", "Amit", "Lior", "Tal", "Neta", "Ori", "Gal" };
            string[] lastNames = { "Levi", "Cohen", "Peretz", "Mizrahi", "BenDavid", "Goldberg", "Klein", "Shapira", "Avraham", "Barak" };

            // Create a uniformly-distributed list of shipment types
            var types = Enum.GetValues(typeof(CourierShipmentType)).Cast<CourierShipmentType>().ToArray();
            var typePool = new List<CourierShipmentType>();
            for (int i = 0; i < count; i++)
            {
                typePool.Add(types[i % types.Length]);
            }
            // shuffle typePool to randomize assignment while keeping uniform distribution
            Shuffle(typePool, rnd);

            var couriers = new List<DO.Courier>(count);

            for (int i = 0; i < count; i++)
            {
                var fn = firstNames[i % firstNames.Length];
                var ln = lastNames[i % lastNames.Length];
                var id = 10000000 + i; // simple unique id
                var fullName = $"{fn} {ln}";
                var email = $"{fn}.{ln}.{i}@example.com".ToLowerInvariant();

                // Generate Israeli-like mobile numbers: 10 digits starting with '05'
                var phone = $"05{rnd.Next(20, 99):D2}{rnd.Next(0, 100000000):D8}";

                var shipmentType = typePool[i];

                // Decide active state: most are active (~80%)
                var isActive = rnd.NextDouble() < 0.80;

                // Decide whether to set a personal max delivery distance (about 60% will have a limit)
                double? maxDistance = null;
                if (rnd.NextDouble() < 0.60)
                {
                    // sensible ranges by transport mode (km), but never exceed companyMaxKm
                    (double min, double max) = shipmentType switch
                    {
                        CourierShipmentType.Car => (10.0, Math.Min(80.0, companyMaxKm)),
                        CourierShipmentType.Motorbike => (5.0, Math.Min(40.0, companyMaxKm)),
                        CourierShipmentType.Bicycle => (2.0, Math.Min(20.0, companyMaxKm)),
                        CourierShipmentType.OnFoot => (0.5, Math.Min(5.0, companyMaxKm)),
                        _ => (1.0, Math.Min(20.0, companyMaxKm))
                    };

                    // ensure min <= max
                    if (max < min) max = min;

                    // pick a double in range with one decimal
                    var value = min + rnd.NextDouble() * (max - min);
                    maxDistance = Math.Round(value, 1);
                }

                // StartWorkAt: random past time between 30 days and 5 years ago
                var daysAgo = rnd.Next(30, 5 * 365);
                var hoursOffset = rnd.Next(0, 24);
                var minutesOffset = rnd.Next(0, 60);
                var startWorkAt = DateTime.Now.AddDays(-daysAgo).AddHours(-hoursOffset).AddMinutes(-minutesOffset);

                // Password: only a few have an initial password set (DAL stores as-is; logic layer handles encryption)
                string? password = (rnd.NextDouble() < 0.2) ? $"InitPass#{i:00}" : null;

                // Create courier using positional record parameters (named for clarity)
                var courier = new DO.Courier(
                    Id: id,
                    FullName: fullName,
                    Phone: phone,
                    Email: email,
                    StartWorkAt: startWorkAt,
                    Password: password,
                    IsActive: isActive,
                    MaxDeliveryDistanceKm: maxDistance,
                    ShipmentType: shipmentType
                );

                couriers.Add(courier);
            }

            // add to shared data source
            DataSource.Couriers.AddRange(couriers);
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
}