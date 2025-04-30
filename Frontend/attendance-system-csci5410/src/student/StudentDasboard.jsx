import "./stu.css";
import StudentHeader from "./StudentHeader";
import { useLocation } from 'react-router-dom';

function StudentDashboard(){
    const location = useLocation();
    const {
        token,
        userId,
        username,
        role
    } = location.state || {};

    return(
        <div>
            <StudentHeader
                token={token}
                userId={userId}
                username={username}
                role={role}
            />
            <div style={{ alignItems: 'center', marginTop: '5%', fontFamily:"Nunito, sans-serif" }}>
                <div className="landing">
                    <h1 style={{fontSize:"3rem"}}>Welcome {username}</h1>
                </div>
            </div>
        </div>
    );
}
export default StudentDashboard;