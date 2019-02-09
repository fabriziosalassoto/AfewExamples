<?php

/********************************* BOF *************************************
* Author: 		  Fabrizio Salas										   *
* Date Creation: 26/11/2015											       *	
* Last Edition:  01/12/2016											   	   *	
* Description:   This library provides all methods to connect the rain_db  *
*                Database in order to export queries as XML format		   *
***************************************************************************/
header("Content-type: text/xml");

include "RainDBApi.php";

final class QuoteAndBookingProcessing{
/*****************************************
* Section 1: Members of the class        *
******************************************/
    /*private $quoteMissingFieldErrors = "";    
    private $bookingMissingFieldErrors = "";
	private $writer = null;
	private $count_errors = 0;   
	public $currency_received = "";
	public $locale_received = "";*/
    
/*****************************************
* Section 2: Constructor                 *
******************************************/
  /*  public function __construct() {
        $quoteMissingFieldErrors = [
          "documentVersion" => 0,
          "advertiserAssignedId" => 0,
          "listingExternalId" => 0,
          "listingChannel" => 0,          
          "reservation" => 0,
        ];                                        
        
        $bookingMissingFieldErrors = [
            "documentVersion" => 0,
            "advertiserAssignedId" => 0,
            "listingExternalId" => 0,
            "listingChannel" => 0,
            "inquirer" => 0,
            "commission" => 0,
            "reservation" => 0,
        ];
    } */
    
/*****************************************
* Section 3: Generic Functions Region    *
******************************************/    
    // generic function: this is the query function to the API provided by Homeaway
    private function QueryAPIforPropertyInformation($listingExternalID, $AdvertiserID, $propertyUrl, $DateFrom, $DateTo){
		$x = new RainDBApi();
		
		$villa = new SimpleXMLElement($x->GetVillaInformation(VillaSearchType::DisplayID, $listingExternalID));				
		$availabilities = new SimpleXMLElement($x->GetAvailabilityCalendar(AvailabilityCalendarSearchType::IDAndInitialDateAndEndDate				
											   ,$listingExternalID,$DateFrom,$DateTo));				
		
		$local_writer = new XMLWriter();
		$local_writer->openMemory();                       
		$local_writer->setIndent(true);
		$local_writer->startDocument("1.0","UTF-8");       
		$local_writer->startElement("properties");
			$local_writer->startElement("property");
				$local_writer->startElement("AdvertiserID");
				$local_writer->text($AdvertiserID);
				$local_writer->endElement();
				$local_writer->startElement("PropertyURL");
				$local_writer->text($propertyUrl);
				$local_writer->endElement();
				$local_writer->startElement("locale");
				$local_writer->text($this->locale_received);
				$local_writer->endElement();
				$local_writer->startElement("restrictions");
					$local_writer->startElement("MinimumLengthOfStay");
					$local_writer->text($villa->record[0]->minstay);
					$local_writer->endElement();
					$local_writer->startElement("MaxOccupancy");
					$local_writer->text($villa->record[0]->maxoccupancy);
					$local_writer->endElement();				            					
				$local_writer->endElement();
				$local_writer->startElement("availabilities");
				$date1 = "";
				$date2 = "";
				foreach($availabilities->availabilities->availability as $availability){
					//$availability->Date;
				}
				
				$local_writer->endElement();
				$local_writer->startElement("rates");
				$local_writer->endElement();
				$local_writer->startElement("fees");
				$local_writer->endElement();
				$local_writer->startElement("paymentpolicy");
				$local_writer->endElement();
				$local_writer->startElement("stayfees");
				$local_writer->endElement();
				$local_writer->startElement("cancellationpolicy");
				$local_writer->endElement();
				$local_writer->startElement("notes");
				$local_writer->endElement();
				$local_writer->startElement("rentalagreement");
				$local_writer->endElement();
			$local_writer->endElement();		
		$local_writer->endElement();
		

		$property_info="";
		//$property_info = simplexml_load_file('xmls/GetPropertyInformation.xml'); // simulating API response         
        return $property_info; 
    }
    
    // This function assigns a 1 or 0 if an error on the required fields is found */
    private function ValidateXML(&$array, $key, $value){
        if($value==null || $value=="")
            $array[$key] = 1;
        else
            $array[$key] = 0;
        return $array[$key];
    }          

    private function CreateLocale($xml){
		$this->writer->startElement("locale");
		$this->writer->text($xml->property[0]->locale);
		$this->writer->endElement();
    }

/********************************************
* Section 4: Generic Errors Handling         *
*********************************************/    
    private function Start_ErrorList(){
		$this->writer->startElement("errorList");
    }
    
    private function End_ErrorList(){
		$this->writer->endElement();
    }
    
