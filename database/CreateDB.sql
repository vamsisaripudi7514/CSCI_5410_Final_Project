CREATE DATABASE IF NOT EXISTS AttendanceSystem;
USE AttendanceSystem;

-- Master table for user details (pre-registered details)
CREATE TABLE IF NOT EXISTS UserMaster (
    master_id INT AUTO_INCREMENT PRIMARY KEY,
    full_name VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL UNIQUE,
    role ENUM('student', 'teacher') NOT NULL
);
--GPT
-- Users table for authentication (stores credentials)
CREATE TABLE IF NOT EXISTS Users (
    user_id INT PRIMARY KEY,
    username VARCHAR(255) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES UserMaster(master_id) ON DELETE CASCADE
);

-- Classes table (teachers create classes)
CREATE TABLE IF NOT EXISTS Classes (
    class_id INT AUTO_INCREMENT PRIMARY KEY,
    class_name VARCHAR(255) NOT NULL,
    teacher_id INT NOT NULL, -- references Users table (the teacher's credentials)
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (teacher_id) REFERENCES Users(user_id) ON DELETE CASCADE
);

-- Enrollments table (links students to classes)
CREATE TABLE IF NOT EXISTS Enrollments (
    enrollment_id INT AUTO_INCREMENT PRIMARY KEY,
    class_id INT NOT NULL,
    student_id INT NOT NULL, -- references Users table (the student's credentials)
    enrollment_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (class_id) REFERENCES Classes(class_id) ON DELETE CASCADE,
    FOREIGN KEY (student_id) REFERENCES Users(user_id) ON DELETE CASCADE
);

-- AttendanceSessions table (holds each attendance session for a class)
CREATE TABLE IF NOT EXISTS AttendanceSessions (
    session_id INT AUTO_INCREMENT PRIMARY KEY,
    class_id INT NOT NULL,
    session_date DATETIME NOT NULL,
    notes VARCHAR(1024),
    FOREIGN KEY (class_id) REFERENCES Classes(class_id) ON DELETE CASCADE
);

-- AttendanceRecords table (records individual student attendance)
CREATE TABLE IF NOT EXISTS AttendanceRecords (
    record_id INT AUTO_INCREMENT PRIMARY KEY,
    session_id INT NOT NULL,
    student_id INT NOT NULL,  -- references Users table
    status ENUM('Present', 'Absent') NOT NULL,
    FOREIGN KEY (session_id) REFERENCES AttendanceSessions(session_id) ON DELETE CASCADE,
    FOREIGN KEY (student_id) REFERENCES Users(user_id) ON DELETE CASCADE
);
