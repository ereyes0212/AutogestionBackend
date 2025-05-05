namespace AutoGestion.models
{
    public class LoginDTo
    {
        public string username { get; set; }
        public string password { get; set; }
    }    
    public class ResetDto
    {
        public string username { get; set; }
        public string newPassword { get; set; }
    }
}
