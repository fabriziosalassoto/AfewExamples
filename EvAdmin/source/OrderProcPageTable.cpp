//	==============================================================================
//	COrderProcPageTable - contacts table for table-driven query page
//	------------------------------------------------------------------------------
//	Copyright ©2001-2009 Evolution Computing, Inc.
//	All rights reserved
//	==============================================================================

#include "fwkw.h"
#include "EvAdmin.h"
#include "CTDQueryPage.h"

//	==============================================================================
//	Page Class factory
//	==============================================================================

//	DO_ORDER fields:
//	order_id cust_id date tax total_price 
//	status notes invoice flags

//	DO_ORDREQ fields:
//	req_id order_id date name company 
//	email phone notes flags status amount

//	DO_PAYMENT fields:
//	pmt_id pmt_type status ref_id ccname 
//	cctype ccnum ccmonth ccyear ccsec 
//	amount order_id

//	DO_PRODUCT fields:
//	prod_sku prodname prodclass skudesc price 
//	disc_class flags

//	DO_CONTACT fields:
//	cust_id date name company 
//	address address2 city state zip country
//	email phone type disc_group fwd_id 

//	DO_AKEY fields:
//	key_id ord_id date prod_sku name 
//	company serno akey flags

//	DO_ORDITEM
//	item_id order_id prod_sku quantity item_price 
//	discount ext_price oldsn flags

//	------------------------------------------------------------------------------

//	order_id date tax total_price pmtfee notes invoice status flags 

static UINT DG1Obj[] = {DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,
						DO_ORDREQ,DO_ORDREQ,DO_ORDREQ};

static UINT DG1FieldNr[] = {1,2,21,27,28,22,1,24,23};

static UINT DG1ColWid[] = {60,80,80,80,80,290,160,150,80};

static UINT DG1ColFlags[] = {DGCF_ARIGHT,DGCF_NONE,DGCF_ARIGHT,DGCF_ARIGHT,DGCF_NONE,
							 DGCF_NONE,DGCF_NONE,DGCF_NONE};

static QLINETABLE qline1 = {8,DG1Obj,DG1FieldNr,DG1ColWid,DG1ColFlags};

//	------------------------------------------------------------------------------

//	email phone product quantity price cdsku cdquantity shipping

static UINT DG2Obj[] = {DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,
						DO_ORDREQ,DO_ORDREQ,DO_ORDREQ};

static UINT DG2FieldNr[] = {11,12,25,15,14,17,18,20};

static UINT DG2ColWid[] = {280,200,100,60,80,100,60,80};

static UINT DG2ColFlags[] = {DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,
							 DGCF_NONE};

static QLINETABLE qline2 = {8,DG2Obj,DG2FieldNr,DG2ColWid,DG2ColFlags};

//	------------------------------------------------------------------------------

//	name company address address2 city state zip country

static UINT DG3Obj[] = {DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,
						DO_ORDREQ,DO_ORDREQ,DO_ORDREQ};

static UINT DG3FieldNr[] = {3,4,5,6,7,8,9,10};

static UINT DG3ColWid[] = {160,190,190, 150,160,100,100,100};

static UINT DG3ColFlags[] = {DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,
							 DGCF_NONE,DGCF_NONE,DGCF_NONE};

static QLINETABLE qline3 = {8,DG3Obj,DG3FieldNr,DG3ColWid,DG3ColFlags};

//	------------------------------------------------------------------------------
/*
//	amount pmt_type status 
//	ref_id ccname cctype ccnum ccmonth ccyear ccsec 

static UINT DG4Obj[] = {DO_PAYMENT,DO_PAYMENT,DO_PAYMENT,DO_PAYMENT,DO_PAYMENT,
						DO_PAYMENT,DO_PAYMENT,DO_PAYMENT,DO_PAYMENT,DO_PAYMENT};

static UINT DG4FieldNr[] = {11,2,3,4,5, 6,7,8,9,10};

static UINT DG4ColWid[] = {80,110,110,120,220, 100,200,70,70,60};

static UINT DG4ColFlags[] = {DGCF_NONE,DGCF_ARIGHT,DGCF_NONE,DGCF_NONE,DGCF_NONE,
							 DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE};

static QLINETABLE qline4 = {10,DG4Obj,DG4FieldNr,DG4ColWid,DG4ColFlags};

//	------------------------------------------------------------------------------

//	Display all order items for order

//	item_id order_id prod_sku quantity item_price 
//	discount ext_price oldsn flags

static UINT DG5Obj[] = {DO_ORDITEM,DO_ORDITEM,DO_ORDITEM,DO_ORDITEM,DO_ORDITEM,
						DO_ORDITEM,DO_ORDITEM,DO_ORDITEM,DO_ORDITEM
};
static UINT DG5FieldNr[] = {1,2,3,4,5, 6,7,8,9};

static UINT DG5ColWid[] = {80,80,80,100,200, 200,180,220,60};

static UINT DG5ColFlags[] = {DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,
							 DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE};

static QLINETABLE qline5 = {9,DG5Obj,DG5FieldNr,DG5ColWid,DG5ColFlags};
*/

//	------------------------------------------------------------------------------

//	Display all auth keys for order

//	key_id ord_id date prod_sku name company serno akey flags

static UINT DG6Obj[] = {DO_AKEY,DO_AKEY,DO_AKEY,DO_AKEY,DO_AKEY,DO_AKEY,
						DO_AKEY,DO_AKEY,DO_AKEY};
static UINT DG6FieldNr[] = {1,2,3,4,5, 6,7,8,9};

static UINT DG6ColWid[] = {80,80,80,100,200, 200,160,240,60};

static UINT DG6ColFlags[] = {DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,
							 DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE};

static QLINETABLE qline6 = {9,DG6Obj,DG6FieldNr,DG6ColWid,DG6ColFlags};

//	------------------------------------------------------------------------------

static QLINK* qlinks[] = {NULL};

//	------------------------------------------------------------------------------

static QLINETABLE* qlines[] = {&qline1,&qline2,&qline3,/*&qline4,&qline5,*/ &qline6,NULL};

//	------------------------------------------------------------------------------

IMObject* MakeOrderProcPage(IMObject* hWindow,IDBConnection* hDBConnection)
{
	IMObject* hPage = MakeTDQueryPage(hWindow,hDBConnection,
		(QLINETABLE**)&qlines,(QLINK**)&qlinks,RSC_OrderProcPageCfg,DO_ORDER,0,DGNOGROUP);
	return hPage; 
}

//	==============================================================================
