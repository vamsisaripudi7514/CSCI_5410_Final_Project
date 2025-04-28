import React from "react";
import "./management.css";
function Classdashboard(){
    return(
        <div className = "manage_class">
         <button className = "logout">LOG OUT</button>
            <h2>Class Management</h2>
           
           <div className = "addclass">
           
           <label for="class" style = {{fontSize : '30px'}}>Choose a class:</label>

          <form method = "POST" className = "class_form">
          <select name="class" id="class" className = "classselect">
             <option value="csci4410">CSCI_4410</option>             
             <option value="csci5410">CSCI_5410</option>         
             <option value="csci6410">CSCI_6410</option>
             <option value="csci5420">CSCI_5420</option>
           </select>
           <button type = "submit" className = "submit_button">ADD CLASS</button>
          </form>
          </div>

        
          <div className = "generate_report">
           
           <label for="class" style = {{fontSize : '30px'}}>Choose a class:</label>

          <form method = "POST" className = "report_form">
          <select name="class" id="class" className = "classselect">
             <option value="csci4410">CSCI_4410</option>             
             <option value="csci5410">CSCI_5410</option>         
             <option value="csci6410">CSCI_6410</option>
             <option value="csci5420">CSCI_5420</option>
           </select>
           <button type = "submit" className = "submit_button">GENERATE_ATTENDANCE_REPORT</button>
          </form>
          </div>
     
    </div>
       
    );
}

export default Classdashboard;