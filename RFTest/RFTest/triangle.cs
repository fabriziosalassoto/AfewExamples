/****************************************************
 * Author:      Fabrizio Salas-Soto
 * Date:        04/15/2014
 * Name:        triangle.cs
 * Specific Description: this is the class that 
 *                       handles the entire 
 *                       operation for the triangle.
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
    public class triangle : figure {
        #region Attributes
                                                        /* The first vertex is the figure itself */
        protected figure vertex2 = new figure();        /* Handles the second vertex of the triangle, inheriting from figure */
        protected figure vertex3 = new figure();        /* Handles the second vertex of the triangle, inheriting from figure */
        #endregion

        #region constructors
        /**********************************************************************
         * Initial conditions for the triangle attributes 
         ***********************************************************************/
        public triangle(string[] pArguments) {
            base.error=ValidateTriangle(pArguments);
            if(base.error == "")
                BuildDescription();
        }        
        #endregion

        #region validations
        /**********************************************************************
         * This method verifies the arguments passed to the triangle are double
         * and the amount of parameters is correct. If it's not correct then
         * it throws an error back the ConsoleHandling for its current display
         ***********************************************************************/
        private string ValidateTriangle(string[] pArguments) {
            string error="";
            int flag = 0;
            if(pArguments.Length != 7) {
                error = "\nYou sent and incorrect number of arguments for a circle\n";
                error+= "the required arguments are name, X axis & Y axis position of,\n";
                error+= "the first vertex, X axis & Y axis position of second vertex\n";
                error+= "X axis & Y axis position of the third vertex\n";
            }else {
                if(pArguments[0].Trim().ToLower() == "triangle")
                    base.type = pArguments[0].Trim().ToLower();
                if(!double.TryParse(pArguments[1], out base.x))
                    flag++;
                if(!double.TryParse(pArguments[2], out base.y))
                    flag++;
                if(!double.TryParse(pArguments[3], out this.vertex2.x))
                    flag++;
                if(!double.TryParse(pArguments[4], out this.vertex2.y))
                    flag++;
                if(!double.TryParse(pArguments[5], out this.vertex3.x))
                    flag++;
                if(!double.TryParse(pArguments[6], out this.vertex3.y))
                    flag++;
                if(flag > 0){
                    error = "\nYou sent an incorrect type of argument for a triangle,\n";
                    error += "the required arguments need to be the name of the figure\n"; 
                    error += "plus 6 decimal values corresponding to X & Y axis for the 3 vertices\n\n";
                }
            }
            return error;
        }
        #endregion

        #region Description
        /**********************************************************************
         * This method sets up the full description for the triangle that is being
         * displayed right after a triangle is entered and add to the memory records.
         ***********************************************************************/
        private void BuildDescription() {
            base.WhatIamI = base.type + " with first vertex at (" + base.x.ToString() + "," + base.y.ToString() + "), second vertex at (" + this.vertex2.x.ToString() + "," + this.vertex2.y.ToString() + ") and third vertex at (" + this.vertex3.x.ToString() + "," + this.vertex3.y.ToString() + ")";
        }        
        #endregion

        #region AreaCalculation
        /**********************************************************************
         * Calculates the specific Area for the stored triangle
         ***********************************************************************/
        public new double CalculateArea() {
            this.Area = CalculateAreaOfTriangle(this.x,this.y,this.vertex2.x,this.vertex2.y,this.vertex3.x,this.vertex3.y);
            return base.CalculateArea();
        }

        /**************************************************************************
        * Algorithmia to calculate the area of the triangle using Analytic Geometry 
        ***************************************************************************/
        private double CalculateAreaOfTriangle(double pX1, double pY1, double pX2, double pY2, double pX3, double pY3) {
            return Math.Abs((pX1 * (pY2 - pY3) + pX2 * (pY3 - pY1) + pX3 * (pY1 - pY2)) / 2.0);
        }
        #endregion

        #region FindPoint
        /*******************************************************************************
         * Verifies if a given X,Y Coordinate exists within this triangle
         * by calculating all the subtriangles formed from the given point being search
         * to the opposite vertices. Then summarizes all the variations and if it's 
         * equal to the main triangle area, the point exists within the triangle.
         *******************************************************************************/
        public new bool FindIfXYareInsideMe(double pX, double pY) {            
            double iBC, iAC, iAB;
            CalculateArea();
            iBC = CalculateAreaOfTriangle(pX, pY, this.vertex2.x, this.vertex2.y, this.vertex3.x, this.vertex3.y);
            iAC = CalculateAreaOfTriangle(this.x, this.y, pX, pY, this.vertex3.x, this.vertex3.y);
            iAB = CalculateAreaOfTriangle(this.x, this.y, this.vertex2.x, this.vertex2.y, pX, pY);
            this.XYInside = (Math.Round((double)this.Area,2) == Math.Round((double)(iBC + iAC + iAB),2));
            return base.FindIfXYareInsideMe(pX, pY);
        }
        #endregion
    }
}