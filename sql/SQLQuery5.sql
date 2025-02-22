
INSERT INTO dbo.users (Username, Password, FirstName, LastName, Email, Phone, HomeAddress)
VALUES
('jdoe', 'password123', 'John', 'Doe', 'jdoe22@gmail.com', '(571) 488-8440', '221 Minton Cir, Moneta, VA 22661'),
('asmith', 'password456', 'Alice', 'Smith', 'asmith77@gmail.com', '(678) 422-3129', '1152 Buford Rd, Alexandria, VA 22211'),
('mjones', 'password789', 'Mike', 'Jones', 'mjones88@gmail.com', '(434) 555-9876', '789 Oak St, Richmond, VA 23220'),
('bwatson', 'password101', 'Barbara', 'Watson', 'bwatson55@gmail.com', '(757) 444-1234', '456 Pine Ave, Norfolk, VA 23502'),
('cclark', 'password112', 'Charles', 'Clark', 'cclark99@gmail.com', '(804) 777-6543', '123 Maple Dr, Charlottesville, VA 22903'),
('dlee', 'password113', 'David', 'Lee', 'dlee44@gmail.com', '(434) 232-6789', '345 Elm St, Lynchburg, VA 24502'),
('efisher', 'password114', 'Evan', 'Fisher', 'efisher88@gmail.com', '(540) 345-7890', '987 Pine St, Roanoke, VA 24018'),
('gmitchell', 'password115', 'Grace', 'Mitchell', 'gmitchell22@gmail.com', '(757) 345-1122', '321 Cedar Ave, Virginia Beach, VA 23456'),
('hmorris', 'password116', 'Henry', 'Morris', 'hmorris33@gmail.com', '(703) 555-6789', '222 Maple St, Fairfax, VA 22030'),
('ijackson', 'password117', 'Ivy', 'Jackson', 'ijackson55@gmail.com', '(804) 678-4444', '678 Spruce Dr, Richmond, VA 23220'),
('kwhite', 'password118', 'Kurt', 'White', 'kwhite88@gmail.com', '(434) 999-5555', '123 Birch Ln, Charlottesville, VA 22903'),
('lgreen', 'password119', 'Linda', 'Green', 'lgreen77@gmail.com', '(540) 111-2222', '456 Cedar St, Blacksburg, VA 24060'),
('mmartin', 'password120', 'Megan', 'Martin', 'mmartin33@gmail.com', '(757) 222-3333', '789 Oak Ln, Chesapeake, VA 23322');

INSERT INTO dbo.grantSupplier (SupplierName, OrgType, BusinessAddress)
VALUES
('TechCorp', 'Private', '500 Innovation Dr, San Francisco, CA 94103'),
('InnovaTech', 'Government', '1200 Federal Plaza, Washington, DC 20500'),
('GreenSolutions', 'Private', '300 Eco Way, Portland, OR 97201'),
('HealthCorp', 'Private', '123 Wellness St, Seattle, WA 98109'),
('EduFund', 'Government', '456 Education Blvd, Boston, MA 02110'),
('BioTech', 'Private', '789 Bio Ave, Austin, TX 78701'),
('AgriGrants', 'Private', '101 Farm Rd, Des Moines, IA 50309'),
('CleanEnergy', 'Private', '202 Green St, Denver, CO 80203'),
('SmartTech', 'Private', '303 Innovation Rd, Raleigh, NC 27601'),
('EcoFunds', 'Private', '404 Eco Dr, San Diego, CA 92101');

INSERT INTO dbo.project (ProjectName, DueDate)
VALUES
('Education','2025-06-01'),
('New System','2025-07-01'),
('Renovation','2025-08-01'),
('Health Initiative','2025-09-01'),
('Green Energy','2025-10-01'),
('Tech Upgrade','2025-11-01'),
('Community Outreach','2025-12-01'),
('Research Development','2026-01-01'),
('Infrastructure Improvement','2026-02-01'),
('Water Conservation','2026-03-01');

