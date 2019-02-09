// JavaScript Document

$(document).ready(function(){	
	"use strict";
	$("#slc_method").on("change",function(){
		$("#txtresponse").html("");
		GetExampleXML($("#slc_method option:selected").val());					
	});	
	
	function GetExampleXML(request){
		var xml_request="";		
		
		switch(request){
			case "blank" : xml_request = ""; 
						   $("#hdnRequestType").val("");
						   break;
			case "SendQuoteRequestWithExample" : xml_request = "xmls/quoterequest.xml"; 
												 $("#hdnRequestType").val("SendQuote");
												 break;
			case "SendQuoteRequestOwn" : xml_request = ""; 
										 $("#hdnRequestType").val("SendQuote");
										 break;	 								   												 
			case "SendBookingRequestWithExample" : xml_request = "xmls/bookingrequestcc.xml"; 
												   $("#hdnRequestType").val("SendBooking");
												   break;
			case "SendBookingRequestOwn" : xml_request = ""; 
												   $("#hdnRequestType").val("SendBooking");
												   break;												   
			case "SendBookingIndexRequestWithExample" : xml_request = "xmls/BookingIndexRequest.xml"; 
													    $("#hdnRequestType").val("SendBookingIndexRequest");														
														break;
			case "SendBookingIndexRequestOwn" : xml_request = ""; 
											    $("#hdnRequestType").val("SendBookingIndexRequest");														
												break;														
		}  				
				
		$.ajax({
			type: 'get',
			url: xml_request,
			contentType: 'text/xml',
			dataType:'text',
			success: function(output){
				$("#txtrequest").html(output);
			}		
		});					
	}
	

	$("#btnQuery").click(function(){
		var func = $("#slc_operation option:selected").val();
		var XmlRequest = $("#txtrequest").val();
		
		$.ajax({
			type: 'get',
			url: 'LacureBookingAPI.php',
			contentType: 'text/xml',
			dataType:'text',
			data: { action: $("#hdnRequestType").val(), request: XmlRequest },
			success: function(output){
				$("#txtresponse").html(output);
			}		
		});			
	});
});