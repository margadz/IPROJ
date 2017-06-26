using HS.Common.Enumerators;
using HS.Common.Exception;
using HS.Common.OutputModel;
using HS.MSSQLRepository.ModelData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HS.MSSQLRepository.Tools
{
    public class DbToOutputConverter
    {
        public static List<DeviceReading> GetDeviceReadingFromDbEntities(List<InstrumentReadings> readings, List<Devices> devices)
        {
            List<DeviceReading> result = new List<DeviceReading>();


            foreach (var dbresult in readings)
            {
                Guid deviceId;

                try
                {
                    deviceId = devices.Where(d => d.DeviceId == dbresult.DeviceId).First().DeviceId;
                }
                catch
                {
                    throw new ConverterException($"DeviceId: {dbresult.DeviceId} could not been found in devices list");
                }

                ReadingType type;
                string typeString = devices.Where(d => d.DeviceId == dbresult.DeviceId).First().TypeOfReading;
                try
                {
                    type = (ReadingType)Enum.Parse(typeof(ReadingType), typeString);
                }
                catch
                {
                    throw new ReadingTypeCastException($"A type: {typeString} is not valid reading type");
                }

                DeviceReading read = new DeviceReading(
                    dbresult.ReadingTimeStamp,
                    dbresult.Value,
                    deviceId,
                    type);
                result.Add(read);
            }

            return result;
        }

        public static List<Device> GetDevicesFromDbEntities(List<Devices> devices)
        {
            List<Device> result = new List<Device>();

            foreach (var dbDevice in devices)
            {
                ReadingType type;
                string typeString = dbDevice.TypeOfReading;
                try
                {
                    type = (ReadingType)Enum.Parse(typeof(ReadingType), typeString);
                }
                catch
                {
                    throw new ReadingTypeCastException($"A type: {typeString} is not valid reading type");
                }

                Device device = new Device(
                    dbDevice.DeviceId,
                    dbDevice.Name,
                    type,
                    dbDevice.ReadingInterval.GetValueOrDefault(),
                    dbDevice.IsActive
                    );
                result.Add(device);
            }
            return result;
        }
    }
}
