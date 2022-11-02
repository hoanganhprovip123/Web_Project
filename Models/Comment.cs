using WEB.Repositories.DAL;

namespace WEB.DAL.Models
{
    public class Comment :TEntity
    {
        public Comment()
        {
            InverseParent = new HashSet<Comment>();
            Notifies = new HashSet<Notify>();
            Reacts = new HashSet<React>();
        }

        public string Content { get; set; } = null!;
        public int UserId { get; set; }
        public int? PostId { get; set; }
        public DateTime CreatedDate { get; set; }

        public int? ParentId { get; set; }
        public virtual Comment? Parent { get; set; }
        public virtual Post? Post { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Comment> InverseParent { get; set; }
        public virtual ICollection<Notify> Notifies { get; set; }
        public virtual ICollection<React> Reacts { get; set; }
        public virtual int CommentSetLength { get; set; } = 0;

    }
}
