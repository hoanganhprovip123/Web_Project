using WEB.Repositories.DAL;

namespace WEB.DAL.Models
{
    public partial class Post : TEntity
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Notifies = new HashSet<Notify>();
            Reacts = new HashSet<React>();
        }
        public string Content { get; set; } = null!;
        public string? Image { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public string? Hashtag { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Comment>? Comments { get; set; }
        public virtual ICollection<Notify>? Notifies { get; set; }
        public virtual ICollection<React>? Reacts { get; set; }
        public virtual int CommentSetLength { get; set; } = 0;

    }
}
