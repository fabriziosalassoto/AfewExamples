//	==============================================================================
//	CCmdCreateDB.cpp - Re-Create the eva database (DANGEROUS!)
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
//	Re-Create the eva database (DANGEROUS!)
//	==============================================================================

void* CCmdCreateDB(IDBConnection* hDBConnection)
{
	IDBRow* hDO;

	//	products table
	hDO = MakeProductDO();
	hDBConnection->DBaseODeleteTable(hDO);
	if(!hDBConnection->DBaseOCreateTable(hDO))
	{
		((IEvAdmin*)hApplication)->EvaDBErr(TEXT("Error creating Products table"),
			hDBConnection->DBaseErrorStg());
		return IM_RTN_NOTHING;
	}
	hDO->Release();

	//	contacts table
	hDO = MakeContactDO();
	hDBConnection->DBaseODeleteTable(hDO);
	if(!hDBConnection->DBaseOCreateTable(hDO))
	{
		((IEvAdmin*)hApplication)->EvaDBErr(TEXT("Error creating Contacts table"),
			hDBConnection->DBaseErrorStg());
		return IM_RTN_NOTHING;
	}
	hDO->Release();

	//	order requests table
	hDO = MakeOrderReqDO();
	hDBConnection->DBaseODeleteTable(hDO);
	if(!hDBConnection->DBaseOCreateTable(hDO))
	{
		((IEvAdmin*)hApplication)->EvaDBErr(TEXT("Error creating Order Requests table"),
			hDBConnection->DBaseErrorStg());
		return IM_RTN_NOTHING;
	}
	hDO->Release();

	//	auth key table
	hDO = MakeAuthKeyDO();
	hDBConnection->DBaseODeleteTable(hDO);
	if(!hDBConnection->DBaseOCreateTable(hDO))
	{
		((IEvAdmin*)hApplication)->EvaDBErr(TEXT("Error creating AuthKeys table"),
			hDBConnection->DBaseErrorStg());
		return IM_RTN_NOTHING;
	}
	hDO->Release();

	//	control table
	hDO = MakeControlDO();
	hDBConnection->DBaseODeleteTable(hDO);
	if(!hDBConnection->DBaseOCreateTable(hDO))
	{
		((IEvAdmin*)hApplication)->EvaDBErr(TEXT("Error creating Control table"),
			hDBConnection->DBaseErrorStg());
		return IM_RTN_NOTHING;
	}
	hDO->Release();

	//	orders table
	hDO = MakeDiscountDO();
	hDBConnection->DBaseODeleteTable(hDO);
	if(!hDBConnection->DBaseOCreateTable(hDO))
	{
		((IEvAdmin*)hApplication)->EvaDBErr(TEXT("Error creating Discounts table"),
			hDBConnection->DBaseErrorStg());
		return IM_RTN_NOTHING;
	}
	hDO->Release();

	return IM_RTN_NOTHING;
}

//	==============================================================================
