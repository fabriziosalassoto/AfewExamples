#region Uses
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Media;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;
#endregion

namespace RDPServer {
    class RDPControlListener : BaseListener {
        #region Fields
        private delegate void SetTextCallback(Control control);
        private Control myControlFound;
        #endregion

        #region Constructor
        public RDPControlListener(Form pForm, string pIP, int pStartingPort, int pEndingPort)
            : base(pForm, pIP, pStartingPort, pEndingPort) {
        }
        #endregion

        #region Listening-Methods
        /*
         * This is the main listening method of the thread, the purpose of this method is to find a control on the form.
         */
        public override void startListening() {
            #region local variables
            int milliseconds = 0;
            string StringReceived = "", ControlName = "", ControlValue = "", ControlFound = "";
            byte[] bytes;
            Point ControlCoordinates;
            #endregion
            while(!Stop) {
                try {
                    InitiateConnection(this.StartingPort+1);
                    bytes = new byte[256];
                    myControlFound = null;                    
                    int i;
                    while((i = s.Read(bytes, 0, bytes.Length))>0){
                        milliseconds = 250;
                        Thread.Sleep(milliseconds);
                        StringReceived = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        ControlName = "";
                        ControlValue = "";                        
                        ControlCoordinates = DecodePoint(ref StringReceived);
                        myControlFound = FindControlByAxis(ControlCoordinates);

                        if(myControlFound != null) {                                
                            myControlFound.Invoke(new Action(() => { ControlValue = myControlFound.Text; ControlName = myControlFound.Name; }));
                            HandlingControl(myControlFound,ControlName,StringReceived);                                
                        } else 
                            InvokerForm.Invoke(new Action(() => { ControlValue = InvokerForm.Text; ControlName = InvokerForm.Name; }));
                        ControlFound = "name=" + ControlName + ";value=" + ControlValue;                              
                        bytes = System.Text.Encoding.ASCII.GetBytes(ControlFound);
                        s.Write(bytes, 0, bytes.Length);                                                         
                    }
                } catch(IOException Exception) {
                    if(mainSocket.IsBound)
                        mainSocket.Close();
                    if(listener != null)
                        listener.Stop();                    
                }
            }
        }
        #endregion

        #region Handling-Control-Events-And-Metods
        /*
         * This is the generic method that identifies if the control found matches a control already selected on the remote client web browser         
         */
        private void HandlingControl(Control control_found, string control_found_name, string control_already_clicked) {
            try {
                control_found.Invoke(new Action(() => {
                    control_found.Focus();
                    if(control_found_name == control_already_clicked) {
                        if(control_found.GetType() == typeof(ComboBox)) {
                            HandleComboBox(control_found);
                        } else if(control_found.GetType() == typeof(Button)) {
                            HandleButton(control_found);
                        }                         
                    }
                }));
            }
            catch(Exception) { }
        }

        /*
         * This method drops down the list on any combobox once it has received the focus and also selects the item within the combo
         */
        private void HandleComboBox(Control control_found) {
            try {
                int selItem = 0;
                var counter = (control_found as ComboBox).Items.Count - 1;
                if((control_found as ComboBox).SelectedText == "") {
                    InvokerForm.Tag = 0;
                }
                else {
                    selItem = (int)InvokerForm.Tag;
                }
                if(((control_found as ComboBox).Tag as string) == "") {
                    (control_found as ComboBox).DroppedDown = true;
                    (control_found as ComboBox).Tag = "dropped";
                }
                else {
                    (control_found as ComboBox).DroppedDown = false;
                    (control_found as ComboBox).Tag = "";
                    (control_found as ComboBox).SelectedText = (control_found as ComboBox).Items[selItem].ToString();
                    selItem = (selItem >= counter) ? 0 : selItem + 1;
                }
                InvokerForm.Tag = selItem;
            }
            catch(IOException ex) {
                throw ex;
            }
        }

        /*
         * This method runs the corresponding click event on a button, once the button has already received the focus.
         */
        private void HandleButton(Control control_found) {
            try {
                (control_found as Button).PerformClick();
            }
            catch(Exception) { }
        }
        #endregion

        #region Finding-Control-on-Form
        /*
         * This method decodes the (x,y) axis point send remotely by the client and decodes also the name of the 
         * control already selected on the remote client in order to find out what actions should be run on the control.
         */
        private Point DecodePoint(ref string pPointReceived) {
            Point myPoint;
            string[] coordinates = Regex.Split(pPointReceived, ";");
            string[] X = Regex.Split(coordinates[0], "=");
            string[] Y = Regex.Split(coordinates[1], "=");
            string[] ControlPointed = Regex.Split(coordinates[2], "=");
            myPoint = new Point(int.Parse(X[1])-lostinLeft, int.Parse(Y[1])-lostInTop);
            pPointReceived = ControlPointed[1];
            return myPoint;
        }

        /*
         * This methid finds a control containing the (X,Y) axis point on the form and return it back
         */
        private Control FindControlByAxis(Point GivenPoint) {
            Control FoundControl = null;            
            foreach(Control CurrentControl in this.InvokerForm.Controls) {
                if((GivenPoint.X >= CurrentControl.Left && GivenPoint.X<=(CurrentControl.Left + CurrentControl.Width)) &&
                   (GivenPoint.Y >= CurrentControl.Top && GivenPoint.Y <= (CurrentControl.Top + CurrentControl.Height))) {
                    FoundControl = CurrentControl;
                    break;
                }
            }
            return FoundControl;
        }
        #endregion
    }
}