import React, { use } from "react";
import { useLocation } from "react-router-dom";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import TeacherHeader from "./TeacherHeader";
function ClassStudents() {
    const location = useLocation();
    const navigate = useNavigate();
    const {
        token,
        userId,
        username,
        role,
        classId
    } = location.state || {};
    const[students, setStudents] = useState([]);
    useEffect(() => {
        async function fetchStudents() {
            const result = await fetch("http://localhost:5092/classes/get-class-students", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: token
                },
                body: JSON.stringify({
                    classId: classId
                })
            });
            if (!result.ok) {
                console.error("Failed to fetch students");
                return;
            }
            const data = await result.json();
            console.log(data);
            setStudents(data);
        }
        fetchStudents();
    }, []);
    return (
        <div>
            <TeacherHeader 
                token={token}
                userId={userId}
                username={username}
                role={role}
            />
            <div className="card" style={{width: "70%", margin: "5px auto"}}>
                    <div className="card-header">
                        <h3 className="card-title">CLASS LIST</h3>
                    </div>
                    <div className="card-body">
                        <table className="table table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>Student ID</th>
                                    <th>Name</th>
                                </tr>
                            </thead>
                            <tbody>
                                {
                                    students.map((student, index) => {
                                        return (
                                            <tr key={index} data-widget="expandable-table" aria-expanded="false">
                                                <td>{student.studentID}</td>
                                                <td>{student.studentName}</td>
                                            </tr>
                                        )
                                    })
                                }
                            </tbody>
                        </table>
                    </div>
                </div>
        </div>
    );
}

export default ClassStudents;