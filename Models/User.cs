using WEB.Repositories.DAL;
using WEB.Repositories.Request;
using BC = BCrypt.Net.BCrypt;


namespace WEB.DAL.Models
{
    public partial class User : TEntity
    {
        public string Uuid { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;   
        public DateTime BirthDay { get; set; }
        public string? Address { get; set; }
        public string? HomeTown { get; set; }
        public int Phone { get; set; }
        public string? Avatar { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserRole { get; set; } = null!;
        public bool? Enable { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Notify> Notifs { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<React> Reacts { get; set; }

        public User()
        {
            Comments = new HashSet<Comment>();
            Notifs = new HashSet<Notify>();
            Posts = new HashSet<Post>();
            Reacts = new HashSet<React>();
        }
        public User(UserReq req)
        {
            FirstName = req.FirstName;
            LastName = req.LastName;
            Email = req.Email;
            Password = BC.HashPassword(req.Password);
            BirthDay = req.BirthDay;
            Address = req.Address;
            HomeTown = req.HomeTown;    
            Phone = req.Phone;
            Avatar = req.Avatar;
        }
    }
}
