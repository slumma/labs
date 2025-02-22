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
	SupplierStatus nvarchar(200), --temporary
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
	GrantStatus nvarchar(200), --temporary
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


INSERT INTO users (Username, Password, FirstName, LastName, Email, Phone, HomeAddress)
VALUES
('samogden', 'password123', 'sam', 'ogden', 'sam@example.com', '555-1234', '123 Elm St'),
('nickclement', 'password456', 'nick', 'clement', 'nickclement@example.com', '555-5678', '456 Oak St'),
('nadeemhudson', 'password789', 'nadeem', 'hudson', 'nadeemhudson@example.com', '555-9876', '789 Pine St'),
('joshwhite', 'password234', 'josh', 'White', 'joshwhite@example.com', '555-2234', '321 Birch St'),
('sharons', 'password567', 'sharon', 'sanchez', 'shrnsnchz@example.com', '555-6789', '654 Cedar St'),
('theGoat', 'password890', 'the', 'goat', 'thegoat17@example.com', '555-7890', '987 Spruce St'),
('haileyWelch', 'password101', 'hailey', 'welch', 'haileyWelch238@example.com', '555-1010', '109 Maple St'),
('hawkTuah', 'password112', 'hawk', 'tuah', 'hawktuah838@example.com', '555-1212', '210 Oak St'),
('yourBoy', 'password213', 'your', 'boy', 'yourboy87392@example.com', '555-1414', '312 Pine St'),
('BabikDmx', 'password314', 'dmytro', 'babik', 'dmytrobabik43@example.com', '555-1515', '413 Elm St'),
('samO', 'password314', 'sam', 'o', 'samoGden@example.com', '555-1515', '413 Elm St');



INSERT INTO grantSupplier (SupplierName, OrgType, SupplierStatus, BusinessAddress)
VALUES
('TechCorp', 'Private', 'Active', '101 Tech Rd'),
('EduFunds', 'Non-Profit', 'Inactive', '202 Education Blvd'),
('HealthGrant', 'Government', 'Pending', '303 Health Ave'),
('ScienceTrust', 'Non-Profit', 'Active', '404 Research Blvd'),
('MedFunds', 'Private', 'Inactive', '505 Healthcare St'),
('EduTech', 'Government', 'Pending', '606 Innovation Way'),
('GreenGrants', 'Non-Profit', 'Active', '707 Sustainability Ave'),
('ArtFunds', 'Private', 'Inactive', '808 Creative St'),
('SocialAid', 'Government', 'Pending', '909 Welfare Blvd'),
('TechInnovators', 'Private', 'Active', '1010 Future Rd');



INSERT INTO project (ProjectName, DueDate)
VALUES
('Project Alpha', '2025-06-01'),
('Project Beta', '2025-08-15'),
('Project Gamma', '2025-12-31'),
('Project Delta', '2025-04-20'),
('Project Epsilon', '2025-07-10'),
('Project Zeta', '2025-09-25'),
('Project Eta', '2025-11-15'),
('Project Theta', '2025-03-05'),
('Project Iota', '2025-10-20'),
('Project Kappa', '2025-05-25');



INSERT INTO BPrep (UserID, CommunicationStatus, SupplierID)
VALUES
(1, 'Active', 1),
(2, 'Inactive', 2),
(3, 'Pending', 3),
(4, 'Active', 4),
(5, 'Inactive', 5),
(6, 'Pending', 6),
(7, 'Active', 7),
(8, 'Inactive', 8),
(9, 'Pending', 9),
(10, 'Active', 10);


INSERT INTO employee (UserID, AdminStatus)
VALUES
(1, 1),
(2, 0),
(3, 0),
(4, 0),
(5, 0),
(6, 0),
(7, 0),
(8, 0),
(9, 0),
(10, 0);


INSERT INTO faculty (UserID)
VALUES
(1),
(2),
(3),
(4),
(5),
(6),
(7),
(8),
(9),
(10);


INSERT INTO nonfaculty (UserID)
VALUES
(11);

