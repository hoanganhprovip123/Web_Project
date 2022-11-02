using System.Text.Json.Serialization;
using WEB.Repositories.Utils;

namespace WEB.Repositories.Response
{
    public class NotifyResponse : BaseResponse
    {
        public int TargetId { get; set; }
        [JsonConverter (typeof(JsonStringEnumConverter))]
        public NotifyTypes TyPe { get; set; }
        public bool IsRead { get; set; }
        public int Count { get; set; }
        public string LastModifiedName { get; set; }
        public string LastModifiedAvatar { get; set; }
        public DateTime LastModified { get; set; }
        public int NotifyId { get; set; }
    }
}
