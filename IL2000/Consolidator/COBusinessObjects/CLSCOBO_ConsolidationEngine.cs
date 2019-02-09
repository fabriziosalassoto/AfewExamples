using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COBusinessObjects
{
    public class CLSCOBO_ConsolidationEngine{
        private const int c_INT_ITERATIONS=10000;        
        private string[,] as_PointsDistanceMatrix = new string[0,0];
        private List<CLSCOBO_ConsolidationSolution> ao_ConsolidationSolutions;
        private List<CLSCOBO_ConsolidationSolution> vo_ConsolidationSolutions;
        CLSCOBO_ConsolidationSolution vo_ConsolidationSolution=null;

        public CLSCOBO_ConsolidationEngine(CLSCOBO_ConsolidationProblem po_ConsolidationProblem){            
            ao_ConsolidationSolutions = new List<CLSCOBO_ConsolidationSolution>();
            as_PointsDistanceMatrix = FillInDistancesMatrix(po_ConsolidationProblem);

            for(int vi_IterationTotal=1;vi_IterationTotal<=1;vi_IterationTotal++) {
                vo_ConsolidationSolutions=new List<CLSCOBO_ConsolidationSolution>();
                for(int vi_Iteration=0;vi_Iteration<2;vi_Iteration++) {
                    vo_ConsolidationSolution=null;
                    CLSCOBO_ConsolidationSolution vo_PreviousConsolidationSolution=null;
                    if(vi_Iteration==0) {
                        vo_ConsolidationSolution=new CLSCOBO_ConsolidationSolution(po_ConsolidationProblem);
                        vo_ConsolidationSolutions.Add(vo_ConsolidationSolution);
                    } else {
                        vo_PreviousConsolidationSolution=vo_ConsolidationSolutions[0];
                        while(vo_PreviousConsolidationSolution.TruckItineraries.Count>4) {
                            vo_ConsolidationSolution=new CLSCOBO_ConsolidationSolution(vo_PreviousConsolidationSolution.TruckItineraries, vi_Iteration);
                            if(vo_ConsolidationSolution.TruckItineraries.Count!=0) {
                                vo_ConsolidationSolutions.Add(vo_ConsolidationSolution);
                            }
                            vo_PreviousConsolidationSolution=vo_ConsolidationSolutions[vo_ConsolidationSolutions.Count-1];                                                   
                        }
                    }
                }
                ao_ConsolidationSolutions.AddRange(vo_ConsolidationSolutions);
                vo_ConsolidationSolutions=null;  
            }
        }

        public List<CLSCOBO_ConsolidationSolution> ConsolidationSolutions{
            get { return ao_ConsolidationSolutions; }
        }

        public int getDistance(string ps_ZipCode1, string ps_ZipCode2){
            bool vb_Found = false;
            int vi_Distance = 0, x = 1, y = 1;

            while (!vb_Found && x < as_PointsDistanceMatrix.GetLength(0)){
                if(as_PointsDistanceMatrix[x,1]==ps_ZipCode1){
                    vb_Found=true;
                }else{
                    x++;
                }
            }
            vb_Found=false;
            while (!vb_Found && y < as_PointsDistanceMatrix.GetLength(1)){
                if (as_PointsDistanceMatrix[1, y] == ps_ZipCode2){
                    vb_Found = true;                    
                }
                else{
                    y++;
                }
            }
            if(vb_Found)
                vi_Distance = int.Parse(as_PointsDistanceMatrix[x, y]);
            return vi_Distance;
        }

        private string[,] FillInDistancesMatrix(CLSCOBO_ConsolidationProblem po_ConsolidationProblem){
            string z="";
            string[,] vs_PointsDistanceMatrix=new string[po_ConsolidationProblem.Deliveries.Count+2, po_ConsolidationProblem.Deliveries.Count+2];
            vs_PointsDistanceMatrix[0, 0]="*";
            CLSCOBO_BasePoint vo_BasePoint1=null, vo_BasePoint2=null;

            for(int x=-1;x<po_ConsolidationProblem.Deliveries.Count;x++) {
                if(x==-1) {
                    z=po_ConsolidationProblem.OriginPoint.ZipCode;
                    vo_BasePoint1=po_ConsolidationProblem.OriginPoint;
                } else {
                    z=po_ConsolidationProblem.Deliveries[x].ZipCode;
                    vo_BasePoint1=po_ConsolidationProblem.Deliveries[x];
                }
                vs_PointsDistanceMatrix[x+2, 0]=z;
                for(int y=-1;y<po_ConsolidationProblem.Deliveries.Count;y++) {
                    if(y==-1) {
                        z=po_ConsolidationProblem.OriginPoint.ZipCode;
                        vo_BasePoint2=po_ConsolidationProblem.OriginPoint;
                    } else {
                        z=po_ConsolidationProblem.Deliveries[y].ZipCode;
                        vo_BasePoint2=po_ConsolidationProblem.Deliveries[y];
                    }
                    vs_PointsDistanceMatrix[0, y+2]=z;
                    vs_PointsDistanceMatrix[x+2, y+2]=(x==y)?"*":CLSCOBO_ConsolidatorUtils.getDistance(vo_BasePoint1, vo_BasePoint2).ToString();
                }
            }
            return vs_PointsDistanceMatrix;
        }
    }
}
