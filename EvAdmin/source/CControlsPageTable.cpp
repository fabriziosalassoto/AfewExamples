//	==============================================================================
//	CControlsPageTable - controls table for table-driven query page
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

//	DO_CTRL fields:
//	ctl_id aztax

//	------------------------------------------------------------------------------

//	1st single-row query datagrid specification

static UINT DG1Obj[] = {DO_CTRL,DO_CTRL};
static UINT DG1FieldNr[] = {1,2};

static UINT DG1ColWid[] = {60,80};

static UINT DG1ColFlags[] = {DGCF_NONE,DGCF_NONE};

static QLINETABLE qline1 = {2,DG1Obj,DG1FieldNr,DG1ColWid,DG1ColFlags};

//	there are no linked data objects needed for this page (everything is DO_PAYMENT)
static QLINK* qlinks = {NULL};

//	selector dgrid is same as 1st query dgrid, so we reuse its specification

static QLINETABLE* qlines[] = {&qline1,&qline1,NULL};

IControl* MakeControlsPage(IWindow* hWindow,IDBConnection* hDBConnection)
{
	IControl* hPage = MakeTDQueryPage(hWindow,hDBConnection,
		(QLINETABLE**)&qlines,(QLINK**)&qlinks,RSC_ControlsPageCfg,DO_CTRL,0,DGNOGROUP);
	return hPage; 
}

//	==============================================================================
