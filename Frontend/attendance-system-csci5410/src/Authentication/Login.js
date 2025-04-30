import React, { use, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import './Login.css';
import { useNavigate } from 'react-router-dom';
import { useLocation } from 'react-router-dom';
import { useEffect } from 'react';
import Swal from 'sweetalert2';

const Login = () => {
  const [form, setForm] = useState({ username: '', password: '' });
  const [message, setMessage] = useState('');
  const navigate = useNavigate();
  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };
  useEffect(() => {
    if (isRegistered) {
      Swal.fire({
        title: 'Registration Successful',
        text: 'You can now log in with your credentials.',
        icon: 'success',
        confirmButtonText: 'OK'
      });
    }
  }, []);
  const location = useLocation();
  const {
    isRegistered
  } = location.state || {};

  const handleSubmit = async (e) => {
    e.preventDefault();
    try{
      const response = await fetch("http://localhost:5092/Auth/login",{
        method: "POST",
        headers:{
          "Content-Type": "application/json",
          // Authorization: `Bearer ${localStorage.getItem("token")}`
        },
        body: JSON.stringify(form)
      });
      if(!response.ok){
        setMessage("Login failed. Please check your credentials.");
        return;
      }
      const data = await response.json();
      console.log(data);
      if(data.role === "teacher"){
        navigate("/teacher",{
          state:{
            token: data.jwTtoken,
            userId: data.userId,
            username: form.username,
            role: data.role
          }
        });
      }
      else if(data.role === "student"){
        navigate("/student",{
          state:{
            token: data.jwTtoken,
            userId: data.userId,
            username: form.username,
            role: data.role
          }
        });
      }

    }
    catch(error){

    }
  };

  return (
    <div className="login-container">
      <h2 className="form-title">Login</h2>
      <form onSubmit={handleSubmit} className="login-form">
        <div className="form-group">
          <label className="form-label" htmlFor="username">Username</label>
          <input
            type="text"
            name="username"
            id="username"
            value={form.username}
            onChange={handleChange}
            className="form-input"
            required
          />
        </div>

        <div className="form-group">
          <label className="form-label" htmlFor="password">Password</label>
          <input
            type="password"
            name="password"
            id="password"
            value={form.password}
            onChange={handleChange}
            className="form-input"
            required
          />
        </div>

        <button type="submit" className="btn btn-primary">Login</button>
      </form>

      {message && <p style={{color:"red"}}>{message}</p>}

      <p>
        Don’t have an account? <Link to="/register">Register here</Link>
      </p>
    </div>
  );
};

export default Login;
