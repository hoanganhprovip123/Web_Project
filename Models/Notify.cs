using WEB.Repositories.DAL;

namespace WEB.DAL.Models
{
    public class Notify : TEntity
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int CommentId { get; set; }
        public string Type { get; set; }
        public bool IsRead { get; set; }
        public virtual Comment? Comment { get; set; }
        public virtual Post? Post { get; set; }
        public virtual User User { get; set; }
    }
}
