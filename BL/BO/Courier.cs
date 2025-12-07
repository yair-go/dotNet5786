using System;

namespace BO
{
    /// <summary>
    /// Business logic entity for a courier.
    /// Contains properties from DO.Courier plus computed/aggregated fields.
    /// </summary>
    public class Courier
    {
        /// <summary>
        /// ת"ז שליח - מזהה יחיד. לא ניתן לעדכון.
        /// בצפיה - לקוח מ-DO.Courier
        /// בהוספה - נבדק תקינות בשכבה הלוגית
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// שם מלא (פרטי ומשפחה). ניתן לעדכון ע"י מנהל או שליח.
        /// בצפיה/הוספה/עדכון - נשמר ב-DO.Courier
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// טלפון סלולרי - מחרוזת בת 10 ספרות המתחילה ב-0. ניתן לעדכון ע"י מנהל או שליח.
        /// בצפיה - לקוח מ-DO.Courier
        /// בהוספה/עדכון - נבדק תקינות בשכבה הלוגית ונשמר ב-DO.Courier
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// אימייל - כתובת דואר אלקטרוני. ניתן לעדכון ע"י מנהל או שליח.
        /// בצפיה - לקוח מ-DO.Courier
        /// בהוספה/עדכון - נבדק תקינות בשכבת הלוגית ונשמר ב-DO.Courier
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// סיסמה (תוספת). ניתן לעדכון ע"י מנהל או שליח.
        /// בצפיה - לקוח מ-DO.Courier
        /// בהוספה/עדכון - נבדק תקינות (חוזק סיסמה, הצפנה) בשכבה הלוגית ונשמר ב-DO.Courier
        /// תכונה זו לא תהיה קיימת עבור מי שלא עושה את התוספת.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// האם השליח פעיל. ניתן לעדכון רק ע"י מנהל.
        /// בצפיה - לקוח מ-DO.Courier
        /// בהוספה/עדכון - נבדק תקינות בשכבה הלוגית ונשמר ב-DO.Courier
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// מרחק מירבי אישי למשלוח (קילומטרים). אם null אין הגבלה. ניתן לעדכון ע"י מנהל או שליח.
        /// בצפיה - לקוח מ-DO.Courier
        /// בהוספה/עדכון - נבדק תקינות בשכבה הלוגית ונשמר ב-DO.Courier
        /// </summary>
        public double? MaxDeliveryDistanceKm { get; set; }

        /// <summary>
        /// סוג השילוח (רכב/אופנוע/אופניים/רגלי). ניתן לעדכון ע"י מנהל או שליח.
        /// בצפיה - לקוח מ-DO.Courier
        /// בהוספה/עדכון - נבדק תקינות בשכבה הלוגית ונשמר ב-DO.Courier
        /// </summary>
        public CourierShipmentType ShipmentType { get; set; }

        /// <summary>
        /// זמן תחילת עבודה בחברה. לא ניתן לעדכון.
        /// תכונה לצפיה בלבד - לקוח מ-DO.Courier
        /// בהוספה - מיוצר מתוך שכבת הנתונים בזמן יצירת DO.Courier
        /// </summary>
        public DateTime StartWorkAt { get; init; }

        /// <summary>
        /// סך ההזמנות שסיפק בזמן. תכונה לצפיה בלבד - מחושב בשכבה הלוגית.
        /// ספירת משלוחים עם סוג סיום "סופק" וזמן סיום קטן/שווה מזמן אספקה מירבי.
        /// </summary>
        public int TotalOrdersDeliveredOnTime { get; init; }

        /// <summary>
        /// סך ההזמנות שסיפק באיחור. תכונה לצפיה בלבד - מחושב בשכבה הלוגית.
        /// ספירת משלוחים עם סוג סיום "סופק" וזמן סיום גדול מזמן אספקה מירבי.
        /// </summary>
        public int TotalOrdersDeliveredLate { get; init; }

        /// <summary>
        /// הזמנה בטיפול שליח - תכונה לצפיה בלבד.
        /// שאילתא בשכבה הלוגית המחזירה BO.OrderInProgress במידה וקיימת הזמנה בטיפולו של השליח.
        /// אחרת - null.
        /// </summary>
        public OrderInProgress? CurrentOrder { get; init; }
    }
}   