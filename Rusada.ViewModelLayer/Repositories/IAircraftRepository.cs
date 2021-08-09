using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rusada.ViewModelLayer.Repositories
{
    public interface IAircraftRepository : IRepositoryBase
    {
        List<AirList> GetAirList();
        AirList GetAirDetails(string AirID);
       string UpdateAircraftInfo(AirList editairDetailVM);
        string insertAircraftInfo(AirList newairDetailVM);
    }
}
