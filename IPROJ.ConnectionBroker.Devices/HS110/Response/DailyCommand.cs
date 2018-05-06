using System.Collections.Generic;
using IPROJ.ConnectionBroker.Devices.HS110.Response;

namespace IPROJ.ConnectionBroker.Devices.HS110
{
    public class DailyCommand
    {
        public DailyList get_daystat { get; set; }

        public int err_code { get; set; }
    }
}
