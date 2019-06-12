using System;

namespace Services.Common
{
    public class Constants
    {
        public static int GetUserId()
        {
            return 1;
        }
        public static DateTime GetDateNow()
        {
            return DateTime.Now;
        }
        public static class ErrorMessageCodes
        {
            public static readonly string NoRecordsFound = "2";
            public static readonly string NoRecordsFoundMessage = "No records found.";
        }

    }
}
