/***********************************************
 * Author:      Fabrizio Salas-Soto
 * Date:        04/15/2014
 * Name:        figure.cs
 * Specific Description: this is the base class 
 *                       for all the figures
 *                       circle, rectangle, 
 *                       triangle, donut and
 *                       the square inherit from
 *                       this class.
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
    public class figure {        
        #region Attributes
        internal double x;              /* Coordinate on the X-Axis */
        internal double y;              /* Coordinate on the Y-Axis */
        internal string error;          /* Error message handled by children due to specific validations */
        internal string type;           /* This describes the kind of figure handled */
        internal int ID;                /* Unique ID inherited to the figure */
        protected String WhatIamI;      /* The description that is being displayed right after a figure is created */
        protected double Area;          /* Contains the Calculated Area. Although this value is set only when a match point is raised */
        protected bool XYInside;        /* Internal Flag to determine if a checkpoint has been found */
        #endregion

        #region constructors
        /**********************************************************************
         * Initial conditions for attributes          
         ***********************************************************************/
        public figure() {
            x = 0; y = 0;
            error = ""; 
            type = ""; 
            ID = 0; 
            WhatIamI = ""; 
            Area = 0;
            XYInside = false;
        }        
        #endregion
        
        #region DisplayMethods
        /**********************************************************************
         * Returns the label that internally describes the figure as a circle, 
         * rectangle, square, triangle or donut. 
         ***********************************************************************/
        internal String DescribeMe(){
            return WhatIamI;
        }
        #endregion
        
        #region CalculusMethods
        /**********************************************************************
         * Returns the calculated area for the current figure.          
         ***********************************************************************/
        internal double CalculateArea() {
            return Area;
        }
        #endregion

        #region FindingMethods
        /**********************************************************************
         * Returns a true or false depending ig the X,Y coordinate was found on
         * the current figure. The method is declared because children have their
         * own methods to calculate the Area. Although is not set as an abstract
         * class in case some extra low level calculus might be required.          
         ***********************************************************************/
        internal bool FindIfXYareInsideMe(double pX, double pY) {
            return XYInside;
        }
        #endregion
    }
}
