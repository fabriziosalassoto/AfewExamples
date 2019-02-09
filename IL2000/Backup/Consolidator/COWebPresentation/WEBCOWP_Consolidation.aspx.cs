using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new declarations
using System.Web.UI;
using System.Web.UI.WebControls;
using COWebDataFlow;
using System.Web.Script;

namespace COWebPresentation
{
    public partial class _Default : System.Web.UI.Page
    {
        private CLSCODF_Consolidation ao_Consolidation;
        
        protected void Page_Load(object sender, EventArgs e){
            ao_Consolidation = new CLSCODF_Consolidation(this);
        }       

        protected void BtnGenerateRandomZips_Click(object sender, EventArgs e){                        
            ao_Consolidation.FillInDemoData();            
        }
     
        protected void Button1_Click1(object sender, EventArgs e){
            ao_Consolidation.SendFile();                                 
        }

        protected void BtnCalculateSolution_Click(object sender, EventArgs e){                        
            ao_Consolidation.CalculateConsolidationSolution();
        }

        protected void BtnExportToGoogleMaps_Click(object sender, EventArgs e)
        {

        }        
    }
}