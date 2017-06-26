using System;
using System.Collections.Generic;

namespace HS.MSSQLRepository.ModelData
{
    public partial class InstrumentReadings
    {
        public DateTime ReadingTimeStamp { get; set; }
        public decimal Value { get; set; }
        public Guid DeviceId { get; set; }
        public virtual Devices Device { get; set; }
    }
}
