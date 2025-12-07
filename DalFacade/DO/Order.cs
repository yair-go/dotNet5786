using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

public record Order
(
    int Id,
    int CustomerId,
    int? CourierId,
    DateTime OrderDate,
    DateTime? DeliveryDate,
    string? DeliveryAddress,
    double TotalPrice,
    bool IsDelivered
);
