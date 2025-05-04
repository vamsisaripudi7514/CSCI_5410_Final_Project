CREATE DATABASE IF NOT EXISTS AttendanceSystem;
USE AttendanceSystem;


INSERT INTO UserMaster (master_id, full_name, email, role) VALUES 
    (30001, 'Teacher 1', 'teacher1@example.com', 'teacher'),
    (30002, 'Teacher 2', 'teacher2@example.com', 'teacher'),
    (30003, 'Teacher 3', 'teacher3@example.com', 'teacher');

INSERT INTO UserMaster (master_id, full_name, email, role) VALUES 
    (50001, 'Student 1_1', 'student1_1@example.com', 'student'),
    (50002, 'Student 1_2', 'student1_2@example.com', 'student'),
    (50003, 'Student 1_3', 'student1_3@example.com', 'student'),
    (50004, 'Student 1_4', 'student1_4@example.com', 'student'),
    (50005, 'Student 1_5', 'student1_5@example.com', 'student'),
    (50006, 'Student 1_6', 'student1_6@example.com', 'student'),
    (50007, 'Student 1_7', 'student1_7@example.com', 'student'),
    (50008, 'Student 1_8', 'student1_8@example.com', 'student'),
    (50009, 'Student 1_9', 'student1_9@example.com', 'student'),
    (50010, 'Student 1_10', 'student1_10@example.com', 'student');

INSERT INTO UserMaster (master_id, full_name, email, role) VALUES 
    (50011, 'Student 2_1', 'student2_1@example.com', 'student'),
    (50012, 'Student 2_2', 'student2_2@example.com', 'student'),
    (50013, 'Student 2_3', 'student2_3@example.com', 'student'),
    (50014, 'Student 2_4', 'student2_4@example.com', 'student'),
    (50015, 'Student 2_5', 'student2_5@example.com', 'student'),
    (50016, 'Student 2_6', 'student2_6@example.com', 'student'),
    (50017, 'Student 2_7', 'student2_7@example.com', 'student'),
    (50018, 'Student 2_8', 'student2_8@example.com', 'student'),
    (50019, 'Student 2_9', 'student2_9@example.com', 'student'),
    (50020, 'Student 2_10', 'student2_10@example.com', 'student');

INSERT INTO UserMaster (master_id, full_name, email, role) VALUES 
    (50021, 'Student 3_1', 'student3_1@example.com', 'student'),
    (50022, 'Student 3_2', 'student3_2@example.com', 'student'),
    (50023, 'Student 3_3', 'student3_3@example.com', 'student'),
    (50024, 'Student 3_4', 'student3_4@example.com', 'student'),
    (50025, 'Student 3_5', 'student3_5@example.com', 'student'),
    (50026, 'Student 3_6', 'student3_6@example.com', 'student'),
    (50027, 'Student 3_7', 'student3_7@example.com', 'student'),
    (50028, 'Student 3_8', 'student3_8@example.com', 'student'),
    (50029, 'Student 3_9', 'student3_9@example.com', 'student'),
    (50030, 'Student 3_10', 'student3_10@example.com', 'student');


INSERT INTO Users (user_id, username, password_hash) VALUES 
    (30001, 'teacher1', 'hash1'),
    (30002, 'teacher2', 'hash2'),
    (30003, 'teacher3', 'hash3');

INSERT INTO Users (user_id, username, password_hash) VALUES
    (50001, 'student1_1', 'hash50001'),
    (50002, 'student1_2', 'hash50002'),
    (50003, 'student1_3', 'hash50003'),
    (50004, 'student1_4', 'hash50004'),
    (50005, 'student1_5', 'hash50005'),
    (50006, 'student1_6', 'hash50006'),
    (50007, 'student1_7', 'hash50007'),
    (50008, 'student1_8', 'hash50008'),
    (50009, 'student1_9', 'hash50009'),
    (50010, 'student1_10', 'hash50010');

INSERT INTO Users (user_id, username, password_hash) VALUES
    (50011, 'student2_1', 'hash50011'),
    (50012, 'student2_2', 'hash50012'),
    (50013, 'student2_3', 'hash50013'),
    (50014, 'student2_4', 'hash50014'),
    (50015, 'student2_5', 'hash50015'),
    (50016, 'student2_6', 'hash50016'),
    (50017, 'student2_7', 'hash50017'),
    (50018, 'student2_8', 'hash50018'),
    (50019, 'student2_9', 'hash50019'),
    (50020, 'student2_10', 'hash50020');

INSERT INTO Users (user_id, username, password_hash) VALUES
    (50021, 'student3_1', 'hash50021'),
    (50022, 'student3_2', 'hash50022'),
    (50023, 'student3_3', 'hash50023'),
    (50024, 'student3_4', 'hash50024'),
    (50025, 'student3_5', 'hash50025'),
    (50026, 'student3_6', 'hash50026'),
    (50027, 'student3_7', 'hash50027'),
    (50028, 'student3_8', 'hash50028'),
    (50029, 'student3_9', 'hash50029'),
    (50030, 'student3_10', 'hash50030');


INSERT INTO Classes (class_id, class_name, teacher_id) VALUES
    (1001, 'Class 1', 30001),
    (1002, 'Class 2', 30002),
    (1003, 'Class 3', 30003);



