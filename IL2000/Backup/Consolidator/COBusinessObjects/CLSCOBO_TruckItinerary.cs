using System;
using System.Collections.Generic;
using System.Text;

namespace COBusinessObjects
{
    public class CLSCOBO_TruckItinerary
    {
        private CLSCOBO_OriginPoint ao_OriginPoint;
        private List<CLSCOBO_DeliveryPoint> ao_DestinationPoints;

        public CLSCOBO_TruckItinerary(CLSCOBO_OriginPoint po_OriginPoint, List<CLSCOBO_DeliveryPoint> pl_DeliveryPoints){
            ao_OriginPoint = po_OriginPoint;
            ao_DestinationPoints = new List<CLSCOBO_DeliveryPoint>();
            foreach(CLSCOBO_DeliveryPoint vo_DeliveryPoint in pl_DeliveryPoints){
                ao_DestinationPoints.Add(vo_DeliveryPoint);
            }            
        }

        public int getTotalDistanceFromOriginPoint(){
            int vi_TotalDistance = 0;
            CLSCOBO_BasePoint ao_PreviousPoint=ao_OriginPoint;
            foreach (CLSCOBO_DeliveryPoint ao_DestinationPoint in ao_DestinationPoints){
                vi_TotalDistance+=CLSCOBO_ConsolidatorUtils.getDistance(ao_PreviousPoint, ao_DestinationPoint);
                ao_PreviousPoint=ao_DestinationPoint;
            }
            return vi_TotalDistance;
        }

        public int getTotalDistanceAmongDeliveryPoints() {
            int vi_TotalDistance=0;
            CLSCOBO_BasePoint ao_PreviousPoint=null;
            foreach(CLSCOBO_DeliveryPoint ao_DestinationPoint in ao_DestinationPoints) {
                if(ao_PreviousPoint!=null)
                    vi_TotalDistance+=CLSCOBO_ConsolidatorUtils.getDistance(ao_PreviousPoint, ao_DestinationPoint);
                ao_PreviousPoint=ao_DestinationPoint;
            }
            return vi_TotalDistance;
        }

        public CLSCOBO_OriginPoint OriginPoint {
            get { return ao_OriginPoint; }
        } 

        public List<CLSCOBO_DeliveryPoint> Deliveries {
            get { return ao_DestinationPoints; }
        }

        public int TotalDestinationsCount {
            get { return ao_DestinationPoints.Count; }            
        }
    }
}