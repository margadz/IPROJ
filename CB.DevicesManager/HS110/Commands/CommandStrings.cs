using System;
using System.Text;

namespace CB.DevicesManager.HS110.Commands
{
    public class CommandStrings
    {
        public const string Emeter = "{\"emeter\":{\"get_realtime\":{}}}";
        public const string TurnOff = "{\"system\":{\"set_relay_state\":{\"state\":0}}}";
        public const string TurnOn = "{\"system\":{\"set_relay_state\":{\"state\":1}}}";
        public const string SysInfo = "{\"system\":{\"get_sysinfo\":{}}}";

        public static string MonthStat (DateTime date)
        {
            StringBuilder sb = new StringBuilder(string.Empty);

            sb.Append("{\"emeter\":{\"get_daystat\":{\"month\":");
            sb.Append(date.Month);
            sb.Append(",\"year\":");
            sb.Append(date.Year);
            sb.Append("}}}");
            return sb.ToString();
        }
            
    }
}
