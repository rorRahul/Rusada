using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rusada.ViewModelLayer.Repositories
{
    public interface IRepositoryBase : IDisposable
    {
        bool CanDispose { get; set; }
        void Dispose(bool force);
    }
}
