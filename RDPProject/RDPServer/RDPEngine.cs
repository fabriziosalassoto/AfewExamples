using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime;
using System.Threading;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Media;
using System.Diagnostics;

namespace RDPServer {
    public class RDPEngine {
        #region Local-Fields
        RDPImageListener myImageListener = null;
        RDPControlListener myControlListener = null;
        RDPUpdaterListener myUpdaterListener = null;
        List<Thread> ActiveListeners;
        #endregion

        #region Constructor
        public RDPEngine(Form InvokerForm, string IPAddress, int StartingPort, int EndingPort){
            ActiveListeners = new List<Thread>();
            myImageListener = new RDPImageListener(InvokerForm,IPAddress,StartingPort,EndingPort);
            ActiveListeners.Add(StartEngine(myImageListener));

            myControlListener = new RDPControlListener(InvokerForm, IPAddress, StartingPort, EndingPort);
            ActiveListeners.Add(StartEngine(myControlListener));

            myUpdaterListener = new RDPUpdaterListener(InvokerForm, IPAddress, StartingPort, EndingPort);
            ActiveListeners.Add(StartEngine(myUpdaterListener));            
        }         
        #endregion

        #region EngineManipulation
        private Thread StartEngine(BaseListener pListener){            
            Thread pListenerThread = new Thread(pListener.startListening);
            pListenerThread.Start();
            return pListenerThread;
        }

        private void StopEngine(BaseListener pListener) {
            pListener.KillMe();
            pListener = null;
        }
        #endregion

        #region Termination
        public void KillMe(){
            try {
                StopEngine(myImageListener);
                StopEngine(myControlListener);
                StopEngine(myUpdaterListener);
                foreach(Thread CurrentThread in ActiveListeners) {
                    CurrentThread.Abort();
                }
            }
            catch(Exception) { }
        }
        #endregion
    }
}
