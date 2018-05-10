
namespace IPROJ.ConnectionBroker.Devices.Wemo.Commands
{
    public class SetInsightStateCommand : IWemoCommand
    {
        public static SetInsightStateCommand On = new SetInsightStateCommand(1);
        public static SetInsightStateCommand Off = new SetInsightStateCommand(0);
        private readonly int _state;

        private SetInsightStateCommand(int state)
        {
            _state = state;
        }

        public string SoapAction { get; } = "urn:Belkin:service:basicevent:1#SetBinaryState";

        public string Payload
        {
            get
            {
                return $@"<?xml version=""1.0"" encoding=""utf-8""?><s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><s:Body><u:SetBinaryState xmlns:u=""urn:Belkin:service:basicevent:1""><BinaryState>{_state.ToString()}</BinaryState></u:SetBinaryState></s:Body></s:Envelope>";
            }
        }

        public string ServiceType { get; } = "/upnp/control/basicevent1";
    }
}
