using Rusada.ViewModelLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rusada.ViewModelLayer;
using Dapper;

namespace Rusada.DataLayer
{
    public class AircraftRepository : RepositoryBase, IAircraftRepository
    {
        public AircraftRepository(string connectionString) : base(connectionString)
        {
            
            // EditBrolerDetail GetAirCraftDetails(string AirID);
        }
        public List<AirList> GetAirList()
        {
            var lst = Query<AirList>("dbo.sp_GetAirList").ToList();
            return lst;
        }
        public AirList GetAirDetails(string AirID)
        {
            var airdetail = Query<AirList>("dbo.sp_GetAirDetail", new { AirID = AirID }).FirstOrDefault();
            return airdetail;
        }
        public string UpdateAircraftInfo(AirList editairDetailVM)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Model", editairDetailVM.Model);
                p.Add("@Make", editairDetailVM.Make);
                p.Add("@Registration", editairDetailVM.Registration);
                p.Add("@Location", editairDetailVM.Location);
                p.Add("@Modelyear", editairDetailVM.Modelyear);
                p.Add("@AirID", editairDetailVM.PlanID);
                Execute("sp_UpdateAir", p);
                return "1";
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        public string insertAircraftInfo(AirList newairDetailVM)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@Model", newairDetailVM.Model);
                p.Add("@Make", newairDetailVM.Make);
                p.Add("@Registration", newairDetailVM.Registration);
                p.Add("@Location", newairDetailVM.Location);
                p.Add("@Modelyear", newairDetailVM.Modelyear);
                p.Add("@AirID", newairDetailVM.PlanID);
                Execute("sp_InsertAir", p);
                return "1";
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
    }
}
