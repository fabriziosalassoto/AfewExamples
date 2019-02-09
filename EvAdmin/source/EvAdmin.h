//	==============================================================================
//	EvAdmin.h - EVA database Administration utility program
//	------------------------------------------------------------------------------
//	Copyright ©2009 Evolution Computing, Inc.
//	All rights reserved
//	==============================================================================

#define EVADMIN_MENU	9
#define EVADMIN_XPID	0x1009

extern UINT	RscBase;

IMObject* MakeDlgLogon(IMObject* hNotify);
IMObject* MakeDbAccess(IMObject* hNotify);

#define MSG_DlgLogonNotify		MSGC_USER+1		// callback from logon dialog

//#define MSG_EvaDbConnect		MSGC_USER+101	// connect to database
#define MSG_EvaDBConnectErr		MSGC_USER+102	// database connection error
#define MSG_EvaDBConnectOk		MSGC_USER+103	// database connection successful
#define MSG_EvaDBErr			MSGC_USER+104	// error in SQL operation

#define MSG_EvaSwitchPage		MSGC_USER+200	// switch displayed page
#define MSG_EvaSwitchAndFind	MSGC_USER+201 	// switch displayed page and find record
#define MSG_EvaFindRecord		MSGC_USER+202	// show record with specified primary key

#define MSG_EvaEMailSendNotify	MSGC_USER+301	// error/ok sending email

//	==============================================================================
//	Multi=parameter messages structures
//	==============================================================================

struct MSGS_DlgLogonNotify{IStr* hUName;IStr* hPassword;};
struct MSGS_EvaSwitchAndFind{UINT kDO;WCHAR* pText;};
struct MSGS_EvaFindRecord{UINT ixFindDO;WCHAR* pPrimaryKey;};
struct MSGS_EvaDBErr{WCHAR* pErrTitle;WCHAR* pErrStg;};

//	==============================================================================
//	Eva administration interface object
//	==============================================================================

class IEvAdmin : public IMObject
{
public:
	inline void DlgLogonNotify(IStr* hUName,IStr* hPassword)
	{
		MSGS_DlgLogonNotify msg = {hUName,hPassword};
		Msg(MSG_DlgLogonNotify,&msg);
	}
	 
	inline void EvaDBConnectErr(WCHAR* pError)
	{Msg(MSG_EvaDBConnectErr,pError);}

	inline void EvaDBConnectOk(IDBConnection* hDBConnection)
	{Msg(MSG_EvaDBConnectOk,hDBConnection);}

	inline void EvaDBErr(WCHAR* pErrTitle,WCHAR* pErrStg)
	{
		MSGS_EvaDBErr msg = {pErrTitle,pErrStg};
		Msg(MSG_EvaDBErr,&msg);
	}

	inline IControl* EvaSwitchPage(UINT idPage)
	{return MRTNTYPE(IControl*)Msg(MSG_EvaSwitchPage,VOIDP idPage);}

	inline void EvaSwitchAndFind(UINT kDO,WCHAR* pText)
	{
		MSGS_EvaSwitchAndFind msg = {kDO,pText};
		Msg(MSG_EvaSwitchAndFind,&msg);
	}

	inline void EvaFindRecord(UINT ixFindDO,WCHAR* pPrimaryKey)
	{
		MSGS_EvaFindRecord msg = {ixFindDO,pPrimaryKey};
		Msg(MSG_EvaFindRecord,&msg);
	}

	inline void EvaEMailSendNotify(WCHAR* pErrorMsg)
	{Msg(MSG_EvaEMailSendNotify,pErrorMsg);}
};

//	==============================================================================
//	Resource identifiers
//	==============================================================================

//	images
#define RSCIMG_CompanyLogo		RscBase+1	// company logo image
#define RSCIMG_Need				RscBase+2
#define RSCIMG_Ok				RscBase+3

//	dialog templates
#define RSC_DlgAboutEvAdmin		RscBase+101	// ABOUT dialog template
#define RSC_DlgLogon			RscBase+102	// LOGON dialog template

#define RSC_ProductsPageCfg		RscBase+150	// products page
#define RSC_OrdersPageCfg		RscBase+151	// orders page
#define RSC_UtilitiesPageCfg	RscBase+152	// utilities page
#define RSC_ContactsPageCfg		RscBase+153	// contacts page
#define RSC_AuthKeysPageCfg		RscBase+154	// authorization keys page
#define RSC_PaymentsPageCfg		RscBase+155	// payments page
#define RSC_OrderProcPageCfg	RscBase+156	// order processing page
#define RSC_ControlsPageCfg		RscBase+157	// controls page
#define RSC_DiscountsPageCfg	RscBase+158	// discounts page

//	text prompts
#define RSC_EvAdminVersion		RscBase+201
#define RSC_DBHost				RscBase+202
#define RSC_DBName				RscBase+203
#define RSC_DBConnectionErr		RscBase+204
#define RSC_DBProductUpdateErr	RscBase+205
#define RSC_BackupFileName		RscBase+206
#define RSC_RestoreErr			RscBase+207
#define RSC_BackupFileErr		RscBase+208

//	email keys message
#define RSC_EMKeysPrefix		RscBase+301
#define RSC_EMKeysData			RscBase+302
#define RSC_EMKeysSuffix		RscBase+303

//	==============================================================================
