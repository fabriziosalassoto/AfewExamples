/****************************************************
 * Author:      Fabrizio Salas-Soto
 * Date:        04/15/2014
 * Name:        rectangle.cs
 * Specific Description: this is the class that 
 *                       handles the entire 
 *                       operation for the rectangle.
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
    public class rectangle : figure {
        #region Attributes
        protected double Width = 0;        /* Handles the Width of the rectangle used to multiply the X-Coordinate */
        protected double Length = 0;       /* Handles the Length of the rectangle used to multiply the Y-Coordinate */
        #endregion

        #region Constructors
        /**********************************************************************
         * Initial conditions for the rectangle attributes 
         ***********************************************************************/
        public rectangle(string[] pArguments) {
            base.error=ValidateRectangle(pArguments);
            if(base.error == "")
                BuildDescription();
        }        
        #endregion

        #region validations
        /**********************************************************************
         * This method verifies the arguments passed to the rectangle are double
         * and the amount of parameters is correct. If it's not correct then
         * it throws an error back the ConsoleHandling for its current display
         ***********************************************************************/
        private string ValidateRectangle(string[] pArguments) {
            string error="";
            int flag = 0;
            if(pArguments.Length != 5) {
                error = "\nYou sent and incorrect number of arguments for a rectangle\n";
                error+= "the required arguments are name, position in the X axis,\n";
                error+= "position in the Y axis and the width and the Length\n\n";
            }else {
                if(pArguments[0].Trim().ToLower() == "rectangle")
                    base.type = pArguments[0].Trim().ToLower();
                if(!double.TryParse(pArguments[1], out base.x))
                    flag++;
                if(!double.TryParse(pArguments[2], out base.y))
                    flag++;
                if(!double.TryParse(pArguments[3], out Width))
                    flag++;
                if(!double.TryParse(pArguments[4], out Length))
                    flag++;
                if(flag > 0){
                    error = "\nYou sent an incorrect type of argument for a rectangle,\n";
                    error += "the required arguments need to be the name of the figure\n"; 
                    error += "plus 4 decimal values corresponding to X and Y axis, width and Length\n\n";
                }
            }
            return error;
        }
        #endregion

        #region Description
        /**********************************************************************
         * This method sets up the full description for the rectangle that is being
         * displayed right after a rectangle is entered and add to the memory records.
         ***********************************************************************/
        private void BuildDescription() {
            base.WhatIamI = base.type + " with origin at (" + base.x.ToString() + "," + base.y.ToString() + ") plus width " + this.Width.ToString()+" and Length "+this.Length.ToString();
        }        
        #endregion

        #region AreaCalculation
        /**********************************************************************
         * Calculates the specific Area for the stored rectangle
         ***********************************************************************/
        public new double CalculateArea() {
            this.Area = this.Length * this.Width;
            return base.CalculateArea();
        }
        #endregion

        #region FindPoint
        /******************************************************************************
         * Verifies if a given X,Y Coordinate exists within this square
         * base on logical quadrant verification.
         * Because the width & length of the rectangle are not quadratic formula and
         * the rectangle is not rotated, no other calculus here are necessary
         *****************************************************************************/
        public new bool FindIfXYareInsideMe(double pX, double pY) {
            this.XYInside = true;
            if(pX <= this.x || pX >= (this.x + this.Width) || pY <= this.y || pY >= (this.y + this.Length)) {
                this.XYInside = false;
            }
            return base.FindIfXYareInsideMe(pX, pY);
        }
        #endregion
    }
}