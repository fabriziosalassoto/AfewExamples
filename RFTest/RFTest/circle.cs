/***********************************************
 * Author:      Fabrizio Salas-Soto
 * Date:        04/15/2014
 * Name:        circle.cs
 * Specific Description: this is the class that 
 *                       handles the entire 
 *                       operation for the circle.
 * General Description: Geometry Programming Test
 ***********************************************
 * Email: fsalas@racsa.co.cr
 * Phone: +506 88866743
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFTest {
    public class circle : figure {
        #region Attributes
        protected double radius = 0;        /* handles the radius of the circle */
        #endregion

        #region constructors
        /**********************************************************************
         * Initial conditions for the circle attributes 
         ***********************************************************************/
        public circle(string[] pArguments) : base() {
            base.error=ValidateCircle(pArguments);
            if(base.error == "")
                BuildDescription();
        }        
        #endregion

        #region validations
        /**********************************************************************
         * This method verifies the arguments passed to the circle are double
         * and the amount of parameters is correct. If it's not correct then
         * it throws an error back the ConsoleHandling for its current display
         ***********************************************************************/
        private string ValidateCircle(string[] pArguments) {
            string error="";
            int flag = 0;
            if(pArguments.Length != 4) {
                error = "\nYou sent and incorrect number of arguments for a circle\n";
                error+= "the required arguments are name, position in the X axis,\n";
                error+= "position in the Y axis and radius\n\n";
            }else {
                if(pArguments[0].Trim().ToLower() == "circle")
                    base.type = pArguments[0].Trim().ToLower();
                if(!double.TryParse(pArguments[1], out base.x))
                    flag++;
                if(!double.TryParse(pArguments[2], out base.y))
                    flag++;
                if(!double.TryParse(pArguments[3], out radius))
                    flag++;
                if(flag > 0){
                    error = "\nYou sent an incorrect type of argument for a circle,\n";
                    error += "the required arguments need to be the name of the figure\n"; 
                    error += "plus 3 decimal values corresponding to X and Y axis, plus radius\n\n";
                }
            }
            return error;
        }
        #endregion

        #region Description
        /**********************************************************************
         * This method sets up the full description for the circle that is being
         * displayed right after a circle is entered and add to the memory records.
         ***********************************************************************/
        private void BuildDescription() {
            base.WhatIamI = base.type + " with centre at (" + base.x.ToString() + "," + base.y.ToString() + ") and radius " + this.radius.ToString();
        }        
        #endregion

        #region AreaCalculation
        /**********************************************************************
         * Calculates the specific Area for the stored circle         
         ***********************************************************************/
        internal new double CalculateArea() {
            this.Area = Math.PI * Math.Pow(this.radius, 2);
            return base.CalculateArea();                     
        }
        #endregion

        #region FindPoint
        /**********************************************************************
         * Verifies if a given X,Y Coordinate exists within this circle
         * base on pythagoric algorithmia.
         ***********************************************************************/
        public new bool FindIfXYareInsideMe(double pX, double pY) {
            this.XYInside = (Math.Pow((this.x-pX),2)+Math.Pow((this.y-pY),2))<Math.Pow(this.radius,2);
            return base.FindIfXYareInsideMe(pX, pY);
        }
        #endregion
    }
}