    private function Error_MissingParameter($paramName){
		$this->writer->startElement("error");
		$this->writer->startElement("parameter");
		$this->writer->text($paramName);
		$this->writer->endElement();
		$this->writer->startElement("errorCode");
		$this->writer->text("Not assigned yet");
		$this->writer->endElement();
		$this->writer->startElement("errorType");
		$this->writer->text("MISSING_PARAMETER");
		$this->writer->endElement();
		$this->writer->startElement("message");				
		$this->writer->text(sprintf('Error: The parameter %s is required and it was not received on the current request.',$paramName));
		$this->writer->endElement();
		$this->writer->endElement();			
    }
    
    private function Error_NotAvailableWithinDateRange($Date1, $Date2){
		$this->writer->startElement("error");
		$this->writer->startElement("InitialDate");
		$this->writer->text($Date1);
		$this->writer->endElement();
		$this->writer->startElement("EndDate");
		$this->writer->text($Date2);
		$this->writer->endElement();
		$this->writer->startElement("errorCode");
		$this->writer->text("Not assigned yet");
		$this->writer->endElement();
		$this->writer->startElement("errorType");
		$this->writer->text("NOT_AVAILABLE_WITHIN_DATE_RANGE");
		$this->writer->endElement();
		$this->writer->startElement("message");
		$this->writer->text(sprintf('Error: The current property is not available from %s to %s, '
                                   .'you might need to check another date range.',$Date1,$Date2));
		$this->writer->endElement();
		$this->writer->endElement();				
    }
    
    private function Error_ExceedsMaxOccupancy(){
		$this->writer->startElement("error");
		$this->writer->startElement("errorCode");
		$this->writer->text("Not assigned yet");
		$this->writer->endElement();
		$this->writer->startElement("errorType");
		$this->writer->text("EXCEEDS_MAX_OCCUPANCY");
		$this->writer->endElement();
		$this->writer->startElement("message");
		$this->writer->text("This property cannot accomodate the number of travelers selected.");
		$this->writer->endElement();
		$this->writer->endElement();		
    }
    
    private function Error_ExceedsMaxOccupancy_params($Counted, $MaxAllocation){
		$this->writer->startElement("error");
		$this->writer->startElement("count");
		$this->writer->text($Counted);
		$this->writer->endElement();
		$this->writer->startElement("errorCode");
		$this->writer->text("Not assigned yet");
		$this->writer->endElement();
		$this->writer->startElement("errorType");
		$this->writer->text("EXCEEDS_MAX_OCCUPANCY");
		$this->writer->endElement();
		$this->writer->startElement("message");
		$this->writer->text(sprintf('This property can accomodate %s travelers selected.',$MaxAllocation));
		$this->writer->endElement();
		$this->writer->endElement();
    }
    
    private function Error_MinimumStayNotMet(){
		$this->writer->startElement("error");
		$this->writer->startElement("errorCode");
		$this->writer->text("Not assigned yet");
		$this->writer->endElement();
		$this->writer->startElement("errorType");
		$this->writer->text("MIN_STAY_NOT_MET");
		$this->writer->endElement();
		$this->writer->startElement("message");
		$this->writer->text("This property requires a longer stay.");
		$this->writer->endElement();
		$this->writer->endElement();
    }
    
    private function Error_MinimumStayNotMet_params($Counted, $Days){
		$this->writer->startElement("error");
		$this->writer->startElement("count");
		$this->writer->text($Counted);
		$this->writer->endElement();
		$this->writer->startElement("errorCode");
		$this->writer->text("Not assigned yet");
		$this->writer->endElement();
		$this->writer->startElement("errorType");
		$this->writer->text("MIN_STAY_NOT_MET");
		$this->writer->endElement();
		$this->writer->startElement("message");
		$this->writer->text(sprintf('This property requires a minimum stay of %s days.',$Days));
		$this->writer->endElement();
		$this->writer->endElement();
    }
    
    private function Error_ChangeOverDayMistmach(){
        $error = '<error>'
                    .'<errorCode>Not assigned yet</errorCode>'
                    .'<errorType>CHANGE_OVER_DAY_MISTMATCH</errorType>'
                    .'<message>This property requires your stay begin and end on the same day of the week.</message>'
                .'</error>';
        return $error;
    }
    
    private function Error_ChangeOverDayMistmach_params($Day){
        $error = '<error>'
                    .sprintf('<dayOfWeek>%s</dayOfWeek>',$Day)
                    .'<errorCode>Not assigned yet</errorCode>'
                    .'<errorType>CHANGE_OVER_DAY_MISTMATCH</errorType>'
                    .sprintf('<message>This property requires your stay begin and end on a %s.</message>',$Day)
                .'</error>';
        return $error;
    }
    
