<?php
	/********************************* BOF *************************************
	 * Author: 		  Fabrizio Salas										   *
	 * Date Creation: 12/15/2015											   *	
	 * Last Edition:  01/12/2016											   *	
	 * Description:   This library provides all methods to connect the rain_db *
	 *                Database in order to export queries as XML format		   *
	 ***************************************************************************/
	header("Content-type: text/xml");	    		    		
    
	/**********************************************
     *		 DB Connection - Base Class			  *
	 **********************************************/
    interface DataKeys{
		const Value = "value";
		const Comparison = "comparison";
	}
	 
	interface DataComparison{
		const GreaterThan = "greater-than";
		const GreaterOrEqualThan = "greater-or-equal-than"; 
		const SmallerThan = "smaller-than";
		const SmallerOrEqualThan = "smaller-or-equal-than";
		const Equal = "equal";
		const LeftLike = "left-like";
		const RightLike = "right-like";
		const Like = "like";
	} 
	 
    final class DBConn{
        var $conn = null;
		var $utilities = null;
		
		public function __construct(){
            $this->utilities = new Utilities();        
        }
		
		private function GetFieldType($table_name, $field_name){
			$results = null;
			$sql = "SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS ".
				   "WHERE table_name = '".$table_name."' AND COLUMN_NAME = '".$field_name."'";
			$type = "";
			
            if($this->OpenConnection()){
                $result = mysqli_query($this->conn, $sql);                
				while($row = mysqli_fetch_assoc($result)){                 
                    foreach($row as $name => $value){
             			$type = $value;       
                    }                    
                }
				$this->CloseConnection();
            }
            return $type;			
		}		

        private function OpenConnection(){
            $this->conn = mysqli_connect("localhost", "root", "Sc0!!ard122#","rain_db");
            if($this->conn!=null)
                return true;			
            else
                return false;
        }

        private function RunSQL($sql){
            $result = mysqli_query($this->conn, $sql);
            $writer = new XMLWriter();
			$writer->openMemory();                       
			$writer->setIndent(true);
			//$writer->setIndent("    ");
            $writer->startDocument("1.0","UTF-8");			
            $writer->startElement('XML');
			
            if(($result==null || $result=="") && $result!="0"){
                $writer->startElement("ERROR");
                $writer->text("Query couldn't find any result");
                $writer->endElement();
            } else {
                while($row = mysqli_fetch_assoc($result)){
                    $writer->startElement("record");
                    foreach($row as $name => $value){
                        $writer->startElement($name);
                        $writer->text($value);
                        $writer->endElement();
                    }
                    $writer->endElement();
                }
            }
            $writer->endElement();
            //$writer->endDocument();
            $result = $writer->outputMemory();
            $writer->flush();
            return $result;					
			//return '<xml>'.$sql.'</xml>';
        }

        private function CloseConnection(){
            if(mysqli_close($this->conn ))
                return true;
            else
                return false;
        }

        private function ExecuteQuery($sql){
            $results = null;
            if($this->OpenConnection()){
                $results = $this->RunSQL($sql);
                $this->CloseConnection();
            }
            return $results;
        }
		
		public function ParseQuery($table_name, $field_value_comparison){				
			$sql_String = "select * from ".$table_name;
            $fields = 0;
			
			if($this->utilities->NotEmpty($field_value_comparison)){
				foreach($field_value_comparison as $key => $value){			
					if($this->utilities->NotEmpty($key)){
						$quotes = "";
						$comodinleft = "";
						$comodinright = "";						
						$union = " where ";
						$comparison = "";	
						$field_value = $value["value"];
	
						if($fields > 0)
							$union = " and ";					
						if(in_array($this->GetFieldType($table_name, $key), array("varchar", "text", "char", "date", "datetime"))){
							$quotes = "'";
							if(in_array($this->GetFieldType($table_name, $key), array("varchar", "text", "char"))){
								$field_value = strtoupper($field_value);
								$key = "UPPER(".$key.")";
							} else {
								$key = "DATE_FORMAT(".$key.",'%Y-%m-%d')";
							}
						}

																
						switch($value["comparison"]){
							case DataComparison::GreaterThan : $comparison = " > "; break;
							case DataComparison::GreaterOrEqualThan: $comparison = " >= "; break; 
							case DataComparison::SmallerThan : $comparison = " < "; break;
							case DataComparison::SmallerOrEqualThan : $comparison = " <= "; break;
							case DataComparison::Equal : $comparison = " = "; break;
							case DataComparison::LeftLike : $comparison = " like "; $comodinleft = "%"; break;
							case DataComparison::RightLike : $comparison = " like "; $comodinright = "%"; break;
							case DataComparison::Like : $comparison = " like "; $comodinleft = "%"; $comodinright ="%"; break;
						}
																															
						$sql_String.=$union.$key.$comparison.$quotes.$comodinleft.$field_value.$comodinright.$quotes;
						$fields++;
					}								
				}
			}
			//return "<xml>".$sql_String."</xml>";
            return $this->ExecuteQuery($sql_String);			
		}
    }
	
	/**********************************************
     *				 Utilities Class 			  *
	 **********************************************/	
    final class Utilities{
        public function NotEmpty($parameter){
            if(!isset($parameter)){
                return false;
            } else {
                if(($parameter==null || $parameter=="") && $parameter!=0){
                    return false;
                } else {
                    return true;
                }
            }
        }
    }
    
	/******************************************************
     * DB Queries related to the mr_tblavailability table *
	 ******************************************************/
	interface AvailabilitySearchType{
		const ALL = "All";
		const VillaId = "rainvillaid";
		const BookingType = "bookingtype";
		const BookingId = "bookingid";
		const ArrivalDate = "arrivaldate";
		const IDAndArrivalDate = "id-and-arrivaldate";
		const DepartureDate = "departuredate";
		const IDAndDepartureDate = "id-and-departuredate";
		const ArrivalAndDeparture = "arrival-and-departure";
		const IDAndArrivalAndDeparture = "id-and-arrival-and-departure";				
		const Status = "status";
	}
	
	interface AvailabilityCalendarSearchType{
		const IDAndInitialDateAndEndDate = "id-and-initialdate-and-enddate";				
	}
	
	final class DB_mr_tblavailability{		
        var $connection = null;
		var $table = "mr_tblAvailability";
    
        public function __construct(){
            $this->connection = new DBConn();           
        }
		
		public function GetALLAvailability(){								              			
            return $this->connection->ParseQuery($this->table,null);
		}
		
		public function GetAvailabilityByRainVillaId($rainvillaid){
			$checks = array(
							AvailabilitySearchType::VillaId => array(
										 							DataKeys::Value => $rainvillaid,
																	DataKeys::Comparison => DataComparison::Equal,
							 			  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
	
		public function GetAvailabilityByBookingType($bookingtype){
			$checks = array(
							AvailabilitySearchType::BookingType => array(
										 							DataKeys::Value => $bookingtype,
																	DataKeys::Comparison => DataComparison::Equal,
													 			  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
	
		public function GetAvailabilityByBookingId($bookingid){
			$checks = array(
							AvailabilitySearchType::BookingId => array(
										 							DataKeys::Value => $bookingid,
																	DataKeys::Comparison => DataComparison::Equal,
													 			  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}

		public function GetAvailabilityByArrivalDate($arrivaldate){
			$checks = array(
							AvailabilitySearchType::ArrivalDate => array(
										 							DataKeys::Value => $arrivaldate,
																	DataKeys::Comparison => DataComparison::RightLike,
													 			  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
		
		public function GetAvailabilityByIDAndArrivalDate($rainvillaid, $arrivaldate){			            			
			$checks = array(
							AvailabilitySearchType::VillaId => array(
										 							DataKeys::Value => $rainvillaid,
																	DataKeys::Comparison => DataComparison::Equal,
													 			  ),
							AvailabilitySearchType::ArrivalDate => array(
																	DataKeys::Value => $arrivaldate,
																	DataKeys::Comparison => DataComparison::RightLike,
																  )
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}

		public function GetAvailabilityByDepartureDate($departuredate){
			$checks = array(
							AvailabilitySearchType::DepartureDate => array(
										 							DataKeys::Value => $departuredate,
																	DataKeys::Comparison => DataComparison::RightLike,
													 			  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
		
		public function GetAvailabilityByIDAndDepartureDate($rainvillaid, $departuredate){
			$checks = array(
							AvailabilitySearchType::VillaId => array(
										 							DataKeys::Value => $rainvillaid,
																	DataKeys::Comparison => DataComparison::Equal,
													 			  ),
							AvailabilitySearchType::DepartureDate => array(
																	DataKeys::Value => $departuredate,
																	DataKeys::Comparison => DataComparison::RightLike,
																  )
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
		
		public function GetAvailabilityByArrivalAndDeparture($arrivaldate,$departuredate){
			$checks = array(
							AvailabilitySearchType::ArrivalDate => array(
										 							DataKeys::Value => $arrivaldate,
																	DataKeys::Comparison => DataComparison::GreaterOrEqualThan,
													 			  ),
							AvailabilitySearchType::DepartureDate => array(
																	DataKeys::Value => $departuredate,
																	DataKeys::Comparison => DataComparison::SmallerOrEqualThan,
																  )
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
		
		public function GetAvailabilityCalendar($rainvillaid, $strDateFrom, $strDateTo){					
			$dates = new SimpleXMLElement($this->GetAvailabilityByRainVillaId($rainvillaid));
		
            $calwriter = new XMLWriter();
			$calwriter->openMemory();                       
			$calwriter->setIndent(true);
            $calwriter->startDocument("1.0","UTF-8");			
            $calwriter->startElement('XML');
			$calwriter->startElement("record");			
				$calwriter->startElement("rainvillaid");			
				$calwriter->text($rainvillaid);
				$calwriter->endElement();	
				$calwriter->startElement("availabilities");			
					$aryRange=array();
				    $iDateFrom=mktime(1,0,0,substr($strDateFrom,5,2),substr($strDateFrom,8,2),substr($strDateFrom,0,4));
		    		$iDateTo=mktime(1,0,0,substr($strDateTo,5,2),substr($strDateTo,8,2),substr($strDateTo,0,4));

				    if ($iDateTo>=$iDateFrom){
						$calwriter->startElement("availability");			
							$calwriter->startElement("date");			
							$calwriter->text(date('Y-m-d',$iDateFrom)); // first entry
							$calwriter->endElement();
							$calwriter->startElement("available");								
							$flag=false;
							foreach($dates->record as $date_booked){								
								if(($date_booked->status=="RESERVED") || ($date_booked->status=="HOLD")){
										if((date('Y-m-d',$iDateFrom)>$date_booked->arrivaldate || date('Y-m-d',$iDateFrom)==$date_booked->arrivaldate) && date('Y-m-d',$iDateFrom)<=$date_booked->departuredate){
											$flag=true;
										}				
								}
							}	
	
							if($flag)							
								$calwriter->text('N');
							else
								$calwriter->text('Y');
							$calwriter->endElement();
						$calwriter->endElement();	
        				while ($iDateFrom<$iDateTo){
				            $iDateFrom+=86400; // add 24 hours
							$calwriter->startElement("availability");		
								$calwriter->startElement("date");			
								$calwriter->text(date('Y-m-d',$iDateFrom));
								$calwriter->endElement();								
								$calwriter->startElement("available");			
								$flag=false;
								$flagi = 0;							

							foreach($dates->record as $date_booked){

								$yourdate = GregorianToJD(date("m", strtotime($iDateFrom)),date("d", strtotime($iDateFrom)),date("Y", strtotime($iDateFrom)));
								$db_arrival = GregorianToJD(date("m", strtotime($date_booked->arrivaldate)),date("d", strtotime($date_booked->arrivaldate)),date("Y", strtotime($date_booked->arrivaldate)));
								$db_departure = GregorianToJD(date("m", strtotime($date_booked->departuredate)),date("d", strtotime($date_booked->departuredate)),date("Y", strtotime($date_booked->departuredate)));


								if(($date_booked->status=="RESERVED") || ($date_booked->status=="HOLD")){
									if((date('Y-m-d',$iDateFrom)>$date_booked->arrivaldate || date('Y-m-d',$iDateFrom)==$date_booked->arrivaldate) && date('Y-m-d',$iDateFrom)<$date_booked->departuredate){
											$flagi++;
									}
									/*$calwriter->startElement("check");			
									$calwriter->text($flagi);
									$calwriter->endElement();*/																		
								}		
							}		
							if($flagi>0){							
								$calwriter->text('N');
							} else {
								$calwriter->text('Y');
							}
							$calwriter->endElement();
							$calwriter->endElement();
		        		}
				    }		    		
				$calwriter->endElement();		// end of availabilities								
			$calwriter->endElement();	// end of record				                 
            $calwriter->endElement();
            $result = $calwriter->outputMemory();
            $calwriter->flush();
            return $result;																					
		}
		
		public function GetAvailabilityByIDAndArrivalAndDeparture($rainvillaid, $arrivaldate, $departuredate){
			$checks = array(
							AvailabilitySearchType::VillaId => array(
										 							DataKeys::Value => $rainvillaid,
																	DataKeys::Comparison => DataComparison::Equal,
													 			  ),
							
							AvailabilitySearchType::ArrivalDate => array(
										 							DataKeys::Value => $arrivaldate,
																	DataKeys::Comparison => DataComparison::GreaterOrEqualThan,
													 			  ),
							AvailabilitySearchType::DepartureDate => array(
																	DataKeys::Value => $departuredate,
																	DataKeys::Comparison => DataComparison::SmallerOrEqualThan,
																  )
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
		
		public function GetAvailabilityByStatus($status){
			$checks = array(
							AvailabilitySearchType::Status => array(
										 							DataKeys::Value => $status,
																	DataKeys::Comparison => DataComparison::Equal,
													 			  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
	}

	/******************************************************
     *	 DB Queries related to the mr_tblbooking table    *
	 ******************************************************/
	interface BookingSearchType{
		const ALL = "All";
		const VillaId = "rainvillaid";
		const StatusId = "statusid";
		const ClientId = "clientid";
		const LeadSourceId = "leadsourceid";
		const PartnerAgentId = "partneragentid";
		const SalesRepId = "salesrepid";
		const VendorId = "vendorid";
		const InvoiceNumber = "invoicenumber";
		const BookedDate = "bookeddate";
		const IDAndBookedDate = "id-and-bookeddate";	
	}
	
	final class DB_mr_tblbooking{
        var $connection = null;
		var $table = "mr_tblBooking";
    
        public function __construct(){            
            $this->connection = new DBConn();           
        }
		
		public function GetALLBookings(){			              			
            return $this->connection->ParseQuery($this->table,null);
		}
		
		public function GetBookingByRainVillaId($rainvillaid){
			$checks = array(
							BookingSearchType::VillaId => array(
										 						DataKeys::Value => $rainvillaid,
																DataKeys::Comparison => DataComparison::Equal,
													 		  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
	
		public function GetBookingByStatusId($statusid){
			$checks = array(
							BookingSearchType::StatusId => array(
										 						DataKeys::Value => $statusid,
																DataKeys::Comparison => DataComparison::Equal,
													 		  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}

		public function GetBookingByClientId($clientid){			
			$checks = array(
							BookingSearchType::ClientId => array(
										 						DataKeys::Value => $clientid,
																DataKeys::Comparison => DataComparison::Equal,
													 		  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);			
		}
		
		public function GetBookingByLeadSourceId($leadsourceid){
			$checks = array(
							BookingSearchType::LeadSourceId => array(
										 						DataKeys::Value => $leadsourceid,
																DataKeys::Comparison => DataComparison::Equal,
													 		  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);				
		}

		public function GetBookingByPartnerAgentId($partneragentid){			
			$checks = array(
							BookingSearchType::PartnerAgentId => array(
										 						DataKeys::Value => $partneragentid,
																DataKeys::Comparison => DataComparison::Equal,
													 		  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);							
		}

		public function GetBookingBySalesRepresentativeId($salesrepid){
			$checks = array(
							BookingSearchType::SalesRepId => array(
										 						DataKeys::Value => $salesrepid,
																DataKeys::Comparison => DataComparison::Equal,
													 		  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);							
		}
		
		public function GetBookingByVendorId($vendorid){
			$checks = array(
							BookingSearchType::VendorId => array(
										 						DataKeys::Value => $vendorid,
																DataKeys::Comparison => DataComparison::Equal,
													 		  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);							
		}

		public function GetBookingByInvoiceNbr($invoicenbr){
			$checks = array(
							BookingSearchType::InvoiceNumber => array(
										 						DataKeys::Value => $invoicenbr,
																DataKeys::Comparison => DataComparison::Equal,
													 		  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);							
			
		}
		
		public function GetBookingByBookedDate($bookeddate){
			$checks = array(
							BookingSearchType::BookedDate => array(
										 						DataKeys::Value => $bookeddate,
																DataKeys::Comparison => DataComparison::RightLike,
													 		  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);										
		}
		
		public function GetBookingByIDAndBookedDate($rainvillaid,$bookeddate){
			$checks = array(
							BookingSearchType::VillaId => array(
																DataKeys::Value => $rainvillaid,
																DataKeys::Comparison => DataComparison::Equal,
																),
			
							BookingSearchType::BookedDate => array(
										 						DataKeys::Value => $bookeddate,
																DataKeys::Comparison => DataComparison::RightLike,
													 		  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);										
		}
	}

	/*************************************************************
     *	 DB Queries related to the mr_tblbookingbilling table    *
	 *************************************************************/	
	interface BookingBillingSearchType{
		const ALL = "All";
		const BookingId = "bookingid";
		const Currency = "currency";
	}
	 
	final class DB_mr_tblbookingbilling{
		var $connection = null;
		var $table = "mr_tblBookingBilling";
    
        public function __construct(){            
            $this->connection = new DBConn();           
        }
		
		public function GetALLBookingBilling(){
            return $this->connection->ParseQuery($this->table,null);
		}
		
		public function GetBookingBillingByBookingId($bookingid){
			$checks = array(
							BookingBillingSearchType::BookingId => array(
										 								DataKeys::Value => $bookingid,
																		DataKeys::Comparison => DataComparison::Equal,
															 		  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}

		public function GetBookingBillingByCurrency($currency){
			$checks = array(
							BookingBillingSearchType::Currency => array(
										 								DataKeys::Value => $currency,
																		DataKeys::Comparison => DataComparison::Equal,
															 		  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
	 }

	/***********************************************************
     *	 DB Queries related to the mr_tblbookingclient table   *
	 ***********************************************************/	
	interface BookingClientSearchType{
		const ALL = "All";
		const BookingId = "bookingid";
		const Email = "email";
		const FirstName = "firstname";
		const LastName = "lastname";
		const FirstNameAndLastName = "first-and-lastname";
		const Phone = "phone";		
	}
	
	final class DB_mr_tblBookingClient{
		var $connection = null;
		var $table = "mr_tblBookingClient";
    
        public function __construct(){            
            $this->connection = new DBConn();           
        }
		
		public function GetALLBookingClient(){
            return $this->connection->ParseQuery($this->table,null);
		}
		
		public function GetBookingClientByBookingId($bookingid){
			$checks = array(
							BookingClientSearchType::BookingId => array(
										 								DataKeys::Value => $bookingid,
																		DataKeys::Comparison => DataComparison::Equal,
															 		  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}

		public function GetBookingClientByEmail($email){
			$checks = array(
							BookingClientSearchType::Email => array(
									 								DataKeys::Value => $email,
																	DataKeys::Comparison => DataComparison::Equal,
														 		  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
		
		public function GetBookingClientByFirstAndLastName($firstname, $lastname){
			$checks = array(
							BookingClientSearchType::FirstName => array(
									 								DataKeys::Value => $firstname,
																	DataKeys::Comparison => DataComparison::Equal,
														 		  ),
							BookingClientSearchType::LastName => array(
									 								DataKeys::Value => $lastname,
																	DataKeys::Comparison => DataComparison::Equal,
														 		  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}

		public function GetBookingClientByPhone($phone){
			$checks = array(
							BookingClientSearchType::Phone => array(
									 								DataKeys::Value => $phone,
																	DataKeys::Comparison => DataComparison::Equal,
														 		  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
	}
	 
	/************************************************************
     *	 DB Queries related to the mr_tblbookingexpense table   *
	 ************************************************************/	
	interface BookingExpenseSearchType{
		const ALL = "All";
		const BookingId = "bookingid";
	}
	
	final class DB_mr_tblBookingExpense{
		var $connection = null;
		var $table = "mr_tblBookingExpense";
    
        public function __construct(){            
            $this->connection = new DBConn();           
        }
		
		public function GetALLBookingExpense(){
            return $this->connection->ParseQuery($this->table,null);
		}
		
		public function GetBookingExpenseByBookingId($bookingid){
			$checks = array(
							BookingExpenseSearchType::BookingId => array(
										 								DataKeys::Value => $bookingid,
																		DataKeys::Comparison => DataComparison::Equal,
															 		  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
	}
	 
	/***********************************************************
     *	 DB Queries related to the mr_tblbookingfee table      *
	 ***********************************************************/	
	interface BookingFeeSearchType{
		const ALL = "All";
		const BookingId = "bookingid";
		const BookingFeeName = "feename";
	}
	
	final class DB_mr_tblBookingFee{
		var $connection = null;
		var $table = "mr_tblBookingFee";
    
        public function __construct(){            
            $this->connection = new DBConn();           
        }
		
		public function GetALLBookingFee(){
            return $this->connection->ParseQuery($this->table,null);
		}
		
		public function GetBookingFeeByBookingId($bookingid){
			$checks = array(
							BookingFeeSearchType::BookingId => array(
										 								DataKeys::Value => $bookingid,
																		DataKeys::Comparison => DataComparison::Equal,
															 		  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
		
		public function GetBookingFeeByFeeName($feename){
			$checks = array(
							BookingFeeSearchType::BookingFeeName => array(
										 								DataKeys::Value => $feename,
																		DataKeys::Comparison => DataComparison::RightLike,
															 		  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
	}
	 
	/***********************************************************
     *	 DB Queries related to the mr_tblbookingitems table    *
	 ***********************************************************/	 
	interface BookingItemsSearchType{
		const ALL = "All";
		const BookingId = "bookingid";
		const ItemName = "itemname";
	}
	
	final class DB_mr_tblBookingItems{
		var $connection = null;
		var $table = "mr_tblBookingItems";
    
        public function __construct(){            
            $this->connection = new DBConn();           
        }
		
		public function GetALLBookingItems(){
            return $this->connection->ParseQuery($this->table,null);
		}
		
		public function GetBookingItemsByBookingId($bookingid){
			$checks = array(
							BookingItemsSearchType::BookingId => array(
										 								DataKeys::Value => $bookingid,
																		DataKeys::Comparison => DataComparison::Equal,
															 		  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
		
		public function GetBookingItemsByItemName($itemname){
			$checks = array(
							BookingItemsSearchType::ItemName => array(
										 								DataKeys::Value => $itemname,
																		DataKeys::Comparison => DataComparison::RightLike,
															 		  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
	}
	 
	/***********************************************************
     *	 DB Queries related to the mr_tblbookingitems table    *
	 ***********************************************************/	 
	interface ClientSearchType{
		const ALL = "All";
		const ID = "id";
		const FirstName = "firstname";
		const LastName = "lastname";
		const FirstNameAndLastName = "first-and-lastname";
		const Phone = "phone";
		const Email = "email";		
	}	 
	
	final class DB_mr_tblClients{
		var $connection = null;
		var $table = "mr_tblClient";
    
        public function __construct(){            
            $this->connection = new DBConn();           
        }
		
		public function GetALLClients(){
            return $this->connection->ParseQuery($this->table,null);
		}
		
		public function GetClientByID($ID){
			$checks = array(
							ClientSearchType::ID => array(
										 					DataKeys::Value => $ID,
															DataKeys::Comparison => DataComparison::Equal,
														  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
		
		public function GetClientByFirstName($firstname){
			$checks = array(
							ClientSearchType::FirstName => array(
										 					DataKeys::Value => $firstname,
															DataKeys::Comparison => DataComparison::RightLike,
														  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
		
		public function GetClientByLastName($lastname){
			$checks = array(
							ClientSearchType::LastName => array(
										 					DataKeys::Value => $lastname,
															DataKeys::Comparison => DataComparison::RightLike,
														  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
		
		public function GetClientByFirstAndLastName($firstname, $lastname){
			$checks = array(
							ClientSearchType::FirstName => array(
										 					DataKeys::Value => $firstname,
															DataKeys::Comparison => DataComparison::RightLike,
														  ),
							ClientSearchType::LastName => array(
										 					DataKeys::Value => $lastname,
															DataKeys::Comparison => DataComparison::RightLike,
														  ),														  
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}

		public function GetClientByPhone($phone){
			$checks = array(
							ClientSearchType::Phone => array(
										 					DataKeys::Value => $phone,
															DataKeys::Comparison => DataComparison::Equal,
														  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}

		public function GetClientByEmail($email){
			$checks = array(
							ClientSearchType::Email => array(
										 					DataKeys::Value => $email,
															DataKeys::Comparison => DataComparison::Equal,
														  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
	}

	/***********************************************************
     *	 DB Queries related to the mr_tblclientnotes table     *
	 ***********************************************************/	 
	interface ClientNotesSearchType{
		const ALL = "All";
		const UserId = "userid";
		const ClientId = "clientid";
	}	 
	
	final class DB_mr_tblClientNotes{
		var $connection = null;
		var $table = "mr_tblClientNotes";
    
        public function __construct(){            
            $this->connection = new DBConn();           
        }
		
		public function GetALLClientNotes(){
            return $this->connection->ParseQuery($this->table,null);
		}
		
		public function GetClientNotesByUserId($userid){
			$checks = array(
							ClientNotesSearchType::UserId => array(
										 					DataKeys::Value => $userid,
															DataKeys::Comparison => DataComparison::Equal,
														  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
		
		public function GetClientNotesByClientId($clientid){
			$checks = array(
							ClientNotesSearchType::ClientId => array(
										 							DataKeys::Value => $clientid,
																	DataKeys::Comparison => DataComparison::Equal,
																  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
		
	}

	/******************************************************
     *	 DB Queries related to the mr_tblcurrency table   *
	 ******************************************************/	 
	interface CurrencySearchType{
		const ALL = "All";
		const Currency = "currency";
		const Description = "description";
	}
	
	final class DB_mr_tblCurrency{
		var $connection = null;
		var $table = "mr_tblCurrency";
    
        public function __construct(){            
            $this->connection = new DBConn();           
        }
		
		public function GetALLCurrencies(){
            return $this->connection->ParseQuery($this->table,null);
		}
		
		public function GetCurrencyByAcronym($acronym){
			$checks = array(
							CurrencySearchType::Currency => array(
										 					DataKeys::Value => $acronym,
															DataKeys::Comparison => DataComparison::Equal,
														  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
		
		public function GetCurrencyByDescription($description){
			$checks = array(
							CurrencySearchType::Description => array(
										 							DataKeys::Value => $description,
																	DataKeys::Comparison => DataComparison::Like,
																  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
	}

	/******************************************************
     *	 DB Queries related to the mr_tblexchange table   *
	 ******************************************************/	 
	interface ExchangeRateSearchType{
		const ALL = "All";
		const Currency = "currency";
		const Startdate = "startdate";
		const CurrencyAndDate = "currency-and-date";
	}
	
	final class DB_mr_tblExchangeRate{
		var $connection = null;
		var $table = "mr_tblExchangeRate";
    
        public function __construct(){            
            $this->connection = new DBConn();           
        }
		
		public function GetALLExchangeRates(){
            return $this->connection->ParseQuery($this->table,null);
		}
		
		public function GetExchangeRateByStartDate($startdate){
			$checks = array(
							ExchangeRateSearchType::Startdate => array(
									 							DataKeys::Value => $startdate,
																DataKeys::Comparison => DataComparison::GreaterOrEqualThan,
																  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
		
		public function GetExchangeRateByCurrency($currency){
			$checks = array(
							ExchangeRateSearchType::Currency => array(
									 							DataKeys::Value => $currency,
																DataKeys::Comparison => DataComparison::Equal,
																  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}		
		
		public function GetExchangeRateByCurrencyAndDate($currency, $startdate){
			$checks = array(
							ExchangeRateSearchType::Currency => array(
									 							DataKeys::Value => $currency,
																DataKeys::Comparison => DataComparison::Equal,
																  ),
							ExchangeRateSearchType::Startdate => array(
									 							DataKeys::Value => $startdate,
																DataKeys::Comparison => DataComparison::GreaterOrEqualThan,
																  ),
																
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}		
	}

	/**************************************************
     *	 DB Queries related to the mr_tbllead table   *
	 **************************************************/	 
	interface LeadSearchType{
		const ALL = "All";
		const ID = "id";
		const PriSourceLead = "primaryleadsourceid";
		const SecSourceLead = "secondaryleadsourceid";		
	}
	
	final class DB_mr_tblLead{
		var $connection = null;
		var $table = "mr_tblLead";
    
        public function __construct(){            
            $this->connection = new DBConn();           
        }
		
		public function GetALLLeads(){
            return $this->connection->ParseQuery($this->table,null);
		}
		
		public function GetLeadByID($ID){
			$checks = array(
							LeadSearchType::ID => array(
									 					DataKeys::Value => $ID,
														DataKeys::Comparison => DataComparison::Equal,
													  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}		

		public function GetLeadByPrimarySourceLead($primarysourceleadid){
			$checks = array(
							LeadSearchType::PriSourceLead => array(
									 								DataKeys::Value => $primarysourceleadid,
																	DataKeys::Comparison => DataComparison::Equal,
																  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}			

		public function GetLeadBySecondarySourceLead($secondarysourceleadid){
			$checks = array(
							LeadSearchType::SecSourceLead => array(
									 								DataKeys::Value => $secondarysourceleadid,
																	DataKeys::Comparison => DataComparison::Equal,
																  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}		
	}

	/**********************************************************
     *	 DB Queries related to the mr_tblpartneragent table   *
	 **********************************************************/	 
	interface PartnerAgentSearchType{
		const ALL = "All";
		const ID = "id";
		const AgentCode = "agentcode";
		const ParnerCompanyID = "partnercompanyid";
		const FirstName = "firstname";
		const LastName = "lastname";
		const FirstAndLastName = "first-and-lastname";
		const Phone = "phone";
		const Email = "email";
	}
	
	final class DB_mr_tblpartneragent{
		var $connection = null;
		var $table = "mr_tblPartnerAgent";
    
        public function __construct(){            
            $this->connection = new DBConn();           
        }
		
		public function GetALLPartnerAgents(){
            return $this->connection->ParseQuery($this->table,null);
		}
		
		public function GetPartnerAgentByID($ID){
			$checks = array(
							PartnerAgentSearchType::ID => array(
									 							DataKeys::Value => $ID,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}				

		public function GetPartnerAgentByAgentCode($agentcode){
			$checks = array(
							PartnerAgentSearchType::AgentCode => array(
										 								DataKeys::Value => $agentcode,
																		DataKeys::Comparison => DataComparison::Equal,
													  				),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}

		public function GetPartnerAgentByPartnerCompanyID($partnercompanyid){
			$checks = array(
							PartnerAgentSearchType::ParnerCompanyID => array(
										 								DataKeys::Value => $partnercompanyid,
																		DataKeys::Comparison => DataComparison::Equal,
													  					),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}

		public function GetPartnerAgentByFirstName($firstname){
			$checks = array(
							PartnerAgentSearchType::FirstName => array(
										 							DataKeys::Value => $firstname,
																	DataKeys::Comparison => DataComparison::RightLike,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}	

		public function GetPartnerAgentByLastName($lastname){
			$checks = array(
							PartnerAgentSearchType::LastName => array(
										 							DataKeys::Value => $lastname,
																	DataKeys::Comparison => DataComparison::RightLike,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}	

		public function GetPartnerAgentByFirstAndLastName($firstname, $lastname){
			$checks = array(
							PartnerAgentSearchType::FirstName => array(
										 							DataKeys::Value => $firstname,
																	DataKeys::Comparison => DataComparison::RightLike,
													  			),
							PartnerAgentSearchType::LastName => array(
										 							DataKeys::Value => $lastname,
																	DataKeys::Comparison => DataComparison::RightLike,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}	

		public function GetPartnerAgentByPhone($phone){
			$checks = array(
							PartnerAgentSearchType::Phone => array(
										 							DataKeys::Value => $phone,
																	DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}	
		
		public function GetPartnerAgentByEmail($email){
			$checks = array(
							PartnerAgentSearchType::Email => array(
										 							DataKeys::Value => $email,
																	DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}	

	}
	
	/************************************************************
     *	 DB Queries related to the mr_tblpartnercompany table   *
	 ************************************************************/	  
	interface PartnerCompanySearchType{
		const ALL = "All";
		const ID = "id";
		const Name = "name";
		const Phone = "phone";
		const Email = "email";
	}
	
	final class DB_mr_tblpartnercompany{
		var $connection = null;
		var $table = "mr_tblPartnerCompany";
    
        public function __construct(){            
            $this->connection = new DBConn();           
        }
		
		public function GetALLPartnerCompanies(){
            return $this->connection->ParseQuery($this->table,null);
		}
		
		public function GetPartnerCompanyByID($ID){
			$checks = array(
							PartnerCompanySearchType::ID => array(
									 							DataKeys::Value => $ID,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}				

		public function GetPartnerCompanyByName($name){
			$checks = array(
							PartnerCompanySearchType::Name => array(
									 							DataKeys::Value => $name,
																DataKeys::Comparison => DataComparison::Like,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}	

		public function GetPartnerCompanyByPhone($phone){
			$checks = array(
							PartnerCompanySearchType::Phone => array(
									 							DataKeys::Value => $phone,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}	

		public function GetPartnerCompanyByEmail($email){
			$checks = array(
							PartnerCompanySearchType::Email => array(
									 							DataKeys::Value => $email,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}	

	}

	/*****************************************************
     *	 DB Queries related to the mr_tblRainAPI table   *
	 *****************************************************/	  
	interface RainAPISearchType{
		const ALL = "All";
		const ID = "id";
		const Name = "name";
		const Active = "active";
	}
	
	final class DB_mr_tblrainapi{
		var $table = "mr_tblRainAPI";
    
        public function __construct(){            
            $this->connection = new DBConn();           
        }
		
		public function GetALLRainAPIs(){
            return $this->connection->ParseQuery($this->table,null);
		}
		
		public function GetRainAPIByID($ID){
			$checks = array(
							RainAPISearchType::ID => array(
									 							DataKeys::Value => $ID,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}			
		
		public function GetRainAPIByName($name){
			$checks = array(
							RainAPISearchType::Name => array(
									 							DataKeys::Value => $name,
																DataKeys::Comparison => DataComparison::Like,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}			
		
		public function GetRainAPIByActive($active){
			$checks = array(
							RainAPISearchType::Active => array(
									 							DataKeys::Value => $active,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}			
	}
	
	/****************************************************************
     *	 DB Queries related to the mr_tblrainapitransaction table   *
	 ****************************************************************/
	interface RainAPITransactionSearchType{
		const ALL = "All";
		const ID = "id";
		const TransactionStatus = "transtatus";
		const BookingStatus = "bookingstatus";
		const BookingId = "bookingid";
		const RainVillaId = "rainvillaid";
	}
	
	final class DB_mr_tblrainapitransaction{
		var $table = "mr_tblRainAPITransaction";
    
        public function __construct(){            
            $this->connection = new DBConn();           
        }
		
		public function GetALLRainAPITransactions(){
            return $this->connection->ParseQuery($this->table,null);
		}
		
		public function GetRainAPITransactionByID($ID){
			$checks = array(
							RainAPITransactionSearchType::ID => array(
									 							DataKeys::Value => $ID,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}			

		public function GetRainAPITransactionByTransactionStatus($transactionstatus){
			$checks = array(
							RainAPITransactionSearchType::TransactionStatus => array(
									 							DataKeys::Value => $transactionstatus,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}			
		
		public function GetRainAPITransactionByBookingStatus($bookingstatus){
			$checks = array(
							RainAPITransactionSearchType::BookingStatus => array(
									 							DataKeys::Value => $bookingstatus,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}			

		public function GetRainAPITransactionByBookingId($bookingid){
			$checks = array(
							RainAPITransactionSearchType::BookingId => array(
									 							DataKeys::Value => $bookingid,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}			
		
		public function GetRainAPITransactionByRainVillaId($rainvillaid){
			$checks = array(
							RainAPITransactionSearchType::RainVillaId => array(
									 							DataKeys::Value => $rainvillaid,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}			
	}
	
	/******************************************************
     *	 DB Queries related to the mr_tblranvilla table   *
	 ******************************************************/	  
	interface RainVillaSearchType{
		const ALL = "All";
		const ID = "id";
		const DisplayId = "displayid";
		const Name = "name";
		const JetSetterId = "jetsetterid";
		const VendorId = "vendorid";
	}
	
	final class DB_mr_tblrainvilla{
		var $table = "mr_tblRainVilla";
    
        public function __construct(){            
            $this->connection = new DBConn();           
        }
		
		public function GetALLRainVillas(){
            return $this->connection->ParseQuery($this->table,null);
		}
		
		public function GetRainVillaByID($ID){
			$checks = array(
							RainVillaSearchType::ID => array(
									 							DataKeys::Value => $ID,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}			
		
		public function GetRainVillaByDisplayId($displayid){
			$checks = array(
							RainVillaSearchType::DisplayId => array(
									 							DataKeys::Value => $displayid,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}

		public function GetRainVillaByName($name){
			$checks = array(
							RainVillaSearchType::Name => array(
									 							DataKeys::Value => $name,
																DataKeys::Comparison => DataComparison::Like,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}
		
		public function GetRainVillaByJetSetterId($jetsetterid){
			$checks = array(
							RainVillaSearchType::JetSetterId => array(
									 							DataKeys::Value => $jetsetterid,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}

		public function GetRainVillaByVendorId($vendorid){
			$checks = array(
							RainVillaSearchType::VendorId => array(
									 							DataKeys::Value => $vendorid,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}

	}
	
	/**************************************************
     *	 DB Queries related to the mr_tblrate table   *
	 **************************************************/	  
	interface RateSearchType{
		const ALL = "All";
		const RainVillaId = "rainvillaid";
	}
	
	final class DB_mr_tblrate{
		var $table = "mr_tblRate";
    
        public function __construct(){            
            $this->connection = new DBConn();           
        }
		
		public function GetALLRates(){
            return $this->connection->ParseQuery($this->table,null);
		}
		
		public function GetRateByRainVillaId($rainvillaid){
			$checks = array(
							RateSearchType::RainVillaId => array(
									 							DataKeys::Value => $rainvillaid,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}	
	}

	/*****************************************************
     *	 DB Queries related to the mr_tblrequest table   *
	 *****************************************************/	  
	interface RequestSearchType{
		const ALL = "All";
		const ID = "id";
		const Status = "status";
		const Type = "type";
		const LeadSourceId = "leadsourceid";
		const SalesUserId = "salesuserid";
		const ClientFirstName = "client_firstname";
		const ClientLastName = "client_lastname";
		const ClientFirstAndLastName = "client-first-and-lastname";
		const ClientEmail = "client_email";
		const ClientPhone = "client_telephone";
		const ClientId = "clientid";
		const VillaId = "villaid";
	}
	
	final class DB_mr_tblrequest{
		var $table = "mr_tblRequest";
    
        public function __construct(){            
            $this->connection = new DBConn();           
        }
		
		public function GetALLRequests(){
            return $this->connection->ParseQuery($this->table,null);
		}
		
		public function GetRequestById($ID){
			$checks = array(
							RequestSearchType::ID => array(
									 							DataKeys::Value => $ID,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}			

		public function GetRequestByStatus($status){
			$checks = array(
							RequestSearchType::Status => array(
									 							DataKeys::Value => $status,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}			

		public function GetRequestByType($type){
			$checks = array(
							RequestSearchType::Type => array(
									 							DataKeys::Value => $type,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}			

		public function GetRequestByLeadSourceId($leadsourceid){
			$checks = array(
							RequestSearchType::LeadSourceId => array(
									 							DataKeys::Value => $leadsourceid,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}			

		public function GetRequestBySalesUserId($salesuserid){
			$checks = array(
							RequestSearchType::SalesUserId => array(
									 							DataKeys::Value => $salesuserid,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}			

		public function GetRequestByClientFirstName($firstname){
			$checks = array(
							RequestSearchType::ClientFirstName => array(
									 							DataKeys::Value => $firstname,
																DataKeys::Comparison => DataComparison::RightLike,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}			
		
		public function GetRequestByClientLastName($lastname){
			$checks = array(
							RequestSearchType::ClientLastName => array(
									 							DataKeys::Value => $lastname,
																DataKeys::Comparison => DataComparison::RightLike,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}			
		
		public function GetRequestByClientFirstAndLastName($firstname, $lastname){
			$checks = array(
							RequestSearchType::ClientFirstName => array(
									 							DataKeys::Value => $firstname,
																DataKeys::Comparison => DataComparison::RightLike,
													  			),
							RequestSearchType::ClientLastName => array(
									 							DataKeys::Value => $lastname,
																DataKeys::Comparison => DataComparison::RightLike,
													  			),
																
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}			

		public function GetRequestByClientEmail($email){
			$checks = array(
							RequestSearchType::ClientEmail => array(
									 							DataKeys::Value => $email,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}			

		public function GetRequestByClientPhone($phone){
			$checks = array(
							RequestSearchType::ClientPhone => array(
									 							DataKeys::Value => $phone,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}			

		public function GetRequestByClientId($clientid){
			$checks = array(
							RequestSearchType::ClientId => array(
									 							DataKeys::Value => $clientid,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}			

		public function GetRequestByVillaId($villaid){
			$checks = array(
							RequestSearchType::VillaId => array(
									 							DataKeys::Value => $villaid,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}			
	}
  		 
	/****************************************************
     *	 DB Queries related to the mr_tblvendor table   *
	 ****************************************************/	   
	interface VendorSearchType{
		const ALL = "All";
		const ID = "id";
		const VendorCode = "vendorcode";
		const Name = "name";
		const Phone = "phone";
		const Email = "email";		
	}

	final class DB_mr_tblvendor{
		var $table = "mr_tblVendor";
    
        public function __construct(){            
            $this->connection = new DBConn();           
        }
		
		public function GetALLVendors(){
            return $this->connection->ParseQuery($this->table,null);
		}
		
		public function GetVendorById($ID){
			$checks = array(
							VendorSearchType::ID => array(
									 							DataKeys::Value => $ID,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}				

		public function GetVendorByVendorCode($vendorcode){
			$checks = array(
							VendorSearchType::VendorCode => array(
									 							DataKeys::Value => $vendorcode,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}				

		public function GetVendorByName($name){
			$checks = array(
							VendorSearchType::Name => array(
									 							DataKeys::Value => $name,
																DataKeys::Comparison => DataComparison::Like,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}				
		
		public function GetVendorByPhone($phone){
			$checks = array(
							VendorSearchType::Phone => array(
									 							DataKeys::Value => $phone,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}				
		
		public function GetVendorByEmail($email){
			$checks = array(
							VendorSearchType::Email => array(
									 							DataKeys::Value => $email,
																DataKeys::Comparison => DataComparison::Equal,
													  			),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
		}				

	}
	 	
	/***********************************************
     * DB Queries related to the mr_tblVilla table *
	 ***********************************************/
	interface VillaSearchType{
        const ALL = "All";
        const ID = "id";
        const DisplayID = "displayid";    
        const VendorID = "vendorid";
        const Apicode = "apicode";
        const Name = "name";
        const Status = "status";
        const Currency = "currency";
    }
	 
    final class DB_mr_tblvilla{
        var $connection = null;
		var $table = "mr_tblVilla";
    
        public function __construct(){
            $this->connection = new DBConn();           
        }
		
		public function GetALLVillaInformation(){
			return $this->connection->ParseQuery($this->table,null);
        }
    
        public function GetVillaInformationById($id){
			$checks = array(
							VillaSearchType::ID => array(
										 				DataKeys::Value => $id,
														DataKeys::Comparison => DataComparison::Equal,
							 			  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
        }    		
	
        public function GetVillaInformationByDisplayId($displayid){
			$checks = array(
							VillaSearchType::DisplayID => array(
										 				DataKeys::Value => $displayid,
														DataKeys::Comparison => DataComparison::Equal,
							 			  ),
							);
													              			
            return $this->connection->ParseQuery($this->table,$checks);
        }
    
        public function GetVillaInformationByVendorId($vendorid){
			$checks = array(
							VillaSearchType::VendorID => array(
										 				DataKeys::Value => $vendorid,
														DataKeys::Comparison => DataComparison::Equal,
							 			  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
        }
    
        public function GetVillaInformationByApicode($apicode){
			$checks = array(
							VillaSearchType::Apicode => array(
										 						DataKeys::Value => $apicode,
																DataKeys::Comparison => DataComparison::Equal,
												 			  ),
							);
							
										              			
            return $this->connection->ParseQuery($this->table,$checks);
        }
    
        public function GetVillaInformationByName($name){
			$checks = array(
							VillaSearchType::Name => array(
										 				DataKeys::Value => $name,
														DataKeys::Comparison => DataComparison::Equal,
							 			  ),
							);							
										              			
            return $this->connection->ParseQuery($this->table,$checks);
        }
    
        public function GetVillaInformationByStatus($status){
			$checks = array(
							 VillaSearchType::Status => array(
										 				DataKeys::Value => $status,
														DataKeys::Comparison => DataComparison::Equal,
							 			  ),
							);
																	              			
            return $this->connection->ParseQuery($this->table,$checks);
        }
    
        public function GetVillaInformationByCurrency($currency){            
			$checks = array(
							VillaSearchType::Currency => array(
										 				DataKeys::Value => $currency,
														DataKeys::Comparison => DataComparison::Equal,
							 			  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
        }              
    }

	/***************************************************
     * DB Queries related to the mr_tblVillaFee table  *
	 ***************************************************/
	interface VillaFeeSearchType{
		const ALL = "All";
		const VillaId = "villaid";		
		const villaname = "villaname";
		const feename = "fee";
		const percentage = "percentage";
		const flat_fee = "flat_fee";
		const before_taxes = "before_taxes";
		const apply_to_quote = "apply_to_quote";
	}
	
	final class DB_mr_tblvillafee{
		var $connection = null;
		var $table = "mr_tblVillaFee";
    
        public function __construct(){
            $this->connection = new DBConn();           
        }
		
		public function GetALLVillaFeeInformation(){
			return $this->connection->ParseQuery($this->table,null);
        }
    
        public function GetVillaFeeInformationByVillaId($villaid){
			$checks = array(
							VillaFeeSearchType::VillaId => array(
										 				DataKeys::Value => $villaid,
														DataKeys::Comparison => DataComparison::Equal,
							 			  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
        }
		
		public function GetVillaFeeInformationByVillaName($villaname){
			$checks = array(
							VillaFeeSearchType::villaname => array(
										 				DataKeys::Value => $villaname,
														DataKeys::Comparison => DataComparison::RightLike,
							 			  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
        }		
		
		public function GetVillaFeeInformationByFee($feename){
			$checks = array(
							VillaFeeSearchType::feename => array(
										 				DataKeys::Value => $feename,
														DataKeys::Comparison => DataComparison::RightLike,
							 			  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
        }				
		
		public function GetVillaFeeInformationByPercentage($criteria){
			$checks = array(
							VillaFeeSearchType::percentage => array(
										 				DataKeys::Value => $criteria,
														DataKeys::Comparison => DataComparison::Equal,
							 			  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
        }
		
		public function GetVillaFeeInformationByFlatFee($criteria){
			$checks = array(
							VillaFeeSearchType::flat_fee => array(
										 				DataKeys::Value => $criteria,
														DataKeys::Comparison => DataComparison::Equal,
							 			  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
        }
		
		public function GetVillaFeeInformationByBeforeTaxes($criteria){
			$checks = array(
							VillaFeeSearchType::before_taxes => array(
										 				DataKeys::Value => $criteria,
														DataKeys::Comparison => DataComparison::Equal,
							 			  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
        }
		
		public function GetVillaFeeInformationByApplyToQuote($criteria){
			$checks = array(
							VillaFeeSearchType::apply_to_quote => array(
										 				DataKeys::Value => $criteria,
														DataKeys::Comparison => DataComparison::Equal,
							 			  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
        }				
	}

	/***************************************************
     * DB Queries related to the mr_tblVillarate table *
	 ***************************************************/
	interface VillaRateSearchType{
		const ALL = "All";
		const VillaId = "villaid";		
	}
	
	final class DB_mr_tblvillarate{
		var $connection = null;
		var $table = "mr_tblVillaRate";
    
        public function __construct(){
            $this->connection = new DBConn();           
        }
		
		public function GetALLVillaRatesInformation(){
			return $this->connection->ParseQuery($this->table,null);
        }
    
        public function GetVillaRateInformationByVillaId($villaid){
			$checks = array(
							VillaRateSearchType::VillaId => array(
										 				DataKeys::Value => $villaid,
														DataKeys::Comparison => DataComparison::Equal,
							 			  ),
							);
										              			
            return $this->connection->ParseQuery($this->table,$checks);
        }		
	}
	
		
	/******************************************************
     * 					API Handling method 			  *
	 ******************************************************/	
	class RainDBApi{
		var $dbavailability = null;
		var $dbbookings = null;
		var $dbbookingbilling = null;
		var $dbbookingclient = null;
		var $dbbookingexpense = null;
		var $dbbookingfee = null;
		var $dbbookingitems = null;
		var $dbclients = null;
		var $dbclientnotes = null;
		var $dbcurrencies = null;
		var $dbexchangerates = null;
		var $dbleads = null;
		var $dbpartneragents = null;
		var $dbpartnercompanies = null;
		var $dbrainapis = null;
		var $dbrainapitransactions = null;
		var $dbrainvilla = null;
		var $dbrates = null;
		var $dbrequest = null;
		var $dbvendor = null;
		var $dbvilla = null;	
		var $dbvillafee = null;
		var $dbvillarate = null;	
        var $utilities = null;
    
        public function __construct(){
            $this->utilities = new Utilities();
			$this->dbavailability = new DB_mr_tblavailability();
			$this->dbbookings = new DB_mr_tblbooking();
			$this->dbbookingbilling = new DB_mr_tblbookingbilling();
			$this->dbbookingclient = new DB_mr_tblBookingClient();
			$this->dbbookingexpense = new DB_mr_tblBookingExpense();
			$this->dbbookingfee = new DB_mr_tblBookingFee();
			$this->dbbookingitems = new DB_mr_tblBookingItems();
			$this->dbclients = new DB_mr_tblClients();
			$this->dbclientnotes = new DB_mr_tblClientNotes();
			$this->dbcurrencies = new DB_mr_tblCurrency();
			$this->dbexchangerates = new DB_mr_tblExchangeRate();
			$this->dbleads = new DB_mr_tblLead();
			$this->dbpartneragents = new DB_mr_tblpartneragent();
			$this->dbpartnercompanies = new DB_mr_tblpartnercompany();
			$this->dbrainapis = new DB_mr_tblrainapi();
			$this->dbrainapitransactions = new DB_mr_tblrainapitransaction();
			$this->dbrainvilla = new DB_mr_tblrainvilla();
			$this->dbrates = new DB_mr_tblrate();
			$this->dbrequest = new DB_mr_tblrequest();
			$this->dbvendor = new DB_mr_tblvendor();
            $this->dbvilla = new DB_mr_tblvilla();		
			$this->dbvillafee = new DB_mr_tblvillafee();
			$this->dbvillarate = new DB_mr_tblvillarate();	
        }				 				                    	
		
		public function GetAvailabilityInformation($query_type, $param1, $param2, $param3){        
	    	$res = null;
	        if($this->utilities->NotEmpty($query_type)){
    	            switch($query_type){
       		        case AvailabilitySearchType::ALL:
           		    $res = $this->dbavailability->GetALLAvailability();
               		    break;
	                case AvailabilitySearchType::VillaId:
    	                    $res = $this->dbavailability->GetAvailabilityByRainVillaId($param1);
       		            break;
	                case AvailabilitySearchType::BookingType:
    	                    $res = $this->dbavailability->GetAvailabilityByBookingType($param1);
       		            break;
	                case AvailabilitySearchType::BookingId:
    	                    $res = $this->dbavailability->GetAvailabilityByBookingId($param1);
       		            break;
	                case AvailabilitySearchType::ArrivalDate:
    	                    $res = $this->dbavailability->GetAvailabilityByArrivalDate($param1);
       		            break;
	                case AvailabilitySearchType::IDAndArrivalDate:
    	                    $res = $this->dbavailability->GetAvailabilityByIDAndArrivalDate($param1, $param2);
       		            break;
	                case AvailabilitySearchType::DepartureDate:
    	                    $res = $this->dbavailability->GetAvailabilityByDepartureDate($param1);
       		            break;
	                case AvailabilitySearchType::IDAndDepartureDate:
    	                    $res = $this->dbavailability->GetAvailabilityByIDAndDepartureDate($param1, $param2);
       		            break;
			case AvailabilitySearchType::ArrivalAndDeparture:
    	                    $res = $this->dbavailability->GetAvailabilityByArrivalAndDeparture($param1, $param2);
       		            break;
			case AvailabilitySearchType::IDAndArrivalAndDeparture:
    	                    $res = $this->dbavailability->GetAvailabilityByIDAndArrivalAndDeparture($param1, $param2, $param3);
       		            break;
			case AvailabilitySearchType::Status:
    	                    $res = $this->dbavailability->GetAvailabilityByStatus($param1);
       		            break;						
		    }
		}
    		return $res;        
		}
		
		public function GetAvailabilityCalendar($query_type, $param1, $param2, $param3){        
	    	$res = null;
	        if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
					case AvailabilityCalendarSearchType::IDAndInitialDateAndEndDate:
    	                $res = $this->dbavailability->GetAvailabilityCalendar($param1, $param2, $param3);
       		            break;
		        }
			}
    		return $res;        
		}
		
		public function GetBookingInformation($query_type, $param1, $param2){
			$res = null;
	        if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
       		        case BookingSearchType::ALL:
           		        $res = $this->dbbookings->GetALLBookings();
               		    break;
					case BookingSearchType::VillaId:
          		        $res = $this->dbbookings->GetBookingByRainVillaId($param1);
               		    break;
					case BookingSearchType::StatusId:
          		        $res = $this->dbbookings->GetBookingByStatusId($param1);
               		    break;
					case BookingSearchType::ClientId:
          		        $res = $this->dbbookings->GetBookingByClientId($param1);
               		    break;				
					case BookingSearchType::LeadSourceId:
          		        $res = $this->dbbookings->GetBookingByLeadSourceId($param1);
               		    break;					
					case BookingSearchType::PartnerAgentId:
          		        $res = $this->dbbookings->GetBookingByPartnerAgentId($param1);
               		    break;					
					case BookingSearchType::SalesRepId:
          		        $res = $this->dbbookings->GetBookingBySalesRepresentativeId($param1);
               		    break;					
					case BookingSearchType::VendorId:
          		        $res = $this->dbbookings->GetBookingByVendorId($param1);
               		    break;					
					case BookingSearchType::InvoiceNumber:
          		        $res = $this->dbbookings->GetBookingByInvoiceNbr($param1);
               		    break;					
					case BookingSearchType::BookedDate:
          		        $res = $this->dbbookings->GetBookingByBookedDate($param1);
               		    break;					
					case BookingSearchType::IDAndBookedDate:
          		        $res = $this->dbbookings->GetBookingByIDAndBookedDate($param1,$param2);
               		    break;	                					
		        }
			}
    		return $res; 
		}

		public function GetBookingBillingInformation($query_type, $param1){        
	    	$res = null;
	        if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
       		        case BookingBillingSearchType::ALL:
           		        $res = $this->dbbookingbilling->GetALLBookingBilling();
               		    break;
	                case BookingBillingSearchType::BookingId:
    	                $res = $this->dbbookingbilling->GetBookingBillingByBookingId($param1);
       		            break;
					case BookingBillingSearchType::Currency:
    	                $res = $this->dbbookingbilling->GetBookingBillingByCurrency($param1);
       		            break;
		        }
			}
    		return $res;        
		} 
		
		public function GetBookingClientInformation($query_type, $param1, $param2){        
	    	$res = null;
	        if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
       		        case BookingClientSearchType::ALL:
           		        $res = $this->dbbookingclient->GetALLBookingClient();
               		    break;
	                case BookingClientSearchType::BookingId:
    	                $res = $this->dbbookingclient->GetBookingClientByBookingId($param1);
       		            break;
					case BookingClientSearchType::Email:
    	                $res = $this->dbbookingclient->GetBookingClientByEmail($param1);
       		            break;
					case BookingClientSearchType::FirstNameAndLastName:
    	                $res = $this->dbbookingclient->GetBookingClientByFirstAndLastName($param1, $param2);
       		            break;
					case BookingClientSearchType::Phone:
    	                $res = $this->dbbookingclient->GetBookingClientByPhone($param1);
       		            break;
		        }
			}
    		return $res;        
		} 

		public function GetBookingExpenseInformation($query_type, $param1){        
	    	$res = null;
	        if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
       		        case BookingExpenseSearchType::ALL:
           		        $res = $this->dbbookingexpense->GetALLBookingExpense();
               		    break;
	                case BookingExpenseSearchType::BookingId:
    	                $res = $this->dbbookingexpense->GetBookingExpenseByBookingId($param1);
       		            break;
		        }
			}
    		return $res;        
		} 		
		
		public function GetBookingFeeInformation($query_type, $param1){        
	    	$res = null;
	        if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
       		        case BookingFeeSearchType::ALL:
           		        $res = $this->dbbookingfee->GetALLBookingFee();
               		    break;
	                case BookingFeeSearchType::BookingId:
    	                $res = $this->dbbookingfee->GetBookingFeeByBookingId($param1);
       		            break;
					case BookingFeeSearchType::BookingFeeName:
						$res = $this->dbbookingfee->GetBookingFeeByFeeName($param1);
       		            break;
		        }
			}
    		return $res;        
		} 		
		
		public function GetBookingItemsInformation($query_type, $param1){        
	    	$res = null;
	        if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
       		        case BookingItemsSearchType::ALL:
           		        $res = $this->dbbookingitems->GetALLBookingItems();
               		    break;
	                case BookingItemsSearchType::BookingId:
    	                $res = $this->dbbookingitems->GetBookingItemsByBookingId($param1);
       		            break;
					case BookingItemsSearchType::ItemName:
    	                $res = $this->dbbookingitems->GetBookingItemsByItemName($param1);
       		            break;
		        }
			}
    		return $res;        
		} 		
		
		public function GetClientInformation($query_type, $param1, $param2){        
	    	$res = null;
	        if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
       		        case ClientSearchType::ALL:
           		        $res = $this->dbclients->GetALLClients();
               		    break;
	                case ClientSearchType::ID:
    	                $res = $this->dbclients->GetClientByID($param1);
       		            break;
					case ClientSearchType::FirstName:
    	                $res = $this->dbclients->GetClientByFirstName($param1);
       		            break;
					case ClientSearchType::LastName:
    	                $res = $this->dbclients->GetClientByLastName($param1);
       		            break;
					case ClientSearchType::FirstNameAndLastName:
    	                $res = $this->dbclients->GetClientByFirstAndLastName($param1, $param2);
       		            break;
					case ClientSearchType::Email:
    	                $res = $this->dbclients->GetClientByEmail($param1);						
       		            break;
					case ClientSearchType::Phone:
    	                $res = $this->dbclients->GetClientByPhone($param1);
       		            break;
		        }
			}
    		return $res;        
		} 		

		public function GetClientNotesInformation($query_type, $param1){        
	    	$res = null;
	        if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
       		        case ClientNotesSearchType::ALL:
           		        $res = $this->dbclientnotes->GetALLClientNotes();
               		    break;
	                case ClientNotesSearchType::UserId:
    	                $res = $this->dbclientnotes->GetClientNotesByUserId($param1);
       		            break;
					case ClientNotesSearchType::ClientId:
    	                $res = $this->dbclientnotes->GetClientNotesByClientId($param1);
       		            break;
		        }
			}
    		return $res;        
		} 		
		
		public function GetCurrencyInformation($query_type, $param1){        
	    	$res = null;
	        if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
       		        case CurrencySearchType::ALL:
           		        $res = $this->dbcurrencies->GetALLCurrencies();
               		    break;
	                case CurrencySearchType::Currency:
    	                $res = $this->dbcurrencies->GetCurrencyByAcronym($param1);
       		            break;
					case CurrencySearchType::Description:
    	                $res = $this->dbcurrencies->GetCurrencyByDescription($param1);
       		            break;
		        }
			}
    		return $res;        
		} 		

		public function GetExchangeRatesInformation($query_type, $param1, $param2){        
	    	$res = null;
	        if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
       		        case ExchangeRateSearchType::ALL:
           		        $res = $this->dbexchangerates->GetALLExchangeRates();
               		    break;
	                case ExchangeRateSearchType::Currency:
    	                $res = $this->dbexchangerates->GetExchangeRateByCurrency($param1);
       		            break;
					case ExchangeRateSearchType::Startdate:
    	                $res = $this->dbexchangerates->GetExchangeRateByStartDate($param1);
       		            break;
					case ExchangeRateSearchType::CurrencyAndDate:
    	                $res = $this->dbexchangerates->GetExchangeRateByCurrencyAndDate($param1, $param2);
       		            break;					
		        }
			}
    		return $res;        
		} 		

		public function GetLeadInformation($query_type, $param1){        
	    	$res = null;
	        if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
       		        case LeadSearchType::ALL:
           		        $res = $this->dbleads->GetALLLeads();
               		    break;
	                case LeadSearchType::ID:
    	                $res = $this->dbleads->GetLeadByID($param1);
       		            break;
					case LeadSearchType::PriSourceLead:
    	                $res = $this->dbleads->GetLeadByPrimarySourceLead($param1);
       		            break;
					case LeadSearchType::SecSourceLead:
    	                $res = $this->dbleads->GetLeadBySecondarySourceLead($param1);
       		            break;					
		        }
			}
    		return $res;        
		} 
		
		public function GetPartnerAgentInformation($query_type, $param1, $param2){
			$res = null;
			if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
       		        case PartnerAgentSearchType::ALL:
           		        $res = $this->dbpartneragents->GetALLPartnerAgents();
               		    break;
	                case PartnerAgentSearchType::ID:
    	                $res = $this->dbpartneragents->GetPartnerAgentByID($param1);
       		            break;
	                case PartnerAgentSearchType::AgentCode:
    	                $res = $this->dbpartneragents->GetPartnerAgentByAgentCode($param1);
       		            break;
	                case PartnerAgentSearchType::ParnerCompanyID:
    	                $res = $this->dbpartneragents->GetPartnerAgentByPartnerCompanyID($param1);
       		            break;
	                case PartnerAgentSearchType::FirstName:
    	                $res = $this->dbpartneragents->GetPartnerAgentByFirstName($param1);
       		            break;
	                case PartnerAgentSearchType::LastName:
    	                $res = $this->dbpartneragents->GetPartnerAgentByLastName($param1);
       		            break;
	                case PartnerAgentSearchType::FirstAndLastName:
    	                $res = $this->dbpartneragents->GetPartnerAgentByFirstAndLastName($param1,$param2);
       		            break;
	                case PartnerAgentSearchType::Phone:
    	                $res = $this->dbpartneragents->GetPartnerAgentByPhone($param1);
       		            break;
	                case PartnerAgentSearchType::Email:
    	                $res = $this->dbpartneragents->GetPartnerAgentByEmail($param1);
       		            break;
		        }
			}
    		return $res;   
		}		
		
		public function GetPartnerCompanyInformation($query_type, $param1){
			$res = null;
			if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
       		        case PartnerCompanySearchType::ALL:
           		        $res = $this->dbpartnercompanies->GetALLPartnerCompanies();
               		    break;
	                case PartnerCompanySearchType::ID:
    	                $res = $this->dbpartnercompanies->GetPartnerCompanyByID($param1);
       		            break;
	                case PartnerCompanySearchType::Name:
    	                $res = $this->dbpartnercompanies->GetPartnerCompanyByName($param1);
       		            break;
	                case PartnerCompanySearchType::Phone:
    	                $res = $this->dbpartnercompanies->GetPartnerCompanyByPhone($param1);
       		            break;
	                case PartnerCompanySearchType::Email:
    	                $res = $this->dbpartnercompanies->GetPartnerCompanyByEmail($param1);
       		            break;						
				}
			}
			return $res;
		}

		public function GetRainAPIInformation($query_type, $param1){
			$res = null;
			if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
       		        case RainAPISearchType::ALL:
           		        $res = $this->dbrainapis->GetALLRainAPIs();
               		    break;
	                case RainAPISearchType::ID:
    	                $res = $this->dbrainapis->GetRainAPIByID($param1);
       		            break;
	                case RainAPISearchType::Name:
    	                $res = $this->dbrainapis->GetRainAPIByName($param1);
       		            break;
	                case RainAPISearchType::Active:
    	                $res = $this->dbrainapis->GetRainAPIByActive($param1);
       		            break;
						
				}
			}
			return $res;
		}

		public function GetRainAPITransactionInformation($query_type, $param1){
			$res = null;
			if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
       		        case RainAPITransactionSearchType::ALL:
           		        $res = $this->dbrainapitransactions->GetALLRainAPITransactions();
               		    break;
	                case RainAPITransactionSearchType::ID:
    	                $res = $this->dbrainapitransactions->GetRainAPITransactionByID($param1);
       		            break;
	                case RainAPITransactionSearchType::TransactionStatus:
    	                $res = $this->dbrainapitransactions->GetRainAPITransactionByTransactionStatus($param1);
       		            break;
	                case RainAPITransactionSearchType::BookingStatus:
    	                $res = $this->dbrainapitransactions->GetRainAPITransactionByBookingStatus($param1);
       		            break;
	                case RainAPITransactionSearchType::BookingId:
    	                $res = $this->dbrainapitransactions->GetRainAPITransactionByBookingId($param1);						
       		            break;
	                case RainAPITransactionSearchType::RainVillaId:
    	                $res = $this->dbrainapitransactions->GetRainAPITransactionByRainVillaId($param1);
       		            break;										
				}
			}
			return $res;
		}
		
		public function GetRainVillaInformation($query_type, $param1){
			$res = null;
			if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
       		        case RainVillaSearchType::ALL:
           		        $res = $this->dbrainvilla->GetALLRainVillas();
               		    break;
	                case RainVillaSearchType::ID:
    	                $res = $this->dbrainvilla->GetRainVillaByID($param1);
       		            break;
					case RainVillaSearchType::DisplayId:
    	                $res = $this->dbrainvilla->GetRainVillaByDisplayId($param1);
       		            break;
					case RainVillaSearchType::Name:
    	                $res = $this->dbrainvilla->GetRainVillaByName($param1);
       		            break;
					case RainVillaSearchType::JetSetterId:
    	                $res = $this->dbrainvilla->GetRainVillaByJetSetterId($param1);
       		            break;						
					case RainVillaSearchType::VendorId:
    	                $res = $this->dbrainvilla->GetRainVillaByVendorId($param1);
       		            break;					
				}
			}
			return $res;
		}

		public function GetRateInformation($query_type, $param1){
			$res = null;
			if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
       		        case RateSearchType::ALL:
           		        $res = $this->dbrates->GetALLRates();
               		    break;
	                case RateSearchType::RainVillaId:
    	                $res = $this->dbrates->GetRateByRainVillaId($param1);
       		            break;
				}
			}
			return $res;
		}

		public function GetRequestInformation($query_type, $param1, $param2){
			$res = null;
			if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
       		        case RequestSearchType::ALL:
           		        $res = $this->dbrequest->GetALLRequests();
               		    break;
	                case RequestSearchType::ID:
    	                $res = $this->dbrequest->GetRequestById($param1);
       		            break;					
					case RequestSearchType::Status:
    	                $res = $this->dbrequest->GetRequestByStatus($param1);
       		            break;
	                case RequestSearchType::Type:
    	                $res = $this->dbrequest->GetRequestByType($param1);
       		            break;
	                case RequestSearchType::LeadSourceId:
    	                $res = $this->dbrequest->GetRequestByLeadSourceId($param1);
       		            break;
	                case RequestSearchType::SalesUserId:
    	                $res = $this->dbrequest->GetRequestBySalesUserId($param1);
       		            break;
	                case RequestSearchType::ClientFirstName:
    	                $res = $this->dbrequest->GetRequestByClientFirstName($param1);
       		            break;
	                case RequestSearchType::ClientLastName:
    	                $res = $this->dbrequest->GetRequestByClientLastName($param1);
       		            break;
	                case RequestSearchType::ClientFirstAndLastName:
    	                $res = $this->dbrequest->GetRequestByClientFirstAndLastName($param1, $param2);
       		            break;
	                case RequestSearchType::ClientEmail:
    	                $res = $this->dbrequest->GetRequestByClientEmail($param1);
       		            break;
	                case RequestSearchType::ClientPhone:
    	                $res = $this->dbrequest->GetRequestByClientPhone($param1);
       		            break;
	                case RequestSearchType::ClientId:
    	                $res = $this->dbrequest->GetRequestByClientId($param1);
       		            break;
	                case RequestSearchType::VillaId:
    	                $res = $this->dbrequest->GetRequestByVillaId($param1);
       		            break;
				}
			}
			return $res;
		}

		public function GetVendorInformation($query_type, $param1){
			$res = null;
			if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
       		        case VendorSearchType::ALL:
           		        $res = $this->dbvendor->GetALLVendors();
               		    break;
	                case VendorSearchType::ID:
    	                $res = $this->dbvendor->GetVendorById($param1);
       		            break;								
	                case VendorSearchType::VendorCode:
    	                $res = $this->dbvendor->GetVendorByVendorCode($param1);
       		            break;								
	                case VendorSearchType::Name:
    	                $res = $this->dbvendor->GetVendorByName($param1);
       		            break;								
	                case VendorSearchType::Phone:
    	                $res = $this->dbvendor->GetVendorByPhone($param1);
       		            break;								
	                case VendorSearchType::Email:
    	                $res = $this->dbvendor->GetVendorByEmail($param1);
       		            break;														
				}
			}
			return $res;
		}
		
		public function GetVillaInformation($query_type, $param1){        
	    	$res = null;
	        if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
       		        case VillaSearchType::ALL:
           		        $res = $this->dbvilla->GetALLVillaInformation();
               		    break;
	                case VillaSearchType::ID:
    	                $res = $this->dbvilla->GetVillaInformationById($param1);
       		            break;
	                case VillaSearchType::DisplayID:
    	                $res = $this->dbvilla->GetVillaInformationByDisplayId($param1);
       		            break;
	                case VillaSearchType::VendorID:
    	                $res = $this->dbvilla->GetVillaInformationByVendorId($param1);
       		            break;
	                case VillaSearchType::Apicode:
    	                $res = $this->dbvilla->GetVillaInformationByApicode($param1);
       		            break;
    	  	    	case VillaSearchType::Currency:
	                    $res = $this->dbvilla->GetVillaInformationByCurrency($param1);
    	                break;
       		        case VillaSearchType::Name:
            	        $res = $this->dbvilla->GetVillaInformationByName($param1);
		                break;
    		        case VillaSearchType::Status:
        		        $res = $this->dbvilla->GetVillaInformationByStatus($param1);
            		    break;
		        }
			}
    		return $res;        
		}       		
		
		public function GetVillaFeeInformation($query_type, $param1){
			$res = null;
	        if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
       		        case VillaFeeSearchType::ALL:
           		        $res = $this->dbvillafee->GetALLVillaFeeInformation();
               		    break;
	                case VillaFeeSearchType::VillaId:
    	                $res = $this->dbvillafee->GetVillaFeeInformationByVillaId($param1);
       		            break;			
					case VillaFeeSearchType::villaname:
    	                $res = $this->dbvillafee->GetVillaFeeInformationByVillaName($param1);
       		            break;			
					case VillaFeeSearchType::feename:
					    $res = $this->dbvillafee->GetVillaFeeInformationByFee($param1);
       		            break;					
					case VillaFeeSearchType::percentage:
					    $res = $this->dbvillafee->GetVillaFeeInformationByPercentage($param1);						
       		            break;					
					case VillaFeeSearchType::flat_fee:
					    $res = $this->dbvillafee->GetVillaFeeInformationByFlatFee($param1);
       		            break;											
					case VillaFeeSearchType::before_taxes:
					    $res = $this->dbvillafee->GetVillaFeeInformationByBeforeTaxes($param1);
       		            break;					
					case VillaFeeSearchType::apply_to_quote:		
					    $res = $this->dbvillafee->GetVillaFeeInformationByApplyToQuote($param1);
       		            break;											
				}
			}
			return $res;
		}
		
		public function GetVillaRateInformation($query_type, $param1){
			$res = null;
	        if($this->utilities->NotEmpty($query_type)){
    	        switch($query_type){
       		        case VillaRateSearchType::ALL:
           		        $res = $this->dbvillarate->GetALLVillaRatesInformation();
               		    break;
	                case VillaRateSearchType::VillaId:
    	                $res = $this->dbvillarate->GetVillaRateInformationByVillaId($param1);
       		            break;			
				}
			}
			return $res;
		}
		
	}
	
	/******************************************************
     * 				Main Invokation to the API			  *
	 ******************************************************/
	$x = new RainDBApi();
	$result = "";
	$func_name = $_GET['action'];
	$param1 = null;
	$param2 = null;
	$param3= null;
	
	
	if(isset($_GET['filter_name']))
		$filter_name = $_GET['filter_name'];		
	if(isset($_GET['param1']))
		$param1 = $_GET['param1'];
	if(isset($_GET['param2']))
		$param2 = $_GET['param2'];
	if(isset($_GET['param3']))
		$param3 = $_GET['param3'];
	
	switch($func_name){		
		case "GetAvailabilityInformation" : $result = $x->GetAvailabilityInformation($filter_name, $param1, $param2, $param3);
							 				break;									
		case "GetAvailabilityCalendar" : $result = $x->GetAvailabilityCalendar($filter_name, $param1, $param2, $param3);
							 				break;																				
		case "GetBookingInformation" : $result = $x->GetBookingInformation($filter_name, $param1, $param2);
							 				break;
		case "GetBookingBillingInformation" : $result = $x->GetBookingBillingInformation($filter_name, $param1);
											  break;
		case "GetBookingClientInformation" : $result = $x->GetBookingClientInformation($filter_name, $param1, $param2);
											 break;									
		case "GetBookingExpenseInformation" : $result = $x->GetBookingExpenseInformation($filter_name, $param1);
											 break;		
		case "GetBookingFeeInformation" : $result = $x->GetBookingFeeInformation($filter_name, $param1);
											 break;		
		case "GetBookingItemsInformation" : $result = $x->GetBookingItemsInformation($filter_name, $param1);
											 break;		
		case "GetClientInformation" : $result = $x->GetClientInformation($filter_name, $param1, $param2);
											 break;		
		case "GetClientNotesInformation" : $result = $x->GetClientNotesInformation($filter_name, $param1);
											 break;		
		case "GetCurrencyInformation" : $result = $x->GetCurrencyInformation($filter_name, $param1);
											 break;		
		case "GetExchangeRatesInformation" :  $result = $x->GetExchangeRatesInformation($filter_name, $param1, $param2);
											  break;
		case "GetLeadInformation" : $result = $x->GetLeadInformation($filter_name, $param1);
									break;
		case "GetPartnerAgentInformation" : $result = $x->GetPartnerAgentInformation($filter_name, $param1, $param2);
											break;
		case "GetPartnerCompanyInformation" : $result = $x->GetPartnerCompanyInformation($filter_name, $param1);
											   break;
		case "GetRainAPIInformation" : $result = $x->GetRainAPIInformation($filter_name, $param1);
									   break;
		case "GetRainAPITransactionInformation" : $result = $x->GetRainAPITransactionInformation($filter_name, $param1);
									   break;
		case "GetRainVillaInformation" : $result = $x->GetRainVillaInformation($filter_name, $param1);
									     break;
		case "GetRateInformation" : $result = $x->GetRateInformation($filter_name, $param1);
									     break;
		case "GetRequestInformation" : $result = $x->GetRequestInformation($filter_name, $param1, $param2);
									   break;
		case "GetVendorInformation" : $result = $x->GetVendorInformation($filter_name, $param1);
										break;								   
		case "GetVillaInformation" : $result = $x->GetVillaInformation($filter_name, $param1);
							 		 break;
		case "GetVillaFeeInformation" : $result = $x->GetVillaFeeInformation($filter_name, $param1);														
									 break;									 									 
		case "GetVillaRateInformation" : $result = $x->GetVillaRateInformation($filter_name, $param1);														
									 break;
	}	
	echo $result;		
	/******************************* EOF ***********************************/
?>	