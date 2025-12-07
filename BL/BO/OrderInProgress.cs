using System;

namespace BO;

/// <summary>
/// Represents an order currently being handled by a courier.
/// Read-only entity used for display purposes in BO.Courier.
/// All values are calculated/fetched from DO.Order and DO.Delivery in the business logic layer.
/// </summary>
public class OrderInProgress
{
    /// <summary>
    /// מספר מזהה של ישות המשלוח. לא יופיע בתצוגה.
    /// נלקח מ-DO.Delivery על ידי חיפוש משלוח פתוח (לא נסגר) עם Id של השליח.
    /// </summary>
    public int DeliveryId { get; init; }

    /// <summary>
    /// מספר מזהה רץ של ישות ההזמנה.
    /// נלקח מ-DO.Delivery על ידי חיפוש משלוח פתוח (לא נסגר) עם Id של השליח.
    /// </summary>
    public int OrderId { get; init; }

    /// <summary>
    /// סוג הזמנה (מוצר/שירות).
    /// נלקח מ-DO.Order.
    /// </summary>
    public OrderType OrderType { get; init; }

    /// <summary>
    /// תיאור מילולי של ההזמנה (אופציונלי).
    /// נלקח מ-DO.Order.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// כתובת מלאה של ההזמנה.
    /// נלקח מ-DO.Order.
    /// </summary>
    public string DeliveryAddress { get; init; } = string.Empty;

    /// <summary>
    /// מרחק אווירי מהחברה (קילומטרים).
    /// מחושב בשכבה הלוגית - חישוב מתמטי פשוט שאינו מושפע מסוג השילוח.
    /// </summary>
    public double AirDistanceKm { get; init; }

    /// <summary>
    /// מרחק בפועל (קילומטרים). אופציונלי.
    /// נלקח מ-DO.Delivery.
    /// </summary>
    public double? ActualDistanceKm { get; init; }

    /// <summary>
    /// שם מלא של המזמין.
    /// נלקח מ-DO.Order (או מצורף מישות לקוח).
    /// </summary>
    public string CustomerFullName { get; init; } = string.Empty;

    /// <summary>
    /// טלפון של המזמין.
    /// נלקח מ-DO.Order (או מצורף מישות לקוח).
    /// </summary>
    public string CustomerPhone { get; init; } = string.Empty;

    /// <summary>
    /// זמן פתיחת ההזמנה.
    /// נלקח מ-DO.Order.
    /// </summary>
    public DateTime OrderCreatedAt { get; init; }

    /// <summary>
    /// זמן תחילת המשלוח.
    /// נלקח מ-DO.Delivery.
    /// </summary>
    public DateTime DeliveryStartedAt { get; init; }

    /// <summary>
    /// זמן אספקה צפוי.
    /// מחושב בשכבה הלוגית על פי זמן תחילת המשלוח הנוכחי ובהתחשב בסוג השילוח
    /// (מרחק ההזמנה ומהירות ממוצעת של השליח).
    /// </summary>
    public DateTime ExpectedDeliveryTime { get; init; }

    /// <summary>
    /// זמן אספקה מירבי.
    /// מחושב בשכבה הלוגית בהתבסס על טווח האספקה המירבי שהחברה התחייבה אליו
    /// ועל הזמן שבו ההזמנה נפתחה (בלי להתחשב בסוג השילוח).
    /// </summary>
    public DateTime MaxDeliveryTime { get; init; }

    /// <summary>
    /// סטטוס הזמנה בהיבט של מצב המשלוח האחרון.
    /// מחושב בשכבה הלוגית על סמך הנתונים ב-DO.Order ו-DO.Delivery.
    /// </summary>
    public OrderStatus OrderStatus { get; init; }

    /// <summary>
    /// סטטוס עמידה בזמנים של המשלוח האחרון.
    /// מחושב בשכבה הלוגית על סמך הנתונים ב-DO.Order, DO.Delivery וישות התצורה.
    /// </summary>
    public ScheduleStatus ScheduleStatus { get; init; }

    /// <summary>
    /// סך זמן שנותר לסיום ההזמנה.
    /// הפרש הזמנים בין זמן האספקה המירבי של ההזמנה לבין הזמן הנוכחי של שעון המערכת.
    /// </summary>
    public TimeSpan TimeRemainingUntilMaxDelivery { get; init; }
}