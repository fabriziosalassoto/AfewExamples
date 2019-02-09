using System;
using System.Collections.Generic;
using System.Text;

namespace COBusinessObjects
{
    public class CLSCOBO_DeliveryPoint : CLSCOBO_BasePoint
    {
        private double ai_Weight;

        public CLSCOBO_DeliveryPoint() : base()
        {
            ai_Weight = 0;            
        }

        public CLSCOBO_DeliveryPoint(string ps_ZipCode) : base(ps_ZipCode){
            ai_Weight = 0;
            base.ZipCode = ps_ZipCode;
        }

        public double Weight
        {
            set { ai_Weight = value; }
            get { return ai_Weight; }
        }
    }
}