    private function Error_StartDayMismatch(){
        $error = '<error>'
                    .'<errorCode>Not assigned yet</errorCode>'
                    .'<errorType>START_DAY_MISTMATCH</errorType>'
                    .'<message>This property requires your stay to begin on a different day.</message>'
                .'</error>';
        return $error;
    }
    
    private function Error_StartDayMismatch_params($Day){
        $error = '<error>'
                    .sprintf('<dayOfWeek>%s</dayOfWeek>',$Day)
                    .'<errorCode>Not assigned yet</errorCode>'
                    .'<errorType>START_DAY_MISTMATCH</errorType>'
                    .sprintf('<message>This property requires your stay to begin on a %s.</message>',$Day)
                .'</error>';
        return $error;
    }
    
    private function Error_EndDayMismatch(){
        $error = '<error>'
                    .'<errorCode>Not assigned yet</errorCode>'
                    .'<errorType>END_DAY_MISTMATCH</errorType>'
                    .'<message>This property requires your stay to end on a different day.</message>'
                .'</error>';
        return $error;
    }
    
    private function Error_EndDayMismatch_params($Day){
        $error = '<error>'
                    .sprintf('<dayOfWeek>%s</dayOfWeek>',$Day)
                    .'<errorCode>Not assigned yet</errorCode'
                    .'<errorType>END_DAY_MISTMATCH</errorType>'
                    .sprintf('<message>This property requires your stay to end on a %s.</message>',$Day)
                .'</error>';
        return $error;
    }
    
    private function Error_MinimumAdvancedNoticeNotMet(){
		$this->writer->startElement("error");
		$this->writer->startElement("errorCode");
		$this->writer->text("Not assigned yet");
		$this->writer->endElement();
		$this->writer->startElement("errorType");
		$this->writer->text("MIN_ADVANCED_NOTICE_NOT_MET");
		$this->writer->endElement();
		$this->writer->startElement("message");
		$this->writer->text("This property requires more advance notice to book.");
		$this->writer->endElement();
		$this->writer->endElement();		
    }
    
    private function Error_MinimumAdvancedNoticeNotMet_params($Counted, $Days){
		$this->writer->startElement("error");
		$this->writer->startElement("count");
		$this->writer->text($Counted);
		$this->writer->endElement();
		$this->writer->startElement("errorCode");
		$this->writer->text("Not assigned yet");
		$this->writer->endElement();
		$this->writer->startElement("errorType");
		$this->writer->text("MIN_ADVANCED_NOTICE_NOT_MET");
		$this->writer->endElement();
		$this->writer->startElement("message");
		$this->writer->text(sprintf('This property requires %s days advance notice to book.',$Days));
		$this->writer->endElement();
		$this->writer->endElement();
    }
    
    private function Error_StayNightIncrementMistmatch(){
        $error = '<error>'
                    .'<errorCode>Not assigned yet</errorCode>'
                    .'<errorType>STAY_NIGHT_INCREMENT_MISTMATCH</errorType>'
                    .'<message>This property requires stays to be booked in specific increments.</message>'
                .'</error>';
        return $error;
    }
    
