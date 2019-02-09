//	==============================================================================
//	CCmdRestoreDB.cpp - Restore the eva database from backups (DANGEROUS!)
//	------------------------------------------------------------------------------
//	Copyright ©2009-2015 Evolution Computing, Inc.
//	All rights reserved
//	==============================================================================

#include "fwkw.h"
#include "EvAdmin.h"

IDBRow* MakeProductDO(void);
IDBRow* MakeContactDO(void);
IDBRow* MakeAuthKeyDO(void);
IDBRow* MakeOrderReqDO(void);
IDBRow* MakeDiscountDO(void);
IDBRow* MakeControlDO(void);

//	==============================================================================
//	Restore the eva database (DANGEROUS!)
//	==============================================================================

void* CCmdRestoreDB(IDBConnection* hDBConnection)
{
	IDBRow* hDO;

	IStr* hFile = hSvc->SvcLoadTextFile(RscText(RSC_BackupFileName));
	if(hFile)
	{
		hFile->StrScanFreeForm(true);	// CR and LF are considered whitespace

		//	products table
		hDO = MakeProductDO();
		if(!hDBConnection->DBaseORestore(hDO,hFile))
		{
			((IEvAdmin*)hApplication)->EvaDBErr(RscText(RSC_RestoreErr),
				hDBConnection->DBaseErrorStg());
			return IM_RTN_NOTHING;
		}
		hDO->Release();

		//	contacts table
		hDO = MakeContactDO();
		if(!hDBConnection->DBaseORestore(hDO,hFile))
		{
			((IEvAdmin*)hApplication)->EvaDBErr(RscText(RSC_RestoreErr),
				hDBConnection->DBaseErrorStg());
			return IM_RTN_NOTHING;
		}
		hDO->Release();

		//	order requests table
		hDO = MakeOrderReqDO();
		if(!hDBConnection->DBaseORestore(hDO,hFile))
		{
			((IEvAdmin*)hApplication)->EvaDBErr(RscText(RSC_RestoreErr),
				hDBConnection->DBaseErrorStg());
			return IM_RTN_NOTHING;
		}
		hDO->Release();

		//	authorization keys table
		hDO = MakeAuthKeyDO();
		if(!hDBConnection->DBaseORestore(hDO,hFile))
		{
			((IEvAdmin*)hApplication)->EvaDBErr(RscText(RSC_RestoreErr),
				hDBConnection->DBaseErrorStg());
			return IM_RTN_NOTHING;
		}
		hDO->Release();

		//	control table
		hDO = MakeControlDO();
		if(!hDBConnection->DBaseORestore(hDO,hFile))
		{
			((IEvAdmin*)hApplication)->EvaDBErr(RscText(RSC_RestoreErr),
				hDBConnection->DBaseErrorStg());
			return IM_RTN_NOTHING;
		}
		hDO->Release();

		//	discounts table
		hDO = MakeDiscountDO();
		if(!hDBConnection->DBaseORestore(hDO,hFile))
		{
			((IEvAdmin*)hApplication)->EvaDBErr(RscText(RSC_RestoreErr),
				hDBConnection->DBaseErrorStg());
			return IM_RTN_NOTHING;
		}
		hDO->Release();

	}
	else
		((IEvAdmin*)hApplication)->EvaDBErr(RscText(RSC_RestoreErr),
			RscText(RSC_BackupFileErr));

	hFile->Release();
	return IM_RTN_NOTHING;
}

//	==============================================================================
