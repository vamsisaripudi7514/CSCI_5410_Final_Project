import "./stu.css";

function Studentdashboard(){
    return(
        <header>
            <div className = "one">
                 <h2>STUDENT DASHBOARD</h2>
            </div>
            <div className = "two">
                <button className = "bt1">CHECK ATTENDANCE</button> 
                                                                                  
                <button className = "bt2">LOG OUT</button>
           
                </div>
        </header>
    );
}
export default Studentdashboard;