    private function Error_StayNightIncrementMistmatch_params($Days){
        $error = '<error>'
                    .sprintf('<count>%s</count>',$Days)
                    .'<errorCode>Not assigned yet</errorCode>'
                    .'<errorType>STAY_NIGHT_INCREMENT_MISTMATCH</errorType>'
                    .sprintf('<message>This property requires stays to be booked in increments of %s days.</message>',$Days)
                .'</error>';
        return $error;
    }
    
/********************************************
* Section 5: Quote Errors Handling          *
*********************************************/
    //private function ValidateQuoteRequest(&$FormingQuoteResponse, $IncomingQuoteRequest, $PropertyInformation = null){
	private function ValidateQuoteRequest($IncomingQuoteRequest, $PropertyInformation = null){
        $this->count_errors = 0;
        if($PropertyInformation == null || $PropertyInformation == ''){
            $this->count_errors = $this::DetermineErrorsOnIncomingQuote($IncomingQuoteRequest);
            if($this->count_errors>0){
				$this::Start_ErrorList();
				$this::ThrowQuoteErrorOnRequiredFields();                
			}
        } else {
            $date1 = $IncomingQuoteRequest->quoteRequestDetails[0]->reservation[0]->reservationDates[0]->beginDate[0];
            $date2 = $IncomingQuoteRequest->quoteRequestDetails[0]->reservation[0]->reservationDates[0]->endDate[0];
            $nbrAdults = $IncomingQuoteRequest->quoteRequestDetails[0]->reservation[0]->numberOfAdults;
            $nbrChildren = $IncomingQuoteRequest->quoteRequestDetails[0]->reservation[0]->numberOfChildren;
            $nbrPets = $IncomingQuoteRequest->quoteRequestDetails[0]->reservation[0]->numberOfPets;
            
            // Check if the request is within the availability of the Villa.
            $Available = 0;            
            foreach($PropertyInformation->property->availabilities->availability as $q){                
				if((new DateTime($date1) >= new DateTime($q->dateFrom)) && (new DateTime($date2) <= new DateTime($q->dateTo)))                    $Available++;                
            }                        
            if($Available==0){                
				if($this->count_errors==0)
					$this::Start_ErrorList();
				$this::Error_NotAvailableWithinDateRange($date1,$date2);
                $this->count_errors++;
            }
            
            // Check if the minimum stay is within range required           
            $d1 = new DateTime($date1);
            $d2 = new DateTime($date2);
            $days_of_stay = $d1->diff($d2);
            if($days_of_stay->days < $PropertyInformation->property[0]->restrictions[0]->MinimumLengthOfStay){
				if($this->count_errors==0)
					$this::Start_ErrorList();
				$this::Error_MinimumStayNotMet_params($days_of_stay->days,$PropertyInformation->property[0]->restrictions[0]->MinimumLengthOfStay);		
                $this->count_errors++;
            }
            
            $people = $nbrAdults + $nbrChildren;
            if($people > $PropertyInformation->property[0]->restrictions[0]->MaxOccupancy){
				if($this->count_errors==0)
					$this::Start_ErrorList();
				$this::Error_ExceedsMaxOccupancy_params($people,$PropertyInformation->property[0]->restrictions[0]->MaxOccupancy);
                $this->count_errors++;
            } else {
                
                foreach($PropertyInformation->property->rates->seasons->season as $current_season){
                    if(($d1 >= new DateTime($current_season->dateFrom)) && ($d2 <= new DateTime($current_season->dateTo))){
						$today = new DateTime(); 
                        $d1 = new DateTime($date1);
                        $diff = $d1->diff($today);
                                                
                        if($diff->days < $current_season->rate[0]->deadlineInDays){
							if($this->count_errors==0)
								$this::Start_ErrorList();
							$this::Error_MinimumAdvancedNoticeNotMet_params($diff->days,$current_season->rate[0]->deadlineInDays);
                            $this->count_errors++;
                        }                                                           
                    }
                }
            }
        }
        if($this->count_errors>0){
			$this::End_ErrorList();
        }       
    }
    
    // Finds errors on the structure of the incoming quote XML to determine if required fields are not present */
    private function DetermineErrorsOnIncomingQuote($quote){
        $errorCounter = 0;
        $errorCounter+=$this::ValidateXML($this->quoteMissingFieldErrors,"documentVersion", $quote->documentVersion);
        $errorCounter+=$this::ValidateXML($this->quoteMissingFieldErrors,"advertiserAssignedId", $quote->quoteRequestDetails[0]->advertiserAssignedId);
        $errorCounter+=$this::ValidateXML($this->quoteMissingFieldErrors,"listingExternalId", $quote->quoteRequestDetails[0]->listingExternalId);
        $errorCounter+=$this::ValidateXML($this->quoteMissingFieldErrors,"listingChannel", $quote->quoteRequestDetails[0]->listingChannel);
        $errorCounter+=$this::ValidateXML($this->quoteMissingFieldErrors,"reservation", $quote->quoteRequestDetails[0]->reservation);
        return $errorCounter;
    }
    
    // Create Errors on syntax received when a required field is not sent. Ask Leif
    private function ThrowQuoteErrorOnRequiredFields(){
		$error_count = 0;
        foreach($this->quoteMissingFieldErrors as $key=>$value){
            if($value==1){
				if($error_count==0){
					$this::CreateHeaderQuoteResponse();
				}					
				$error_count++;
				$this::Error_MissingParameter($key);
			}
        }        
    }
    
/*****************************************
* Section 6: Quote Functions             *
* ****************************************/           
    // Receives the incoming quote in order to process it
    public function SendQuote($input){
		$this->writer = null;
		$this->count_errors = 0;
		$this::CreateHeaderQuoteResponse();                           
        $this::ValidateQuoteRequest($input);
                               
        if($this->count_errors == 0){
            $advertiser_id = $input->quoteRequestDetails[0]->advertiserAssignedId;        
            $property_url = $input->quoteRequestDetails[0]->propertyUrl;
            $listing_External_ID = $input->quoteRequestDetails[0]->listingExternalId;                                                                       
            $date_from = $input->quoteRequestDetails[0]->reservation[0]->reservationDates[0]->beginDate[0];
            $date_to = $input->quoteRequestDetails[0]->reservation[0]->reservationDates[0]->endDate[0];
                        
            $propertychk = $this::QueryAPIforPropertyInformation($listing_External_ID, $advertiser_id, $property_url, $date_from, 
																$date_to);
            if($propertychk!=null && $propertychk!="" && $this::ValidateQuoteRequest($input, $propertychk)==0){                
                $this::CreateQuoteResponseDetails($propertychk);
                $this::CreateBodyQuoteResponse($propertychk, $date_from, $date_to);
                $this::CreateRentalAgreementAndQuoteResponseDetailsClosing($propertychk);
            }                                                                                                                    
        }
        $this::CreateFooterQuoteResponse();
		$this->writer->endDocument();
        $result = $this->writer->outputMemory();
        $this->writer->flush();		
        return $result;
    }  
    