INSERT INTO dbo.BPrep (UserID, CommunicationStatus, SupplierID)
VALUES 
  ((SELECT UserID FROM dbo.users WHERE Username = 'jdoe'), 'Not Started', (SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'TechCorp')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'asmith'), 'In Progress', (SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'GreenSolutions')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'mjones'), 'Completed', (SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'InnovaTech')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'bwatson'), 'Not Started', (SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'HealthCorp')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'cclark'), 'In Progress', (SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'EduFund')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'dlee'), 'Completed', (SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'BioTech')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'efisher'), 'Not Started', (SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'AgriGrants')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'gmitchell'), 'In Progress', (SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'CleanEnergy')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'hmorris'), 'Completed', (SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'SmartTech')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'ijackson'), 'Not Started', (SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'EcoFunds'));


  INSERT INTO dbo.employee (UserID, AdminStatus)
VALUES 
  ((SELECT UserID FROM dbo.users WHERE Username = 'bwatson'), 1),
  ((SELECT UserID FROM dbo.users WHERE Username = 'cclark'), 0),
  ((SELECT UserID FROM dbo.users WHERE Username = 'dlee'), 0),
  ((SELECT UserID FROM dbo.users WHERE Username = 'efisher'), 1),
  ((SELECT UserID FROM dbo.users WHERE Username = 'gmitchell'), 0),
  ((SELECT UserID FROM dbo.users WHERE Username = 'hmorris'), 1),
  ((SELECT UserID FROM dbo.users WHERE Username = 'ijackson'), 0),
  ((SELECT UserID FROM dbo.users WHERE Username = 'kwhite'), 1),
  ((SELECT UserID FROM dbo.users WHERE Username = 'lgreen'), 0),
  ((SELECT UserID FROM dbo.users WHERE Username = 'mmartin'), 1);
 
 
 INSERT INTO dbo.faculty (UserID)
VALUES 
  ((SELECT UserID FROM dbo.users WHERE Username = 'jdoe')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'asmith')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'bwatson')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'cclark')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'dlee')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'efisher')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'kwhite')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'lgreen')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'mmartin'));


  INSERT INTO dbo.nonfaculty (UserID)
VALUES 
  ((SELECT UserID FROM dbo.users WHERE Username = 'mjones')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'gmitchell')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'hmorris')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'ijackson')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'dlee')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'efisher')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'kwhite')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'lgreen')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'mmartin'));


  INSERT INTO dbo.projectStaff (ProjectID, UserID, Leader, Active)
VALUES 
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Education'), (SELECT UserID FROM dbo.users WHERE Username = 'jdoe'), 1, 1),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'New System'), (SELECT UserID FROM dbo.users WHERE Username = 'asmith'), 0, 1),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Renovation'), (SELECT UserID FROM dbo.users WHERE Username = 'mjones'), 1, 1),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Health Initiative'), (SELECT UserID FROM dbo.users WHERE Username = 'bwatson'), 0, 1),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Green Energy'), (SELECT UserID FROM dbo.users WHERE Username = 'cclark'), 1, 1),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Tech Upgrade'), (SELECT UserID FROM dbo.users WHERE Username = 'dlee'), 0, 1),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Community Outreach'), (SELECT UserID FROM dbo.users WHERE Username = 'efisher'), 1, 1),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Research Development'), (SELECT UserID FROM dbo.users WHERE Username = 'gmitchell'), 0, 1),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Infrastructure Improvement'), (SELECT UserID FROM dbo.users WHERE Username = 'hmorris'), 1, 1),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Water Conservation'), (SELECT UserID FROM dbo.users WHERE Username = 'ijackson'), 0, 1);


  INSERT INTO dbo.task (ProjectID, DueDate, Objective, Staff)
VALUES
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Education'), '2025-05-15', 'Initial Planning Phase', 'jdoe'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'New System'), '2025-06-15', 'Prepare Final Report', 'asmith'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Renovation'), '2025-07-15', 'Renovation Kickoff', 'mjones'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Health Initiative'), '2025-08-15', 'Health Assessment', 'bwatson'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Green Energy'), '2025-09-15', 'Energy Planning', 'cclark'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Tech Upgrade'), '2025-10-15', 'Upgrade Assessment', 'dlee'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Community Outreach'), '2025-11-15', 'Outreach Planning', 'efisher'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Research Development'), '2025-12-15', 'Research Planning', 'gmitchell'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Infrastructure Improvement'), '2026-01-15', 'Infrastructure Assessment', 'hmorris'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Water Conservation'), '2026-02-15', 'Conservation Planning', 'ijackson');


  INSERT INTO dbo.TaskStaff (TaskID, UserID, TaskOverview, DueDate)
