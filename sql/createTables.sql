CREATE TABLE users(
    UserID int Identity(1,1) PRIMARY KEY,
    Username nvarchar(200),
    Password nvarchar(200),
    FirstName nvarchar(200),
    LastName nvarchar(200),
    Email nvarchar(200),
    Phone nvarchar(200),
    HomeAddress nvarchar(200));

CREATE TABLE grantSupplier(
    SupplierID int Identity(1,1) PRIMARY KEY,
    SupplierName nvarchar(200),
    OrgType nvarchar(200),
    BusinessAddress nvarchar(200));

CREATE TABLE project(
    ProjectID int Identity(1,1) PRIMARY KEY,
    ProjectName nvarchar(200), 
    DueDate datetime);

CREATE TABLE BPrep (
    UserID INT PRIMARY KEY,
    CommunicationStatus nvarchar(200),
    SupplierID int,
    FOREIGN KEY (SupplierID) REFERENCES grantSupplier(SupplierID),
    FOREIGN KEY (UserID) REFERENCES users(UserID));

CREATE TABLE employee (
    UserID INT PRIMARY KEY,
    AdminStatus bit default 0,
    FOREIGN KEY (UserID) REFERENCES users(UserID));

CREATE TABLE faculty (
    UserID INT PRIMARY KEY,
    FOREIGN KEY (UserID) REFERENCES users(UserID));

CREATE TABLE nonfaculty (
    UserID INT PRIMARY KEY,
    FOREIGN KEY (UserID) REFERENCES users(UserID));

CREATE TABLE projectStaff(
    ProjectStaffID int Identity(1,1) PRIMARY KEY, 
    ProjectID int, 
    UserID int, 
    Leader bit,
    Active bit,
    FOREIGN KEY (ProjectID) REFERENCES project(ProjectID),
    FOREIGN KEY (UserID) REFERENCES faculty(UserID));

CREATE TABLE task(
    TaskID int Identity(1,1) PRIMARY KEY,
    ProjectID int, 
    DueDate date,
    Objective nvarchar(200),
    FOREIGN KEY (ProjectID) REFERENCES project(ProjectID));

CREATE TABLE TaskStaff(
    TaskStaffID int Identity(1,1) PRIMARY KEY,
    TaskID int, 
    AssigneeID int,
	AssignerID int,
    DueDate date,
    FOREIGN KEY (TaskID) REFERENCES task(TaskID),
    FOREIGN KEY (AssigneeID) REFERENCES employee(UserID),
	FOREIGN KEY (AssignerID) REFERENCES employee(UserID));

CREATE TABLE meeting(
    MeetingID int Identity(1,1) PRIMARY KEY,
    ProjectID int, 
    MeetingDate date,
    Purpose nvarchar(200),
    FOREIGN KEY (ProjectID) REFERENCES project(ProjectID));

CREATE TABLE attendance(
    AttendanceID int Identity(1,1) PRIMARY KEY,
    MeetingID int,
    UserID int,
    FOREIGN KEY (MeetingID) REFERENCES meeting(MeetingID),
    FOREIGN KEY (UserID) REFERENCES users(UserID));

CREATE TABLE meetingMinutes(
    MinutesID int Identity(1,1) PRIMARY KEY,
    MeetingID int,
    UserID int, 
    MinutesDate date,
    FOREIGN KEY (MeetingID) REFERENCES meeting(MeetingID),
    FOREIGN KEY (UserID) REFERENCES employee(UserID));

CREATE TABLE notes(
    NotesID int Identity(1,1) PRIMARY KEY,
    ProjectID int, 
    Content text, 
    noteDate datetime DEFAULT GETDATE(),
    FOREIGN KEY (ProjectID) REFERENCES project(ProjectID));

CREATE TABLE grants(
    GrantID int Identity(1,1) PRIMARY KEY,
    SupplierID int, 
    ProjectID int,
    StatusName nvarchar(200),
    Category nvarchar(200),
    SubmissionDate date, 
    descriptions text,
    AwardDate date,
    Amount float,
    FOREIGN KEY (SupplierID) REFERENCES grantSupplier(SupplierID),
    FOREIGN KEY (ProjectID) REFERENCES project(ProjectID));

CREATE TABLE grantStatus(
    StatusID int Identity(1,1) PRIMARY KEY,
    GrantID int,
    StatusName nvarchar(200),
    ChangeDate datetime DEFAULT GETDATE(),
    FOREIGN KEY (GrantID) REFERENCES grants(GrantID));

CREATE TABLE supplierStatus(
    StatusID int Identity(1,1) PRIMARY KEY,
    SupplierID int, 
    StatusName nvarchar(200),
    ChangeDate datetime DEFAULT GETDATE(),
    FOREIGN KEY (SupplierID) REFERENCES grantSupplier(SupplierID));

CREATE TABLE userMessage(
    MessageID int Identity(1,1) PRIMARY KEY,
    SenderID int,
    RecipientID int,
    SubjectTitle text,
    Contents text,
    SentTime datetime DEFAULT GETDATE(),
    FOREIGN KEY (SenderID) REFERENCES users(UserID));
