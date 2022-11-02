namespace WEB.Repositories.Request
{
    [Serializable]
    public class CommentReq
    {
        public int? CommentId { get; set; }
        public string? Content { get; set; }
        public int PostId { get; set; }
    }
}
