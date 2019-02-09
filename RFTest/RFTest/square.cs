/***********************************************
 * Author:      Fabrizio Salas-Soto
 * Date:        04/15/2014
 * Name:        square.cs
 * Specific Description: this is the class that 
 *                       handles the entire 
 *                       operation for the square.
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
    public class square : figure {
        #region Attributes
        protected double length = 0;    /* Controls the length of each one of the sides of the square */
        #endregion

        #region constructors
        /**********************************************************************
         * Initializes the conditions for the square.         
         ***********************************************************************/
        public square(string[] pArguments) : base(){
            base.error=ValidateSquare(pArguments);
            if(base.error == "")
                BuildDescription();
        }        
        #endregion

        #region validations
        /**********************************************************************
         * This method verifies the arguments passed to the square are double
         * and the amount of parameters is correct. If it's not correct then
         * it throws an error back the ConsoleHandling for its current display
         ***********************************************************************/
        private string ValidateSquare(string[] pArguments) {
            string error="";
            int flag = 0;
            if(pArguments.Length != 4) {
                error = "\nYou sent and incorrect number of arguments for a square\n";
                error+= "the required arguments are name, position in the X axis,\n";
                error+= "position in the Y axis and the length of the all four sides\n\n";
            }else {
                if(pArguments[0].Trim().ToLower() == "square")
                    base.type = pArguments[0].Trim().ToLower();
                if(!double.TryParse(pArguments[1], out base.x))
                    flag++;
                if(!double.TryParse(pArguments[2], out base.y))
                    flag++;
                if(!double.TryParse(pArguments[3], out length))
                    flag++;
                if(flag > 0){
                    error = "\nYou sent an incorrect type of argument for a square,\n";
                    error += "the required arguments need to be the name of the figure\n"; 
                    error += "plus 3 decimal values corresponding to X and Y axis, plus length of sides\n\n";
                }
            }
            return error;
        }
        #endregion

        #region Description
        /**********************************************************************
         * This method sets up the full description for the square that is being
         * displayed right after a square is entered and add to the memory records.
         ***********************************************************************/
        private void BuildDescription() {
            base.WhatIamI = base.type + " with origin at (" + base.x.ToString() + "," + base.y.ToString() + ") and length of its sides " + this.length.ToString();
        }        
        #endregion

        #region AreaCalculation
        /**********************************************************************
         * Calculates the specific Area for the stored square         
         ***********************************************************************/
        public new double CalculateArea() {
            this.Area = this.length * this.length;
            return base.CalculateArea();
        }
        #endregion

        #region FindPoint
        /**********************************************************************
         * Verifies if a given X,Y Coordinate exists within this square
         * base on logical quadrant verification.
         * Because the length of the square is not a quadratic formula and
         * the square is not rotated, no other calculus here are necessary
         ***********************************************************************/
        public new bool FindIfXYareInsideMe(double pX, double pY) {
            this.XYInside = true;
            if(pX <= this.x || pX >= (this.x + this.length) || pY <= this.y || pY >= (this.y + this.length)) {
                this.XYInside = false;
            }            
            return base.FindIfXYareInsideMe(pX, pY);
        }
        #endregion
    }
}