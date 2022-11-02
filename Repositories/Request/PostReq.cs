namespace WEB.Repositories.Request
{
    [Serializable]
    public class PostReq
    {
        public string? Content { get; set; }
        public string? Hashtag { get; set; }
        public string? ImageUrl { get; set; }
    }
}