VALUES
  ((SELECT TaskID FROM dbo.task WHERE Objective = 'Initial Planning Phase'), (SELECT UserID FROM dbo.users WHERE Username = 'bwatson'), 'Outline project requirements', '2025-05-10'),
  ((SELECT TaskID FROM dbo.task WHERE Objective = 'Prepare Final Report'), (SELECT UserID FROM dbo.users WHERE Username = 'cclark'), 'Review final report draft', '2025-06-10'),
  ((SELECT TaskID FROM dbo.task WHERE Objective = 'Renovation Kickoff'), (SELECT UserID FROM dbo.users WHERE Username = 'dlee'), 'Prepare renovation plan', '2025-07-10'),
  ((SELECT TaskID FROM dbo.task WHERE Objective = 'Health Assessment'), (SELECT UserID FROM dbo.users WHERE Username = 'efisher'), 'Conduct health assessments', '2025-08-10'),
  ((SELECT TaskID FROM dbo.task WHERE Objective = 'Energy Planning'), (SELECT UserID FROM dbo.users WHERE Username = 'gmitchell'), 'Develop energy plans', '2025-09-10'),
  ((SELECT TaskID FROM dbo.task WHERE Objective = 'Upgrade Assessment'), (SELECT UserID FROM dbo.users WHERE Username = 'hmorris'), 'Assess technology needs', '2025-10-10'),
  ((SELECT TaskID FROM dbo.task WHERE Objective = 'Outreach Planning'), (SELECT UserID FROM dbo.users WHERE Username = 'ijackson'), 'Plan community outreach', '2025-11-10'),
  ((SELECT TaskID FROM dbo.task WHERE Objective = 'Research Planning'), (SELECT UserID FROM dbo.users WHERE Username = 'jdoe'), 'Outline research goals', '2025-12-10'),
  ((SELECT TaskID FROM dbo.task WHERE Objective = 'Infrastructure Assessment'), (SELECT UserID FROM dbo.users WHERE Username = 'asmith'), 'Evaluate infrastructure', '2026-01-10'),
  ((SELECT TaskID FROM dbo.task WHERE Objective = 'Conservation Planning'), (SELECT UserID FROM dbo.users WHERE Username = 'mjones'), 'Develop conservation plans', '2026-02-10');


INSERT INTO dbo.meeting (ProjectID, MeetingDate, Purpose)
VALUES
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Education'), '2025-05-01', 'Project Kickoff Meeting'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'New System'), '2025-06-01', 'Progress Check-in Meeting'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Renovation'), '2025-07-01', 'Renovation Planning Meeting'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Health Initiative'), '2025-08-01', 'Health Initiative Meeting'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Green Energy'), '2025-09-01', 'Green Energy Meeting'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Tech Upgrade'), '2025-10-01', 'Tech Upgrade Meeting'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Community Outreach'), '2025-11-01', 'Outreach Planning Meeting'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Research Development'), '2025-12-01', 'Research Development Meeting'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Infrastructure Improvement'), '2026-01-01', 'Infrastructure Meeting'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Water Conservation'), '2026-02-01', 'Water Conservation Meeting');


  INSERT INTO dbo.attendance (MeetingID, UserID)
VALUES
  ((SELECT MeetingID FROM dbo.meeting WHERE Purpose = 'Project Kickoff Meeting'), (SELECT UserID FROM dbo.users WHERE Username = 'jdoe')),  
  ((SELECT MeetingID FROM dbo.meeting WHERE Purpose = 'Progress Check-in Meeting'), (SELECT UserID FROM dbo.users WHERE Username = 'asmith')),  
  ((SELECT MeetingID FROM dbo.meeting WHERE Purpose = 'Renovation Planning Meeting'), (SELECT UserID FROM dbo.users WHERE Username = 'mjones')),  
  ((SELECT MeetingID FROM dbo.meeting WHERE Purpose = 'Health Initiative Meeting'), (SELECT UserID FROM dbo.users WHERE Username = 'bwatson')),  
  ((SELECT MeetingID FROM dbo.meeting WHERE Purpose = 'Green Energy Meeting'), (SELECT UserID FROM dbo.users WHERE Username = 'cclark')),  
  ((SELECT MeetingID FROM dbo.meeting WHERE Purpose = 'Tech Upgrade Meeting'), (SELECT UserID FROM dbo.users WHERE Username = 'dlee')),  
  ((SELECT MeetingID FROM dbo.meeting WHERE Purpose = 'Outreach Planning Meeting'), (SELECT UserID FROM dbo.users WHERE Username = 'efisher')),  
  ((SELECT MeetingID FROM dbo.meeting WHERE Purpose = 'Research Development Meeting'), (SELECT UserID FROM dbo.users WHERE Username = 'gmitchell')),  
  ((SELECT MeetingID FROM dbo.meeting WHERE Purpose = 'Infrastructure Meeting'), (SELECT UserID FROM dbo.users WHERE Username = 'hmorris')),  
  ((SELECT MeetingID FROM dbo.meeting WHERE Purpose = 'Water Conservation Meeting'), (SELECT UserID FROM dbo.users WHERE Username = 'ijackson'));  



  INSERT INTO dbo.meetingMinutes (MeetingID, UserID, MinutesDate)
