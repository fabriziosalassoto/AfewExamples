using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
using System.Xml;

namespace COBusinessObjects
{
    [Serializable()]
    public class CLSCOBO_ConsolidationProblem
    {        
        private CLSCOBO_OriginPoint ao_origin;
        private List<CLSCOBO_DeliveryPoint> ao_deliveries;

        public CLSCOBO_ConsolidationProblem(){
            ao_origin = new CLSCOBO_OriginPoint();
            ao_deliveries = new List<CLSCOBO_DeliveryPoint>();            
        }        

        public CLSCOBO_OriginPoint OriginPoint{
            set { ao_origin = (CLSCOBO_OriginPoint) value; }
            get { return ao_origin; }
        }

        public List<CLSCOBO_DeliveryPoint> Deliveries
        {
            get { return ao_deliveries; }
        }

        public CLSCOBO_ConsolidationSolution GetSolutionZero(){
            return null; //removed, under modification.            
        }

        public void AddDeliveryPoint(CLSCOBO_DeliveryPoint po_DeliveryPoint){
            bool vb_Found = false;
            foreach (CLSCOBO_DeliveryPoint vo_DeliveryPoint in ao_deliveries)
            {
                if (vo_DeliveryPoint.ZipCode == po_DeliveryPoint.ZipCode)                
                    vb_Found = true;                
            }
            if (!vb_Found)
                ao_deliveries.Add(po_DeliveryPoint);
        }
    }
}
