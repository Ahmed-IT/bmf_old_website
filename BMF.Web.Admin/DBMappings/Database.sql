-- Accounts:
-- 1. KERMSAdmin:admin1234
-- Production:
-- 1. KERMSAdmin:Adm!n1234

/***********************************/
/**********               **********/
/********** Define Tables **********/
/**********               **********/
/***********************************/

DROP TABLE dbo.D_Driver;
DROP TABLE dbo.D_Team;
DROP TABLE dbo.D_Transponder;
DROP TABLE dbo.D_RaceSession;
DROP TABLE dbo.D_Race;

CREATE TABLE dbo.D_Race(
	-- PRIMARY KEY
	ID BIGINT PRIMARY KEY IDENTITY(1,1),
	-- FOREIGN KEYS
	-- TABLE SPECIFIC
	[Name] NVARCHAR(120) NOT NULL,
	[Date_Start] DATETIME NULL,
	[Date_End] DATETIME NULL,
	[Duration_Race_H] INT NOT NULL DEFAULT (0),
	[Duration_Rest_M] INT NOT NULL DEFAULT (0),
	[Stint_Min_M] INT NOT NULL DEFAULT (0),
	[Drivers_Min] INT NOT NULL DEFAULT (0),
	[Drivers_Max] INT NOT NULL DEFAULT (0),
	-- SYSTEM DEFAULTS
	[S_Status] BIT NOT NULL DEFAULT (1), 
	-- CONSTRAINTS
);

