XPID 0x1009 // EvaAdmin Resources

//	text strings

*201,EvAdmin version 0.02
*202,127.0.0.1
//*202,domainname.com
*203,eva
*204,Database Connection Error
*205,Product update error
*206,#evbackup.xml
*207,Restore Tables Error
*208,Unable to open backup file


//	email form strings
MLTEXT 301
Thank you for purchasing from us. We appreciate and value your
business and will do our best to satisfy you.

When you start your program, the authorization dialog will appear.
Please click on the "Enter Authorization Code" button.
Enter the following items in the "Enter Authorization Data" dialog.

*

MLTEXT 302
Product license %5:
 
Enter the following items in the "Enter Authorization Data" dialog:
 
Name: "%01"
Company: "%02"
Serial number: "%03"
Authorization key: "%04"

*

MLTEXT 303 
=== IMPORTANT! =======================================================
 
Please save this information as you WILL need it if you choose to move
your program to a different computer, or to make a full reinstall later
for any reason.

You may download the current update or full install at any time from:
%01

If you have any questions or difficulties with installation,
please email us at:
 
support@fastcad.com

or call us at 480-967-8633 9 A.M.-5 P.M. Mountain Standard Time.

We will do our best to get you up and running quickly. Please check
our web site regularly, as we frequently post free updates
containing new features and improvements as well as bug fixes.

Michael Riddle
CEO, Evolution Computing
*

//	images

IMAGE 1,"logo.png"
IMAGE 2,"need.png"
IMAGE 3,"ok.png"

//	dialogs

MLTEXT 101
DIALOG,800,336,200,"About EvAdmin",MODAL
{
BUTTON,500,PARENT,20,44,64,64,0,NOFRAME,"","",NONE
LABEL,510,PARENT,100,44,220,20,0,"EvComp Database Administration",NONE
LABEL,511,PARENT,100,64,220,20,0,"©2010 Evolution Computing Inc.",NONE
LABEL,512,PARENT,100,84,220,24,0,"All rights reserved",NONE
LABEL,513,PARENT,100,104,220,24,0,"Version: 1.05",NONE
BUTTON,598,PARENT,132,136,60,24,0,BTN3D,"Ok","",ENDDLG
}
END
*

MLTEXT 102
DIALOG,500,420,174,"Logon",MODAL
{
BUTTON,500,PARENT,20,42,64,64,0,NOFRAME,"","",NONE
LABEL,-1,PARENT,100,44,50,24,0,"Name:",NONE
EDIT,501,PARENT,180,44,200,24,0,"username"
LABEL,-1,PARENT,100,80,50,24,0,"Password:",NONE
EDIT,502,PARENT,180,80,200,24,0,"password"
BUTTON,598,PARENT,180,118,60,24,0,BTN3D,"Ok","",ENDDLG
}
END
*

//	Products Page
MLTEXT 150
LABEL,600,PARENT,14,6,200,24,0,"Evolution Computing Products:",NONE
LABEL,-1,PARENT,220,6,50,24,0,"Status:",NONE
LABEL,614,PARENT,280,6,-20,24,0,"",FRAME
DATAGRID,610,PARENT,20,40,-20,50,0
BUTTON,611,PARENT,20,90,60,24,0,BTN3D,"Clear","",NONE
BUTTON,612,PARENT,100,90,90,24,0,BTN3D,"Find Records","",NONE
BUTTON,613,PARENT,210,90,110,24,0,BTN3D,"Add New Record","",NONE
BUTTON,615,PARENT,340,90,100,24,0,BTN3D,"Update Record","",NONE
BUTTON,616,PARENT,460,90,100,24,0,BTN3D,"Delete Record","",NONE
BUTTON,618,PARENT,-320,90,-200,24,0,BTN3D,"Show Open Orders","",NONE
BUTTON,619,PARENT,-180,90,-20,24,0,BTN3D,"Show Completed Orders","",NONE
LABEL,-1,PARENT,20,128,-20,4,0,"",FRAME
DATAGRID,620,PARENT,20,146,-20,-20,0
END
*

//	Order Requests Page
MLTEXT 151
LABEL,600,PARENT,14,6,240,24,0,"Evolution Computing Open Orders:",NONE
LABEL,-1,PARENT,244,6,50,24,0,"Status:",NONE
LABEL,614,PARENT,300,6,-20,24,0,"",FRAME
DATAGRID,610,PARENT,20,40,-20,50,0
DATAGRID,620,PARENT,20,94,-20,50,0
DATAGRID,630,PARENT,20,148,-20,50,0
BUTTON,611,PARENT,20,202,60,24,0,BTN3D,"Clear","",NONE
BUTTON,612,PARENT,100,202,90,24,0,BTN3D,"Find Records","",NONE
BUTTON,680,PARENT,210,202,100,24,0,BTN3D,"Mark Paid","",NONE
BUTTON,681,PARENT,330,202,100,24,0,BTN3D,"EMail Keys","",NONE
BUTTON,682,PARENT,450,202,100,24,0,BTN3D,"Mark Shipped","",NONE
BUTTON,683,PARENT,570,202,100,24,0,BTN3D,"Mark Invoiced","",NONE
BUTTON,615,PARENT,690,202,100,24,0,BTN3D,"Update Order","",NONE
BUTTON,616,PARENT,810,202,100,24,0,BTN3D,"Delete Order","",NONE
BUTTON,618,PARENT,-320,202,-200,24,0,BTN3D,"Show Open Orders","",NONE
BUTTON,619,PARENT,-180,202,-20,24,0,BTN3D,"Show Completed Orders","",NONE
LABEL,-1,PARENT,20,240,-20,4,0,"",FRAME
DATAGRID,640,PARENT,20,258,-20,-20,0
END
*

