using System;
using System.Collections.Generic;
using System.Text;

namespace COBusinessObjects{
    public class CLSCOBO_ConsolidationSolution
    {
        private List<CLSCOBO_TruckItinerary> al_TruckItineraries;

        public List<CLSCOBO_TruckItinerary> TruckItineraries {
            get { return al_TruckItineraries; }
        }

        public CLSCOBO_ConsolidationSolution(CLSCOBO_ConsolidationProblem po_ConsolidationProblem){
            List<CLSCOBO_DeliveryPoint> vl_DeliveryPoints;
            al_TruckItineraries = new List<CLSCOBO_TruckItinerary>();

            foreach (CLSCOBO_DeliveryPoint vo_DeliveryPoint in po_ConsolidationProblem.Deliveries){
                vl_DeliveryPoints = new List<CLSCOBO_DeliveryPoint>();
                vl_DeliveryPoints.Add(vo_DeliveryPoint);
                CLSCOBO_TruckItinerary vo_TruckItinerary = new CLSCOBO_TruckItinerary(po_ConsolidationProblem.OriginPoint, vl_DeliveryPoints);
                al_TruckItineraries.Add(vo_TruckItinerary);
            }            
        }

        public CLSCOBO_ConsolidationSolution(List<CLSCOBO_TruckItinerary> pl_TruckItineraries, int pi_Iteration) {
            int vi_RandomPoint=CLSCOBO_FunctionsRepository.GenerateRandomNumber(0, pl_TruckItineraries.Count-1);
            int vi_PointerMover=0, vi_PointerInitializer=0;
            int vi_LeastDistance=int.MaxValue, vi_CurrentDistance=0;
            CLSCOBO_OriginPoint vo_OriginPoint=pl_TruckItineraries[vi_RandomPoint].OriginPoint;
            bool vb_SameQuadrant=true;
            bool vb_Reverse=false;            
            List<CLSCOBO_DeliveryPoint> vl_DeliveryPoints;
            List<CLSCOBO_DeliveryPoint> vo_DeliveryPoints1, vo_DeliveryPoints2;
            CLSCOBO_TruckItinerary vo_TruckItinerary=null;
            List<CLSCOBO_TruckItinerary> vo_TruckItineraries;// = new List<CLSCOBO_TruckItinerary>();
            al_TruckItineraries = new List<CLSCOBO_TruckItinerary>();

            for(int vi_Possibillity=1;vi_Possibillity<=(2*(pl_TruckItineraries.Count-1));vi_Possibillity++) {                
                vi_PointerMover=vi_PointerInitializer;                
                vo_TruckItineraries=new List<CLSCOBO_TruckItinerary>();
                vb_SameQuadrant=true;
                for(int vi_Pointer=0;vi_Pointer<pl_TruckItineraries.Count;vi_Pointer++) {
                    vl_DeliveryPoints=new List<CLSCOBO_DeliveryPoint>();                    
                    if(vi_Pointer!=vi_RandomPoint) {                        
                        vo_DeliveryPoints1=pl_TruckItineraries[vi_RandomPoint].Deliveries;      
                        vo_DeliveryPoints2=pl_TruckItineraries[vi_PointerMover].Deliveries;                  
                        if(vi_PointerMover==vi_Pointer){                           
                            if(!vb_Reverse) {                                
                                vl_DeliveryPoints.AddRange(vo_DeliveryPoints1);
                                vl_DeliveryPoints.AddRange(vo_DeliveryPoints2);
                                vb_SameQuadrant=IsSameQuadrant(vo_OriginPoint,vo_DeliveryPoints1[vo_DeliveryPoints1.Count-1], vo_DeliveryPoints2[0]);
                                vb_Reverse=true;
                            } else {
                                vl_DeliveryPoints.AddRange(vo_DeliveryPoints2);                                
                                vl_DeliveryPoints.AddRange(vo_DeliveryPoints1);
                                vb_SameQuadrant=IsSameQuadrant(vo_OriginPoint,vo_DeliveryPoints2[vo_DeliveryPoints2.Count-1], vo_DeliveryPoints1[0]);
                                vb_Reverse=false;
                            }                            
                        } else {
                            vi_PointerMover=(vi_Pointer<vi_PointerMover)?vi_PointerMover:vi_PointerMover+1;
                            vo_DeliveryPoints2=pl_TruckItineraries[vi_Pointer].Deliveries;
                            vl_DeliveryPoints.AddRange(vo_DeliveryPoints2);
                        }
                        vo_TruckItinerary=new CLSCOBO_TruckItinerary(vo_OriginPoint, vl_DeliveryPoints);
                        vo_TruckItineraries.Add(vo_TruckItinerary);                        
                    } else {
                        vi_PointerMover=(vi_Possibillity<(2*(pl_TruckItineraries.Count-1))-1)?vi_PointerMover+1:vi_PointerMover;
                    }
                }
                if(vi_Possibillity%2==0){
                    vi_PointerInitializer++;
                    if(vi_PointerInitializer==vi_RandomPoint)
                        vi_PointerInitializer++;
                }
                vi_CurrentDistance=getLeastDistanceTruckItinerary(vo_TruckItineraries);
                if((vi_CurrentDistance<vi_LeastDistance) && vb_SameQuadrant) {
                    vi_LeastDistance=vi_CurrentDistance;
                    al_TruckItineraries=vo_TruckItineraries;
                } 
            }                       
        }

        private bool IsSameQuadrant(CLSCOBO_BasePoint po_OriginPoint, CLSCOBO_DeliveryPoint po_DeliveryPointFrom, CLSCOBO_DeliveryPoint po_DeliveryPointTo) {
            bool vb_SameQuadrant;
            if(CLSCOBO_FunctionsRepository.CalculateCuadrant(po_OriginPoint,po_DeliveryPointFrom)==CLSCOBO_FunctionsRepository.CalculateCuadrant(po_OriginPoint,po_DeliveryPointTo))
                vb_SameQuadrant=true;
            else
                vb_SameQuadrant=false;            
            return vb_SameQuadrant;
        }
        
        public int ComputeUtility(){
            int vi_Utility=0;

            foreach(CLSCOBO_TruckItinerary vo_TruckItinerary in al_TruckItineraries){
                vi_Utility+=vo_TruckItinerary.getTotalDistanceFromOriginPoint();
            }
            vi_Utility *= 500; // Later on it needs to add the amount of trucks            
            return vi_Utility;
        }

        private int getLeastDistanceTruckItinerary(List<CLSCOBO_TruckItinerary> po_TruckItineraries) {            
            int vi_TotalDistance=0;
            int vi_ComposedItineraryPointer=0;
            foreach(CLSCOBO_TruckItinerary vo_TruckItineraryPointer in po_TruckItineraries) {
                if(vo_TruckItineraryPointer.TotalDestinationsCount>vi_ComposedItineraryPointer) {
                    //vi_TotalDistance=vo_TruckItineraryPointer.getTotalDistanceAmongDeliveryPoints();                    
                    vi_TotalDistance=vo_TruckItineraryPointer.getTotalDistanceFromOriginPoint();//
                    vi_ComposedItineraryPointer=vo_TruckItineraryPointer.TotalDestinationsCount;
                }
            }
            return vi_TotalDistance;
        }        
    }     
}