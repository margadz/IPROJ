using System.Collections.Generic;

namespace CB.DevicesManager.HS110.Response
{
    public class DailyCommand
    {
        public DailyList get_daystat { get; set; }

        public int err_code { get; set; }
    }
}
