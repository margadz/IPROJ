using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.Devices.HS110.Commands;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Device.Discovery;

namespace IPROJ.ConnectionBroker.Devices.HS110.Discovery
{
    public class HS110DeviceFinder : IDeviceFinder
    {
        private const int Port = 9999;
        private const string Adress = "192.168.1.255";
        private const string RegexString = "model.{1,5}HS110";
        private static readonly Regex Regex = new Regex(RegexString);
        private readonly UdpClient _client;
        private readonly ICollection<byte[]> _buffer;
        private readonly byte[] _discoveryPacket = HS110Coding.Encrypt(Hs110Commands.SysInfo, false);
        private readonly IList<UdpReceiveResult> _results = new List<UdpReceiveResult>();

        /// <summary>Intilizes new instance of <see cref="HS110DeviceFinder"/>.</summary>
        public HS110DeviceFinder()
        {
            _client = new UdpClient();
            _client.Client.Bind(new IPEndPoint(IPAddress.Any, Port));
            _buffer = new List<byte[]>();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<DeviceDescription>> Discover(CancellationToken cancellationToken)
        {
            _results.Clear();
            using (var tokenSource = new CancellationTokenSource(500))
            {
                Task.Run(async () => await ListenToUdp(tokenSource.Token), tokenSource.Token);
                await _client.SendAsync(_discoveryPacket, _discoveryPacket.Length, Adress, Port);
                await Task.Delay(500, cancellationToken);
                tokenSource.Cancel();
            }

            return ParseResult();
        }

        private async Task ListenToUdp(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _results.Add(await _client.ReceiveAsync());
            }
        }


        private IEnumerable<DeviceDescription> ParseResult()
        {
            var result = new List<DeviceDescription>(_results.Count);

            foreach (var updResult in _results)
            {
                var payload = HS110Coding.Decrypt(updResult.Buffer, false);
                var match = Regex.Match(payload);
                if (match.Success)
                {
                    result.Add(new DeviceDescription() { Host = $"{updResult.RemoteEndPoint.Address}:{Port}", TypeOfDevice = DeviceType.HS110, TypeOfReading = ReadingType.PowerConsumption, IsActive = true });
                }
            }

            return result;
        }
    }
}
