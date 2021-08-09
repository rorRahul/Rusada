
using AutoMapper;
using Rusada.Models;
using Rusada.ViewModelLayer;
using Rusada.ViewModelLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rusada.Controllers
{
    public class AirController : Controller
    {
        // GET: Air
        private IAircraftRepository _airRepository = null;
        public AirController(IAircraftRepository airRepository)
        {
            _airRepository = airRepository;
        }
        
        public ActionResult AirCraftList()
        {
            AirCraftListmodel airlistViewModel = new AirCraftListmodel();
            List<AirlistViewModel> lstair = new List<AirlistViewModel>();
            //airlistViewModel.AirListModel lstmodel = new airlistViewModel.AirListModel();
            var returnlist = _airRepository.GetAirList();
            foreach(var item in returnlist)
            {
                AirlistViewModel lstitem = new AirlistViewModel();

                lstitem.PlanID = item.PlanID;
                lstitem.Make = item.Make;
                lstitem.Model = item.Model;
                lstitem.Location = item.Location;
                lstitem.Registration = item.Registration;
                lstitem.Modelyear = item.Modelyear;
                lstair.Add(lstitem);
            }
            airlistViewModel.AirListModel = lstair;
            return PartialView("AirList", airlistViewModel);
        }
        public ActionResult AirDetail(string AirID, string ActiveAction)
        {
            AirlistViewModel airlistViewModel = new AirlistViewModel();
            if (ActiveAction == "Edit")
            {
                var ReturnValue = _airRepository.GetAirDetails(AirID);
                if (ReturnValue != null)
                {
                    airlistViewModel.PlanID = ReturnValue.PlanID;
                    airlistViewModel.Make = ReturnValue.Make;
                    airlistViewModel.Model = ReturnValue.Model;
                    airlistViewModel.Registration = ReturnValue.Registration;
                    airlistViewModel.Location = ReturnValue.Location;
                    airlistViewModel.Modelyear = ReturnValue.Modelyear;
                }
            }
            else
            {
                airlistViewModel.PlanID = 0;
            }
            return PartialView("AirCraftlstDetail", airlistViewModel);
        }
        public ActionResult UpdateAirCraftDetail(AirlistViewModel editairDetailVM)
        {
            try
            {
                AirList craftinfo = new AirList();
                craftinfo.Make = editairDetailVM.Make;
                craftinfo.Model = editairDetailVM.Model;
                craftinfo.Location = editairDetailVM.Location;
                craftinfo.Registration = editairDetailVM.Registration;
                craftinfo.Modelyear = editairDetailVM.Modelyear;
                craftinfo.PlanID = editairDetailVM.PlanID;
                string updateInfo = _airRepository.UpdateAircraftInfo(craftinfo);
                return Content(updateInfo.ToString());
            }
            catch (Exception ex) { return Content(ex.ToString()); }

        }
        public ActionResult RegisterNew(AirlistViewModel newairDetailVM)
        {
            try
            {
                AirList craftinfo = new AirList();// Mapper.Map<AirList>(editairDetailVM);
                craftinfo.Make = newairDetailVM.Make;
                craftinfo.Model = newairDetailVM.Model;
                craftinfo.Location = newairDetailVM.Location;
                craftinfo.Registration = newairDetailVM.Registration;
                craftinfo.Modelyear = newairDetailVM.Modelyear;
                craftinfo.PlanID = newairDetailVM.PlanID;
                string updateInfo = _airRepository.insertAircraftInfo(craftinfo);
                return Content(updateInfo.ToString());
            }
            catch (Exception ex) { return Content(ex.ToString()); }
        }
    }
}