//	Utilities Page
MLTEXT 152
LABEL,600,PARENT,13,8,400,20,0,"Evolution Computing Database Utilities",NONE
BUTTON,610,PARENT,-220,10,-20,24,0,SWITCH,"Enable Dangerous Operations","Disable Dangerous Operations",NONE
LABEL,611,PARENT,3,34,-2,-2,0,"1",GROUP
BUTTON,620,PARENT,20,60,130,24,0,BTN3D,"Create Database","",NONE
LABEL,-1,PARENT,160,60,400,20,0,"CAUTION! This deletes everything and creates a new empty database!",NONE
BUTTON,630,PARENT,20,94,130,24,0,BTN3D,"Load Orders Log","",NONE
LABEL,-1,PARENT,160,94,400,20,0,"Reload contacts and keys from an orders.log file.",NONE
LABEL,711,PARENT,3,134,-2,-2,0,"2",GROUP
BUTTON,720,PARENT,20,160,130,24,0,BTN3D,"Backup Database","",NONE
LABEL,-1,PARENT,160,160,400,20,0,"This saves the current database to a backup file.",NONE
BUTTON,730,PARENT,20,194,130,24,0,BTN3D,"Restore Database","",NONE
LABEL,-1,PARENT,160,194,400,20,0,"CAUTION! This deletes everything and restores to the earlier saved state!",NONE
END
*

//	Contacts Page
MLTEXT 153
LABEL,600,PARENT,14,6,200,24,0,"Evolution Computing Contacts:",NONE
LABEL,-1,PARENT,220,6,50,24,0,"Status:",NONE
LABEL,614,PARENT,280,6,-20,24,0,"",FRAME
DATAGRID,610,PARENT,20,44,950,50,0
DATAGRID,620,PARENT,20,98,-20,50,0
BUTTON,611,PARENT,20,150,60,24,0,BTN3D,"Clear","",NONE
BUTTON,612,PARENT,100,150,90,24,0,BTN3D,"Find Records","",NONE
BUTTON,613,PARENT,210,150,110,24,0,BTN3D,"Add New Record","",NONE
BUTTON,615,PARENT,340,150,100,24,0,BTN3D,"Update Record","",NONE
BUTTON,616,PARENT,460,150,100,24,0,BTN3D,"Delete Record","",NONE
BUTTON,618,PARENT,-300,150,-200,24,0,BTN3D,"Show Open Orders","",NONE
BUTTON,619,PARENT,-180,150,-20,24,0,BTN3D,"Show Completed Orders","",NONE
LABEL,-1,PARENT,20,188,-20,4,0,"",FRAME
DATAGRID,630,PARENT,20,206,-20,-20,0
END
*

//	Authorization Page
MLTEXT 154
LABEL,600,PARENT,14,6,200,24,0,"Evolution Computing Authorization Keys:",NONE
LABEL,-1,PARENT,270,6,50,24,0,"Status:",NONE
LABEL,614,PARENT,290,6,-20,24,0,"",FRAME
DATAGRID,610,PARENT,20,44,-20,50,0
DATAGRID,620,PARENT,20,98,950,50,0
DATAGRID,630,PARENT,20,152,-20,50,0
BUTTON,611,PARENT,20,204,60,24,0,BTN3D,"Clear","",NONE
BUTTON,612,PARENT,100,204,90,24,0,BTN3D,"Find Records","",NONE
BUTTON,613,PARENT,210,204,110,24,0,BTN3D,"Add New Record","",NONE
BUTTON,615,PARENT,340,204,100,24,0,BTN3D,"Update Record","",NONE
BUTTON,616,PARENT,460,204,100,24,0,BTN3D,"Delete Record","",NONE
BUTTON,699,PARENT,-430,204,-330,24,0,BTN3D,"EMail Keys","",NONE
BUTTON,618,PARENT,-300,204,-200,24,0,BTN3D,"Show Open Orders","",NONE
BUTTON,619,PARENT,-180,204,-20,24,0,BTN3D,"Show Completed Orders","",NONE
LABEL,-1,PARENT,20,242,-20,4,0,"",FRAME
DATAGRID,640,PARENT,20,260,-20,-20,0
END
*