    // Adds the common header.
    private function CreateHeaderQuoteResponse(){
		$this->writer = new XMLWriter();
		$this->writer->openMemory();                       
		$this->writer->setIndent(true);
        $this->writer->startDocument("1.0","UTF-8"); 		
		$this->writer->startElement("documentVersion");
		$this->writer->text("1.1");
		$this->writer->endElement();
    }
    
    // Adds the quoteResponseDetails section where locale option appears
    private function CreateQuoteResponseDetails($xml){
		$this->writer->startElement("quoteResponseDetails");
        $this::CreateLocale($xml);     
    } 
    
    private function CreateBodyQuoteResponse($xml,  $date1, $date2){
        $mAmount = 0;
        $mCurrency = ""; 
        $mDueDate = "";		
        
		$this->writer->startElement("orderList");
			$this->writer->startElement("order");
				$this->writer->startElement("currency");
					$this->writer->text($xml->property[0]->currency);
				$this->writer->endElement();
				$this::CreateOrderItemList($xml, $date1, $date2, $mAmount, $mCurrency, $mDueDate);
				$this->writer->startElement("paymentSchedule");
					$this::CreateAcceptedPaymentForms();
        			$this::CreatePaymentScheduleItemList($mAmount, $mCurrency, $mDueDate);
				$this->writer->endElement();
				$this->writer->startElement("reservationcancellationpolicy");
					$this->writer->startElement("description");
						$this->writer->text($xml->property[0]->cancellationpolicy[0]->notes);
					$this->writer->endElement();
				$this->writer->endElement();
				$this::CreateStayFees($xml);
			$this->writer->endElement();
		$this->writer->endElement();				
    }
    
    private function CreateFooterQuoteResponse(){
		$this->writer->endElement();
    }
    
    private function CreateRentalAgreementAndQuoteResponseDetailsClosing($xml){
  	$this->writer->startElement("rentalAgreement");
		$this->writer->startElement("agreementText");
		$this->writer->text($xml->property[0]->rentalagreement);
		$this->writer->endElement();
		$this->writer->endElement();
		$this->writer->endElement();
    }
    
    // This function creates the OrderItemList and adds the rates and fees, plus calculates the total for the Schedule Payment section
    private function CreateOrderItemList($xml, $date1, $date2, &$pAmount, &$pCurrency, &$pDueDate){
		$this->writer->startElement("orderitemlist");
        foreach($xml->property->rates->seasons->season as $current_season){
			if((new DateTime($date1) >= new DateTime($current_season->dateFrom)) && (new DateTime($date2) <= new DateTime($current_season->dateTo))){
                $pAmount+=doubleval($current_season->rate[0]->totalAmount);
                $pCurrency = $current_season->rate[0]->preTaxAmount['currency'];
                $pDueDate = "2015-12-12";
				
				$this->writer->startElement("orderitem");
					$this->writer->startElement("description");
						$this->writer->text($current_season->rate[0]->rateDesc);
					$this->writer->endElement();
					$this->writer->startElement("feetype");
						$this->writer->text($current_season->rate[0]->rateType);
					$this->writer->endElement();
					$this->writer->startElement("name");
						$this->writer->text($current_season->rate[0]->rateName);
					$this->writer->endElement();
					$this->writer->startElement("pretaxamount");
						$this->writer->text($current_season->rate[0]->preTaxAmount);
					$this->writer->endElement();
					$this->writer->startElement("totalamount");
						$this->writer->text($current_season->rate[0]->totalAmount);
					$this->writer->endElement();
				$this->writer->endElement();
            }
        }
        foreach($xml->property->fees->fee as $current_fee){
            $pAmount+=doubleval($current_fee->totalAmount);
			$this->writer->startElement("orderitem");
				$this->writer->startElement("description");
					$this->writer->text($current_fee->feeDesc);
				$this->writer->endElement();
				$this->writer->startElement("feetype");
					$this->writer->text($current_fee->feeType);
				$this->writer->endElement();
				$this->writer->startElement("name");
					$this->writer->text($current_fee->feeName);
				$this->writer->endElement();
				$this->writer->startElement("pretaxamount");
					$this->writer->text($current_fee->preTaxAmount);
				$this->writer->endElement();
				$this->writer->startElement("totalamount");
					$this->writer->text($current_fee->totalAmount);
				$this->writer->endElement();
			$this->writer->endElement();			
        }
		$this->writer->endElement();

        $initialDate = new DateTime($date1);
        $calc_interval = "P".$xml->property[0]->cancellationpolicy->deadlineInDays."D";    
        $interval = new DateInterval($calc_interval);        
        $initialDate->sub($interval);
        $pDueDate = $initialDate->format('Y-m-d');                    
    }
    
