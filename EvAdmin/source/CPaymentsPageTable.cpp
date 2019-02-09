//	==============================================================================
//	CPaymentsPageTable - payments table for table-driven query page
//	------------------------------------------------------------------------------
//	Copyright ©2001-2009 Evolution Computing, Inc.
//	All rights reserved
//	==============================================================================

#include "fwkw.h"
#include "EvAdmin.h"
#include "CTDQueryPage.h"

//	NOT Currently in use by EvAdmin program...

//	==============================================================================
//	Page Class factory
//	==============================================================================

//	DO_PAYMENT fields:
//	pmt_id pmt_type status ref_id ccname 
//	cctype ccnum ccmonth ccyear ccsec 
//	amount order_id

//	------------------------------------------------------------------------------

//	pmt_id ord_id pmt_type amount status 
//	ref_id ccname cctype ccnum ccmonth ccyear ccsec 

//	1st single-row query datagrid specification

static UINT DG1Obj[] = {DO_PAYMENT,DO_PAYMENT,DO_PAYMENT,DO_PAYMENT,DO_PAYMENT,
						DO_PAYMENT,DO_PAYMENT,DO_PAYMENT,DO_PAYMENT,DO_PAYMENT,
						DO_PAYMENT,DO_PAYMENT,DO_PAYMENT};
static UINT DG1FieldNr[] = {1,12,11,2,3, 4,5,6,7,8, 9,10,13};

static UINT DG1ColWid[] = {60,60,60,60,100, 120,210,80,180,70, 60,50,70};

static UINT DG1ColFlags[] = {DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,
							 DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,
							 DGCF_NONE,DGCF_NONE,DGCF_NONE};

static QLINETABLE qline1 = {13,DG1Obj,DG1FieldNr,DG1ColWid,DG1ColFlags};

//	there are no linked data objects needed for this page (everything is DO_PAYMENT)
static QLINK* qlinks = {NULL};

//	selector dgrid is same as 1st query dgrid, so we reuse its specification

static QLINETABLE* qlines[] = {&qline1,&qline1,NULL};

IMObject* MakePaymentsPage(IMObject* hWindow,IDBConnection* hDBConnection)
{
	IMObject* hPage = MakeTDQueryPage(hWindow,hDBConnection,
		(QLINETABLE**)&qlines,(QLINK**)&qlinks,RSC_PaymentsPageCfg,DO_PAYMENT,0,DGNOGROUP);
	return hPage; 
}

//	==============================================================================
