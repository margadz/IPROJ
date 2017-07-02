using System.Diagnostics.CodeAnalysis;

namespace CB.DevicesManager.HS110.Response
{
    public class SystemInformation
    {
        public string err_code { get; set; }
        public string sw_ver { get; set; }
        public string hw_ver { get; set; }
        public string type { get; set; }
        public string model { get; set; }
        public string mac { get; set; }
        public string deviceId { get; set; }
        public string hwId { get; set; }
        public string fwId { get; set; }
        public string oemId { get; set; }
        public string alias { get; set; }    
        public string dev_name { get; set; }
        public string icon_hash { get; set; }
        public string relay_state { get; set; }
        public string on_time { get; set; }
        public string active_mode { get; set; }
        public string feature { get; set; }
        public string updating { get; set; }
        public string rssi { get; set; }
        public string led_off { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
}
