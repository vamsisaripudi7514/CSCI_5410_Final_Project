import React, { use } from "react";
import { useLocation } from "react-router-dom";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import TeacherHeader from "./TeacherHeader";
import Swal from "sweetalert2";
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
    const [students, setStudents] = useState([]);
    useEffect(() => {
        
        fetchStudents();
    }, []);
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

        setStudents(data);
    }
    async function handleDropStudent(studentId,studentName) {
        //write a confirmation alert here using confirm in js not swal
        const result = await Swal.fire({
            title: `Are you sure want to drop ${studentName}?`,
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, drop it!'
            });
        if (!result.isConfirmed) {return;}
        try{
            const response = await fetch("http://localhost:5092/classes/drop-student",{
                method:"POST",
                headers:{
                    "Content-Type":"application/json",
                    Authorization:token
                },
                body:JSON.stringify({
                    studentId:studentId,
                    classId:classId
                })
            });
            if(!response.ok){
                console.error("Failed to drop student:", response.statusText);
                return;
            }
            const data = await response.json();
            console.log("Student dropped:", data);
            Swal.fire({
                title: 'Action Completed',
                text: `${studentName} has been dropped from the class.`,
                icon: 'success',
                confirmButtonText: 'OK'
              });
              fetchStudents();
        }
        catch(error){
            console.error("Error dropping student:", error);
        }
    }
    return (
        <div>
            <TeacherHeader
                token={token}
                userId={userId}
                username={username}
                role={role}
            />
            <div className="card" style={{ width: "70%", margin: "5px auto" }}>
                <div className="card-header">
                    <h3 className="card-title">CLASS LIST</h3>
                </div>
                <div className="card-body">
                    <table className="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>Student ID</th>
                                <th>Name</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                students.map((student, index) => {
                                    return (
                                        <tr key={index} data-widget="expandable-table" aria-expanded="false">
                                            <td>{student.studentID}</td>
                                            <td>{student.studentName}</td>
                                            <td style={{width:"10%"}}><button className="btn btn-sm btn-danger" style={{ marginLeft: "10px" }}
                                                onClick={() => { handleDropStudent(student.studentID,student.studentName) }}>

                                                DROP</button></td>
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