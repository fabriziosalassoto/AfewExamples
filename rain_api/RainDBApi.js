$(document).ready(function(){	
	"use strict";
	$("#slc_operation").on("change",function(){
		switch($("#slc_operation option:selected").val()){
			case "GetVillaInformation" : $("#slc_filter option").remove();
										 $("#slc_filter").append("<option selected='selected' value='All'>All</option>" +
							 			  "<option value='id'>By ID</option>"+
							  	 		  "<option value='displayid'>By DisplayID</option>"+
										  "<option value='vendorid'>By VendorID</option>"+
										  "<option value='apicode'>By Apicode</option>"+
										  "<option value='currency'>By Currency</option>"+
										  "<option value='name'>By Name</option>"+
										  "<option value='status'>By Status</option>");
			 							 break;
			case "GetAvailabilityCalendar" : $("#slc_filter option").remove();
										 $("#slc_filter").append(
										 	"<option value='id-and-initialdate-and-enddate'>By VillaID, InitialDate and EndDate</option>");
			 							 break;
			case "GetAvailabilityInformation" : $("#slc_filter option").remove();
	   			 	   						    $("#slc_filter").append(
												"<option selected='selected' value='All'>All</option>"+
							 			         "<option value='rainvillaid'>By RainVillaID</option>"+
  							  	 		         "<option value='bookingtype'>By BookingType</option>"+
												 "<option value='bookingid'>By BookingID</option>"+
										         "<option value='arrivaldate'>By Arrival Date</option>"+
												 "<option value='id-and-arrivaldate'>By RainVillaID and Arrival Date</option>"+
										         "<option value='departuredate'>By Departure Date</option>"+
												 "<option value='id-and-departuredate'>By RainVillaID AND Departure Date</option>"+
										         "<option value='arrival-and-departure'>By Arrival and Departure</option>"+
											    "<option value='id-and-arrival-and-departure'>By RainVillaID, Arrival and Departure</option>"+										    										     "<option value='status'>By Status</option>");
												 break;
			case "GetBookingInformation" : $("#slc_filter option").remove();
   			 	   						    $("#slc_filter").append(		
											"<option selected='selected' value='All'>All</option>"+
											 "<option value='rainvillaid'>By Rain Villa ID</option>"+
								 			 "<option value='statusid'>By Status ID</option>"+
								 			 "<option value='clientid'>By Client ID</option>"+
								 			 "<option value='leadsourceid'>By Lead Source ID</option>"+
								 			 "<option value='partneragentid'>By Partner Agent Id</option>"+
								 			 "<option value='salesrepid'>By Sales Representative ID</option>"+
								 			 "<option value='vendorid'>Vendor ID</option>"+
								 			 "<option value='invoicenumber'>Invoice Number</option>"+
								 			 "<option value='bookeddate'>Boodked Date</option>"+
								 			 "<option value='id-and-bookeddate'>Rain Villa ID and Booked Date</option>");
											 break;
			case "GetBookingBillingInformation" : $("#slc_filter option").remove();
   			 	   						 	      $("#slc_filter").append(		
												  "<option selected='selected' value='All'>All</option>"+
											      "<option value='bookingid'>By Booking ID</option>"+
								 			      "<option value='currency'>By Currency</option>");
											 	  break;
			case "GetBookingClientInformation" : $("#slc_filter option").remove();
   			 	   							     $("#slc_filter").append(		
												 "<option selected='selected' value='All'>All</option>"+
											     "<option value='bookingid'>By Booking ID</option>"+
								 			     "<option value='email'>By Email</option>"+
												 "<option value='phone'>By Phone</option>"+
												 "<option value='first-and-lastname'>By First Name and Last Name</option>");
											 	 break;	
			case "GetBookingExpenseInformation" : $("#slc_filter option").remove();
   			 	   						 	      $("#slc_filter").append(		
												  "<option selected='selected' value='All'>All</option>"+
											      "<option value='bookingid'>By Booking ID</option>");
											 	  break;											
			case "GetBookingFeeInformation" : $("#slc_filter option").remove();
   			 	   						 	      $("#slc_filter").append(		
												  "<option selected='selected' value='All'>All</option>"+
											      "<option value='bookingid'>By Booking ID</option>"+
												  "<option value='feename'>By Booking Fee Name</option>");
											 	  break;	
			case "GetBookingItemsInformation" : $("#slc_filter option").remove();
   			 	   						 	      $("#slc_filter").append(		
												  "<option selected='selected' value='All'>All</option>"+
											      "<option value='bookingid'>By Booking ID</option>"+
												  "<option value='itemname'>By Booking Item Name</option>");
											 	  break;													  
			case "GetClientInformation" : $("#slc_filter option").remove();
   			 	   						 	      $("#slc_filter").append(		
												  "<option selected='selected' value='All'>All</option>"+
											      "<option value='id'>By ID</option>"+
											      "<option value='firstname'>By First Name</option>"+
											      "<option value='lastname'>By Last Name</option>"+
											      "<option value='first-and-lastname'>By First and Last Name</option>"+
											      "<option value='phone'>By Phone</option>"+
											      "<option value='email'>By E-mail</option>");
											 	  break;
			case "GetClientNotesInformation" : $("#slc_filter option").remove();
   			 	   						 	   $("#slc_filter").append(		
											   "<option selected='selected' value='All'>All</option>"+
											    "<option value='userid'>By User Id</option>"+
												"<option value='clientid'>By Client Id</option>");
											 	break;												  	
			case "GetCurrencyInformation" : $("#slc_filter option").remove();
   			 	   						 	   $("#slc_filter").append(		
											   "<option selected='selected' value='All'>All</option>"+
											    "<option value='currency'>By Currency</option>"+
												"<option value='description'>By Description</option>");
											 	break;												  	
			case "GetExchangeRatesInformation" : $("#slc_filter option").remove();
   			 	   						 	     $("#slc_filter").append(
										        "<option selected='selected' value='All'>All</option>"+
												 "<option value='startdate'>By Start Date</option>"+
												 "<option value='currency-and-date'>By Currency and Date</option>");		
												 break;
			case "GetLeadInformation" : $("#slc_filter option").remove();
   			 	   						 	     $("#slc_filter").append(
												 "<option selected='selected' value='All'>All</option>"+
												 "<option value='id'>By Id</option>"+
												 "<option value='primaryleadsourceid'>By Primary Lead Source Id</option>"+
												 "<option value='secondaryleadsourceid'>By Secondary Lead Source Id</option>");		
												 break;
			case "GetPartnerAgentInformation" : $("#slc_filter option").remove();
   			 	   						 	     $("#slc_filter").append(
											   "<option selected='selected' value='All'>All</option>"+
												"<option value='id'>By Id</option>"+
												"<option value='agentcode'>By Agent Code</option>"+
												"<option value='partnercompanyid'>By Partner Company Id</option>"+
												"<option value='firstname'>By First Name</option>"+
												"<option value='lastname'>By Last Name</option>"+
												"<option value='first-and-lastname'>By First and Last Name</option>"+
												"<option value='phone'>By Phone</option>"+
												"<option value='email'>By Email</option>");		
												 break;
			case "GetPartnerCompanyInformation" : $("#slc_filter option").remove();
   			 	   						 	     $("#slc_filter").append(
											    "<option selected='selected' value='All'>All</option>"+
												"<option value='id'>By Id</option>"+
												"<option value='name'>By Name</option>"+
												"<option value='phone'>By Phone</option>"+
												"<option value='email'>By Email</option>");		
												 break;
			case "GetRainAPIInformation" : $("#slc_filter option").remove();
   			 	   						 	     $("#slc_filter").append(
											    "<option selected='selected' value='All'>All</option>"+												 												"<option value='id'>By Id</option>"+
												"<option value='name'>By Name</option>"+
												"<option value='active'>By Active</option>");		
												 break;
			case "GetRainAPITransactionInformation" : $("#slc_filter option").remove();
   			 	   						 	     $("#slc_filter").append(
											   "<option selected='selected' value='All'>All</option>"+
												"<option value='id'>By Id</option>"+
												"<option value='transtatus'>By Transaction Status</option>"+
												"<option value='bookingstatus'>By Booking Status</option>"+
												"<option value='bookingid'>By Booking Id</option>"+
												"<option value='rainvillaid'>By Rain Villa Id</option>");		
												 break;
			case "GetRainVillaInformation" : $("#slc_filter option").remove();
   			 	   						 	     $("#slc_filter").append(
											   "<option selected='selected' value='All'>All</option>"+
												"<option value='id'>By Id</option>"+
												"<option value='displayid'>By Display Id</option>"+
												"<option value='name'>By Name</option>"+
												"<option value='jetsetterid'>By JetSetterId</option>"+
												"<option value='vendorid'>By Vendor Id</option>");		
												 break;
			case "GetRateInformation" : $("#slc_filter option").remove();
   			 	   						 	     $("#slc_filter").append(
											    "<option selected='selected' value='All'>All</option>"+												 												"<option value='rainvillaid'>By Rain Villa Id</option>");		
												 break;
			case "GetRequestInformation" : $("#slc_filter option").remove();
   			 	   						 	     $("#slc_filter").append(
											   "<option selected='selected' value='All'>All</option>"+												 												"<option value='id'>By Id</option>"+
												"<option value='status'>By Status</option>"+
												"<option value='type'>By Type</option>"+
												"<option value='leadsourceid'>By Lead Source Id</option>"+
												"<option value='salesuserid'>By Sales User Id</option>"+
												"<option value='client_firstname'>By Client First Name</option>"+
												"<option value='client_lastname'>By CLient Last Name</option>"+
												"<option value='client-first-and-lastname'>By CLient First and Last Name</option>"+
												"<option value='client_email'>By Email</option>"+
												"<option value='client_telephone'>By Phone</option>"+
												"<option value='clientid'>By Client Id</option>"+
												"<option value='villaid'>By Villa Id</option>");		
												 break;
			case "GetVendorInformation" : $("#slc_filter option").remove();
   			 	   						 	     $("#slc_filter").append(
											   "<option selected='selected' value='All'>All</option>"+
											   "<option value='id'>By Id</option>"+
											   "<option value='vendorcode'>By Vendor Code</option>"+
												"<option value='name'>By Name</option>"+
												"<option value='phone'>By Phone</option>"+
												"<option value='email'>By Email</option>");		
												 break;			
			case "GetVillaFeeInformation" : $("#slc_filter option").remove();
   			 	   						 	     $("#slc_filter").append(
												"<option selected='selected' value='All'>All</option>"+
											    "<option value='villaid'>By Villa Id</option>"+
												"<option value='villaname'>By Villa Name</option>"+
												"<option value='fee'>By Fee</option>"+
												"<option value='percentage'>By Percentage</option>"+
												"<option value='flat_fee'>By Flat Fee</option>"+
												"<option value='before_taxes'>By Before to Taxes</option>"+
												"<option value='apply_to_quote'>By Apply to quote</option>");
												break;
			case "GetVillaRateInformation" : $("#slc_filter option").remove();
   			 	   						 	     $("#slc_filter").append(
											    "<option selected='selected' value='All'>All</option>"+												 												"<option value='villaid'>By Villa Id</option>");		
												 break;												
		}
	});	

	$("#btnQuery").click(function(){
		var func = $("#slc_operation option:selected").val();
		var filter = $("#slc_filter option:selected").val();
		var x = $("#txtparameter1").val();
		var y = $("#txtparameter2").val();
		var z = $("#txtparameter3").val();

		$.ajax({
			type: 'get',
			url: 'RainDBApi.php',
			contentType: 'text/xml',
			dataType:'text',
			data: { action: func, filter_name: filter, param1: x, param2: y, param3: z },
			success: function(output){
				$("#text").html(output);
			}		
		});			
	});
});