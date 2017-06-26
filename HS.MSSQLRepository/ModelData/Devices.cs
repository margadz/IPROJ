using System;
using System.Collections.Generic;

namespace HS.MSSQLRepository.ModelData
{
    public partial class Devices
    {
        public Devices()
        {
            InstrumentReadings = new HashSet<InstrumentReadings>();
        }

        public Guid DeviceId { get; set; }
        public string Name { get; set; }
        public string TypeOfReading { get; set; }
        public int? ReadingInterval { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<InstrumentReadings> InstrumentReadings { get; set; }
    }
}
