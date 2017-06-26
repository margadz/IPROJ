using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HS.MSSQLRepository.Context;
using HS.MSSQLRepository.ModelData;
using HS.MSSQLRepository.Tools;
using IPROJ.Contracts.Data;
using IPROJ.Contracts.DataModel;
using Microsoft.EntityFrameworkCore;

namespace HS.MSSQLRepository.Repository
{
    public class MSSQLDataRepository : IDataRepository
    {
        private HomeServerContext context;
        private readonly string _connectionString;

        public MSSQLDataRepository()
            : this(@"Data Source=KOMP;Initial Catalog=HomeServer;Integrated Security=True")
        { }

        public MSSQLDataRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddDeviceAsync(Device device)
        {
            throw new NotImplementedException();
        }

        public async Task AddReadingsAsync(IEnumerable<DeviceReading> reading)
        {
            List<InstrumentReadings> rawReadings = new List<InstrumentReadings>();
            foreach(var read in reading)
            {
                rawReadings.Add(new InstrumentReadings()
                {
                    DeviceId = read.DeviceId,
                    ReadingTimeStamp = read.ReadingTimeStamp,
                    Value = read.Value
                });
            }
            using (context = Context)
            {
                await context.InstrumentReadings.AddRangeAsync(rawReadings);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Device>> GetAllActiveDevicesAsync()
        {
            List<Devices> dbDevices;
            using (context = Context)
            {
                dbDevices = await (from device in context.Devices
                                   where device.IsActive == true
                                   select device).ToListAsync();
            }
            return DbToOutputConverter.GetDevicesFromDbEntities(dbDevices);
        }

        public async Task<List<Device>> GetAllDevicesAsync()
        {
            List<Devices> dbDevices;
            using (context = Context)
            {
                dbDevices = await context.Devices.ToListAsync();
            }
            return DbToOutputConverter.GetDevicesFromDbEntities(dbDevices);
        }

        public async Task<List<Device>> GetAllInactiveDevicesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<DeviceReading>> GetAllReadingsAsync()
        {
            List<InstrumentReadings> dbResults;
            List<Devices> dbDevices;
            using (context = Context)
            {
                dbDevices = await context.Devices.ToListAsync();
                dbResults = await context.InstrumentReadings.ToListAsync();
            }

            return DbToOutputConverter.GetDeviceReadingFromDbEntities(dbResults, dbDevices);
        }

        public async Task<List<DeviceReading>> GetAllReadingsFromDeviceAsync(Guid deviceId)
        {
            List<InstrumentReadings> dbResults;
            List<Devices> dbDevices;
            using (context = Context)
            {
                dbDevices = await context.Devices.Where(x => x.DeviceId == deviceId).ToListAsync();
                dbResults = await context.InstrumentReadings.Where(x => x.DeviceId == deviceId).ToListAsync();
            }

            return DbToOutputConverter.GetDeviceReadingFromDbEntities(dbResults, dbDevices);
        }

        public async Task<List<DeviceReading>> GetAllReadingsOfTypeAsync(string type)
        {
            List<InstrumentReadings> dbResults;
            List<Devices> dbDevices;
            using (context = Context)
            {
                dbDevices = await context.Devices.Where(x => x.TypeOfReading == type && x.IsActive == true).ToListAsync();
                dbResults = await (from readings in context.InstrumentReadings
                                   join devices in dbDevices on readings.DeviceId equals devices.DeviceId
                                   select readings).ToListAsync();
            }
            return DbToOutputConverter.GetDeviceReadingFromDbEntities(dbResults, dbDevices);
        }

        public async Task<List<DeviceReading>> GetLastReadingsAsync()
        {
            List<InstrumentReadings> dbResults;
            List<Devices> dbDevices;
            List<InstrumentReadings> tmpDbResults = new List<InstrumentReadings>();
            using (context = Context)
            {
                dbDevices = await context.Devices.Where(x => x.IsActive == true).ToListAsync();
                dbResults = await context.InstrumentReadings.ToListAsync();
                foreach (var device in dbDevices.Where(r => r.InstrumentReadings.Count > 0))
                {
                    tmpDbResults.Add((from reading in dbResults
                                      where reading.DeviceId == device.DeviceId
                                      orderby reading.ReadingTimeStamp descending
                                      select reading).FirstOrDefault());
                }
            }

            return DbToOutputConverter.GetDeviceReadingFromDbEntities(tmpDbResults, dbDevices);
        }

        public async Task RemoveDeviceAsync(Device device)
        {
            throw new NotImplementedException();
        }

        public async Task SetDeviceActivityAsync(Device device, bool isActive)
        {
            throw new NotImplementedException();
        }

        private HomeServerContext Context
        {
            get
            {
                return new HomeServerContext(_connectionString);
            }
        }
    }
}