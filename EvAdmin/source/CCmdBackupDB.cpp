//	==============================================================================
//	CCmdBackupDB.cpp - Backup the eva database to backups (DANGEROUS!)
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
//	Backup the eva database (DANGEROUS!)
//	==============================================================================

void* CCmdBackupDB(IDBConnection* hDBConnection)
{
	IDBRow* hDO;

	IStr* hFile = MakeStr(TEXT("<?xml> version \"1.0\" encoding=\"UTF-8\" ?>\r\n"));
	hFile->StrAppendW(TEXT("<database name=\""));
	hFile->StrAppendW(RscText(RSC_DBName));
	hFile->StrAppendW(TEXT("\">\r\n"));

	//	products table
	hDO = MakeProductDO();
	if(!hDBConnection->DBaseOBackup(hDO,hFile))
	{
		((IEvAdmin*)hApplication)->EvaDBErr(RscText(RSC_BackupFileErr),
			hDBConnection->DBaseErrorStg());
		return IM_RTN_NOTHING;
	}
	hDO->Release();

	//	contacts table
	hDO = MakeContactDO();
	if(!hDBConnection->DBaseOBackup(hDO,hFile))
	{
		((IEvAdmin*)hApplication)->EvaDBErr(RscText(RSC_BackupFileErr),
			hDBConnection->DBaseErrorStg());
		return IM_RTN_NOTHING;
	}
	hDO->Release();

	//	order request table
	hDO = MakeOrderReqDO();
	if(!hDBConnection->DBaseOBackup(hDO,hFile))
	{
		((IEvAdmin*)hApplication)->EvaDBErr(RscText(RSC_BackupFileErr),
			hDBConnection->DBaseErrorStg());
		return IM_RTN_NOTHING;
	}
	hDO->Release();

	//	authorizations table
	hDO = MakeAuthKeyDO();
	if(!hDBConnection->DBaseOBackup(hDO,hFile))
	{
		((IEvAdmin*)hApplication)->EvaDBErr(RscText(RSC_BackupFileErr),
			hDBConnection->DBaseErrorStg());
		return IM_RTN_NOTHING;
	}
	hDO->Release();

	//	contacts table
	hDO = MakeControlDO();
	if(!hDBConnection->DBaseOBackup(hDO,hFile))
	{
		((IEvAdmin*)hApplication)->EvaDBErr(RscText(RSC_BackupFileErr),
			hDBConnection->DBaseErrorStg());
		return IM_RTN_NOTHING;
	}
	hDO->Release();

	//	discounts table
	hDO = MakeDiscountDO();
	if(!hDBConnection->DBaseOBackup(hDO,hFile))
	{
		((IEvAdmin*)hApplication)->EvaDBErr(RscText(RSC_BackupFileErr),
			hDBConnection->DBaseErrorStg());
		return IM_RTN_NOTHING;
	}
	hDO->Release();

	hFile->StrAppendW(TEXT("</database>\r\n"));
	hSvc->SvcSaveTextFile(RscText(RSC_BackupFileName),hFile);
	hFile->Release();
	return IM_RTN_NOTHING;
}

//	==============================================================================
