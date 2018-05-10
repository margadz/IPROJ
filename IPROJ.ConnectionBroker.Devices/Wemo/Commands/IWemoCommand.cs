namespace IPROJ.ConnectionBroker.Devices.Wemo.Commands
{
    public interface IWemoCommand
    {
        string SoapAction { get; }

        string Payload { get; }

        string ServiceType { get; }
    }
}
