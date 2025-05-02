import React, { use, useEffect } from "react";
import { useLocation } from "react-router-dom";
import StudentHeader from "./StudentHeader";
import { useState } from "react";
function AttendanceDisplay() {
    const location = useLocation();
    const {
        token,
        userId,
        username,
        role,
        classId
    } = location.state || {};
    // console.log("AttendanceDisplay", userId, classId, token);
    useEffect(() => {
        async function fetchSessions() {
            console.log(userId, classId);
            const result = await fetch("http://localhost:5092/Students/get-student-records", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: token
                },
                body: JSON.stringify({
                    studentId: userId,
                    classId: classId
                })
            });
            if (!result.ok) {
                console.error("Failed to fetch sessions");
                return;
            }
            const data = await result.json();
            console.log(data);
            setSessions(data);
        }
        fetchSessions();
        async function fetchAnalytics() {
            const result = await fetch("http://localhost:5092/Students/get-student-analytics", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: token
                },
                body: JSON.stringify({
                    studentId: userId,
                    classId: classId
                })
            });
            if (!result.ok) {
                console.error("Failed to fetch analytics");
                return;
            }
            const data = await result.json();
            console.log(data);
            setAnalytics(data);

        }
        fetchAnalytics();
    }, []);

    // const [piedata, setPieData] = useState();
    // const [pieoptions, setPieOptions] = useState();

    const [sessions, setSessions] = useState([]);
    const [analytics, setAnalytics] = useState(null);
    function processString(date) {
        const dateParts = date.split("T")[0].split("-");
        const year = dateParts[0];
        const month = dateParts[1];
        const day = dateParts[2];
        return `${month}/${day}/${year}`;
    }

    return (
        <div >
            <StudentHeader
                token={token}
                userId={userId}
                username={username}
                role={role}
            />
            {/* <h1>This is student attendance display component</h1> */}
            <div style={{ fontFamily: "Nunito, sans-serif" }}>
                <div className="card" >
                    <div className="card card-danger">
                        <div className="card-body"><div className="chartjs-size-monitor"><div className="chartjs-size-monitor-expand"><div className=""></div></div><div className="chartjs-size-monitor-shrink"><div className=""></div></div></div>
                            <div className="row">
                                <div className="col-lg-3 col-6">
                                    <div className="small-box bg-info">
                                        <div className="inner">
                                            <h3 style={{marginTop:"15px"}}>{analytics && analytics.total_classes}</h3>

                                            <p>Total Sessions</p>
                                        </div>
                                        <div className="icon">
                                            <i className="ion ion-bag"></i>
                                        </div>
                                    </div>
                                </div>
                                <div className="col-lg-3 col-6">
                                    <div className="small-box bg-success">
                                        <div className="inner">
                                            <h3 style={{marginTop:"15px"}}>{analytics && analytics.total_present}</h3>

                                            <p>Total Present</p>
                                        </div>
                                        <div className="icon">
                                            <i className="ion ion-stats-bars"></i>
                                        </div>
                                    </div>
                                </div>
                                <div className="col-lg-3 col-6">
                                    <div className="small-box bg-warning">
                                        <div className="inner">
                                            <h3 style={{marginTop:"15px"}}>{analytics && analytics.total_absent}</h3>

                                            <p>Total Absent</p>
                                        </div>
                                        <div className="icon">
                                            <i className="ion ion-person-add"></i>
                                        </div>
                                    </div>
                                </div>
                                <div className="col-lg-3 col-6">
                                    <div className="small-box bg-danger">
                                        <div className="inner">
                                            <h3 style={{marginTop:"15px"}}>{analytics && parseInt(analytics.attendance_percentage)}<sup>%</sup></h3>

                                            <p>Attendance Percentage</p>
                                        </div>
                                        <div className="icon">
                                            <i className="ion ion-pie-graph"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                
            </div>
            <div className="card" style={{width: "70%", margin: "0 auto"}}>
                    <div className="card-header">
                        <h3 className="card-title">ATTEDANCE SHEET</h3>
                    </div>
                    <div className="card-body">
                        <table className="table table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>Session</th>
                                    <th>Date</th>
                                    <th>Status</th>
                                </tr>
                            </thead>
                            <tbody>
                                {
                                    sessions.map((session, index) => {
                                        return (
                                            <tr key={index} data-widget="expandable-table" aria-expanded="false">
                                                <td>{index + 1}</td>
                                                <td>{processString(session.session_date)}</td>
                                                <td>{session.status}</td>
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

export default AttendanceDisplay;