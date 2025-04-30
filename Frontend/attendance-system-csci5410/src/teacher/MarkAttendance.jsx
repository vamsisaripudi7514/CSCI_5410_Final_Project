import react from "react";
import TeacherHeader from "./TeacherHeader";
import { useLocation } from "react-router-dom";
function MarkAttendance() {
    const location = useLocation();
    const {
        token,
        userId,
        username,
        role
    } = location.state || {};
    return (
        <div className="class-management">
            <TeacherHeader
                token={token}
                userId={userId}
                username={username}
                role={role}
            />
            <h2>Class Management</h2>
            <p>This is Attendance Marking Page</p>
        </div>
    );
}

export default MarkAttendance;