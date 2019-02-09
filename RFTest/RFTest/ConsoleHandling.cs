/***********************************************
 * Author:      Fabrizio Salas-Soto
 * Date:        04/15/2014
 * Name:        ConsoleHandling.cs
 * Specific Description: This class controls the
 *                       data flow of the entire
 *                       application.
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
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace RFTest {
    class ConsoleHandling {
        #region Attributes
        private List<figure> myFigures;             /* Stores the entire list of the figures on memory. */
        private int myFiguresCounter;               /* Handles the last ID inserted. */        
        #endregion

        #region Constructor
        /**********************************************************************
         * Initial conditions for the ConsoleHandlingController 
         ***********************************************************************/
        public ConsoleHandling() {
            myFigures = new List<figure>();
            myFiguresCounter = 0;            
        }
        #endregion

        #region MainMenu
        /**********************************************************************
         * Handles the main menu display 
         ***********************************************************************/
        public void MainMenu() {            
            ConsoleKeyInfo a = new ConsoleKeyInfo();
            ConsoleKeyInfo b;
            while(a.KeyChar.ToString() != "0") {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("*****************************************");
                Console.WriteLine("* Author: Fabrizio Salas-Soto           *");
                Console.WriteLine("* Phone:  +506 88866743                 *");
                Console.WriteLine("* Email:  fsalas@racsa.co.cr            *");
                Console.WriteLine("*****************************************");
                Console.WriteLine("*          M A I N   M E N U            *");
                Console.WriteLine("*****************************************");
                Console.WriteLine("*                                       *");
                Console.WriteLine("* 1 - Enter a figure                    *");
                Console.WriteLine("*                                       *");
                Console.WriteLine("* 2 - Find all figures containing a X,Y *");
                Console.WriteLine("*                                       *");
                Console.WriteLine("* 3 - List all figures in memory        *");
                Console.WriteLine("*                                       *");                
                Console.WriteLine("* 4 - Clean entered figures             *");
                Console.WriteLine("*                                       *");
                Console.WriteLine("* 5 - Load figures from File            *");
                Console.WriteLine("*                                       *");
                Console.WriteLine("* 6 - Help                              *");
                Console.WriteLine("*                                       *"); 
                Console.WriteLine("* 0 - Exit Program                      *");
                Console.WriteLine("*                                       *");
                Console.WriteLine("*****************************************");
                Console.ResetColor();
                Console.Write("Enter the number corresponding to your choice: ");
                a = Console.ReadKey();
                switch(a.KeyChar.ToString()) {
                    case "0": ExitProgram();
                              break;

                    case "1": EnterLine();
                              break;

                    case "2": FindFiguresWithXY();
                              break;

                    case "3": DisplayExistingFigures();
                              break;                    

                    case "4": CleanFiguresInMemmory();
                              break;

                    case "5": BatchProcess();
                              break;

                    case "6": DisplayHelp();
                              break;

                    default: Console.WriteLine("\n\n");
                             Console.Write("Error: options are only 0-6! hit any key to continue");
                             b = Console.ReadKey();
                             break;
                }                
            }            
        }
        #endregion

        #region FiguresCreation
        
        #region EnteringLine
        /**********************************************************************
         * Handles the screen to enter the figures one by one and data flow of
         * events.
         ***********************************************************************/
        private void EnterLine() {
            string figurestream = "";                        
            while(figurestream.Trim().ToLower() != "exit" && figurestream.Trim().ToLower() != "back") {                
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("*****************************************");
                Console.WriteLine("* Author: Fabrizio Salas-Soto           *");
                Console.WriteLine("* Phone:  +506 88866743                 *");
                Console.WriteLine("* Email:  fsalas@racsa.co.cr            *");
                Console.WriteLine("*****************************************");
                Console.WriteLine("*      F I G U R E   C R E A T I O N    *");
                Console.WriteLine("*****************************************\n");
                Console.ResetColor();
                Console.WriteLine("(to go back to main menu type the word back and enter\n");
                Console.WriteLine("(or type the word exit and enter to quit the program)\n");
                Console.WriteLine("Enter a figure characteristcs [name] [coordinates]...");                
                Console.WriteLine("");
                figurestream = Console.ReadLine();
                CommandsValidation(() => InnerEnterLineProcess(figurestream), figurestream);                
            }
        }

        public void InnerEnterLineProcess(string pFigureStream) {
            string error = "";
            ConsoleKeyInfo b;
            error = FigureValidation(pFigureStream);
            if(error.Trim() == "")
                DisplayFigureRecentlyCreated();
            else
                Console.Write(error);
            b = Console.ReadKey();            
        }
        #endregion
        /**********************************************************************
         * This is the method where the creation of each of the figures takes place
         * Here is validated before being add to the List attribute.
         ***********************************************************************/
        private String FigureValidation(string pFigureStreamRead) {            
            figure NewFigure = null;
            string error = "";
            string[] bruteresults = pFigureStreamRead.Split(' ');
            string[] results = bruteresults.Where(criteria => criteria.Trim().Length > 0).ToArray();

            switch(results[0].Trim().ToLower()){                    
                case "circle":
                    NewFigure = new circle(results);                                
                    break;
                case "square":
                    NewFigure = new square(results);
                    break;
                case "rectangle":
                    NewFigure = new rectangle(results);
                    break;
                case "triangle":
                    NewFigure = new triangle(results);
                    break;
                case "donut":
                    NewFigure = new donut(results);
                    break;
                default: 
                    error = results[0].ToString()+" is not a valid figure\n\n";                       
                    break;
            }            
            if(error == "") {
                NewFigure.ID = ++myFiguresCounter; 
                myFigures.Add(NewFigure);                
            } else
                error += "Please hit any key to continue";                                            
            return error;
        }

        /*****************************************************************************
         * Displays on the screen the last figure succesfuly stored in memory records 
         *****************************************************************************/
        private void DisplayFigureRecentlyCreated() {
            figure NewFigure = myFigures[myFigures.Count - 1];
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nFigure succesfully entered.");
            Console.WriteLine("figure {0}: {1}\n", NewFigure.ID, NewFigure.DescribeMe());
            Console.ResetColor();
            Console.WriteLine("hit any key to continue");
        }

        #endregion       

        #region BatchProcessing
        /**********************************************************************
         * This is the screen that controls the data flow of events and provides
         * the interface to read the figures from a physical file.
         * this method uses the same as the validation previously described
         ***********************************************************************/
        private void BatchProcess() {
            string file = "";            
            while(file.Trim().ToLower() != "back") {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("*****************************************");
                Console.WriteLine("* Author: Fabrizio Salas-Soto           *");
                Console.WriteLine("* Phone:  +506 88866743                 *");
                Console.WriteLine("* Email:  fsalas@racsa.co.cr            *");
                Console.WriteLine("*****************************************");
                Console.WriteLine("*    B A T C H   F I G U R E   L O A D  *");
                Console.WriteLine("*****************************************\n");
                Console.ResetColor();
                Console.WriteLine("Batch Figures load from file\n");
                Console.WriteLine("(You can type the word back and enter to go back to main menu\n");
                Console.WriteLine("(or type the word exit and enter to exit the current program)\n");
                Console.WriteLine();
                Console.WriteLine("Please enter the path and filename you want to load:\n");
                file = Console.ReadLine();
                CommandsValidation(() => BatchInnerProcess(file), file);                
            }
        }

        /****************************************************************************
         * Subprocess of the BatchProcessing in order to be used as Parameter Method
         ****************************************************************************/
        private void BatchInnerProcess(string pStream) {
            ConsoleKeyInfo b;
            try {
                string[] FiguresRead = File.ReadAllLines(@pStream);
                int i = 0, j = 0;
                foreach(string FigureStream in FiguresRead) {
                    if(FigureValidation(FigureStream) == "")
                        i++;
                    j++;
                }
                Console.WriteLine("{0} figures of {1} succesfully added to memory\n", i, j);
            }
            catch(IOException Ex) {
                Console.WriteLine("Error on path or file\n");
            } finally {
                Console.Write("Please hit any key to continue");
                b = Console.ReadKey();
            }
        }         
        #endregion      

        #region MatchingFigures
        /**********************************************************************
         * This method handles the data flow of events when the program looks
         * for a XY coordinate entered by the user in order to locate the matching
         * figures. Also takes place the calculus and consolidation.
         ***********************************************************************/
        private void FindFiguresWithXY() {
            string figurestream = "";                        

            while(figurestream.Trim().ToLower() != "exit" && figurestream.Trim().ToLower() != "back") {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("*********************************************");
                Console.WriteLine("* Author: Fabrizio Salas-Soto               *");
                Console.WriteLine("* Phone:  +506 88866743                     *");
                Console.WriteLine("* Email:  fsalas@racsa.co.cr                *");
                Console.WriteLine("*********************************************");
                Console.WriteLine("* F I N D   M A T C H I N G   F I G U R E S *");
                Console.WriteLine("*********************************************\n");
                Console.ResetColor();
                Console.WriteLine("(to go back to main menu type the word back and enter\n");
                Console.WriteLine("(or type the word exit and enter to quit the program)\n");
                Console.WriteLine("Enter the X Y coordinates...");
                Console.WriteLine("");
                figurestream = Console.ReadLine();
                CommandsValidation(() => InnerFindFiguresWithXY(figurestream), figurestream);                
            }
        }

        /*********************************************************************
         * Subprocess of FindFigures in order to be used as Parameter Method
         *********************************************************************/
        private void InnerFindFiguresWithXY(string pFigureStream) {
            ConsoleKeyInfo b;
            string error = "";
            string[] Coordinates;
            double FindX = 0, FindY = 0, TotalArea = 0;
            int i = 0, j = 0;

            error = ValidateCoordinates(pFigureStream);
            if(error.Trim() != "") {
                Console.Write(error + "\nPlease hit any key to try again");
                b = Console.ReadKey();
            } else {
                Coordinates = pFigureStream.Split(' ');
                FindX = double.Parse(Coordinates[0]);
                FindY = double.Parse(Coordinates[1]);
                Console.WriteLine("\nThe matching figures found for coordinates ({0},{1}) are:\n", Coordinates[0], Coordinates[1]);
                TotalArea = 0;

                foreach(figure figurePointer in myFigures) {
                    double localArea = 0;
                    string areaMessage = "";
                    
                    switch(figurePointer.type.Trim().ToLower()) {
                        case "circle": if((figurePointer as circle).FindIfXYareInsideMe(FindX, FindY)) {
                                           localArea = (figurePointer as circle).CalculateArea();
                                           areaMessage = "Thse area of the circle is: " + localArea.ToString();
                                       }
                                       break;

                        case "square": if((figurePointer as square).FindIfXYareInsideMe(FindX, FindY)) {
                                           localArea = (figurePointer as square).CalculateArea();
                                           areaMessage = "The area of the square is: " + localArea.ToString();
                                       }
                                       break;

                        case "rectangle": if((figurePointer as rectangle).FindIfXYareInsideMe(FindX, FindY)) {
                                              localArea = (figurePointer as rectangle).CalculateArea();
                                              areaMessage = "The area of the rectangle is: " + localArea.ToString();
                                          }
                                          break;

                        case "triangle": if((figurePointer as triangle).FindIfXYareInsideMe(FindX, FindY)) {
                                             localArea = (figurePointer as triangle).CalculateArea();
                                             areaMessage = "The area of the triangle is: " + localArea.ToString();
                                         }
                                         break;

                        case "donut": if((figurePointer as donut).FindIfXYareInsideMe(FindX, FindY)) {
                                          localArea = (figurePointer as donut).CalculateArea();
                                          areaMessage = "The area of the donut is: " + localArea.ToString();
                                      }
                                      break;                        
                    }

                    if(areaMessage != "") {
                        TotalArea += localArea;
                        Console.WriteLine("figure {0}: {1}", figurePointer.ID, figurePointer.DescribeMe());
                        Console.WriteLine(areaMessage + "\n");
                        i++; j++;
                        DisplayCounterBar(ref i, j, -1);                    
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                if(TotalArea > 0)
                    Console.WriteLine("\nThe total accumulated Area is: {0}\n", TotalArea.ToString());
                else
                    Console.WriteLine("\nNo figures found containing those coordinates\n");
                Console.ResetColor();
                Console.Write("Please hit any key to try again");
                b = Console.ReadKey();
            }
        }

        /**********************************************************************
         * This is a validation method that checks up the XY coordinates entered
         * by the user and throws a message if an error is found.
         ***********************************************************************/
        private string ValidateCoordinates(string pStream) {            
            string error = "";
            byte errorCount = 0;
            string[] Coordinates = pStream.Split(' ');
            double FormattedX, FormattedY;
            if(Coordinates.Length!=2) {
                error = (Coordinates.Length>2)?"Error: you sent more than two parameters\n":"Error: you sent less than two parameters\n";
                errorCount++;
            }            
            if(Coordinates.Length > 0) {
                if(!double.TryParse(Coordinates[0], out FormattedX)){
                    error += ((errorCount>0)?"and ":"")+"the X coordinate is not a decimal number";
                    errorCount++;
                }
                if(Coordinates.Length > 1) {
                    if(!double.TryParse(Coordinates[1], out FormattedY))
                        error += ((errorCount > 0) ? "also " : "") + "the Y coordinate is not a decimal number.";                    
                }
            }            
            return error;
        }
        #endregion

        #region ListingMethods
        /**********************************************************************
         * This method controls the data flow of events when the program displays
         * the figures stored in memory.
         ***********************************************************************/
        private void DisplayExistingFigures() {
            ConsoleKeyInfo b;
            int i = 0, j = 0;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*****************************************");
            Console.WriteLine("* Author: Fabrizio Salas-Soto           *");
            Console.WriteLine("* Phone:  +506 88866743                 *");
            Console.WriteLine("* Email:  fsalas@racsa.co.cr            *");
            Console.WriteLine("*****************************************");
            Console.WriteLine("*        F I G U R E   L I S T          *");
            Console.WriteLine("*****************************************\n");
            Console.ResetColor();
            Console.WriteLine("List of the existing figures in memmory\n");            
            foreach(figure FigurePointer in myFigures) {
                i++; j++;
                Console.WriteLine("\nfigure {0}: {1}\n", FigurePointer.ID, FigurePointer.DescribeMe());
                DisplayCounterBar(ref i, j, myFigures.Count);
            }
            Console.Write("\n\nPlease hit any key to continue");
            b = Console.ReadKey();
        }
        #endregion

        #region CleaningMethods
        /**********************************************************************
         * This method controls the data flow of events when the program erases
         * the figures stored in memory and sends a confirmation back to screen
         ***********************************************************************/
        private void CleanFiguresInMemmory() {
            Console.Clear();
            myFigures.Clear();
            myFiguresCounter = 0;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*****************************************");
            Console.WriteLine("* Author: Fabrizio Salas-Soto           *");
            Console.WriteLine("* Phone:  +506 88866743                 *");
            Console.WriteLine("* Email:  fsalas@racsa.co.cr            *");
            Console.WriteLine("*****************************************");
            Console.WriteLine("*        F I G U R E   E R A S E        *");
            Console.WriteLine("*****************************************\n");
            Console.ResetColor();
            Console.WriteLine("All the existing figures in memmory have been cleared\n");            
            Console.WriteLine();
            Console.Write("Please hit any key to continue");
            ConsoleKeyInfo b = Console.ReadKey();
        }
        #endregion

        #region Miscelaneous
        /****************************************************************************
         * This is the main controller for the system commands, it's been refactored
         * to centralized the entire validations and call a method sent as a parameter
         ****************************************************************************/
        private void CommandsValidation(Action pMethod, string StreamToValidate){
            ConsoleKeyInfo b;
            switch(StreamToValidate.Trim().ToLower()) {
                case "back": break;

                case "exit": ExitProgram();
                             break;

                case "help": DisplayHelp();
                             break;

                case "": Console.Write("Error: you entered no arguments! Please hit any key to try again");
                         b = Console.ReadKey();
                         break;

                default: pMethod();
                         break;
            }
        }

        /****************************************************************************
         * This method displays the bar counter for any listing method on the system
         * and adds the pause to continue once the user read the data.
         ****************************************************************************/
        private void DisplayCounterBar(ref int pI, int pJ, int pTotal) {
            ConsoleKeyInfo b;
            string DisplayText = (pTotal!=-1)?"\n  <=====[{0} FIGURES OF {1}]===================[HIT ANY KEY TO CONTINUE]=====>":"\n  <=====[{0} FIGURES SHOWN]=====================[HIT ANY KEY TO CONTINUE]=====>";
            if(pI == 9) {
                pI = 0;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(DisplayText, pJ, pTotal);
                Console.ResetColor();
                b = Console.ReadKey();
            }
        }
        #endregion

        #region Help
        /**********************************************************************
         * This method provides the help user manual by screen on how to use
         * this program.
         ***********************************************************************/
        private void DisplayHelp() {            
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*************************************************");
            Console.WriteLine("* Author: Fabrizio Salas-Soto                   *");
            Console.WriteLine("* Phone:  +506 88866743                         *");
            Console.WriteLine("* Email:  fsalas@racsa.co.cr                    *");
            Console.WriteLine("*************************************************");
            Console.WriteLine("*        H E L P   -  C O M M A N D S           *");
            Console.WriteLine("*************************************************");
            Console.WriteLine("* help - displays help.                         *");
            Console.WriteLine("* exit - terminates execution.                  *");
            Console.WriteLine("* back - return to prior menu.                  *");
            Console.WriteLine("*************************************************");
            Console.WriteLine("*       F I G U R E   E N T E R I N G           *");
            Console.WriteLine("*************************************************");
            Console.WriteLine("* Choose option 1 from main meny then, proceed  *");
            Console.WriteLine("* to enter a figure, the format should be very  *");
            Console.WriteLine("* precise as follows, do not add commas between *");
            Console.WriteLine("* commas between values and no parenthensis     *");
            Console.WriteLine("* either, only spaces. Except for the name of   *");
            Console.WriteLine("* the figure, the values should be real numbers *");
            Console.WriteLine("* numbers adding a dot to enter the fraccional  *");
            Console.WriteLine("* part.                                         *");
            Console.WriteLine("*                                               *");
            Console.WriteLine("* circle X-position Y-position Radius           *");
            Console.WriteLine("* example: circle 1.7 -5.05 6.9                 *");
            InnerDisplayHelp();
            Console.WriteLine("*                                               *");            
            Console.WriteLine("* square X-position Y-position length           *");
            Console.WriteLine("* example: square 3.55 4.1 2.77                 *");
            Console.WriteLine("*                                               *");
            Console.WriteLine("* rectangle X-position Y-position width height  *");
            Console.WriteLine("* example: rectangle 3.5 2.0 5.6 7.2            *");
            Console.WriteLine("*                                               *");
            Console.WriteLine("* triangle X1 Y1 X2 Y2 X3 Y3                    *");
            Console.WriteLine("* (Xi as X-Axis value and Yi as Y-Axis value)   *");
            Console.WriteLine("* example: triangle 4.5 1 -2.5 -33 23 0.3       *");
            Console.WriteLine("*                                               *");
            Console.WriteLine("* donut X-position Y-position radius1 radius2   *");
            Console.WriteLine("* example: donut 4.5 7.8 1.5 1.8                *");
            Console.WriteLine("*                                               *");            
            Console.WriteLine("*************************************************");
            Console.WriteLine("*       F I N D    A    (X,Y) P O I N T         *");
            Console.WriteLine("*************************************************");
            Console.WriteLine("* Choose option 2 in main menu then you will be *");
            Console.WriteLine("* asked to enter the values of the (X,Y) point. *");
            Console.WriteLine("* Enter the two numbers without symbols just    *");
            Console.WriteLine("* separated by spaces, adding the positive or   *");
            Console.WriteLine("* negative symbol plus dot to separate the      *");
            Console.WriteLine("* fraccional values                             *");
            Console.WriteLine("*                                               *");
            InnerDisplayHelp();
            Console.WriteLine("* Example: 7.95 -18.36                          *");
            Console.WriteLine("*                                               *");            
            Console.WriteLine("*************************************************");
            Console.WriteLine("*       L I S T   A L L   F I G U R E S         *");
            Console.WriteLine("*************************************************");
            Console.WriteLine("* Choose option 3 in main menu then you will be *");
            Console.WriteLine("* able to automatically display all the figures *");
            Console.WriteLine("* entered until the current time.               *");
            Console.WriteLine("*                                               *");
            Console.WriteLine("*************************************************");
            Console.WriteLine("*       C L E A N   A L L   F I G U R E S       *");
            Console.WriteLine("*************************************************");
            Console.WriteLine("* Choose option 4 in main menu then you will be *");
            Console.WriteLine("* able to automatically erase all the figures   *");
            Console.WriteLine("* entered until the current time.               *");
            Console.WriteLine("*                                               *");            
            Console.WriteLine("*************************************************");
            Console.WriteLine("*     B A T C H   L O A D   P R O C E S S       *");
            Console.WriteLine("*************************************************");
            Console.WriteLine("* Choose option 5 in main menu then you will be *");
            Console.WriteLine("* taken to the screen that asks for the path &  *");
            Console.WriteLine("* filename where the figures are stored.        *");
            Console.WriteLine("* Please enter the full path and filename as    *");
            Console.WriteLine("* the following example:                        *");
            InnerDisplayHelp();
            Console.WriteLine("*                                               *");
            Console.WriteLine("* c:\\figures.txt                                *");
            Console.WriteLine("*                                               *");
            Console.WriteLine("* The file should be a flat text file with the  *");
            Console.WriteLine("* figures in the same format explained on the   *");
            Console.WriteLine("* section: 'Figure Entering' (please refer to   *");
            Console.WriteLine("* this section if you have any question).       *");            
            Console.WriteLine("* A good example of how the file content should *");
            Console.WriteLine("* look like is the following:                   *");
            Console.WriteLine("*                                               *");
            Console.WriteLine("* circle 1.7 -5.05 6.9                          *");
            Console.WriteLine("* square 3.55 4.1 2.77                          *");
            Console.WriteLine("* rectangle 3.5 2.0 5.6 7.2                     *");
            Console.WriteLine("* triangle 4.5 1 -2.5 -33 23 0.3                *");
            Console.WriteLine("* donut 4.5 7.8 1.5 1.8                         *");
            Console.WriteLine("*                                               *");
            Console.WriteLine("*************************************************");
            Console.WriteLine("*                  H E L P                      *");
            Console.WriteLine("*************************************************");
            Console.WriteLine("* Choose option 6 in main menu then you will be *");
            Console.WriteLine("* provided with the current help information or *");
            Console.WriteLine("* type the word help on any section where you   *");
            Console.WriteLine("* might be allowed to type a word.              *");
            Console.WriteLine("*                                               *");
            InnerDisplayHelp();            
            Console.WriteLine("*************************************************");
            Console.WriteLine("*         E X I T  T H E   P R O G R A M        *");
            Console.WriteLine("*************************************************");
            Console.WriteLine("* Choose option 0 in main menu in order to exit *");
            Console.WriteLine("* the application or type the word exit on any  *");
            Console.WriteLine("* section where you might be allowed to type    *");
            Console.WriteLine("* a word.                                       *");
            Console.WriteLine("*                                               *");
            Console.WriteLine("*************************************************");
            Console.WriteLine("*          THANKS FOR YOUR ATTENTION            *");
            Console.WriteLine("*************************************************");            
            InnerDisplayHelp();
            Console.ResetColor();
        }

        /****************************************************************************
         * This method does the internal wait key part for the help display part.
         ****************************************************************************/
        private void InnerDisplayHelp() {
            ConsoleKeyInfo b;
            Console.ResetColor();
            Console.Write("     < ----- hit any key to continue ---->       ");                        
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            b = Console.ReadKey();            
        }
        #endregion

        #region Quit
        /**********************************************************************
         * This method controls the end of the entire execution.
         ***********************************************************************/
        private void ExitProgram() {
            Console.Clear();
            Console.Write("\n\nGood bye, thanks for your time!\nHit any key to close the command window");            
            ConsoleKeyInfo b = Console.ReadKey();
            Environment.Exit(0);
        }
        #endregion               
    }    
}