INSERT INTO Enrollments (enrollment_id, class_id, student_id) VALUES
    (1, 1001, 50001),
    (2, 1001, 50002),
    (3, 1001, 50003),
    (4, 1001, 50004),
    (5, 1001, 50005),
    (6, 1001, 50006),
    (7, 1001, 50007),
    (8, 1001, 50008),
    (9, 1001, 50009),
    (10, 1001, 50010),
    
    (11, 1002, 50011),
    (12, 1002, 50012),
    (13, 1002, 50013),
    (14, 1002, 50014),
    (15, 1002, 50015),
    (16, 1002, 50016),
    (17, 1002, 50017),
    (18, 1002, 50018),
    (19, 1002, 50019),
    (20, 1002, 50020),
    
    (21, 1003, 50021),
    (22, 1003, 50022),
    (23, 1003, 50023),
    (24, 1003, 50024),
    (25, 1003, 50025),
    (26, 1003, 50026),
    (27, 1003, 50027),
    (28, 1003, 50028),
    (29, 1003, 50029),
    (30, 1003, 50030);



INSERT INTO AttendanceSessions (session_id, class_id, session_date, notes) VALUES
    (2001, 1001, '2025-04-01 09:00:00', 'Session 1 for Class 1'),
    (2002, 1001, '2025-04-02 09:00:00', 'Session 2 for Class 1'),
    
    (2003, 1002, '2025-04-01 10:00:00', 'Session 1 for Class 2'),
    (2004, 1002, '2025-04-02 10:00:00', 'Session 2 for Class 2'),
    
    (2005, 1003, '2025-04-01 11:00:00', 'Session 1 for Class 3'),
    (2006, 1003, '2025-04-02 11:00:00', 'Session 2 for Class 3');



INSERT INTO AttendanceRecords (record_id, session_id, student_id, status) VALUES
    (3001, 2001, 50001, 'Present'),
    (3002, 2001, 50002, 'Present'),
    (3003, 2001, 50003, 'Present'),
    (3004, 2001, 50004, 'Present'),
    (3005, 2001, 50005, 'Present'),
    (3006, 2001, 50006, 'Present'),
    (3007, 2001, 50007, 'Present'),
    (3008, 2001, 50008, 'Present'),
    (3009, 2001, 50009, 'Present'),
    (3010, 2001, 50010, 'Present');

INSERT INTO AttendanceRecords (record_id, session_id, student_id, status) VALUES
    (3011, 2002, 50001, 'Absent'),
    (3012, 2002, 50002, 'Present'),
    (3013, 2002, 50003, 'Absent'),
    (3014, 2002, 50004, 'Present'),
    (3015, 2002, 50005, 'Absent'),
    (3016, 2002, 50006, 'Present'),
    (3017, 2002, 50007, 'Absent'),
    (3018, 2002, 50008, 'Present'),
    (3019, 2002, 50009, 'Absent'),
    (3020, 2002, 50010, 'Present');

INSERT INTO AttendanceRecords (record_id, session_id, student_id, status) VALUES
    (3021, 2003, 50011, 'Present'),
    (3022, 2003, 50012, 'Present'),
    (3023, 2003, 50013, 'Present'),
    (3024, 2003, 50014, 'Present'),
    (3025, 2003, 50015, 'Present'),
    (3026, 2003, 50016, 'Present'),
    (3027, 2003, 50017, 'Present'),
    (3028, 2003, 50018, 'Present'),
    (3029, 2003, 50019, 'Present'),
    (3030, 2003, 50020, 'Present');

INSERT INTO AttendanceRecords (record_id, session_id, student_id, status) VALUES
    (3031, 2004, 50011, 'Absent'),
    (3032, 2004, 50012, 'Present'),
    (3033, 2004, 50013, 'Absent'),
    (3034, 2004, 50014, 'Present'),
    (3035, 2004, 50015, 'Absent'),
    (3036, 2004, 50016, 'Present'),
    (3037, 2004, 50017, 'Absent'),
    (3038, 2004, 50018, 'Present'),
    (3039, 2004, 50019, 'Absent'),
    (3040, 2004, 50020, 'Present');

INSERT INTO AttendanceRecords (record_id, session_id, student_id, status) VALUES
    (3041, 2005, 50021, 'Present'),
    (3042, 2005, 50022, 'Present'),
    (3043, 2005, 50023, 'Present'),
    (3044, 2005, 50024, 'Present'),
    (3045, 2005, 50025, 'Present'),
    (3046, 2005, 50026, 'Present'),
    (3047, 2005, 50027, 'Present'),
    (3048, 2005, 50028, 'Present'),
    (3049, 2005, 50029, 'Present'),
    (3050, 2005, 50030, 'Present');

INSERT INTO AttendanceRecords (record_id, session_id, student_id, status) VALUES
    (3051, 2006, 50021, 'Absent'),
    (3052, 2006, 50022, 'Present'),
    (3053, 2006, 50023, 'Absent'),
    (3054, 2006, 50024, 'Present'),
    (3055, 2006, 50025, 'Absent'),
    (3056, 2006, 50026, 'Present'),
    (3057, 2006, 50027, 'Absent'),
    (3058, 2006, 50028, 'Present'),
    (3059, 2006, 50029, 'Absent'),
    (3060, 2006, 50030, 'Present');
