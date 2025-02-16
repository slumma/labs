-- show user messages
SELECT 
    um.MessageID,
    sender.Username AS Sender,
    recipient.Username AS Recipient,
    um.SubjectTitle,
	um.Contents,
    um.SentTime
FROM dbo.userMessage um
JOIN dbo.users sender ON um.SenderID = sender.UserID
JOIN dbo.users recipient ON um.RecipientID = recipient.UserID
ORDER BY um.SentTime DESC;


-- show projects with grant suppliers 
SELECT 
    g.GrantID,
    gs.SupplierName,
    p.ProjectID,
    p.DueDate,
    g.SubmissionDate,
    g.AwardDate,
    g.Amount
FROM dbo.grants g
JOIN dbo.grantSupplier gs ON g.SupplierID = gs.SupplierID
JOIN dbo.project p ON g.ProjectID = p.ProjectID
ORDER BY g.AwardDate DESC;


-- show users on project
SELECT 
    g.GrantID,
    gs.SupplierName,
    p.ProjectID,
    p.DueDate,
    ps.UserID AS ProjectStaffUserID,
    u.FirstName + ' ' + u.LastName AS ProjectStaffName,
    ps.Leader,
    ps.Active
FROM dbo.grants g
JOIN dbo.grantSupplier gs ON g.SupplierID = gs.SupplierID
JOIN dbo.project p ON g.ProjectID = p.ProjectID
JOIN dbo.projectStaff ps ON ps.ProjectID = p.ProjectID
JOIN dbo.users u ON ps.UserID = u.UserID
ORDER BY p.DueDate ASC, ps.Leader DESC;


-- view meetings and their attendees
SELECT 
    m.MeetingID,
    m.MeetingDate,
    m.Purpose,
    u.FirstName + ' ' + u.LastName AS AttendeeName,
    a.AttendanceID,
    CASE 
        WHEN a.UserID IS NOT NULL THEN 'Attended'
        ELSE 'Not Attended'
    END AS AttendanceStatus
FROM dbo.meeting m
JOIN dbo.attendance a ON m.MeetingID = a.MeetingID
JOIN dbo.users u ON a.UserID = u.UserID
ORDER BY m.MeetingDate DESC, AttendanceStatus ASC;


-- another, cleaner, message query showing conversations between users.
SELECT 
    um.MessageID,
    um.SubjectTitle,
    um.SentTime,
    u1.FirstName + ' ' + u1.LastName AS SenderName,
    u2.FirstName + ' ' + u2.LastName AS RecipientName,
    um.Contents
FROM dbo.userMessage um
JOIN dbo.users u1 ON um.SenderID = u1.UserID
JOIN dbo.users u2 ON um.RecipientID = u2.UserID
ORDER BY um.SentTime DESC;
