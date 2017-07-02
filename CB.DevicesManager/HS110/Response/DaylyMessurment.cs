using System;
using System.Collections.Generic;
using System.Text;

namespace CB.DevicesManager.HS110.Response
{
    public class DaylyMessurment
    {
        public DaylyMessurment()
        {
        }

        public DaylyMessurment(int year, int month, int day, decimal power)
        {
            Date = new DateTime(year, month, day);
            PowerComsumption = power;
        }

        public DateTime Date { get; set; }

        public decimal PowerComsumption { get; set; }
    }
}
