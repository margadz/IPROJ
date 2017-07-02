namespace CB.DevicesManager.HS110.Response
{
    public class RealTimeMessurement
    {
        public RealTimeMessurement(decimal current, decimal voltage, decimal power, decimal total, int error)
        {
            Current = current;
            Voltage = voltage;
            Power = power;
            Total = total;
            Error = error;
        }

        public decimal Current { get; }

        public decimal Voltage { get; }

        public decimal Power { get; }

        public decimal Total { get; }

        public int Error { get; }
    }
}
