//	==============================================================================
//	CAuthKeysPageTable - Authorization Keys table for table-driven query page
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

//	DO_AKEY fields:
//	key_id ord_id date prod_sku name 
//	company serno akey flags

//	DO_CONTACT fields:
//	cust_id date name company 
//	address address2 city state zip country
//	email phone type disc_group fwd_id 

//	------------------------------------------------------------------------------
//	ord_id date prod_sku name company serno akey

static UINT DG1Obj[] = {DO_AKEY,DO_AKEY,DO_AKEY,DO_AKEY,DO_AKEY,DO_AKEY,
						DO_AKEY,DO_AKEY,DO_AKEY};
static UINT DG1FieldNr[] = {1,2,3,4,5, 6,7,8,9};

static UINT DG1ColWid[] = {80,80,80,100,200, 200,160,240,60};

static UINT DG1ColFlags[] = {DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,
							 DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE};

static QLINETABLE qline1 = {9,DG1Obj,DG1FieldNr,DG1ColWid,DG1ColFlags};

//	------------------------------------------------------------------------------

//	cust_id date email phone type disc_group fwd_id 

static UINT DG2Obj[] = {DO_CONTACT,DO_CONTACT,DO_CONTACT,DO_CONTACT,DO_CONTACT,
						DO_CONTACT,DO_CONTACT};

static UINT DG2FieldNr[] = {1,2,11,12,13,14,15};

static UINT DG2ColWid[] = {90,90,280,200,100,110,60};

static UINT DG2ColFlags[] = {DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,
							 DGCF_NONE,DGCF_NONE};

static QLINETABLE qline2 = {7,DG2Obj,DG2FieldNr,DG2ColWid,DG2ColFlags};

//	------------------------------------------------------------------------------

//	name company address address2 city state zip country

static UINT DG3Obj[] = {DO_CONTACT,DO_CONTACT,DO_CONTACT,DO_CONTACT,DO_CONTACT,
						DO_CONTACT,DO_CONTACT,DO_CONTACT};

static UINT DG3FieldNr[] = {3,4,5, 6,7,8,9,10};

static UINT DG3ColWid[] = {160,190,190, 150,160,100,100,100};

static UINT DG3ColFlags[] = {DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,
							 DGCF_NONE,DGCF_NONE,DGCF_NONE};

static QLINETABLE qline3 = {8,DG3Obj,DG3FieldNr,DG3ColWid,DG3ColFlags};

//	------------------------------------------------------------------------------

static QLINK qlink1 = {DO_AKEY,10,DO_CONTACT,1};
static QLINK* qlinks[] = {&qlink1,NULL};

//	selector dgrid is same as 1st query dgrid, so we reuse its specification

static QLINETABLE* qlines[] = {&qline1,&qline2,&qline3,&qline1,NULL};

IControl* MakeAuthKeysPage(IWindow* hWindow,IDBConnection* hDBConnection)
{
	IControl* hPage = MakeTDQueryPage(hWindow,hDBConnection,
		(QLINETABLE**)&qlines,(QLINK**)&qlinks,RSC_AuthKeysPageCfg,DO_AKEY,0,DGNOGROUP);
	return hPage; 
}

//	==============================================================================