VALUES
  ((SELECT MeetingID FROM dbo.meeting WHERE Purpose = 'Health Initiative Meeting'), (SELECT UserID FROM dbo.users WHERE Username = 'efisher'), '2025-08-01'),  
  ((SELECT MeetingID FROM dbo.meeting WHERE Purpose = 'Green Energy Meeting'), (SELECT UserID FROM dbo.users WHERE Username = 'gmitchell'), '2025-09-01'),  
  ((SELECT MeetingID FROM dbo.meeting WHERE Purpose = 'Tech Upgrade Meeting'), (SELECT UserID FROM dbo.users WHERE Username = 'hmorris'), '2025-10-01'),  
  ((SELECT MeetingID FROM dbo.meeting WHERE Purpose = 'Outreach Planning Meeting'), (SELECT UserID FROM dbo.users WHERE Username = 'ijackson'), '2025-11-01'),  
  ((SELECT MeetingID FROM dbo.meeting WHERE Purpose = 'Research Development Meeting'), (SELECT UserID FROM dbo.users WHERE Username = 'jdoe'), '2025-12-01'),  
  ((SELECT MeetingID FROM dbo.meeting WHERE Purpose = 'Infrastructure Meeting'), (SELECT UserID FROM dbo.users WHERE Username = 'asmith'), '2026-01-01'),  
  ((SELECT MeetingID FROM dbo.meeting WHERE Purpose = 'Water Conservation Meeting'), (SELECT UserID FROM dbo.users WHERE Username = 'mjones'), '2026-02-01');


  INSERT INTO dbo.notes (ProjectID, Content, noteDate)
VALUES
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Education'), 'Initial planning discussions and key deliverables outlined', '2025-05-01'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'New System'), 'Final report draft completed for review', '2025-06-01'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Renovation'), 'Renovation plan finalized', '2025-07-01'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Health Initiative'), 'Health assessment results reviewed', '2025-08-01'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Green Energy'), 'Energy plans approved', '2025-09-01'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Tech Upgrade'), 'Technology needs assessed and documented', '2025-10-01'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Community Outreach'), 'Outreach strategy created', '2025-11-01'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Research Development'), 'Research goals defined', '2025-12-01'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Infrastructure Improvement'), 'Infrastructure improvement plan developed', '2026-01-01'),
  ((SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Water Conservation'), 'Conservation strategy outlined', '2026-02-01');


  INSERT INTO dbo.grants (SupplierID, ProjectID, SubmissionDate, AwardDate, Amount, Category, descriptions)
VALUES
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'TechCorp'), (SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Education'), '2025-04-01', '2025-05-01', 100000, 'Federal', 'N/A'),
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'InnovaTech'), (SELECT ProjectID FROM dbo.project WHERE ProjectName = 'New System'), '2025-05-01', '2025-06-01', 50000, 'State', 'N/A'),
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'GreenSolutions'), (SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Renovation'), '2025-06-01', '2025-07-01', 75000, 'Private', 'N/A'),
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'HealthCorp'), (SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Health Initiative'), '2025-07-01', '2025-08-01', 120000, 'Private', 'N/A'),
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'EduFund'), (SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Green Energy'), '2025-08-01', '2025-09-01', 95000, 'Government', 'N/A'),
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'BioTech'), (SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Tech Upgrade'), '2025-09-01', '2025-10-01', 110000, 'Private', 'N/A'),
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'AgriGrants'), (SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Community Outreach'), '2025-10-01', '2025-11-01', 85000, 'Private', 'N/A'),
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'CleanEnergy'), (SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Research Development'), '2025-11-01', '2025-12-01', 105000, 'Private', 'N/A'),
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'SmartTech'), (SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Infrastructure Improvement'), '2025-12-01', '2026-01-01', 98000, 'Private', 'N/A'),
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'EcoFunds'), (SELECT ProjectID FROM dbo.project WHERE ProjectName = 'Water Conservation'), '2026-01-01', '2026-02-01', 115000, 'Private', 'N/A');


  INSERT INTO dbo.grantStatus (GrantID, StatusName)
