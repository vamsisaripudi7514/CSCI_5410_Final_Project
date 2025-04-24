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
}

export default App;
