import "./teacher.css";

function Teacherdashboard(){
    return(
        <header>
            <div className = "top">
                 <h2>FACULTY DASHBOARD</h2>
            </div>
            <div className = "bottom">
                <button>HOME</button> 
                <button>MARK ATTENDANCE</button>
                <button>CLASS MANAGEMENT</button>
                <button>LOGOUT</button>                                                                      
                
           </div>
        </header>
        
        
    );
}
export default Teacherdashboard;