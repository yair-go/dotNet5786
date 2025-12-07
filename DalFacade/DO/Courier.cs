using System;

namespace DO;

/// <summary>
/// Data object for a courier (DAL layer).
/// Value correctness (format, checksums, password strength, etc.) is the responsibility of the business/logic layer;
/// the DAL assumes values are valid.
/// </summary>
public record Courier(
    int Id,
    string FullName,
    string Phone,
    string Email,
    DateTime StartWorkAt,
    string? Password = null,
    bool IsActive = true,
    double? MaxDeliveryDistanceKm = null,
    CourierShipmentType ShipmentType = CourierShipmentType.OnFoot
);

