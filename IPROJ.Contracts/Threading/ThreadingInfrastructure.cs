using System.Threading;

namespace IPROJ.Contracts.Threading
{
    public class ThreadingInfrastructure : IThreadingInfrastructure
    {
        private CancellationTokenSource _tokenSource;

        public ThreadingInfrastructure()
        {
            _tokenSource = new CancellationTokenSource();
        }

        public CancellationToken CancellationToken
        {
            get { return _tokenSource.Token; }
        }

        public void Cancel()
        {
            _tokenSource.Cancel();
        }

        public void Dispose()
        {
            _tokenSource.Dispose();
        }
    }
}
