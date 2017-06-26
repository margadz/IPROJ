using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CB.Common.Interfaces
{
    public interface IQueueWriter : IDisposable
    {
        Task Put(string message);
    }
}
