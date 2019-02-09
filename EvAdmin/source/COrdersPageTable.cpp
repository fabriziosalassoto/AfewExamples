//	==============================================================================
//	COrdersPageTable - contacts table for table-driven query page
//	------------------------------------------------------------------------------
//	Copyright ©2001-2015 Evolution Computing, Inc.
//	All rights reserved
//	==============================================================================

#include "fwkw.h"
#include "EvAdmin.h"
#include "CTDQueryPage.h"

//	==============================================================================
//	Page Class factory
//	==============================================================================

//	DO_ORDREQ fields:
//	req_id date name, company, address, address2, city, state, zip, country, 
//	email, phone, prod_sku, item_price, quantity, old_sn, 
//	cd_sku, cd_price, cd_quantity,
//	shipping, tax, notes, flags, status, proddesc, cddesc, total, pmtfee, txnid

//	------------------------------------------------------------------------------

//	order_id date tax total_price pmtfee notes invoice status flags 

static UINT DG1Obj[] = {DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,
						DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,DO_ORDREQ};

static UINT DG1FieldNr[] = {1,2,21,27,28,22,30,24,23};

static UINT DG1ColWid[] = {70,80,80,80,80,290,150,150,80};

static UINT DG1ColFlags[] = {DGCF_ARIGHT,DGCF_NONE,DGCF_ARIGHT,DGCF_ARIGHT,DGCF_NONE,
							 DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE};

static QLINETABLE qline1 = {9,DG1Obj,DG1FieldNr,DG1ColWid,DG1ColFlags};

//	------------------------------------------------------------------------------

//	email phone product quantity price cdsku cdquantity shipping

static UINT DG2Obj[] = {DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,
						DO_ORDREQ,DO_ORDREQ,DO_ORDREQ};

static UINT DG2FieldNr[] = {11,12,25,15,14,17,18,20};

static UINT DG2ColWid[] = {280,200,120,60,80,100,80,80};

static UINT DG2ColFlags[] = {DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,
							 DGCF_NONE};

static QLINETABLE qline2 = {8,DG2Obj,DG2FieldNr,DG2ColWid,DG2ColFlags};

//	------------------------------------------------------------------------------

//	name company address address2 city state zip country

static UINT DG3Obj[] = {DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,
						DO_ORDREQ,DO_ORDREQ,DO_ORDREQ};

static UINT DG3FieldNr[] = {3,4,5,6,7,8,9,10};

static UINT DG3ColWid[] = {160,190,190, 150,160,80,100,80};

static UINT DG3ColFlags[] = {DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,
							 DGCF_NONE,DGCF_NONE,DGCF_NONE};

static QLINETABLE qline3 = {8,DG3Obj,DG3FieldNr,DG3ColWid,DG3ColFlags};

//	------------------------------------------------------------------------------

//	req_id date total name company email phone status

static UINT DG4Obj[] = {DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,
						DO_ORDREQ,DO_ORDREQ,DO_ORDREQ,DO_ORDREQ};

static UINT DG4FieldNr[] = {1,2,27,3,4, 10,11,24,23};

static UINT DG4ColWid[] = {60,80,80,210,210, 160,160,100,40};

static UINT DG4ColFlags[] = {DGCF_RO,DGCF_NONE,DGCF_ARIGHT,DGCF_NONE,
							 DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,
							 DGCF_NONE};

static QLINETABLE qline4 = {9,DG4Obj,DG4FieldNr,DG4ColWid,DG4ColFlags};

//	------------------------------------------------------------------------------

static QLINETABLE* qlines[] = {&qline1,&qline2,&qline3,&qline4,NULL};

//	there are no linked data objects needed for this page (everything is DO_ORDREQ)
static QLINK* qlinks = {NULL};

//	------------------------------------------------------------------------------

//	selector uses same layout as order request query

IControl* MakeOrdersPage(IWindow* hWindow,IDBConnection* hDBConnection)
{
	IControl* hPage = MakeTDQueryPage(hWindow,hDBConnection,
		(QLINETABLE**)&qlines,(QLINK**)&qlinks,RSC_OrdersPageCfg,DO_ORDREQ,0,DGNOGROUP);
	return hPage; 
}

//	==============================================================================
