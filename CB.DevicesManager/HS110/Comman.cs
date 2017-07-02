namespace CB.DevicesManager.HS110
{
    public class Comman
    {
        public const string Emeter = "{\"emeter\":{\"get_realtime\":{}}}";
        public const string TurnOff = "{\"system\":{\"set_relay_state\":{\"state\":0}}}";
        public const string TurnOn = "{\"system\":{\"set_relay_state\":{\"state\":1}}}";
        public const string SysInfo = "{\"system\":{\"get_sysinfo\":{}}}";
        public const string MonthStat = "{\"emeter\":{\"get_daystat\":{\"month\":7,\"year\":2017}}}";
    }
}
