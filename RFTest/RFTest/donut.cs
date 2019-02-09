/****************************************************
 * Author:      Fabrizio Salas-Soto
 * Date:        04/15/2014
 * Name:        donut.cs
 * Specific Description: this is the class that 
 *                       handles the entire 
 *                       operation for the donut.
 * General Description: Geometry Programming Test
 ****************************************************
 * Email: fsalas@racsa.co.cr
 * Phone: +506 88866743
 ****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFTest {
    public class donut : figure {
        #region Attributes
                                             /* There's no difference on outter or inner one */
        protected double radius1 = 0;        /* Handles the first radius of the donut */
        protected double radius2 = 0;        /* Handles the second radius of the donut */
        #endregion

        #region Constructors
        /**********************************************************************
         * Initial conditions for the donut attributes 
         ***********************************************************************/
        public donut(string[] pArguments){
            base.error=ValidateDonut(pArguments);
            if(base.error == "")
                BuildDescription();
        }        
        #endregion

        #region validations
        /**********************************************************************
         * This method verifies the arguments passed to the donut are double
         * and the amount of parameters is correct. If it's not correct then
         * it throws an error back the ConsoleHandling for its current display
         ***********************************************************************/
        private string ValidateDonut(string[] pArguments) {
            string error="";
            int flag = 0;
            if(pArguments.Length != 5) {
                error = "\nYou sent and incorrect number of arguments for a donut\n";
                error+= "the required arguments are name, position in the X axis,\n";
                error+= "position in the Y axis and two radiuses\n\n";
            }else {
                if(pArguments[0].Trim().ToLower() == "donut")
                    base.type = pArguments[0].Trim().ToLower();
                if(!double.TryParse(pArguments[1], out base.x))
                    flag++;
                if(!double.TryParse(pArguments[2], out base.y))
                    flag++;
                if(!double.TryParse(pArguments[3], out radius1))
                    flag++;
                if(!double.TryParse(pArguments[4], out radius2))
                    flag++;
                if(flag > 0){
                    error = "\nYou sent an incorrect type of argument for a circle,\n";
                    error += "the required arguments need to be the name of the figure\n"; 
                    error += "plus 3 decimal values corresponding to X and Y axis,\n";
                    error += "plus 2 decimal values corresponding to the two radiuses\n\n";
                }
            }
            return error;
        }
        #endregion

        #region Description
        /**********************************************************************
         * This method sets up the full description for the donut that is being
         * displayed right after a donut is entered and add to the memory records.
         ***********************************************************************/
        private void BuildDescription() {
            base.WhatIamI = base.type + " with centre at (" + base.x.ToString() + "," + base.y.ToString() + ") and first radius " + this.radius1.ToString() + " plus second radius " + this.radius2.ToString();
        }        
        #endregion

        #region AreaCalculation
        /**********************************************************************
         * Calculates the specific Area for the donut by substracting the inner
         * circle to the outter circle.
         ***********************************************************************/
        public new double CalculateArea() {
            double Area1, Area2;
            Area1 = Math.PI * Math.Pow(this.radius1, 2);
            Area2 = Math.PI * Math.Pow(this.radius2, 2);
            if(Area1 > Area2)
                this.Area = Area1 - Area2;
            else
                this.Area = Area2 - Area1;
            return base.CalculateArea();
        }
        #endregion

        #region FindPoint
        /**********************************************************************
         * Verifies the existence of the given XY coordinate by calculating
         * pythagoric location on both circles and location the overlapping area
         ***********************************************************************/
        public new bool FindIfXYareInsideMe(double pX, double pY) {
            this.XYInside = false;
            bool FirstCircle = (Math.Pow((this.x - pX), 2) + Math.Pow((this.y - pY), 2)) < Math.Pow(this.radius1, 2);
            bool SecondCircle = (Math.Pow((this.x - pX), 2) + Math.Pow((this.y - pY), 2)) < Math.Pow(this.radius2, 2);
            if(radius1 > radius2) {
                if(FirstCircle && !SecondCircle)
                    this.XYInside = true;                
            } else if(radius2 > radius1){
                if(SecondCircle && !FirstCircle)
                    this.XYInside = true;                                    
            }
            return base.FindIfXYareInsideMe(pX, pY);
        }
        #endregion
    }
}