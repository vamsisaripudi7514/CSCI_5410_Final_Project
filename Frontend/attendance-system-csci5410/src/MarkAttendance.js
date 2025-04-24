import React, { useState, useEffect } from 'react';
import './MarkAttendance.css';
import axios from 'axios';

const MarkAttendance = () => {
  const [classes, setClasses] = useState([]);
  const [selectedClass, setSelectedClass] = useState('');
  const [students, setStudents] = useState([]);
  const [attendance, setAttendance] = useState({});

  useEffect(() => {
    const fetchClasses = async () => {
      try {
        const res = await axios.get('http://localhost:5000/classes');
        setClasses(res.data);
      } catch (err) {
        console.error('Error fetching classes:', err);
      }
    };
    fetchClasses();
  }, []);

  const handleClassChange = async (e) => {
    const classId = e.target.value;
    setSelectedClass(classId);

    try {
      const res = await axios.get(`http://localhost:5000/classes/${classId}/students`);
      setStudents(res.data);

      const initialAttendance = {};
      res.data.forEach(student => {
        initialAttendance[student.id] = 'present';
      });
      setAttendance(initialAttendance);
    } catch (err) {
      console.error('Error fetching students:', err);
    }
  };

  const handleCheckboxChange = (id, status) => {
    // Toggle selection
    setAttendance(prev => ({
      ...prev,
      [id]: prev[id] === status ? '' : status
    }));
  };

  const handleSubmit = async () => {
    try {
      await axios.post('http://localhost:5000/attendance', {
        classId: selectedClass,
        records: attendance
      });
      alert('Attendance submitted successfully!');
    } catch (err) {
      console.error('Error submitting attendance:', err);
      alert('Failed to submit attendance.');
    }
  };

  return (
    <div className="mark-attendance-container">
      <h2>Mark Attendance</h2>

      <select value={selectedClass} onChange={handleClassChange}>
        <option value="">Select Class</option>
        {classes.map(cls => (
          <option key={cls.id} value={cls.id}>{cls.name}</option>
        ))}
      </select>

      {!selectedClass && <p style={{ textAlign: 'center' }}>Please select a class to begin.</p>}

      {students.length > 0 && (
        <div className="students-list">
          {students.map(student => (
            <div key={student.id} className="student-item">
              <span>{student.name}</span>

              <label>
                <input
                  type="checkbox"
                  checked={attendance[student.id] === 'present'}
                  onChange={() => handleCheckboxChange(student.id, 'present')}
                />
                Present
              </label>

              <label>
                <input
                  type="checkbox"
                  checked={attendance[student.id] === 'absent'}
                  onChange={() => handleCheckboxChange(student.id, 'absent')}
                />
                Absent
              </label>
            </div>
          ))}
        </div>
      )}

      {students.length > 0 && (
        <button className="submit-btn" onClick={handleSubmit}>
          Submit
        </button>
      )}
    </div>
  );
};

export default MarkAttendance;
