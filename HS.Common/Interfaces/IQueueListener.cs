using System.Threading.Tasks;

namespace HS.Common.Interfaces
{
    public interface IQueueListener
    {
        Task MonitorQueue(IDataRepository repository); 
    }
}
