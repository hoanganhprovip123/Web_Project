namespace WEB.Repositories.Response
{
    public class BaseResponse
    {
        #region Properties
        public bool Success { get; private set; }
        public string Code { get; set; }
        public string Message 
        {
            get
            {
                if (Success)
                {
                    return msg;
                }
                else
                {
                    return Dev ? msg : err;
                }
            }
        }
        public string Variant
        {
            get
            {
                return Success ? "success" : "error";
            }
        }
        public string Title
        {
            get
            {
                return Success ? "Success" : TitleError;
            }
        }
        public static bool Dev { get; set; }
        #endregion

        #region Field
        private readonly string TitleError;
        private string msg;
        private readonly string err;
        #endregion

        #region Methods
        public BaseResponse()
        {
            Success = true;
            msg = string.Empty;
            TitleError = "Error";

            Dev = true; //TODO

            if(string.IsNullOrEmpty(err))
            {
                err = "Please Update Common Error in Custom Settings";
            }
        }
        public BaseResponse(string message) : this()
        {
            msg = message;
        }
        public BaseResponse(string message, string titleError) : this(message)
        {
            this.TitleError = titleError;
        }
        public void SetError(string message)
        {
            Success = false;
            msg = message;
        }
        public void SetError(string code, string message)
        {
            Success = false;
            Code = code;
            msg = message;
        }
        public void SetMassage(string message)
        {
            msg = message;
        }
        public void TestError()
        {
            SetError("We are testing to show error message, please ignore it...");
        }
        #endregion
    }
}
