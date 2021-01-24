-- Accounts:
-- 1. KERMSAdmin:admin1234
-- Production:
-- 1. KERMSAdmin:Adm!n1234

/***********************************/
/**********               **********/
/********** Define Tables **********/
/**********               **********/
/***********************************/


/*
App Full Design

Website Pages
Main Page
About Us
History
Board Members
Mission Statement
Services
Rules and Regs
Competition License
CPD Application
Calendar
Results
Contact Us
Clubs and Links
Gallery
News

BMF Admin Portal

Admin Will do below
- Manage News
- Manage Events Category
- Manage Events Rounds
- Manage Gallery
- Manage Users
- Manage Profiles
- Manage Admin Access
- Manage Season
- Manage Rounds Results
- Manage Rules and Regs
- Manage License
- Manage Drivers License  (Add, Edit, Approve Renew)
- Manage Clubs
- Manage Links
- Manage CPD Application

Tables

Manage News
----------------
News_Date_Time
Subject
Body
Image

Manage Event Category
----------------
Title
Category_Image_Url

Season
----------------
Title
is_active

Events Rounds
----------------
Event_Category_Id
Round_Number (1,2,3,etc and N/A)
Start_Date_Time
End_Date_Time
Add_to_Calendar
Add_to_Countdown
Season_Id

Rules and Regs
----------------
Category_Id
Title
PDF_File


Results
----------------
Category_Id
Round_Id
User_Id
Points

Results_PDF
----------------
Round_Id
PDF_File_Url

Users Account
----------------
Email_Address
CPR_Number
Password
Is_Admin

Users Profile
----------------
User_Id
Full_Name
Nationality
DOB
Mobile_Number
Full_Address
Blood_Group
Place_Of_Birth
Photo_Url

My_License
----------------
User_Id
License_Number
License_Type
Expire_Date

License_Renew_History
----------------
User_Id
License_Number
Issue_Date

Medical_Forms
----------------
License_Renew_History_Id
Type (New or Renew)
Date
Pdf_File

Gallery_Category
----------------
Title
Category_Image_Url

Gallery_Images
----------------
Gallery_Category_Id
Image_Url

Clubs
----------------
Title
Description
Logo_Url
Website_link

Quick_Links
----------------
Title
Website_link

CPD_Application
----------------
VEHICLE_OWNER
Vehicle_Driver1
Vehicle_Driver2
Vehicle_Driver3
Phone_Number
Mobile_Number
CPR_Number
Registered_in_country
license_plate_no
Year_of_manufacture
Net_weight_of_vehicle (kg)
Chassis_No
Make
No_of_cylinders
Type of vehicle (car, motorcycle, truck, trailer,etc)
Color
Upholstery (FABRIC or LEATHER)
No_of_seats_or_carrying_capacity
Radio (yes or no)
no_tyres_Spare
Other_equipment

My_Races
----------------
User_Id
Round_Id

My_Fav_Races
----------------
User_Id
Round_Id

License
----------------
License_Type
License_Title
License_Image_Front_Url
License_Image_Back_Url