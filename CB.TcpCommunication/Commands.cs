using System;
using System.Collections.Generic;
using System.Text;

namespace IPROJ_TcpCommunication
{
   public  class Commands
    {
        public const string Emeter = "{\"emeter\":{\"get_realtime\":{},\"get_vgain_igain\":{}}}";
        public const string TurnOff = "{\"system\":{\"set_relay_state\":{\"state\":0}}}";
        public const string TurnOn = "{\"system\":{\"set_relay_state\":{\"state\":1}}}";
        public const string SysInfoAndEmeter = "{\"system\":{\"get_sysinfo\":{}},\"emeter\":{\"get_realtime\":{},\"get_vgain_igain\":{}}}";
        public const string MonthStat = "{\"emeter\":{\"get_daystat\":{\"month\":5,\"year\":2017}}}";
    }
}
