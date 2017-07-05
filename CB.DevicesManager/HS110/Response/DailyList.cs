using System;
using System.Collections.Generic;
using System.Text;

namespace CB.DevicesManager.HS110.Response
{
    public class DailyList
    {
        public IEnumerable<DailyMessurement> day_list { get; set; }
    }
}