//	Payments Page
MLTEXT 155
LABEL,600,PARENT,14,6,200,24,0,"Evolution Computing Payments:",NONE
LABEL,-1,PARENT,210,6,50,24,0,"Status:",NONE
LABEL,614,PARENT,260,6,-20,24,0,"",FRAME
DATAGRID,610,PARENT,20,40,-20,50,0
BUTTON,611,PARENT,20,90,60,24,0,BTN3D,"Clear","",NONE
BUTTON,612,PARENT,100,90,90,24,0,BTN3D,"Find Records","",NONE
BUTTON,613,PARENT,210,90,110,24,0,BTN3D,"Add New Record","",NONE
BUTTON,615,PARENT,340,90,100,24,0,BTN3D,"Update Record","",NONE
BUTTON,616,PARENT,460,90,100,24,0,BTN3D,"Delete Record","",NONE
BUTTON,618,PARENT,-300,90,-200,24,0,BTN3D,"Show Open Orders","",NONE
BUTTON,619,PARENT,-180,90,-20,24,0,BTN3D,"Show Completed Orders","",NONE
LABEL,-1,PARENT,20,128,-20,4,0,"",FRAME
DATAGRID,620,PARENT,20,146,-20,-20,0
END
*

//	Order Processing Page
MLTEXT 156
LABEL,600,PARENT,14,6,200,24,0,"Evolution Computing Orders:",NONE
LABEL,-1,PARENT,220,6,50,24,0,"Status:",NONE
LABEL,614,PARENT,260,6,-20,24,0,"",FRAME
DATAGRID,610,PARENT,20,44,-20,50,0
DATAGRID,620,PARENT,20,98,900,50,0
DATAGRID,630,PARENT,20,152,-20,50,0
DATAGRID,640,PARENT,20,206,-20,50,0
DATAGRID,650,PARENT,20,260,-20,100,0
BUTTON,613,PARENT,20,362,100,24,0,BTN3D,"Mark Paid","",NONE
BUTTON,617,PARENT,140,362,100,24,0,BTN3D,"Mark Invoiced","",NONE
BUTTON,612,PARENT,260,362,100,24,0,BTN3D,"EMail Keys","",NONE
BUTTON,611,PARENT,380,362,100,24,0,BTN3D,"Mark Shipped","",NONE
BUTTON,615,PARENT,600,362,100,24,0,BTN3D,"Update Order","",NONE
BUTTON,616,PARENT,720,362,100,24,0,BTN3D,"Delete Order","",NONE
BUTTON,618,PARENT,-300,362,-200,24,0,BTN3D,"Show Open Orders","",NONE
BUTTON,619,PARENT,-180,362,-20,24,0,BTN3D,"Show Completed Orders","",NONE
LABEL,-1,PARENT,20,400,-20,4,0,"",FRAME
DATAGRID,660,PARENT,20,418,-20,-20,0
END
*

//	Controls Page
MLTEXT 157
LABEL,600,PARENT,14,6,200,24,0,"Evolution Computing Controls:",NONE
LABEL,-1,PARENT,210,6,50,24,0,"Status:",NONE
LABEL,614,PARENT,270,6,-20,24,0,"",FRAME
DATAGRID,610,PARENT,20,40,-20,50,0
BUTTON,611,PARENT,20,90,60,24,0,BTN3D,"Clear","",NONE
BUTTON,612,PARENT,100,90,90,24,0,BTN3D,"Find Records","",NONE
BUTTON,613,PARENT,210,90,110,24,0,BTN3D,"Add New Record","",NONE
BUTTON,615,PARENT,340,90,100,24,0,BTN3D,"Update Record","",NONE
BUTTON,616,PARENT,460,90,100,24,0,BTN3D,"Delete Record","",NONE
BUTTON,618,PARENT,-300,90,-200,24,0,BTN3D,"Show Open Orders","",NONE
BUTTON,619,PARENT,-180,90,-20,24,0,BTN3D,"Show Completed Orders","",NONE
LABEL,-1,PARENT,20,128,-20,4,0,"",FRAME
DATAGRID,620,PARENT,20,146,-20,-20,0
END
*

//	Discounts Page
MLTEXT 158
LABEL,600,PARENT,14,6,200,24,0,"Evolution Computing Discounts:",NONE
LABEL,-1,PARENT,220,6,50,24,0,"Status:",NONE
LABEL,614,PARENT,280,6,-20,24,0,"",FRAME
DATAGRID,610,PARENT,20,40,-20,50,0
BUTTON,611,PARENT,20,90,60,24,0,BTN3D,"Clear","",NONE
BUTTON,612,PARENT,100,90,90,24,0,BTN3D,"Find Records","",NONE
BUTTON,613,PARENT,210,90,110,24,0,BTN3D,"Add New Record","",NONE
BUTTON,615,PARENT,340,90,100,24,0,BTN3D,"Update Record","",NONE
BUTTON,616,PARENT,460,90,100,24,0,BTN3D,"Delete Record","",NONE
BUTTON,618,PARENT,-300,90,-200,24,0,BTN3D,"Show Open Orders","",NONE
BUTTON,619,PARENT,-180,90,-20,24,0,BTN3D,"Show Completed Orders","",NONE
LABEL,-1,PARENT,20,128,-20,4,0,"",FRAME
DATAGRID,620,PARENT,20,146,-20,-20,0
END
*

END
