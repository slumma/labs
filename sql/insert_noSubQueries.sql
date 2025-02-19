INSERT INTO users (Username, Password, FirstName, LastName, Email, Phone, HomeAddress) VALUES
('user1', 'pass1', 'John', 'Doe', 'john.doe@example.com', '1234567890', '123 Main St'),
('user2', 'pass2', 'Jane', 'Smith', 'jane.smith@example.com', '2345678901', '456 Elm St'),
('user3', 'pass3', 'Alice', 'Johnson', 'alice.johnson@example.com', '3456789012', '789 Oak St'),
('user4', 'pass4', 'Bob', 'Brown', 'bob.brown@example.com', '4567890123', '101 Pine St'),
('user5', 'pass5', 'Charlie', 'Davis', 'charlie.davis@example.com', '5678901234', '202 Maple St'),
('user6', 'pass6', 'Daniel', 'Miller', 'daniel.miller@example.com', '6789012345', '303 Birch St'),
('user7', 'pass7', 'Eve', 'Wilson', 'eve.wilson@example.com', '7890123456', '404 Cedar St'),
('user8', 'pass8', 'Frank', 'Moore', 'frank.moore@example.com', '8901234567', '505 Spruce St'),
('user9', 'pass9', 'Grace', 'Taylor', 'grace.taylor@example.com', '9012345678', '606 Fir St'),
('user10', 'pass10', 'Henry', 'Anderson', 'henry.anderson@example.com', '0123456789', '707 Willow St');


INSERT INTO grantSupplier (SupplierName, OrgType, BusinessAddress) VALUES
('Supplier1', 'Non-Profit', '123 Charity St'),
('Supplier2', 'Government', '456 Gov St'),
('Supplier3', 'Corporate', '789 Corp St'),
('Supplier4', 'Educational', '101 Edu St'),
('Supplier5', 'Non-Profit', '202 Help St'),
('Supplier6', 'Corporate', '303 Biz St'),
('Supplier7', 'Government', '404 Admin St'),
('Supplier8', 'Educational', '505 Learn St'),
('Supplier9', 'Non-Profit', '606 Aid St'),
('Supplier10', 'Corporate', '707 Industry St');

INSERT INTO project (ProjectName, DueDate) VALUES
('Project Alpha', '2025-03-01'),
('Project Beta', '2025-04-01'),
('Project Gamma', '2025-05-01'),
('Project Delta', '2025-06-01'),
('Project Epsilon', '2025-07-01'),
('Project Zeta', '2025-08-01'),
('Project Eta', '2025-09-01'),
('Project Theta', '2025-10-01'),
('Project Iota', '2025-11-01'),
('Project Kappa', '2025-12-01');

INSERT INTO BPrep (UserID, CommunicationStatus, SupplierID) VALUES
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

INSERT INTO employee (UserID, AdminStatus) VALUES
(1, 1),
(2, 0),
(3, 0),
(4, 1),
(5, 0),
(6, 0),
(7, 1),
(8, 0),
(9, 0),
(10, 1);

INSERT INTO faculty (UserID) VALUES
(1),
(3),
(5),
(7),
(9);


INSERT INTO nonfaculty (UserID) VALUES
(2),
(4),
(6),
(8),
(10);


INSERT INTO projectStaff (ProjectID, UserID, Leader, Active) VALUES
(1, 1, 1, 1),
(2, 3, 0, 1),
(3, 5, 0, 1),
(4, 7, 1, 0),
(5, 9, 0, 1);


INSERT INTO task (ProjectID, DueDate, Objective, Staff) VALUES
(1, '2025-03-01', 'Task Objective 1', 'Staff 1'),
(2, '2025-04-01', 'Task Objective 2', 'Staff 2'),
(3, '2025-05-01', 'Task Objective 3', 'Staff 3'),
(4, '2025-06-01', 'Task Objective 4', 'Staff 4'),
(5, '2025-07-01', 'Task Objective 5', 'Staff 5'),
(6, '2025-08-01', 'Task Objective 6', 'Staff 6'),
(7, '2025-09-01', 'Task Objective 7', 'Staff 7'),
(8, '2025-10-01', 'Task Objective 8', 'Staff 8'),
(9, '2025-11-01', 'Task Objective 9', 'Staff 9'),
(10, '2025-12-01', 'Task Objective 10', 'Staff 10');

INSERT INTO TaskStaff (TaskID, UserID, TaskOverview, DueDate) VALUES
(1, 1, 'Overview 1', '2025-03-01'),
(2, 2, 'Overview 2', '2025-04-01'),
(3, 3, 'Overview 3', '2025-05-01'),
(4, 4, 'Overview 4', '2025-06-01'),
(5, 5, 'Overview 5', '2025-07-01'),
(6, 6, 'Overview 6', '2025-08-01'),
(7, 7, 'Overview 7', '2025-09-01'),
(8, 8, 'Overview 8', '2025-10-01'),
(9, 9, 'Overview 9', '2025-11-01'),
(10, 10, 'Overview 10', '2025-12-01');