CREATE TABLE dbo.D_RaceSession(
	-- PRIMARY KEY
	ID BIGINT PRIMARY KEY IDENTITY(1,1),
	-- FOREIGN KEYS
	[ID_Race] BIGINT NOT NULL,
	-- TABLE SPECIFIC
	[Name] NVARCHAR(120) NOT NULL,
	[Date_Start] DATETIME NULL,
	[Date_End] DATETIME NULL,
	-- SYSTEM DEFAULTS
	[S_Status] BIT NOT NULL DEFAULT (1), 
	-- CONSTRAINTS
	CONSTRAINT DRS_FK_1 FOREIGN KEY ([ID_Race]) REFERENCES D_Race([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
);

CREATE TABLE dbo.D_Transponder(
	-- PRIMARY KEY
	ID BIGINT PRIMARY KEY IDENTITY(1,1),
	-- FOREIGN KEYS
	-- TABLE SPECIFIC
	[ShortCode] INT NOT NULL,
	[TextCode] NVARCHAR(225) NOT NULL,
	-- SYSTEM DEFAULTS
	[S_Status] BIT NOT NULL DEFAULT (1), 
	-- CONSTRAINTS
);

CREATE TABLE dbo.D_Team(
	-- PRIMARY KEY
	ID BIGINT PRIMARY KEY IDENTITY(1,1),
	-- FOREIGN KEYS
	-- TABLE SPECIFIC
	[Name] NVARCHAR(120) NOT NULL,
	[SWS] NVARCHAR(120) NULL,
	[Captain_Name] NVARCHAR(120) NULL,
	[Captain_Mobile] NVARCHAR(120) NULL,
	[Captain_Email] NVARCHAR(120) NULL,
	[TeamClass] NVARCHAR(120) NOT NULL,
	-- SYSTEM DEFAULTS
	[S_Status] BIT NOT NULL DEFAULT (1), 
	-- CONSTRAINTS
);

ALTER TABLE dbo.D_Team 
ALTER COLUMN [Captain_Name] NVARCHAR(120) NULL;

ALTER TABLE dbo.D_Team 
ALTER COLUMN [Captain_Mobile] NVARCHAR(120) NULL;

ALTER TABLE dbo.D_Team 
ALTER COLUMN [Captain_Email] NVARCHAR(120) NULL;

CREATE TABLE dbo.D_Driver(
	-- PRIMARY KEY
	ID BIGINT PRIMARY KEY IDENTITY(1,1),
	-- FOREIGN KEYS
	[ID_Team] BIGINT NOT NULL,
	-- TABLE SPECIFIC
	[Name] NVARCHAR(120) NOT NULL,
	[SWS] NVARCHAR(120) NULL,
	[Date_Birth] DATETIME NULL,
	[Weight] DECIMAL(10,3) NULL,
	[Ballast] NVARCHAR(120) NULL,
	[BMFLicense] NVARCHAR(120) NULL,
	[PersonalImage] VARBINARY(MAX) NULL,
	-- SYSTEM DEFAULTS
	[S_Status] BIT NOT NULL DEFAULT (1), 
	-- CONSTRAINTS
	CONSTRAINT DD_FK_1 FOREIGN KEY ([ID_Team]) REFERENCES D_Team([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
);

ALTER TABLE dbo.D_Driver 
ALTER COLUMN [Weight] DECIMAL(10,3) NULL;

/***********************************/
/**********               **********/
/********** Master Tables **********/
/**********               **********/
/***********************************/

DROP TABLE dbo.D_RaceDriverRegistration;
DROP TABLE dbo.D_RaceRegistration;
DROP TABLE dbo.M_RaceDriverRegistration;
DROP TABLE dbo.M_RaceRegistration;

CREATE TABLE dbo.M_RaceRegistration(
	-- PRIMARY KEY
	ID BIGINT PRIMARY KEY IDENTITY(1,1),
	-- FOREIGN KEYS
	[ID_Race] BIGINT NOT NULL,
	[ID_Team] BIGINT NOT NULL,
	[ID_Transponder] BIGINT NULL,
	-- TABLE SPECIFIC
	[Code_Team] NVARCHAR(120) NOT NULL,
	-- SYSTEM DEFAULTS
	[S_Status] BIT NOT NULL DEFAULT (1), 
	-- CONSTRAINTS
	CONSTRAINT MRR_FK_1 FOREIGN KEY ([ID_Race]) REFERENCES D_Race([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT MRR_FK_2 FOREIGN KEY ([ID_Team]) REFERENCES D_Team([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
	CONSTRAINT MRR_FK_3 FOREIGN KEY ([ID_Transponder]) REFERENCES D_Transponder([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
);

ALTER TABLE dbo.M_RaceRegistration 
ALTER COLUMN [ID_Transponder] BIGINT NULL;

CREATE TABLE dbo.M_RaceDriverRegistration(
	-- PRIMARY KEY
	ID BIGINT PRIMARY KEY IDENTITY(1,1),
	-- FOREIGN KEYS
	[ID_RaceRegistration] BIGINT NOT NULL,
	[ID_Driver] BIGINT NOT NULL,
	-- TABLE SPECIFIC
	[Code_Driver] NVARCHAR(120) NOT NULL,
	-- SYSTEM DEFAULTS
	[S_Status] BIT NOT NULL DEFAULT (1), 
	-- CONSTRAINTS
	CONSTRAINT MRDR_FK_1 FOREIGN KEY ([ID_RaceRegistration]) REFERENCES M_RaceRegistration([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT MRDR_FK_2 FOREIGN KEY ([ID_Driver]) REFERENCES D_Driver([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
);

CREATE TABLE dbo.M_RaceDriverStint(
	-- PRIMARY KEY
	ID BIGINT PRIMARY KEY IDENTITY(1,1),
	-- FOREIGN KEYS
	[ID_RaceSession] BIGINT NOT NULL,
	[ID_RaceDriverRegistration] BIGINT NOT NULL,
	-- TABLE SPECIFIC
	[Date_Start] DATETIME NULL,
	[Date_End] DATETIME NULL,
	-- SYSTEM DEFAULTS
	[S_Status] BIT NOT NULL DEFAULT (1), 
	-- CONSTRAINTS
	CONSTRAINT MRDS_FK_1 FOREIGN KEY ([ID_RaceSession]) REFERENCES D_RaceSession([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT MRDS_FK_2 FOREIGN KEY ([ID_RaceDriverRegistration]) REFERENCES M_RaceDriverRegistration([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
);


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