import react, { useEffect } from "react";
import TeacherHeader from "./TeacherHeader";
import { useLocation } from "react-router-dom";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
function MarkAttendance() {
    const location = useLocation();
    const navigate = useNavigate();
    const [students, setStudents] = useState([]);
    const [attendance, setAttendance] = useState({});
    const {
        token,
        userId,
        username,
        role,
        classId
    } = location.state || {};
    console.log(token, userId, username, role, classId);
    useEffect(() => {

        async function fetchClassStudents() {
            try {
                const response = await fetch("http://localhost:5092/attendance/get-students", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                        Authorization: `Bearer ${token}`
                    },
                    body: JSON.stringify({
                        classId: classId
                    })
                });
                if (!response.ok) {
                    console.error("Failed to fetch class students:", response.statusText);
                    return;
                }
                const data = await response.json();
                console.log("Class students data:", data);
                setStudents(data);
                const defaultAttendance = {};
                data.forEach((student) => {
                    defaultAttendance[student.student_id] = "Present";
                });
                setAttendance(defaultAttendance);
            }
            catch (error) {
                console.error("Error fetching class students:", error);
            }
        }
        fetchClassStudents();

    }, []);
    const toggleAttendance = (studentId) => {
        setAttendance((prev) => ({
            ...prev,
            [studentId]: prev[studentId] === "Present" ? "Absent" : "Present"
        }));
    };
    const handleSubmit = async () => {
        const attendanceArray = Object.entries(attendance)
            .filter(([_, status]) => status === "Present")
            .map(([studentId, status]) => ({
                studentId: parseInt(studentId),
                status
            }));

        try {
            const response = await fetch("http://localhost:5092/attendance/mark-attendance", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`
                },
                body: JSON.stringify({
                    classID: classId,
                    studentIDs: attendanceArray.map(item => item.studentId),
                    date: new Date().toISOString(),
                    notes: "Attendance marked"
                })
            });

            if (response.ok) {
                alert("Attendance submitted!");
                navigate("/class-management", { state: { token, userId, username, role } });
            } else {
                console.error("Failed to submit attendance");
            }
        } catch (error) {
            console.error("Error submitting attendance:", error);
        }
    };

    return (
        <div className="class-management">
            <TeacherHeader
                token={token}
                userId={userId}
                username={username}
                role={role}
            />
            <div style={{ 
                margin: '50px auto', 
                width: '80%', 
                border: '1px solid black', 
                padding: '20px', 
                borderRadius: '10px', 
                background: 'linear-gradient(to right, #a1c4fd, #c2e9fb)' 
            }}>
                <div className="container-fluid">
                    <div className="row">
                        {students.map((student) => (
                            <div key={student.student_id} className="col-sm-3 col-md-3 col-lg-2">
                                <div
                                    className={`card text-white shadow student-card ${attendance[student.student_id] === "Present" ? "bg-success" : "bg-danger"
                                        }`}
                                    onClick={() => toggleAttendance(student.student_id)}
                                    style={{ cursor: "pointer" }}
                                >
                                    <div className="card-body d-flex align-items-center justify-content-center text-center p-3">
                                        <h5 className="card-title mb-0">{student.full_name}</h5>
                                    </div>
                                </div>
                            </div>
                        ))}
                    </div>

                    <div className="row mt-4">
                        <div className="col text-center">
                            <button
                                className="btn btn-primary btn-lg"
                                onClick={handleSubmit}
                            >
                                Submit Attendance
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default MarkAttendance;