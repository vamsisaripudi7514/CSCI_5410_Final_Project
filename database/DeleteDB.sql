-- Disable foreign key checks to allow truncation in any order
SET FOREIGN_KEY_CHECKS = 0;
USE AttendanceSystem;

-- Delete all records from AttendanceRecords
TRUNCATE TABLE AttendanceRecords;

-- Delete all records from AttendanceSessions
TRUNCATE TABLE AttendanceSessions;

-- Delete all records from Enrollments
TRUNCATE TABLE Enrollments;

-- Delete all records from Classes
TRUNCATE TABLE Classes;

-- Delete all records from Users
TRUNCATE TABLE Users;

-- Delete all records from UserMaster
TRUNCATE TABLE UserMaster;

-- Re-enable foreign key checks
SET FOREIGN_KEY_CHECKS = 1;

-- DROP DATABASE IF EXISTS AttendanceSystem;
