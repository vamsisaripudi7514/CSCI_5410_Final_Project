// import "./stu.css";
import { Link } from 'react-router-dom';

function TeacherHeader({ token, userId, username, role }) {
    return (
        <div className="student-dashboard">
            <div className="one">
                <h2>FACULTY DASHBOARD</h2>
            </div>
            <nav className="main-header navbar navbar-expand navbar-white navbar-light" style={{ marginLeft: "0px", paddingLeft: "7px" }}>
                <ul className="navbar-nav" style={{ width: "100%", display: "flex", alignItems: "center" }}>
                    <li className="nav-item">
                        <Link to="/teacher" className="nav-link"
                            state={{
                                token: token,
                                userId: userId,
                                username: username,
                                role: role
                            }}
                        >
                            HOME
                        </Link>

                    </li>
                    <li className="nav-item">
                        <Link to="/class-management" className="nav-link"
                            state={{
                                token: token,
                                userId: userId,
                                username: username,
                                role: role
                            }}
                        >
                            CLASS MANAGEMENT
                        </Link>
                    </li>
                    <li className="nav-item">
                        <Link to="/mark-attendance" className="nav-link"
                            state={{
                                token: token,
                                userId: userId,
                                username: username,
                                role: role
                            }}
                        >
                            MARK ATTENDANCE
                        </Link>
                    </li>

                    <li className="nav-item" style={{ marginLeft: "auto" }}>
                        <Link to="/" className="nav-link"
                            state={{
                                token: token,
                                userId: userId,
                                username: username,
                                role: role
                            }}
                        >
                            LOGOUT
                        </Link>
                    </li>
                </ul>
            </nav>
        </div>
    );
}

export default TeacherHeader;
