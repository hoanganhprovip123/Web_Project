namespace WEB.Repositories.Response
{
    public class SingleResponse : BaseResponse
    {
        public object Data { get; set; }
        public SingleResponse() : base() { }
        public SingleResponse(string message) : base(message) { }
        public SingleResponse(string message, string titleError ): base(message, titleError) { }
        public void SetData(string code, object data)
        {
            Code = code;
            Data = data;
        }
    }
}
