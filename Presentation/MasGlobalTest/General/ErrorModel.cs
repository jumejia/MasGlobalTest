namespace MasGlobalTest.General
{
    public class ErrorModel
    {
        public string ErrorCode { get; set; }

        public string Message { get; set; }

        public ErrorModel(string errorCode, string message) => (ErrorCode, Message) = (errorCode, message);
    }
}