namespace DAPM.ClientApi.Models.DTOs
{
    public class SignupForm
    {
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Guid Organization { get; set; }
        public string Password { get; set; }
    }
}

