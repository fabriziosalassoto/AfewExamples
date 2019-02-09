using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COBusinessObjects;

namespace COWebDataFlow
{        
    public class CLSCODF_Test
    {       
        private CLSCOBO_BasePoint ao_BasePoint;       
                         
        public CLSCODF_Test()
        {              
            ao_BasePoint = new CLSCOBO_BasePoint();
        }

        public string asZipCode{
            set { ao_BasePoint.ZipCode = value; }
            get { return ao_BasePoint.ZipCode; }
        }

        public string asLatitude
        {
            //set { as_Latitude = value; }
            get { return ao_BasePoint.Latitude.ToString(); }
        }

        public string asLongitude
        {
            //set { as_Longitude = value; }
            get { return ao_BasePoint.Longitude.ToString(); }
        }

        public void Refresh()
        {
            ao_BasePoint = new CLSCOBO_BasePoint(asZipCode);
        }
    }
}
