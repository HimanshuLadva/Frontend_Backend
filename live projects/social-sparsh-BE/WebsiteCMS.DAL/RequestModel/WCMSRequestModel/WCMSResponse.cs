using Microsoft.AspNetCore.Http;
using WebsiteCMS.DAL.RequestModel.BOTRequestModel;

namespace WebsiteCMS.DAL.RequestModel.WCMSRequestModel
{
    public class WCMSResponse
    {
        public bool Success;
        public object data;
        public string? error;
        public int status;

        public WCMSResponse ActionResultData(object? data, int code)
        {
            ResponseMetadata<object> metaData = new() { records = data! };
            WCMSResponse response = new()
            {
                data = metaData,
                status = code,
                error = data == null ? "No records found!" : null,
            };
            response.Success = response.status.ToString().StartsWith('2') || response.status.ToString().StartsWith('3');
            return response;
        }

        public WCMSResponse ExceptionResult(Exception err)
        {
            WCMSResponse response = new()
            {
                Success = false,
                error = err.Message,
                status = StatusCodes.Status500InternalServerError,
                data = new ResponseMetadata<object>() { records = null }
            };
            return response;
        }
    }

    public class ResponseMetadata<T>
    {
        public T records;
    }
}
