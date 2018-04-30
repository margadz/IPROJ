using System;
using System.Collections.Generic;
using FluentAssertions;
using IPROJ.ConnectionBroker.DevicesManager.Wemo.Response;
using IPROJ.Contracts.DataModel;
using NUnit.Framework;

namespace IPROJ.Given_instance_of.InsightParamsWemoResponse_class
{
    [TestFixture]
    public class when_serializing
    {
        private DateTime dateTime = DateTime.UtcNow;

        [Test]
        [TestCaseSource(nameof(GetInstantTestData))]
        public void Should_parse_instant_reading_correctly(string rawResponse, DeviceReading resultReadings)
        {
            ReadingComparision(GetInsightParamsWemoResponse.FromRawResponse(rawResponse).InstantReading, resultReadings).Should().BeTrue();
        }

        [Test]
        [TestCaseSource(nameof(GetDailyTestData))]
        public void Should_parse_daily_reading_correctly(string rawResponse, DeviceReading resultReadings)
        {
            ReadingComparision(GetInsightParamsWemoResponse.FromRawResponse(rawResponse).DailyReading, resultReadings).Should().BeTrue();
        }

        private static IEnumerable<TestCaseData> GetDailyTestData()
        {
            return new[]
            {
                new TestCaseData(
                        @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><s:Body><u:GetInsightParamsResponse xmlns:u=""urn:Belkin:service:metainfo:1""><InsightParams>1|1524153919|372|682|667|1209600|295|89625|3230381|3230381.000000|8000</InsightParams></u:GetInsightParamsResponse></s:Body></s:Envelope>",
                        new DeviceReading(DateTime.UtcNow, 0.053839684410127m, Guid.Empty, ReadingType.PowerComsumption, ReadingCharacter.Daily, DeviceState.On)).SetName("Should_parse_daily_reading_correctly"),
                new TestCaseData(
                        @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><s:Body><u:GetInsightParamsResponse xmlns:u=""urn:Belkin:service:metainfo:1""><InasdtParams>1|1524153919|372|682|667|1209600|295|89625|3230381|3230381.000000|8000</InsightParams></u:GetInsightParamsResponse></s:Body></s:Envelope>",
                        null).SetName("Should_return_null_daily_reading_if_markup_doesnt_match"),
                new TestCaseData(
                        @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><s:Body><u:GetInsightParamsResponse xmlns:u=""urn:Belkin:service:metainfo:1""><InsightParams>1|1524153919|372|682}1209600|295|89625|3230381|3230381.000000|8000</InsightParams></u:GetInsightParamsResponse></s:Body></s:Envelope>",
                        null).SetName("Should_return_null_if_data_is_incomplete")
            };
        }

        private static IEnumerable<TestCaseData> GetInstantTestData()
        {
            return new[]
            {
                new TestCaseData(
                        @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><s:Body><u:GetInsightParamsResponse xmlns:u=""urn:Belkin:service:metainfo:1""><InsightParams>1|1524153919|372|682|667|1209600|295|89625|3230381|3230381.000000|8000</InsightParams></u:GetInsightParamsResponse></s:Body></s:Envelope>",
                        new DeviceReading(DateTime.UtcNow, 89.625m, Guid.Empty, ReadingType.PowerComsumption, ReadingCharacter.Instant, DeviceState.On)).SetName("Should_parse_instant_reading_correctly"),
                new TestCaseData(
                        @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><s:Body><u:GetInsightParamsResponse xmlns:u=""urn:Belkin:service:metainfo:1""><InasdtParams>1|1524153919|372|682|667|1209600|295|89625|3230381|3230381.000000|8000</InsightParams></u:GetInsightParamsResponse></s:Body></s:Envelope>",
                        null).SetName("Should_return_null_instant_reading_if_markup_doesnt_match"),
                new TestCaseData(
                        @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><s:Body><u:GetInsightParamsResponse xmlns:u=""urn:Belkin:service:metainfo:1""><InsightParams>1|1524153919|372|682}1209600|295|89625|3230381|3230381.000000|8000</InsightParams></u:GetInsightParamsResponse></s:Body></s:Envelope>",
                        null).SetName("Should_return_null_instant_reading_if_data_is_incomplete")
            };
        }

        private bool ReadingComparision(DeviceReading one, DeviceReading two)
        {
            if (one == null && two == null)
            {
                return true;
            }

            if (one == null || two == null)
            {
                return false;
            }

            return one.DeviceState == two.DeviceState && one.Value == two.Value && one.TypeOfReading == two.TypeOfReading && one.ReadingCharacter == two.ReadingCharacter;
        }
    }
}