    // This is hardcoded function that includes the accepted payments
    private function CreateAcceptedPaymentForms(){
		$this->writer->startElement("acceptedPaymentForms");			
		
		$this->writer->startElement("paymentCardDescriptor");			
		$this->writer->startElement("paymentFormType");			
		$this->writer->text("CARD");
		$this->writer->endElement();
		$this->writer->startElement("cardCode");			
		$this->writer->text("VISA");
		$this->writer->endElement();
		$this->writer->startElement("cardType");			
		$this->writer->text("CREDIT");
		$this->writer->endElement();
		$this->writer->endElement();
		
		$this->writer->startElement("paymentCardDescriptor");			
		$this->writer->startElement("paymentFormType");			
		$this->writer->text("CARD");
		$this->writer->endElement();
		$this->writer->startElement("cardCode");			
		$this->writer->text("MASTERCARD");
		$this->writer->endElement();
		$this->writer->startElement("cardType");			
		$this->writer->text("CREDIT");
		$this->writer->endElement();
		$this->writer->endElement();
		
		$this->writer->startElement("paymentCardDescriptor");			
		$this->writer->startElement("paymentFormType");			
		$this->writer->text("CARD");
		$this->writer->endElement();
		$this->writer->startElement("cardCode");			
		$this->writer->text("DISCOVER");
		$this->writer->endElement();
		$this->writer->startElement("cardType");			
		$this->writer->text("CREDIT");
		$this->writer->endElement();
		$this->writer->endElement();

		$this->writer->startElement("paymentCardDescriptor");			
		$this->writer->startElement("paymentFormType");			
		$this->writer->text("INVOICE");
		$this->writer->endElement();
		$this->writer->startElement("paymentNote");			
		$this->writer->text("A note about invoice processing");
		$this->writer->endElement();
		$this->writer->endElement();
		
		$this->writer->endElement();
    }
   
    // This function adds the total amount of the quote.
    private function CreatePaymentScheduleItemList($Amount, $Currency, $DueDate){
		$this->writer->startElement("paymentscheduleitemlist");
		$this->writer->startElement("paymentscheduleitem");
		$this->writer->startElement("amount");		
		$this->writer->text($Amount);
		$this->writer->writeAttribute("currency",$Currency);		
		$this->writer->endElement();
		$this->writer->startElement("duedate");		
		$this->writer->text($DueDate);
		$this->writer->endElement();
		$this->writer->endElement();
		$this->writer->endElement();		
    }      
    
