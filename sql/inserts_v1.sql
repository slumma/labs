-- Insert users into the users table
INSERT INTO dbo.users (Username, Password, FirstName, LastName)
VALUES
('jdoe', 'password123', 'John', 'Doe'),
('asmith', 'password456', 'Alice', 'Smith'),
('mjones', 'password789', 'Mike', 'Jones'),
('bwatson', 'password101', 'Barbara', 'Watson'),
('cclark', 'password112', 'Charles', 'Clark');


-- Insert suppliers into the grantSupplier table
INSERT INTO dbo.grantSupplier (SupplierName, OrgType)
VALUES
('TechCorp', 'Private'),
('InnovaTech', 'Government'),
('GreenSolutions', 'Private');


-- Insert projects into the project table
INSERT INTO dbo.project (DueDate)
VALUES
('2025-06-01'),
('2025-07-01'),
('2025-08-01');


-- Insert into BPrep using subqueries
INSERT INTO dbo.BPrep (UserID, CommunicationStatus)
VALUES 
  ((SELECT UserID FROM dbo.users WHERE Username = 'jdoe'), 'Not Started'),
  ((SELECT UserID FROM dbo.users WHERE Username = 'asmith'), 'In Progress'),
  ((SELECT UserID FROM dbo.users WHERE Username = 'mjones'), 'Completed');

-- Insert into employee using subqueries
INSERT INTO dbo.employee (UserID, AdminStatus)
VALUES 
  ((SELECT UserID FROM dbo.users WHERE Username = 'bwatson'), 1),
  ((SELECT UserID FROM dbo.users WHERE Username = 'cclark'), 0);

-- Insert into faculty using subqueries
INSERT INTO dbo.faculty (UserID)
VALUES 
  ((SELECT UserID FROM dbo.users WHERE Username = 'jdoe')),
  ((SELECT UserID FROM dbo.users WHERE Username = 'asmith'));

-- Insert into nonfaculty using subqueries
INSERT INTO dbo.nonfaculty (UserID)
VALUES 
  ((SELECT UserID FROM dbo.users WHERE Username = 'mjones'));


-- Insert into projectStaff using subqueries
INSERT INTO dbo.projectStaff (ProjectID, UserID, Leader, Active)
VALUES 
  ((SELECT ProjectID FROM dbo.project WHERE DueDate = '2025-06-01'), (SELECT UserID FROM dbo.users WHERE Username = 'jdoe'), 1, 1),  -- jdoe is Leader for Project 1
  ((SELECT ProjectID FROM dbo.project WHERE DueDate = '2025-07-01'), (SELECT UserID FROM dbo.users WHERE Username = 'asmith'), 0, 1);  -- asmith is Not Leader for Project 2

-- Insert into task
INSERT INTO dbo.task (ProjectID, DueDate, Objective, Staff)
VALUES
  ((SELECT ProjectID FROM dbo.project WHERE DueDate = '2025-06-01'), '2025-05-15', 'Initial Planning Phase', 'jdoe'),
  ((SELECT ProjectID FROM dbo.project WHERE DueDate = '2025-07-01'), '2025-06-15', 'Prepare Final Report', 'asmith');

-- Insert into TaskStaff using subqueries
INSERT INTO dbo.TaskStaff (TaskID, UserID, TaskOverview, DueDate)
VALUES
  ((SELECT TaskID FROM dbo.task WHERE Objective = 'Initial Planning Phase'), (SELECT UserID FROM dbo.users WHERE Username = 'bwatson'), 'Outline project requirements', '2025-05-10'),  -- bwatson assigned to Task 1
  ((SELECT TaskID FROM dbo.task WHERE Objective = 'Prepare Final Report'), (SELECT UserID FROM dbo.users WHERE Username = 'cclark'), 'Review final report draft', '2025-06-10');  -- cclark assigned to Task 2

-- Insert into meeting
INSERT INTO dbo.meeting (ProjectID, MeetingDate, Purpose)
VALUES
  ((SELECT ProjectID FROM dbo.project WHERE DueDate = '2025-06-01'), '2025-05-01', 'Project Kickoff Meeting'),
  ((SELECT ProjectID FROM dbo.project WHERE DueDate = '2025-07-01'), '2025-06-01', 'Progress Check-in Meeting');

-- Insert into attendance using subqueries
INSERT INTO dbo.attendance (MeetingID, UserID)
VALUES
  ((SELECT MeetingID FROM dbo.meeting WHERE MeetingDate = '2025-05-01'), (SELECT UserID FROM dbo.users WHERE Username = 'jdoe')),  -- jdoe attending Project 1 kickoff
  ((SELECT MeetingID FROM dbo.meeting WHERE MeetingDate = '2025-06-01'), (SELECT UserID FROM dbo.users WHERE Username = 'asmith'));  -- asmith attending Project 2 check-in

-- Insert into meetingMinutes using subqueries
INSERT INTO dbo.meetingMinutes (MeetingID, UserID, MinutesDate)
VALUES
  ((SELECT MeetingID FROM dbo.meeting WHERE MeetingDate = '2025-05-01'), (SELECT UserID FROM dbo.users WHERE Username = 'bwatson'), '2025-05-01'),  -- bwatson took minutes for Project 1 kickoff
  ((SELECT MeetingID FROM dbo.meeting WHERE MeetingDate = '2025-06-01'), (SELECT UserID FROM dbo.users WHERE Username = 'cclark'), '2025-06-01');  -- cclark took minutes for Project 2 check-in

-- Insert into notes
INSERT INTO dbo.notes (ProjectID, Content)
VALUES
  ((SELECT ProjectID FROM dbo.project WHERE DueDate = '2025-06-01'), 'Initial planning discussions and key deliverables outlined'),
  ((SELECT ProjectID FROM dbo.project WHERE DueDate = '2025-07-01'), 'Final report draft completed for review');

-- Insert into grants
INSERT INTO dbo.grants (SupplierID, ProjectID, SubmissionDate, AwardDate, Amount)
VALUES
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'TechCorp'), (SELECT ProjectID FROM dbo.project WHERE DueDate = '2025-06-01'), '2025-04-01', '2025-05-01', 100000),
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'InnovaTech'), (SELECT ProjectID FROM dbo.project WHERE DueDate = '2025-07-01'), '2025-05-01', '2025-06-01', 50000);

-- Insert into grantStatus using subqueries
INSERT INTO dbo.grantStatus (GrantID, StatusName)
VALUES
  ((SELECT GrantID FROM dbo.grants WHERE SubmissionDate = '2025-04-01'), 'Pending'),
  ((SELECT GrantID FROM dbo.grants WHERE SubmissionDate = '2025-05-01'), 'Approved');

-- Insert into supplierStatus using subqueries
INSERT INTO dbo.supplierStatus (SupplierID, StatusName)
VALUES
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'TechCorp'), 'Active'),
  ((SELECT SupplierID FROM dbo.grantSupplier WHERE SupplierName = 'InnovaTech'), 'Inactive');

-- Insert into userMessage
INSERT INTO dbo.userMessage (SenderID, RecipientID, SubjectTitle, Contents)
VALUES
  ((SELECT UserID FROM dbo.users WHERE Username = 'jdoe'), (SELECT UserID FROM dbo.users WHERE Username = 'asmith'), 'Project Update', 'Here is the latest update on the project.'),
  ((SELECT UserID FROM dbo.users WHERE Username = 'mjones'), (SELECT UserID FROM dbo.users WHERE Username = 'bwatson'), 'Task Review', 'Please review the task progress.');




