namespace Neco.DataTransferObjects
{
    public class ResponseBase
    {
        public ResponseBase()
        {
            RequestResult = true;
        }

        //In order to associate Request with response
        public long Token { get; set; }

        //false means timeout or connection close
        public bool RequestResult { get; set; }

        public CommonErrors Error { get; set; }
    }

    public enum CommonErrors
    {
        //negative values are not errors ;)
        Success = 0,
        Maintenance,
        Banned,
    }
}