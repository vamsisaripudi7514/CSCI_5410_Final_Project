namespace AttendanceSystem.DTOs.Auth
{
    public class UserRegistrationResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string JWTtoken { get; set; }
    }
}
