//	==============================================================================
//	CContactsPageTable - contacts table for table-driven query page
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

//	DO_CONTACT fields:
//	cust_id date name company 
//	address address2 city state zip country
//	email phone type disc_group fwd_id 

//	------------------------------------------------------------------------------

//	cust_id date email phone type disc_group fwd_id 

static UINT DG1Obj[] = {DO_CONTACT,DO_CONTACT,DO_CONTACT,DO_CONTACT,DO_CONTACT,
						DO_CONTACT,DO_CONTACT};

static UINT DG1FieldNr[] = {1,2,11,12,13,14,15};

static UINT DG1ColWid[] = {90,90,280,200,100,110,60};

static UINT DG1ColFlags[] = {DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,
							 DGCF_NONE,DGCF_NONE};

static QLINETABLE qline1 = {7,DG1Obj,DG1FieldNr,DG1ColWid,DG1ColFlags};

//	------------------------------------------------------------------------------

//	name company address address2 city state zip country

static UINT DG2Obj[] = {DO_CONTACT,DO_CONTACT,DO_CONTACT,DO_CONTACT,DO_CONTACT,
						DO_CONTACT,DO_CONTACT,DO_CONTACT};

static UINT DG2FieldNr[] = {3,4,5, 6,7,8,9,10};

static UINT DG2ColWid[] = {160,190,190, 150,160,100,100,100};

static UINT DG2ColFlags[] = {DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,
							 DGCF_NONE,DGCF_NONE,DGCF_NONE};

static QLINETABLE qline2 = {8,DG2Obj,DG2FieldNr,DG2ColWid,DG2ColFlags};

//	------------------------------------------------------------------------------

//	cust_id email phone name company city state zip country

static UINT DG3Obj[] = {DO_CONTACT,DO_CONTACT,DO_CONTACT,DO_CONTACT,DO_CONTACT,
						DO_CONTACT,DO_CONTACT,DO_CONTACT,DO_CONTACT};

static UINT DG3FieldNr[] = {1,11,12,3,4, 7,8,9,10};

static UINT DG3ColWid[] = {50,200,180,160,190, 130,50,80,90};

static UINT DG3ColFlags[] = {DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,
							 DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE};

static QLINETABLE qline3 = {9,DG3Obj,DG3FieldNr,DG3ColWid,DG3ColFlags};

//	------------------------------------------------------------------------------

//	there are no linked data objects needed for this page (everything is DO_CONTACT)
static QLINK* qlinks = {NULL};

static QLINETABLE* qlines[] = {&qline1,&qline2,&qline3,NULL};

IControl* MakeContactsPage(IWindow* hWindow,IDBConnection* hDBConnection)
{
	IControl* hPage = MakeTDQueryPage(hWindow,hDBConnection,
		(QLINETABLE**)&qlines,(QLINK**)&qlinks,RSC_ContactsPageCfg,DO_CONTACT,0,DGNOGROUP);
	return hPage; 
}

//	==============================================================================
