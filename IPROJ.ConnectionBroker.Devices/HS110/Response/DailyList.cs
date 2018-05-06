using System;
using System.Collections.Generic;
using System.Text;

namespace IPROJ.ConnectionBroker.Devices.HS110.Response
{
    public class DailyList
    {
        public IEnumerable<DailyMessurement> day_list { get; set; }
    }
}
