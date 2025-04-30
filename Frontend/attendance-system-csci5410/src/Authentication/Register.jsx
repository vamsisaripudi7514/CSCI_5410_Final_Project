import "./Register.css";
 import React, { useState } from "react";
 import { Link } from 'react-router-dom';
 import { useNavigate } from 'react-router-dom';
 
 function Register() {
   const [formData, setFormData] = useState({
     mid: "",
     username: "",
     password: "",
   });
 
   const [message, setMessage] = useState("");
   const navigate = useNavigate();
   const handleChange = (e) => {
     setFormData({ ...formData, [e.target.name]: e.target.value });
   };
 
   const handleSubmit = async (e) => {
     e.preventDefault();
     console.log("Registration Data Submitted:", formData);
     try{
       const result = await fetch("http://localhost:5092/Auth/register",{
         method: "POST",
         headers:{
           "Content-Type": "application/json",
         },
         body:JSON.stringify({
           userId: formData.mid,
           username: formData.username,
           password: formData.password
         })
       });
       const data = await result.json();
       console.log(data);
       if(data.message !== null && data.message !== "User Registered Successfully!!"){
         setMessage(data.message);
         console.log("Registration failed. Please try again.");
         console.log(data.message);
         return;
       }
 
       console.log(data);
       // await new Promise((resolve) => setTimeout(resolve, 2000)); // Simulate a delay
       navigate("/",{
         state: {
           isRegistered: true
         }
       });
     }
     catch(error){
       setMessage("Registration failed. Please try again.");
       console.error("Error during registration:", error);
     }
 
   };
 
   return (
     <div className="register-container">
       <form onSubmit={handleSubmit} className="register-form">
         <h2 className="form-title">Student Registration</h2>
 
         <div className="form-group">
           <label className="form-label" htmlFor="mid">MID</label>
           <input
             type="text"
             id="mid"
             name="mid"
             value={formData.mid}
             onChange={handleChange}
             className="form-input"
             required
           />
         </div>
 
         <div className="form-group">
           <label className="form-label" htmlFor="username">Username</label>
           <input
             type="text"
             id="username"
             name="username"
             value={formData.username}
             onChange={handleChange}
             className="form-input"
             required
           />
         </div>
 
         <div className="form-group">
           <label className="form-label" htmlFor="password">Password</label>
           <input
             type="password"
             id="password"
             name="password"
             value={formData.password}
             onChange={handleChange}
             className="form-input"
             required
           />
         </div>
 
         <button type="submit" className="btn btn-primary">
           Submit
         </button>
 
         {message && <p style={{color:"red"}}>{message}</p>}
         <p>
           Already have an account? <Link to="/">Login</Link>
         </p>
       </form>
     </div>
   );
 }
 export default Register;