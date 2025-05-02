import React, { useEffect } from "react";
import StudentHeader from "./StudentHeader";
import { useLocation } from 'react-router-dom';
import { useNavigate } from "react-router-dom";
import { useState } from "react";
function ClassAttendance() {
    const navigate = useNavigate();
    useEffect(() => {
        async function fetchData() {
            console.log("token", token);
            const result = await fetch("http://localhost:5092/Students/get-student-classes", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: token
                },
                body: JSON.stringify({
                    studentId: userId
                })
            });
            if (!result.ok) {
                console.error("Failed to fetch classes");
                return;
            }
            const data = await result.json();
            console.log(data);
            setClasses(data);
            // console.log(token);
        }
        fetchData();
    }, []);
    const [classes, setClasses] = useState([]);
    const [selectedClass, setSelectedClass] = useState();
    const location = useLocation();
    const {
        token,
        userId,
        username,
        role
    } = location.state || {};
    function handleSubmit(e) {
        e.preventDefault();
        console.log("Selected class:", selectedClass);
        if (selectedClass) {
            navigate("/student-attendance-display", {
                state: {
                    token: token,
                    userId: userId,
                    username: username,
                    role: role,
                    classId: selectedClass
                }
            });
        }
    }
    function handleChange(e) {
        setSelectedClass(e.target.value);
    }
    return (

        <div >
            <StudentHeader
                token={token}
                userId={userId}
                username={username}
                role={role}
            />

            <div className="card card-primary" style={{ alignItems: "center", height: "100vh" }}>
                <form style={{ borderRadius: "5px", margin: "50px auto", border: "1px solid #007bff", width: "30%" }} onSubmit={handleSubmit}>
                    <div className="card-header" style={{ backgroundColor: "#007bff", color: "white", alignContent: "center", alignItems: "center" }}>
                        <h2 className="card-title" style={{ color: "white" }}>CLASS ATTENDANCE</h2>
                    </div>

                    <div className="card-body" style={{ alignContent: "center", alignItems: "center" }} >
                        <div className="d-flex justify-content-center align-items-center">
                            <div className="row">
                                <div className="d-flex justify-content-center align-items-center" style={{ width: "100%" }}>
                                    <div className="form-group">

                                        {/* <label>SELECT CLASS</label> */}
                                        <select className="form-control select2 select2-hidden-accessible" data-select2-id="1" tabIndex="-1" onChange={handleChange}
                                        >
                                            <option selected="selected" data-select2-id="3">{"Select Class"}</option>
                                            {
                                                classes.map((element, index) => (
                                                    <option key={index} data-select2-id="3" value={element.class_id}>{element.class_name}</option>
                                                ))
                                            }
                                        </select></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="card-footer" >
                        <div className="d-flex justify-content-center align-items-center">
                            <button type="submit" className="btn btn-primary" >Submit</button>
                        </div>

                    </div>
                </form>
            </div>
        </div>
    );
}

export default ClassAttendance;