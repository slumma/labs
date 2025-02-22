SELECT 
    g.GrantID, 
	p.ProjectID,
    s.SupplierName AS Supplier, 
    p.ProjectName AS Project, 
    g.Amount,
	g.Category,
    gstat.StatusName, 
    g.descriptions,
    g.SubmissionDate, 
    g.AwardDate
FROM grants g
JOIN grantSupplier s ON g.SupplierID = s.SupplierID
JOIN grantStatus gstat ON g.GrantID = gstat.GrantID
JOIN project p ON g.ProjectID = p.ProjectID
LEFT JOIN grantStatus gs ON g.GrantID = gs.GrantID;


SELECT 
    u.UserID,
    u.Username,
    u.FirstName,
    u.LastName,
    u.Email,
    u.Phone,
    u.HomeAddress,
    ps.ProjectID,
    p.ProjectName,
    ps.Leader,
    ps.Active
FROM 
    projectStaff ps
JOIN 
    faculty f ON ps.UserID = f.UserID
JOIN 
    users u ON ps.UserID = u.UserID
JOIN 
    project p ON ps.ProjectID = p.ProjectID;

	
WHERE p.ProjectID = 10;


SELECT * from projectStaff WHERE ProjectID = 3;

INSERT INTO projectStaff (UserID, ProjectID, Leader, Active)
VALUES (3, 3, 1, 1);


SELECT * FROM project WHERE ProjectID = 3;

SELECT u.* FROM users u INNER JOIN  faculty f ON u.UserID = f.UserID;


-- sender
SELECT 
    userMessage.*,
    sender.Username AS SenderUsername,
    recipient.Username AS RecipientUsername
FROM 
    userMessage
JOIN 
    Users AS sender ON userMessage.SenderID = sender.UserID
JOIN 
    Users AS recipient ON userMessage.RecipientID = recipient.UserID
WHERE 
    userMessage.RecipientID = 1;

-- recipient 
SELECT 
    userMessage.RecipientID, 
	userMessage.SubjectTitle,
	userMessage.Contents,
    recipient.UserID, 
    recipient.Username 
FROM 
    userMessage
JOIN 
    Users AS recipient ON userMessage.RecipientID = recipient.UserID
WHERE 
    userMessage.RecipientID = 1;



	--CREATE TABLE grants(
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

select * from grantSupplier;

SELECT SupplierID, SupplierName FROM grantSupplier WHERE SupplierID = 1;


select * from grants;


SELECT 
    g.GrantID, 
    p.ProjectID,
    s.SupplierName AS Supplier, 
    p.ProjectName AS Project, 
    g.Amount,
    g.Category,
    g.StatusName, 
    g.descriptions,
    g.SubmissionDate, 
    g.AwardDate
FROM grants g
JOIN grantSupplier s ON g.SupplierID = s.SupplierID
LEFT JOIN project p ON g.ProjectID = p.ProjectID;


INSERT INTO grants (SupplierID, StatusName, Category, SubmissionDate, descriptions, AwardDate, Amount)
                              VALUES (1, 'Active', 'State', '2025-04-01', 'd', '2025-04-01', 1233);


select * from users;