VALUES
  ((SELECT GrantID FROM dbo.grants WHERE Amount = 100000), 'Pending'),
  ((SELECT GrantID FROM dbo.grants WHERE Amount = 50000), 'Approved'),
  ((SELECT GrantID FROM dbo.grants WHERE Amount = 75000), 'In Progress'),
  ((SELECT GrantID FROM dbo.grants WHERE Amount = 120000), 'Approved'),
  ((SELECT GrantID FROM dbo.grants WHERE Amount = 95000), 'Pending'),
  ((SELECT GrantID FROM dbo.grants WHERE Amount = 110000), 'In Progress'),
  ((SELECT GrantID FROM dbo.grants WHERE Amount = 85000), 'Completed'),
  ((SELECT GrantID FROM dbo.grants WHERE Amount = 105000), 'Pending'),
  ((SELECT GrantID FROM dbo.grants WHERE Amount = 98000), 'Approved'),
  ((SELECT GrantID FROM dbo.grants WHERE Amount = 115000), 'In Progress');


  INSERT INTO dbo.supplierStatus (SupplierID, StatusName)
VALUES
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'TechCorp'), 'Active'),
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'InnovaTech'), 'Inactive'),
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'GreenSolutions'), 'Active'),
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'HealthCorp'), 'Inactive'),
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'EduFund'), 'Active'),
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'BioTech'), 'Active'),
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'AgriGrants'), 'Active'),
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'CleanEnergy'), 'Active'),
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'SmartTech'), 'Inactive'),
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'EcoFunds'), 'Active');



  INSERT INTO dbo.userMessage (SenderID, RecipientID, SubjectTitle, Contents)
VALUES
  ((SELECT UserID FROM dbo.users WHERE Username = 'jdoe'), (SELECT UserID FROM dbo.users WHERE Username = 'asmith'), 'Project Update', 'Here is the latest update on the project.'),
  ((SELECT UserID FROM dbo.users WHERE Username = 'mjones'), (SELECT UserID FROM dbo.users WHERE Username = 'bwatson'), 'Task Review', 'Please review the task progress.'),
  ((SELECT UserID FROM dbo.users WHERE Username = 'cclark'), (SELECT UserID FROM dbo.users WHERE Username = 'dlee'), 'Meeting Schedule', 'Our next meeting is scheduled for next week.'),
  ((SELECT UserID FROM dbo.users WHERE Username = 'efisher'), (SELECT UserID FROM dbo.users WHERE Username = 'gmitchell'), 'Research Findings', 'Here are the latest research findings.'),
  ((SELECT UserID FROM dbo.users WHERE Username = 'hmorris'), (SELECT UserID FROM dbo.users WHERE Username = 'ijackson'), 'Budget Approval', 'The budget has been approved.'),
  ((SELECT UserID FROM dbo.users WHERE Username = 'kwhite'), (SELECT UserID FROM dbo.users WHERE Username = 'lgreen'), 'Client Feedback', 'The client has provided feedback on the recent deliverables.'),
  ((SELECT UserID FROM dbo.users WHERE Username = 'mmartin'), (SELECT UserID FROM dbo.users WHERE Username = 'mjones'), 'Project Planning', 'We need to start planning the next phase of the project.'),
  ((SELECT UserID FROM dbo.users WHERE Username = 'jdoe'), (SELECT UserID FROM dbo.users WHERE Username = 'bwatson'), 'System Update', 'The system update will occur tonight.'),
  ((SELECT UserID FROM dbo.users WHERE Username = 'asmith'), (SELECT UserID FROM dbo.users WHERE Username = 'cclark'), 'Training Session', 'There is a training session scheduled for tomorrow.'),
  ((SELECT UserID FROM dbo.users WHERE Username = 'mjones'), (SELECT UserID FROM dbo.users WHERE Username = 'dlee'), 'Meeting Minutes', 'Please find the attached meeting minutes from our last meeting.');

