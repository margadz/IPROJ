using System;

namespace IPROJ.ConnectionBroker.DevicesManager.HS110.Response
{
    public class DailyMessurement
    {
        public int year { get; set; }

        public int month { get; set; }

        public int day { get; set; }

        public decimal energy { get; set; }
    }
}
