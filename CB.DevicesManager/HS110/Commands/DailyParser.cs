﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CB.DevicesManager.HS110.Response;
using Newtonsoft.Json;

namespace CB.DevicesManager.HS110.Commands
{
    public static class DailyParser
    {
        public static async Task<IEnumerable<DailyMessurement>> AquireDailyPowerComsumption(HS110TcpConnector connector, DateTime date)
        {
            var response = await connector.QueryDevice(CommandStrings.MonthStat(date));

            return JsonConvert.DeserializeObject<DailyResponse>(response).emeter.get_daystat.day_list;
        }
    }
}