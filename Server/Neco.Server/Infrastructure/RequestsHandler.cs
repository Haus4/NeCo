using Neco.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Neco.Server.Infrastructure
{
    public static class RequestsHandler
    {
        private static readonly Dictionary<long, TaskCompletionSource<ResponseBase>> _tasks;

        static RequestsHandler()
        {
            _tasks = new Dictionary<long, TaskCompletionSource<ResponseBase>>();
        }

        public static void AppendResponse(ResponseBase response)
        {
            if (response == null)
                return;

            lock (_tasks)
            {
                TaskCompletionSource<ResponseBase> task;
                if (_tasks.TryGetValue(response.Token, out task))
                {
                    _tasks.Remove(response.Token);
                    response.RequestResult = true;
                    task.TrySetResult(response);
                }
            }
        }
        
        public static async Task<TResponse> WaitForResponse<TResponse>(RequestBase request, Action action, int timeout = 100000) where TResponse : ResponseBase
        {
            var taskSource = new TaskCompletionSource<ResponseBase>();
            lock (_tasks)
            {
                _tasks[request.Token] = taskSource;
            }
            action();
            StartTimeoutCheck(timeout, request.Token, taskSource);
            var response = await taskSource.Task.ConfigureAwait(false);
            return response as TResponse;
        }

        private static async void StartTimeoutCheck(int timeoutInMs, long token, TaskCompletionSource<ResponseBase> taskSource)
        {
            await Task.Delay(timeoutInMs).ConfigureAwait(false);
            if (taskSource.Task.IsCompleted || 
                taskSource.Task.IsFaulted || 
                taskSource.Task.IsCanceled)
                return;

            lock (_tasks)
            {
                if (_tasks.ContainsKey(token))
                {
                    _tasks.Remove(token);
                    taskSource.TrySetResult(new ResponseBase {RequestResult = false});
                }
            }
        }
    }
}
