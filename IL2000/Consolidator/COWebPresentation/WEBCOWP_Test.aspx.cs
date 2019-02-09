using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COWebDataFlow;

namespace COWebPresentation
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        CLSCODF_Test ao_CODFTest;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ao_CODFTest = new CLSCODF_Test();
        }

        protected void BtnConvert_Click(object sender, EventArgs e)
        {
            ao_CODFTest.asZipCode = this.TxtZipCode.Text;
            ao_CODFTest.Refresh();
            this.TxtLongitude.Text = ao_CODFTest.asLongitude;
            this.TxtLatitude.Text = ao_CODFTest.asLatitude;
            this.TxtZipCode.Text = ao_CODFTest.asZipCode;
        }
    }
}
