using WEB.Repositories.DAL;

namespace WEB.DAL.Models
{
    public class React : TEntity
    {
        public int UserId { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public short Type { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Comment? Comment { get; set; }
        public virtual Post? Post { get; set; }
        public virtual User User { get; set; } = null!;

    }
}
