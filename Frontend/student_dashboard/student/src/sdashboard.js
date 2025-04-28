import "./stu.css";

function Sdashboard(){
    return(
        <header>
            <div className = "top">
                 <h2>STUDENT DASHBOARD</h2>
            </div>
            <div className = "bottom">
                <button style = {{backgroundColor :"rgb(245, 218, 169)"}}>HOME</button> 
                <button style = {{backgroundColor : "rgb(235, 193, 242)"}}>MARK ATTENDANCE</button>
                <button style = {{backgroundColor : "rgb(168, 234, 193)"}}>CLASS MANAGEMENT</button>
                <button style = {{backgroundColor : "rgb(243, 121, 121)"}}>LOGOUT</button>                                                                      
                
           
                </div>
        </header>
    );
}
export default Sdashboard;