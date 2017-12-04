using System;
using System.Threading;

namespace Neco.DataTransferObjects
{
    public class RequestBase
    {
        //In order to associate Request with response
        public long Token { get; set; }

        private static long _lastToken = 0;

        public RequestBase()
        {
            Token = _lastToken;
            AssignNewToken();
        }

        private static void AssignNewToken()
        {
            Interlocked.Increment(ref _lastToken);
        }

        public T CreateResponse<T>() where T : ResponseBase
        {
            var response = Activator.CreateInstance<T>();
            response.Token = Token;
            return response;
        }

        public T CreateResponse<T>(T response) where T : ResponseBase
        {
            response.Token = Token;
            return response;
        }

        public ResponseBase CreateResponse()
        {
            return CreateResponse<ResponseBase>();
        }
    }
}