INSERT INTO meeting (ProjectID, MeetingDate, Purpose) VALUES
(1, '2025-03-01', 'Purpose 1'),
(2, '2025-04-01', 'Purpose 2'),
(3, '2025-05-01', 'Purpose 3'),
(4, '2025-06-01', 'Purpose 4'),
(5, '2025-07-01', 'Purpose 5'),
(6, '2025-08-01', 'Purpose 6'),
(7, '2025-09-01', 'Purpose 7'),
(8, '2025-10-01', 'Purpose 8'),
(9, '2025-11-01', 'Purpose 9'),
(10, '2025-12-01', 'Purpose 10');

INSERT INTO attendance (MeetingID, UserID) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10);

INSERT INTO meetingMinutes (MeetingID, UserID, MinutesDate) VALUES
(1, 1, '2025-03-01'),
(2, 2, '2025-04-01'),
(3, 3, '2025-05-01'),
(4, 4, '2025-06-01'),
(5, 5, '2025-07-01'),
(6, 6, '2025-08-01'),
(7, 7, '2025-09-01'),
(8, 8, '2025-10-01'),
(9, 9, '2025-11-01'),
(10, 10, '2025-12-01');

INSERT INTO notes (ProjectID, Content, noteDate) VALUES
(1, 'Note Content 1', '2025-03-01'),
(2, 'Note Content 2', '2025-04-01'),
(3, 'Note Content 3', '2025-05-01'),
(4, 'Note Content 4', '2025-06-01'),
(5, 'Note Content 5', '2025-07-01'),
(6, 'Note Content 6', '2025-08-01'),
(7, 'Note Content 7', '2025-09-01'),
(8, 'Note Content 8', '2025-10-01'),
(9, 'Note Content 9', '2025-11-01'),
(10, 'Note Content 10', '2025-12-01');

INSERT INTO grants (SupplierID, ProjectID, StatusName, Category, SubmissionDate, descriptions, AwardDate, Amount) VALUES
(1, 1, 'Submitted', 'Research', '2025-01-01', 'Description 1', '2025-01-15', 5000.00),
(2, 2, 'Approved', 'Development', '2025-02-01', 'Description 2', '2025-02-15', 7500.00),
(3, 3, 'Pending', 'Research', '2025-03-01', 'Description 3', '2025-03-15', 10000.00),
(4, 4, 'Rejected', 'Development', '2025-04-01', 'Description 4', '2025-04-15', 2000.00),
(5, 5, 'Approved', 'Research', '2025-05-01', 'Description 5', '2025-05-15', 15000.00),
(6, 6, 'Pending', 'Development', '2025-06-01', 'Description 6', '2025-06-15', 8000.00),
(7, 7, 'Submitted', 'Research', '2025-07-01', 'Description 7', '2025-07-15', 12000.00),
(8, 8, 'Approved', 'Development', '2025-08-01', 'Description 8', '2025-08-15', 6000.00),
(9, 9, 'Rejected', 'Research', '2025-09-01', 'Description 9', '2025-09-15', 3000.00),
(10, 10, 'Pending', 'Development', '2025-10-01', 'Description 10', '2025-10-15', 9000.00);

INSERT INTO grantStatus (GrantID, StatusName, ChangeDate) VALUES
(1, 'Submitted', '2025-01-01'),
(2, 'Approved', '2025-02-01'),
(3, 'Pending', '2025-03-01'),
(4, 'Rejected', '2025-04-01'),
(5, 'Approved', '2025-05-01'),
(6, 'Pending', '2025-06-01'),
(7, 'Submitted', '2025-07-01'),
(8, 'Approved', '2025-08-01'),
(9, 'Rejected', '2025-09-01'),
(10, 'Pending', '2025-10-01');

INSERT INTO supplierStatus (SupplierID, StatusName, ChangeDate) VALUES
(1, 'Active', '2025-01-01'),
(2, 'Inactive', '2025-02-01'),
(3, 'Active', '2025-03-01'),
(4, 'Inactive', '2025-04-01'),
(5, 'Active', '2025-05-01'),
(6, 'Inactive', '2025-06-01'),
(7, 'Active', '2025-07-01'),
(8, 'Inactive', '2025-08-01'),
(9, 'Active', '2025-09-01'),
(10, 'Inactive', '2025-10-01');

INSERT INTO userMessage (SenderID, RecipientID, SubjectTitle, Contents, SentTime) VALUES
(1, 2, 'Subject 1', 'Message Content 1', '2025-01-01'),
(2, 3, 'Subject 2', 'Message Content 2', '2025-02-01'),
(3, 4, 'Subject 3', 'Message Content 3', '2025-03-01'),
(4, 5, 'Subject 4', 'Message Content 4', '2025-04-01'),
(5, 6, 'Subject 5', 'Message Content 5', '2025-05-01'),
(6, 7, 'Subject 6', 'Message Content 6', '2025-06-01'),
(7, 8, 'Subject 7', 'Message Content 7', '2025-07-01'),
(8, 9, 'Subject 8', 'Message Content 8', '2025-08-01'),
(9, 10, 'Subject 9', 'Message Content 9', '2025-09-01'),
(10, 1, 'Subject 10', 'Message Content 10', '2025-10-01');

