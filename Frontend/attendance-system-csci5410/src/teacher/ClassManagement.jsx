import react, { use } from "react";
import TeacherHeader from "./TeacherHeader";
import { useLocation } from "react-router-dom";
import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";
function ClassManagement() {
    const location = useLocation();
    const navigate = useNavigate();
    const {
        token,
        userId,
        username,
        role
    } = location.state || {};
    useEffect(() => {
        fetchClasses();
    }, []);
    async function fetchClasses() {
        try {
            const response = await fetch("http://localhost:5092/teacher/get-teacher-classes", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`
                },
                body: JSON.stringify({
                    teacherId: userId
                })
            });
            if (!response.ok) {
                console.error("Failed to fetch classes:", response.statusText);
                return;
            }
            const data = await response.json();
            console.log("Classes data:", data);
            setClasses(data);
        }
        catch (error) {
            console.error("Error fetching classes:", error);
        }
    }
    const [classes, setClasses] = useState([]);
    const [searchTerm, setSearchTerm] = useState('');
    const filteredClasses = classes.filter((classItem) => {
        if (!searchTerm) return true;

        const idStr = classItem.classId ? classItem.classId.toString() : "";
        const nameStr = classItem.className ? classItem.className.toLowerCase() : "";
        const teacherStr = classItem.teacherId ? classItem.teacherId.toString() : "";

        const term = searchTerm.toLowerCase();

        return idStr.includes(term) || nameStr.includes(term) || teacherStr.includes(term);
    });
    async function handleGenerateReport(classId) {
        try {
            const response = await fetch("http://localhost:5092/report/get-class-report", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`
                },
                body: JSON.stringify({
                    classID: classId
                })
            });
            if (!response.ok) {
                console.error("Failed to generate report:", response.statusText);
                return;
            }
            const pdfBlob = await response.blob();

            const blobUrl = URL.createObjectURL(pdfBlob);

            window.open(blobUrl, '_blank')
        }
        catch (error) {
            console.error("Error generating report:", error);
        }
    }
    async function handleDeleteClass(classId) {
        if (!window.confirm("Are you sure you want to delete this class?")) {
            return;
        }
        try {
            const response = await fetch("http://localhost:5092/classes/delete-class", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`
                },
                body: JSON.stringify({
                    classID: classId,
                    teacherID: userId
                })
            });
            if (!response.ok) {
                console.error("Failed to delete class:", response.statusText);
                return;
            }
            const data = await response.json();
            console.log("Class deleted:", data);
            fetchClasses();
        }
        catch (error) {
            console.error("Error deleting class:", error);
        }
    }
    function handleMarkAttendance(classId) {
        console.log("Marking attendance for class ID:", classId);
        navigate("/mark-attendance", {
            state: {
                token: token,
                userId: userId,
                username: username,
                role: role,
                classId: classId
            }
        });
    }

    return (
        <div className="class-management">
            <TeacherHeader
                token={token}
                userId={userId}
                username={username}
                role={role}
            />
            {/* <h2>Class Management</h2> */}
            {/* <p>This is the Class Management page.</p> */}
            <div className="col-md-5 offset-md-2" style={{ margin: '50px auto' }}>
                <form >
                    <div className="row">
                        <div className="col-md-6">

                        </div>
                        <div className="col-md-6">

                        </div>

                    </div>
                    <div className="input-group">

                        <input type="search" className="form-control form-control-lg" placeholder="Enter Class ID or Class Name" aria-label="Search"
                            onChange={(e) => { setSearchTerm(e.target.value) }} />

                    </div>
                    <div className="float-right" style={{ marginBottom: "10px", marginTop: "10px" }}>
                        <Link to="/create-class" className="btn btn-primary"
                            state={{
                                tokenn: token,
                                userId: userId,
                                username: username,
                                role: role
                            }}
                        >Add Class</Link>
                    </div>

                </form>
            </div>
            <div className="card-body" style={{ width: "80%", margin: "0 auto" }}>
                <table className="table table-bordered">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Class Name</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {Array.isArray(classes) && filteredClasses.map((class_name, index) => (
                            <tr key={class_name.classId}>
                                <td>{class_name.classId}</td>
                                <td>{class_name.className}</td>
                                <td>
                                    <button className="btn btn-sm btn-success" style={{ marginLeft: "10px" }}
                                        onClick={() => { handleMarkAttendance(class_name.classId) }}>

                                        Mark Attendance</button>
                                    <button className="btn btn-sm btn-primary" style={{ marginLeft: "10px" }}
                                        onClick={() => { handleGenerateReport(class_name.classId) }}>

                                        Generate Report</button>


                                    <button className="btn btn-sm btn-danger" style={{ marginLeft: "10px" }}
                                        onClick={() => { handleDeleteClass(class_name.classId) }}>

                                        Delete</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
}

export default ClassManagement;