//	==============================================================================
//	CProductsPageTable - products table for table-driven query page
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

//	DO_PRODUCT fields:
//	prod_sku prodname prodclass skudesc price 
//	disc_class flags

//	1st single-row query datagrid specification

//	prod_sku prodname prodclass skudesc price disc_class flags

static UINT DG1Obj[] = {DO_PRODUCT,DO_PRODUCT,DO_PRODUCT,DO_PRODUCT,DO_PRODUCT,
						DO_PRODUCT,DO_PRODUCT};
static UINT DG1FieldNr[] = {1,2,3,4,5, 6,7};

static UINT DG1ColWid[] = {110,110,80,320,80, 100,80};

static UINT DG1ColFlags[] = {DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,DGCF_NONE,
							 DGCF_NONE,DGCF_NONE};

static QLINETABLE qline1 = {7,DG1Obj,DG1FieldNr,DG1ColWid,DG1ColFlags};

//	there are no linked data objects needed for this page (everything is DO_CONTACT)
static QLINK* qlinks = {NULL};

//	selector dgrid is same as 1st query dgrid, so we reuse its specification

static QLINETABLE* qlines[] = {&qline1,&qline1,NULL};

IControl* MakeProductsPage(IWindow* hWindow,IDBConnection* hDBConnection)
{
	IControl* hPage = MakeTDQueryPage(hWindow,hDBConnection,
		(QLINETABLE**)&qlines,(QLINK**)&qlinks,RSC_ProductsPageCfg,DO_PRODUCT,0,2);
	return hPage; 
}

//	==============================================================================
