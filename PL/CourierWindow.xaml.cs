using System;
using System.Linq;
using System.Windows;
using BO;
using DalFacade;

namespace PL
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CourierWindow : Window
    {
        // Parameterless constructor for designer support
        public CourierWindow()
        {
            InitializeComponent();
        }

        // Construct window by courier id: create a BO.Courier sample with pseudo-random values and set as DataContext.
        // Random is seeded with courierId so the same id produces the same sample.
        public CourierWindow(int courierId) : this()
        {
            // var boCourier = BL.Factory.Get().Courier().GetById(courierId);
            var boCourier = Init.GetCouriers().FirstOrDefault(courier => courier.Id == courierId);
            DataContext = boCourier;
        }

        private static Courier CreateSampleCourier(int id)
        {
            var rnd = new Random(id);

            string[] firstNames = { "Adi", "Eden", "Noam", "Yael", "Itai", "Shira", "Amit", "Lior", "Tal", "Neta", "Ori", "Gal" };
            string[] lastNames = { "Levi", "Cohen", "Peretz", "Mizrahi", "BenDavid", "Goldberg", "Klein", "Shapira", "Avraham", "Barak" };

            var first = firstNames[rnd.Next(firstNames.Length)];
            var last = lastNames[rnd.Next(lastNames.Length)];
            var fullName = $"{first} {last}";

            var email = $"{first}.{last}.{id}@example.com".ToLowerInvariant();

            // Israeli-like mobile: 10 digits starting with '05'
            var phone = $"05{rnd.Next(20, 100):D2}{rnd.Next(0, 100000000):D8}";

            // uniform shipment type selection
            var types = Enum.GetValues(typeof(CourierShipmentType)).Cast<CourierShipmentType>().ToArray();
            var shipmentType = types[rnd.Next(types.Length)];

            // sensible per-type max distance (km) and optional null
            double? maxDistance = null;
            if (rnd.NextDouble() < 0.6) // ~60% have a personal limit
            {
                const double companyMaxKm = 50.0;
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

            // StartWorkAt: between 30 days and 5 years ago
            var daysAgo = rnd.Next(30, 5 * 365);
            var startWorkAt = DateTime.Now.AddDays(-daysAgo).AddHours(-rnd.Next(0, 24)).AddMinutes(-rnd.Next(0, 60));

            // initial password present for a minority
            string? password = (rnd.NextDouble() < 0.2) ? $"Init{rnd.Next(1000, 9999)}" : null;

            var isActive = rnd.NextDouble() < 0.85; // most are active

            return new Courier
            {
                Id = id,
                FullName = fullName,
                Phone = phone,
                Email = email,
                Password = password,
                IsActive = isActive,
                MaxDeliveryDistanceKm = maxDistance,
                ShipmentType = shipmentType,
                StartWorkAt = startWorkAt,

                // computed / read-only BO fields - left as defaults for the sample
                TotalOrdersDeliveredOnTime = 0,
                TotalOrdersDeliveredLate = 0,
                CurrentOrder = null
            };
        }
    }
}
