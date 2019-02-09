using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RDPServer;    // Using the RDP Server library.

namespace RDPForm {    

    public partial class TestForm : Form {        
        RDPEngine myRemoteEngine = null; // Declaration of the current object within the RDPServer Library.

        public TestForm() {
            InitializeComponent();            
        }

        private void Form1_Load(object sender, EventArgs e) {
            txtDate.Text = System.DateTime.Now.ToString();
            LblDate.Text = "Value Sent: "+System.DateTime.Now.ToString();            
            myRemoteEngine = new RDPEngine(this,"127.0.0.1",1400,1450);     // The only invokation needed within the object.                                              
        }               

        private void button1_Click(object sender, EventArgs e) {
            MessageBox.Show("Hello");
        }        

        private void TestForm_FormClosing(object sender, FormClosingEventArgs e) {
            myRemoteEngine.KillMe();
            Environment.Exit(0);            
        }
    }
}
