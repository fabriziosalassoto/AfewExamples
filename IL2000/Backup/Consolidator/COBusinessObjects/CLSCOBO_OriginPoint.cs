using System;
using System.Collections.Generic;
using System.Text;

namespace COBusinessObjects
{
    public class CLSCOBO_OriginPoint : CLSCOBO_BasePoint
    {
        public CLSCOBO_OriginPoint() : base()
        {
          
        }

        public CLSCOBO_OriginPoint(string ps_ZipCode) : base(ps_ZipCode){
            base.ZipCode = ps_ZipCode;                     
        }
    }
}
