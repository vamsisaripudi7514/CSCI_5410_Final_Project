import React from 'react';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import Studentdashboard from './student/sdashboard';
import Teacherdashboard from './teacher/tdashboard';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate to="/teacher" />} /> 
        <Route path="/student" element={<Studentdashboard />} />
        <Route path="/teacher" element={<Teacherdashboard />} />
        
      </Routes>
    </BrowserRouter>
  );
}

export default App;
