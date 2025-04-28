import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Register from './Register';
import Login from './Login';
import MarkAttendance from './MarkAttendance';

function App() {
  return (
    <Router>
      <div className="App">
        <Routes>
          <Route path="/" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route path="/attendance" element={<MarkAttendance />} />
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
