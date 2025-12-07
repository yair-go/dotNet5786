namespace BO;

public enum CourierShipmentType
{
    Car,
    Motorbike,
    Bicycle,
    OnFoot
}


/// <summary>
/// סוג הזמנה - מסוג מוצר או שירות.
/// </summary>
public enum OrderType
{
    Product,
    Service
}

/// <summary>
/// סטטוס הזמנה בהיבט של מצב המשלוח.
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// ההזמנה נפתחה אך עדיין לא שובצה לשליח
    /// </summary>
    Created,

    /// <summary>
    /// ההזמנה שובצה לשליח
    /// </summary>
    Assigned,

    /// <summary>
    /// השליח בדרך לאסוף את ההזמנה
    /// </summary>
    PickedUp,

    /// <summary>
    /// ההזמנה בדרך ללקוח
    /// </summary>
    InTransit,

    /// <summary>
    /// ההזמנה נמסרה ללקוח
    /// </summary>
    Delivered,

    /// <summary>
    /// ההזמנה בוטלה
    /// </summary>
    Cancelled
}

/// <summary>
/// סטטוס עמידה בזמנים של משלוח.
/// </summary>
public enum ScheduleStatus
{
    /// <summary>
    /// המשלוח צפוי להגיע בזמן (בטווח הצפוי)
    /// </summary>
    OnTime,

    /// <summary>
    /// המשלוח מאחר אך עדיין בטווח המירבי
    /// </summary>
    Delayed,

    /// <summary>
    /// המשלוח חרג מהזמן המירבי
    /// </summary>
    Late,

    /// <summary>
    /// המשלוח הסתיים בזמן
    /// </summary>
    CompletedOnTime,

    /// <summary>
    /// המשלוח הסתיים באיחור
    /// </summary>
    CompletedLate
}