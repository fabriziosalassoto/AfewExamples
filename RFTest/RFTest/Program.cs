/***********************************************
 * Author:      Fabrizio Salas-Soto
 * Date:        04/15/2014
 * Name:        Program.cs
 * Specific Description: This is the main class 
 *                       of the application it
 *                       calls the class
 *                       ConsoleHandling for 
 *                       the entire data flow.
 * General Description: Geometry Programming Test
 ***********************************************
 * Email: fsalas@racsa.co.cr
 * Phone: +506 88866743
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFTest {
    public class Program {
        static void Main(string[] args) {
            ConsoleHandling mainController = new ConsoleHandling();
            mainController.MainMenu();
        }        
    }
}