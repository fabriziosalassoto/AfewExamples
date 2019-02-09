using System;
using System.Collections.Generic;
using DBControllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;


namespace BRBusinessObjects
{
    public class BRCls_Locations : List<BRCls_Location>
    {
        #region PrivateProperties
        private DBCls_Locations oDBLocationsController;
        #endregion

        #region Constructors
        public BRCls_Locations()
        {
            oDBLocationsController = new DBCls_Locations();
        }

        public BRCls_Locations(String[,] pArrLocations)
        {
            oDBLocationsController = new DBCls_Locations();
            FillList(pArrLocations);

        }
        #endregion

        #region SearchMethods
        public bool GetLocations()
        {
            String[,] mArrLocations = oDBLocationsController.ListLocations();
            FillList(mArrLocations);
            return true;
        }

        public bool GetStoriesByCriteria(LocationCriteria pCriteriaKey, String pCriteriaValue)
        {
            String[,] mArrLocations = oDBLocationsController.ListLocationsByCriteria(pCriteriaKey, pCriteriaValue);
            FillList(mArrLocations);
            return true;
        }
        #endregion

        #region ManipulationMethods
        public bool SaveAllLocations()
        {
            foreach (BRCls_Location oLocation in this)
            {
                oLocation.SaveLocation();
            }
            return true;
        }

        public String JSONfy()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion

        #region PrivateMethods
        private void FillList(String[,] pArrLocations)
        {
            String[] mArrLocation = null;
            for (int x = 0; x < pArrLocations.GetLength(0); x++)
            {
                mArrLocation = new String[(int)TotalLocationCriteria.cTotal];
                for (int y = 0; y < pArrLocations.GetLength(1); y++)
                {
                    mArrLocation[y] = pArrLocations[x, y];
                }
                BRCls_Location oLocation = new BRCls_Location(mArrLocation);
                this.Add(oLocation);
            }
        }
        #endregion
    }
}