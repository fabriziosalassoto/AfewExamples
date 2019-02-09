//	==============================================================================
//	CTDQueryPage.h - standard table-driven query page object
//	------------------------------------------------------------------------------
//	Copyright ©2001-2009 Evolution Computing, Inc.
//	All rights reserved
//	==============================================================================
#ifndef _CTDQueryPage_H
#define _CTDQueryPage_H

#define DO_PRODUCT		0
#define DO_CONTACT		1
#define DO_AKEY			2
#define DO_ORDREQ		3
#define DO_DISCOUNT		4
#define DO_CTRL			5
#define DO_UTILITIES	100
#define DO_TEST			101
#define DO_MAIN			102

#define DGCF_NONE		0	// no column flags
#define DGCF_RO			1	// column is read-only
#define DGCF_ARIGHT		2	// column is right-aligned
#define DGCF_BOLD		4	// column is bold text

struct QLINETABLE
{
	UINT	nrCols;		// number of columns in this query datagrid
	UINT*	pDOIdx;		// index DG column to type of data object  DO_*
	UINT*	pFieldNr;	// index DG column to DO field number
	UINT*	pColWid;	// column width table
	UINT*	pColFlags;	// table of flags for each DG column
};

struct QLINK
{
	UINT	ixDODef;	// this data object's
	UINT	ixDOField;	// field number is
	UINT	ixDOFind;	// the key to find this matching DO
	UINT	ixMatchField; // match this field in target. 0=primary key
};

IControl* MakeTDQueryPage(IWindow* hWindow,IDBConnection* hDBConnection,
			QLINETABLE** ppPageDefTable,QLINK** ppQLinkTable,
			UINT nRscPageCfg,UINT ixSelectorDO,int ixColPrimary,int nGroupField);

#define DGNOGROUP	-1

#endif
//	==============================================================================