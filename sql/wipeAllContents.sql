-- Disable foreign key checks temporarily to avoid constraint violations
EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';

-- Delete data from all tables in the correct order to avoid foreign key issues
DELETE FROM meetingMinutes;
DELETE FROM attendance;
DELETE FROM userMessage;
DELETE FROM grantStatus;
DELETE FROM supplierStatus;
DELETE FROM grants;
DELETE FROM notes;
DELETE FROM taskStaff;
DELETE FROM task;
DELETE FROM projectStaff;
DELETE FROM meeting;
DELETE FROM faculty;
DELETE FROM nonfaculty;
DELETE FROM employee;
DELETE FROM BPrep;
DELETE FROM project;
DELETE FROM grantSupplier;
DELETE FROM users;

-- Re-enable foreign key checks
EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL';
