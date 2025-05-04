import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Register from './Authentication/Register';
import Login from './Authentication/Login';
import MarkAttendance from './teacher/MarkAttendance';
import TeacherDashboard from './teacher/TeacherDashboard';
import StudentDashboard from './student/StudentDasboard';
import ClassAttendance from './student/ClassAttendance';
import AttendanceDisplay from './student/AttendanceDisplay';
import ClassManagement from './teacher/ClassManagement';
import ClassAdd from './teacher/ClassAdd';
import ClassStudents from './teacher/ClassStudents';

function App() {
  return (
    <Router>
      <div className="App">
        <Routes>
          <Route path="/" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route path="/attendance" element={<MarkAttendance />} />
          <Route path ="/teacher" element={<TeacherDashboard/>}/>
          <Route path="/student" element={<StudentDashboard/>} />
          <Route path="/student-attendance-check" element={<ClassAttendance/>} />
          <Route path="/student-attendance-display" element={<AttendanceDisplay/>} />
          <Route path="/class-management" element={<ClassManagement/>} />
          <Route path="/mark-attendance" element={<MarkAttendance/>} />
          <Route path="/create-class" element={<ClassAdd/>}/>
          <Route path="/class-students" element={<ClassStudents/>}/>
        </Routes>
      </div>
    </Router>
  );
//     <BrowserRouter>
//       <Routes>
//         <Route path="/" element={<Navigate to="/teacher" />} /> 
//         <Route path="/student" element={<Studentdashboard />} />
//         <Route path="/teacher" element={<Teacherdashboard />} />
//         <Route path="/classmanagement" element={<Classdashboard/>} />
//       </Routes>
//     </BrowserRouter>
}

export default App;
