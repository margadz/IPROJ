using System;
using System.Text.RegularExpressions;
using IPROJ.Contracts.DataModel;

namespace IPROJ.ConnectionBroker.DevicesManager.Wemo.Response
{
    public class GetInsightParamsWemoResponse
    {
        private const string RegexPattern = @"<InsightParams>([0-9.|]*)<\/InsightParams>";
        private readonly Regex Regex = new Regex(RegexPattern);
        private readonly string _response;

        private GetInsightParamsWemoResponse(string rawResponse)
        {
            _response = rawResponse;
            ParseResponse();
        }

        public DeviceReading InstantReading { get; private set; }

        public DeviceReading DailyReading { get; private set; }

        public static GetInsightParamsWemoResponse FromRawResponse(string rawResponse)
        {
            return new GetInsightParamsWemoResponse(rawResponse);
        }

        private void ParseResponse()
        {
            if (string.IsNullOrEmpty(_response))
            {
                return;
            }

            var match = Regex.Match(_response);
            if (!match.Success)
            {
                return;
            }

            var split = match.Groups[1].Value.Split('|');

            if (split.Length != 11)
            {
                return;
            }

            try
            {
                var deviceState = (DeviceState)int.Parse(split[0]);
                var instantValue = decimal.Parse(split[7]) / 1000;
                var dailyValue = decimal.Parse(split[8]) * 1.6666667e-8m;

                InstantReading = new DeviceReading
                {
                    ReadingCharacter = ReadingCharacter.Instant,
                    DeviceState = deviceState,
                    Value = instantValue,
                    ReadingTimeStamp = DateTime.UtcNow,
                    TypeOfReading = ReadingType.PowerConsumption
                };

                DailyReading = new DeviceReading
                {
                    ReadingCharacter = ReadingCharacter.Daily,
                    DeviceState = deviceState,
                    Value = dailyValue,
                    ReadingTimeStamp = DateTime.UtcNow,
                    TypeOfReading = ReadingType.PowerConsumption
                };
            }
            catch (FormatException)
            {
                return;
            }
        }
    }
}
