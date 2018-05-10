namespace IPROJ.ConnectionBroker.Devices.Wemo.Commands
{
    public class GetInsightParamsWemoCommand : IWemoCommand
    {
        public static readonly GetInsightParamsWemoCommand Command = new GetInsightParamsWemoCommand();

        private GetInsightParamsWemoCommand()
        {
        }

        public string SoapAction { get; } = "urn:Belkin:service:insight:1#GetInsightParams";

        public string Payload { get; } = @"<?xml version=""1.0"" encoding=utf-8""?><s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><s:Body><u:GetInsightParams xmlns:u=""urn:Belkin:service:insight:1""></u:GetInsightParams></s:Body></s:Envelope>";

        public string ServiceType { get; } = "/upnp/control/insight1";
    }
}