INSERT INTO projectStaff (ProjectID, UserID, Leader, Active)
VALUES
(1, 1, 1, 1),
(1, 2, 0, 1),
(2, 3, 1, 1),
(2, 4, 0, 1),
(3, 5, 1, 1),
(3, 6, 0, 1),
(4, 7, 1, 1),
(4, 8, 0, 1),
(5, 9, 1, 1),
(5, 10, 0, 1);


INSERT INTO task (ProjectID, DueDate, Objective)
VALUES
(1, '2025-05-01', 'Initial Research'),
(2, '2025-07-01', 'Development'),
(3, '2025-11-01', 'Final Review'),
(4, '2025-04-01', 'Market Analysis'),
(5, '2025-06-01', 'Product Design'),
(6, '2025-08-01', 'Prototyping'),
(7, '2025-10-01', 'Testing'),
(8, '2025-12-01', 'Launch Plan'),
(9, '2025-03-01', 'Requirement Gathering'),
(10, '2025-09-01', 'Risk Assessment');

INSERT INTO TaskStaff (TaskID, AssigneeID, AssignerID, DueDate)
VALUES
(1, 1, 2, '2025-04-25'),
(2, 2, 3, '2025-06-15'),
(3, 3, 1, '2025-10-01'),
(4, 4, 5, '2025-03-25'),
(5, 5, 6, '2025-05-15'),
(6, 6, 4, '2025-07-25'),
(7, 7, 8, '2025-09-15'),
(8, 8, 9, '2025-11-25'),
(9, 9, 10, '2025-02-15'),
(10, 10, 7, '2025-08-15');


INSERT INTO meeting (ProjectID, MeetingDate, Purpose)
VALUES
(1, '2025-03-01', 'Kick-off Meeting'),
(2, '2025-05-15', 'Progress Update'),
(3, '2025-09-01', 'Final Review'),
(4, '2025-04-01', 'Team Meeting'),
(5, '2025-06-15', 'Strategy Session'),
(6, '2025-08-01', 'Budget Review'),
(7, '2025-10-01', 'Planning Session'),
(8, '2025-12-01', 'Status Update'),
(9, '2025-03-15', 'Client Meeting'),
(10, '2025-09-15', 'Project Wrap-Up');


INSERT INTO attendance (MeetingID, UserID)
VALUES
(1, 1),
(1, 2),
(2, 3),
(2, 4),
(3, 5),
(3, 6),
(4, 7),
(4, 8),
(5, 9),
(5, 10);


INSERT INTO meetingMinutes (MeetingID, UserID, MinutesDate)
VALUES
(1, 1, '2025-03-02'),
(2, 2, '2025-05-16'),
(3, 3, '2025-09-02'),
(4, 4, '2025-04-02'),
(5, 5, '2025-06-16'),
(6, 6, '2025-08-02'),
(7, 7, '2025-10-02'),
(8, 8, '2025-12-02'),
(9, 9, '2025-03-16'),
(10, 10, '2025-09-16');



INSERT INTO notes (ProjectID, Content, noteDate)
VALUES
(1, 'Initial notes for Project Alpha', '2025-02-20'),
(2, 'Development notes for Project Beta', '2025-04-15'),
(3, 'Review notes for Project Gamma', '2025-08-01'),
(4, 'Concept notes for Project Delta', '2025-03-20'),
(5, 'Planning notes for Project Epsilon', '2025-05-15'),
(6, 'Design notes for Project Zeta', '2025-07-01'),
(7, 'Prototype notes for Project Eta', '2025-09-01'),
(8, 'Launch notes for Project Theta', '2025-11-01'),
(9, 'Requirement notes for Project Iota', '2025-02-15'),
(10, 'Assessment notes for Project Kappa', '2025-10-01');