    // Adds the additional fees as a note that might charge to customer as extra.
    private function CreateStayFees($xml){        
		$this->writer->startElement("stayfees");
		$this->writer->startElement("stayfee");
		$this->writer->startElement("description");
		$this->writer->text($xml->property[0]->stayfees[0]->stayfee[0]->note);
		$this->writer->endElement();
		$this->writer->endElement();
		$this->writer->endElement();		
    }    

/********************************************
* Section 7: Booking Errors Handling          *
*********************************************/
    private function ValidateBookingRequest(&$FormingBookingResponse, $IncomingBookingRequest, $PropertyInformation = null){
        $count_errors = 0;
        $error_list = "";
        if($PropertyInformation == null || $PropertyInformation == ''){
            $count_errors = $this::DetermineErrorsOnIncomingBooking($IncomingBookingRequest);
            if($count_errors>0)
                $error_list .= $this::ThrowBookingErrorOnRequiredFields();
        } else {
            $date1 = $IncomingBookingRequest->bookingRequestDetails[0]->reservation[0]->reservationDates[0]->beginDate[0];
            $date2 = $IncomingBookingRequest->bookingRequestDetails[0]->reservation[0]->reservationDates[0]->endDate[0];
            $nbrAdults = $IncomingBookingRequest->bookingRequestDetails[0]->reservation[0]->numberOfAdults;
            $nbrChildren = $IncomingBookingRequest->bookingRequestDetails[0]->reservation[0]->numberOfChildren;
            $nbrPets = $IncomingBookingRequest->bookingRequestDetails[0]->reservation[0]->numberOfPets;
    
            // Check if the request is within the availability of the Villa.
            $Available = 0;
            foreach($PropertyInformation->property->availabilities->availability as $q){
                if((new DateTime($date1) >= new DateTime($q->dateFrom)) && (new DateTime($date2) <= new DateTime($q->dateTo)))
                    $Available++;
            }
            if($Available==0){
                $error_list .= $this::Error_NotAvailableWithinDateRange($date1,$date2);
                $count_errors++;
            }
    
            // Check if the minimum stay is within range required
            $d1 = new DateTime($date1);
            $d2 = new DateTime($date2);
            $days_of_stay = $d1->diff($d2);
            if($days_of_stay->days < $PropertyInformation->property[0]->restrictions[0]->MinimumLengthOfStay){
                $error_list .= $this::Error_MinimumStayNotMet_params($days_of_stay->days,$PropertyInformation->property[0]->restrictions[0]->MinimumLengthOfStay);
                $count_errors++;
            }
    
            $people = $nbrAdults + $nbrChildren;
            if($people > $PropertyInformation->property[0]->restrictions[0]->MaxOccupancy){
                $error_list .= $this::Error_ExceedsMaxOccupancy_params($people,$PropertyInformation->property[0]->restrictions[0]->MaxOccupancy);
                $count_errors++;
            } else {   
                foreach($PropertyInformation->property->rates->seasons->season as $current_season){
                    if(($d1 >= new DateTime($current_season->dateFrom)) && ($d2 <= new DateTime($current_season->dateTo))){
                        $today = new DateTime();
                        $d1 = new DateTime($date1);
                        $diff = $d1->diff($today);
    
                        if($diff->days < $current_season->rate[0]->deadlineInDays){
                            $error_list .= $this::Error_MinimumAdvancedNoticeNotMet_params($diff->days,$current_season->rate[0]->deadlineInDays);
                            $count_errors++;
                        }
                    }
                }
            }
        }
        if($count_errors>0){
            $error_list = $this::Start_ErrorList().$error_list.$this::End_ErrorList();
            $FormingBookingResponse.=$error_list;
        }    
        return $count_errors;
    }
    
    // Finds errors on the structure of the incoming quote XML to determine if required fields are not present */
    private function DetermineErrorsOnIncomingBooking($booking){
        $errorCounter = 0;
        $errorCounter+=$this::ValidateXML($this->bookingMissingFieldErrors,"documentVersion", $booking->documentVersion);
        $errorCounter+=$this::ValidateXML($this->bookingMissingFieldErrors,"advertiserAssignedId", $booking->bookingRequestDetails[0]->advertiserAssignedId);
        $errorCounter+=$this::ValidateXML($this->bookingMissingFieldErrors,"listingExternalId", $booking->bookingRequestDetails[0]->listingExternalId);
        $errorCounter+=$this::ValidateXML($this->bookingMissingFieldErrors,"listingChannel", $booking->bookingRequestDetails[0]->listingChannel);
        $errorCounter+=$this::ValidateXML($this->bookingMissingFieldErrors,"inquirer", $booking->bookingRequestDetails[0]->inquirer);
        $errorCounter+=$this::ValidateXML($this->bookingMissingFieldErrors,"commission", $booking->bookingRequestDetails[0]->commission);
        $errorCounter+=$this::ValidateXML($this->bookingMissingFieldErrors,"reservation", $booking->bookingRequestDetails[0]->reservation);
        return $errorCounter;
    }
    
    // Create Errors on syntax received when a required field is not sent. Ask Leif
    private function ThrowBookingErrorOnRequiredFields(){
        $error = '';
        foreach($this->bookingMissingFieldErrors as $key=>$value){
            if($value==1)
                $error.=$this::Error_MissingParameter($key);
        }
        return $error;
    }

/*****************************************
* Section 8: Booking Functions           *
* ****************************************/        
    /* This methid processes the Booking Request */
    public function SendBooking($input){
        $response = $this::CreateHeaderBookingResponse();          
		$response->
        $errors = $this::ValidateBookingRequest($response, $input);
                               
        if($errors == 0){
            $advertiser_id = $input->bookingRequestDetails[0]->advertiserAssignedId;        
            $property_url = $input->bookingRequestDetails[0]->propertyUrl;
            $listing_External_ID = $input->bookingRequestDetails[0]->listingExternalId;
            $unit_External_ID =  $input->bookingRequestDetails[0]->unitExternalId;
            $date_from = $input->bookingRequestDetails[0]->reservation[0]->reservationDates[0]->beginDate[0];
            $date_to = $input->bookingRequestDetails[0]->reservation[0]->reservationDates[0]->endDate[0];
                        
            $propertychk = $this::QueryAPIforPropertyInformation($listing_External_ID, $advertiser_id, $property_url, $date_from, 					
																 $date_to);
            
            if($propertychk!=null && $propertychk!="" && $this::ValidateQuoteRequest($response, $input, $propertychk)==0){                
                $response .= $this::CreateBookingResponseDetails();
                $response .= $this::CreateBodyBookingResponse($propertychk, $date_from, $date_to);
                $response .= $this::CreateRentalAgreementAndBookingResponseDetailsClosing($propertychk);                                              
            }                                                                                                                                
        }
        $response .= $this::CreateFooterBookingResponse();
        $response = new SimpleXMLElement($response);
        return $response;
    }
    
