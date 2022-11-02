namespace WEB.Repositories.Request
{
    public class UserReq
    {
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;   
        public DateTime BirthDay { get; set; }
        public string? Address { get; set; }
        public string? HomeTown { get; set; }
        public int Phone { get; set; }
        public string? Avatar { get; set; } = null!;

    }
}
