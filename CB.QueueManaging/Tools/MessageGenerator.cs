using System.Collections.Generic;
using System.Text;
using IPROJ.Contracts.DataModel;

namespace CB.QueueManaging.Tools
{
    public static class MessageGenerator
    {
        public static string GetMessage(DeviceReading reading)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(reading.DeviceId.ToString())
                .Append(";")
                .Append(reading.Value)
                .Append(";")
                .Append(reading.TypeOfReading)
                .Append(";")
                .Append(reading.ReadingTimeStamp);

            return sb.ToString();
        }

        public static string GetMessage(IList<DeviceReading> readings)
        {
            StringBuilder sb = new StringBuilder("");

            foreach (var read in readings)
            {
                sb.Append(GetMessage(read))
                    .Append("|");
            }

            return sb.ToString();
        }
    }
}