    // Adds the common header.
    private function CreateHeaderBookingResponse(){
        $header = '<?xml version="1.0" encoding="UTF-8"?>'
                    .'<bookingResponse>'
                    .'<documentVersion>1.1</documentVersion>';
        return $header;
    }

    private function CreateBookingResponseHeadingTags(){
        $xml = '';
        return $xml;
    }
    
    // Adds the quoteResponseDetails section where locale option appears
    private function CreateBookingResponseDetails(){
        $quoteDetails = '<bookingResponseDetails>';                            
        return $quoteDetails;
    }
    
    private function CreateBodyBookingResponse($xml,  $date1, $date2){
        
        $body = '<advertiserAssignedId>' 
                .$xml->bookingRequestDetails[0]->AdvertiserID
               .'</advertiserAssignedId>'
               .'<listingExternalId>'
                .$xml->bookingRequestDetails[0]->listingExternalId 
               .'</listingExternalId>';        
               //unitExternalId        
        
        
        
        /*'
           $mAmount = 0;
           $mCurrency = ""; 
           $mDueDate = "";
          
          <orderList>'
                    .'<order>'         
                        .'<currency>'
                        .$xml->property[0]->currency
                        .'</currency>'
                        .$this::CreateOrderItemList($xml, $date1, $date2, $mAmount, $mCurrency, $mDueDate)
                        .'<paymentSchedule>'
                        .$this::CreateAcceptedPaymentForms()
                        .$this::CreatePaymentScheduleItemList($mAmount, $mCurrency, $mDueDate)
                        .'</paymentSchedule>'
                        .'<reservationcancellationpolicy>'
                            .'<description>'
                            .$xml->property[0]->cancellationpolicy[0]->notes
                            .'</description>'
                        .'</reservationcancellationpolicy>'
                        .$this::CreateStayFees($xml)
                    .'</order>'
                .'</orderList>';*/                     
        return $body;
    }
    
    private function CreateFooterBookingResponse(){
        $footer = '</bookingResponse>';
        return $footer;
    }
    
    /***************************************************************************************************/
    
    public function SendBookingIndexRequest($xml){
        foreach($xml->advertisers->advertiser as $advertiser){
            $assignedId = $advertiser->assignedId;
            $email = $advertiser->inquirers->inquirer[0]->emailAddress[0];
        }
        
        
        return $assignedId." ".$email;  
    }
    
    private function CreateBookingIndexContent(){
        
    }
}

/*****************************************************************************************************
/* Listings for Feeds
/*****************************************************************************************************/

final class Feeds{
	var $pid = null;
	
	public function __construct(){}
	
	public function GetRates(){
	}
	
	public function GetAvailability(){
		$availability = simplexml_load_file('http://www.lacurevillas.com/homeaway-availability/');
		return $availability;
	}
}



echo "here I am";

/*$result = "";
$func_name = $_GET['action'];

if(isset($_GET['currency']))
	$requested_currency = $_GET['currency'];
else
	$requested_currency = 'USD';
	
if(isset($_GET['locale']))
	$requested_locale = $_GET['locale'];
else
	$requested_locale = 'en_US';*/
	
/*if(isset($_GET['pid']))
	$requested_pid = $_GET['pid'];
else
	$requested_pid = null;
	
$xmldoc = null;
	
if(isset($_GET['request']))
	$xmldoc =  simplexml_load_string($_GET['request']);		

$qbproc = new QuoteAndBookingProcessing();	
$qbproc->currency_received = $requested_currency;
$qbproc->locale_received = $requested_locale;

$rateavailproc = new Feeds();
$rateavailproc->pid = $requested_pid;



switch($func_name){		
	case "SendQuote" : echo $qbproc->SendQuote($xmldoc); break;
	case "SendBooking" : echo $qbproc->SendBooking($xmldoc)->asXML(); break;
	case "SendBookingIndexRequest" : echo $qbproc->SendBookingIndexRequest($xmldoc); break;
	case "GetRates" : echo $rateavailproc->GetRates(); break;
	case "GetAvailability" : echo $rateavailproc->GetAvailability(); break;
}*/
?>