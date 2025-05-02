import React from "react";
import TeacherHeader from "./TeacherHeader";
import { useLocation } from "react-router-dom";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
function ClassAdd() {
    const location = useLocation();
    const navigate = useNavigate();
    const {
        token,
        userId,
        username,
        role
    } = location.state || {};
    const [className, setClassName] = useState("");
    const [excelFile, setExcelFile] = useState(null);
    const [message, setMessage] = useState("");
    const handleClassNameChange = (e) => {
        setClassName(e.target.value);
    };
    const handleExcelFileChange = (e) => {
        setExcelFile(e.target.files[0]);
    };
    const handleSubmit = async (e) => {
        e.preventDefault();
        const formData = new FormData();
        formData.append("className", className);
        formData.append("teacherId", userId);
        formData.append("excelFile", excelFile);
        try {
            console.log(token);
            const response = await fetch("http://localhost:5092/classes/create-class", {
                method: "POST",
                headers: {
                    // contentType: "multipart/form-data",
                    Authorization: token
                },
                body: formData
            });
            if (!response.ok) {
                console.error("Failed to create class:", response);
                return;
            }
            const data = await response.json();
            console.log("Class created successfully:", data);
            navigate("/class-management", {
                state: {
                    token: token,
                    userId: userId,
                    username: username,
                    role: role
                }
            });
        }
        catch (error) {
            console.error("Error during class creation:", error);
        }

    }
    return (
        <>
            <TeacherHeader
                token={token}
                userId={userId}
                username={username}
                role={role}
            />
            <div className="container d-flex justify-content-center align-items-center" style={{ minHeight: "100vh", marginTop: "-100px", borderRadius: "20px" }}>
                <div className="card card-primary shadow-lg" style={{ width: "45%" }}>
                    <div className="card-header bg-primary">
                        <h3 className="card-title text-white text-center w-100">ADD CLASS</h3>
                    </div>

                    <form onSubmit={handleSubmit}>
                        <div className="card-body">
                            <div className="form-group">
                                <label htmlFor="class-name-id">Class Name</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    id="class-name-id"
                                    placeholder="Enter Class Name"
                                    value={className}
                                    onChange={handleClassNameChange}
                                    required
                                    style={{ width: "80%" }}
                                />
                            </div>

                            <div className="form-group" style={{ marginTop: "20px" }}>
                                <label htmlFor="excel-file-id">Upload File</label>
                                <div className="border rounded p-4 text-center bg-light" style={{ cursor: "pointer" }}>
                                    <p className="text-muted mb-2">Upload your .xlsx file</p>
                                    <input
                                        type="file"
                                        className=""
                                        id="excel-file-id"
                                        accept=".xlsx"
                                        onChange={handleExcelFileChange}
                                        style={{ marginTop: "10px", margin: "0 auto", width: "80%" }}
                                    />
                                </div>
                            </div>
                        </div>
                        {message && <p style={{ color: "red" }}>{message}</p>}
                        <div className="card-footer text-center">
                            <button type="submit" className="btn btn-primary w-50">
                                Submit
                            </button>
                        </div>
                    </form>
                </div>
            </div>

        </>
    );
}

export default ClassAdd;