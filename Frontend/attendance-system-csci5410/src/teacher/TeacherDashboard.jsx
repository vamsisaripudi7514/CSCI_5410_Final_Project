import react from "react";
import { useLocation } from "react-router-dom";
import TeacherHeader from "./TeacherHeader";
function TeacherDashboard() {
    const location = useLocation();
    const {
        token,
        userId,
        username,
        role
    } = location.state || {};

    return (
        <div>
            <TeacherHeader
                token={token}
                userId={userId}
                username={username}
                role={role}
            />
            <div style={{ alignItems: 'center', marginTop: '5%', fontFamily: "Nunito, sans-serif" }}>
                <div className="landing">
                    <h1 style={{ fontSize: "3rem" }}>Welcome {username}</h1>
                </div>
            </div>
        </div>
    );
}
export default TeacherDashboard;