INSERT INTO grants (SupplierID, ProjectID, Category, SubmissionDate, descriptions, AwardDate, Amount, StatusName)
VALUES
(1, 1,  'Federal', '2025-01-01', 'Grant for tech development', '2025-05-01', 100000, 'Active'),
(2, 2,  'State', '2025-03-01', 'Grant for educational programs', '2025-07-01', 50000, 'Pending'),
(3, 3, 'Business', '2025-06-01', 'Grant for health initiatives', '2025-11-01', 200000, 'Inactive'),
(4, 1,  'University', '2025-02-01', 'Grant for research', '2025-06-01', 150000, 'Active'),
(5, 2, 'Federal', '2025-04-01', 'Grant for tech infrastructure', '2025-08-01', 80000, 'Pending'),
(6, 3, 'State', '2025-05-01', 'Grant for educational tools', '2025-09-01', 120000, 'Inactive'),
(7, 1, 'Business', '2025-01-15', 'Grant for business development', '2025-06-15', 95000, 'Active'),
(8, 2,  'University', '2025-03-15', 'Grant for academic research', '2025-07-15', 50000, 'Pending'),
(9, 3, 'Federal', '2025-06-15', 'Grant for health research', '2025-11-15', 220000, 'Inactive'),
(10, 1,  'State', '2025-02-15', 'Grant for public health', '2025-06-15', 130000, 'Active'),
(4, 2, 'Business', '2025-04-15', 'Grant for business innovation', '2025-08-15', 85000, 'Pending'),
(9, 3,  'University', '2025-05-15', 'Grant for educational research', '2025-09-15', 140000, 'Inactive'),
(3, 1,  'Federal', '2025-01-20', 'Grant for tech advancement', '2025-06-20', 105000, 'Active'),
(5, 2,  'State', '2025-03-20', 'Grant for state projects', '2025-07-20', 75000, 'Pending'),
(1, 3, 'Business', '2025-06-20', 'Grant for business ventures', '2025-11-20', 195000, 'Inactive');



INSERT INTO grantStatus (GrantID, StatusName, ChangeDate)
VALUES
(1, 'Approved', '2025-02-01'),
(2, 'Pending', '2025-04-01'),
(3, 'Denied', '2025-08-01'),
(4, 'Approved', '2025-06-01'),
(5, 'Pending', '2025-08-01'),
(6, 'Denied', '2025-09-01'),
(7, 'Approved', '2025-07-01'),
(8, 'Pending', '2025-09-01'),
(9, 'Denied', '2025-11-01'),
(10, 'Approved', '2025-05-01');



INSERT INTO supplierStatus (SupplierID, StatusName, ChangeDate)
VALUES
(1, 'Active', '2025-01-01'),
(2, 'Inactive', '2025-03-01'),
(3, 'Pending', '2025-06-01'),
(4, 'Active', '2025-02-01'),
(5, 'Inactive', '2025-04-01'),
(6, 'Pending', '2025-05-01'),
(7, 'Active', '2025-01-15'),
(8, 'Inactive', '2025-03-15'),
(9, 'Pending', '2025-06-15'),
(10, 'Active', '2025-02-15');




INSERT INTO userMessage (SenderID, RecipientID, SubjectTitle, Contents, SentTime)
VALUES
(1, 2, 'Hello', 'This is a test message', '2025-02-20 10:00:00'),
(2, 3, 'Reminder', 'Dont forget the meeting tomorrow', '2025-02-21 11:00:00'),
(3, 1, 'Thank you', 'Thanks for your help!', '2025-02-22 09:00:00'),
(4, 5, 'Meeting Update', 'The meeting has been rescheduled', '2025-02-23 08:00:00'),
(5, 6, 'Project Update', 'Here is the latest update on the project', '2025-02-24 07:00:00'),
(6, 4, 'Task Reminder', 'Dont forget to complete your tasks', '2025-02-25 06:00:00'),
(7, 8, 'Client Meeting', 'We have a meeting with the client tomorrow', '2025-02-26 05:00:00'),
(8, 9, 'Weekly Report', 'Please submit your weekly report', '2025-02-27 04:00:00'),
(9, 10, 'feedback request', 'Can you provide feedback', '2025-02-28 03:00:00'),
(10, 7, 'Team Lunch', 'We are having a team lunch on Friday', '2025-03-01 02:00:00');



