using System;
using System.Collections.Generic;
using System.Text;
//comment
namespace COBusinessObjects
{
    static class CLSCOBO_ConsolidatorUtils
    {
        public static int getDistance(CLSCOBO_BasePoint po_OriginPoint, CLSCOBO_BasePoint po_DestinationPoint){
            double vd_Distance = CLSCOBO_FunctionsRepository.getDistance(po_OriginPoint.Longitude, po_OriginPoint.Latitude, po_DestinationPoint.Longitude, po_DestinationPoint.Latitude,"M");
            return (int)Convert.ToInt32(vd_Distance);            
        }        
    }
}
