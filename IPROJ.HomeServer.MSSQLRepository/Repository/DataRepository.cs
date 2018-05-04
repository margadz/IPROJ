using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.DataRepository;
using IPROJ.MSSQLRepository.Context;
using Microsoft.EntityFrameworkCore;

namespace IPROJ.MSSQLRepository.Repository
{
    public class DataRepository : IDataRepository
    {
        private readonly string _connectionString;

        public DataRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddDeviceAync(DeviceDescription device)
        {
            if (device == null)
            {
                return;
            }

            using (var context = GenerateContext())
            {
                var existingDevice = await context.Devices.Where(dev => dev.DeviceId == device.DeviceId).FirstOrDefaultAsync();
                if (existingDevice != null)
                {
                    existingDevice.IsActive = device.IsActive;
                    existingDevice.Name = device.Name;
                }
                else
                {
                    context.Devices.Add(device);
                }


                await context.SaveChangesAsync();
            }
        }

        public async Task AddReadingsAsync(IEnumerable<DeviceReading> readings)
        {
            if (readings == null || !readings.Any())
            {
                return;
            }

            using (var context = GenerateContext())
            {
                context.DeviceReadings.AddRange(readings);

                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<DeviceDescription>> GetAllDevicesAsync()
        {
            using (var context = GenerateContext())
            {
                return await(from device in context.Devices
                              select device).ToListAsync();
            }
        }

        public async Task<IEnumerable<DeviceReading>> GetAllReadingsAsync()
        {
            using (var context = GenerateContext())
            {
                return await(from reading in context.DeviceReadings
                              select reading).ToListAsync();
            }
        }

        public async Task<IEnumerable<DeviceReading>> GetAllReadingsFromDeviceAsync(Guid deviceId)
        {
            if (deviceId == null)
            {
                return Array.Empty<DeviceReading>();
            }

            using (var context = GenerateContext())
            {
                return await(from reading in context.DeviceReadings
                              where reading.DeviceId == deviceId
                              select reading).ToListAsync();
            }
        }

        private HomeServerDbContext GenerateContext()
        {
            return new HomeServerDbContext(_connectionString);
        }
    }
}
