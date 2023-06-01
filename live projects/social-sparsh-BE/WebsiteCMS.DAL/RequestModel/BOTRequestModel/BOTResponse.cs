using Microsoft.AspNetCore.Http;
namespace WebsiteCMS.DAL.RequestModel.BOTRequestModel
{
    public class BOTResponse
    {
        public bool success;
        public object? data;
        public string? error;
        public int status = StatusCodes.Status404NotFound;

        public BOTResponse ActionResultData(object? data, int code)
        {
            ResponseMetadata<object> metaData = new() { records = data! };
            BOTResponse response = new()
            {
                data = metaData,
                status = code,
                error = data == null ? "No records found!" : null,
            };
            response.success = response.status.ToString().StartsWith('2') || response.status.ToString().StartsWith('3');
            return response;
        }

        public BOTResponse ExceptionResult(Exception err)
        {
            BOTResponse response = new()
            {
                success = false,
                error = err.Message,
                status = StatusCodes.Status500InternalServerError,
                data = new ResponseMetadata<object>() { records = null }
            };
            return response;
        }
    }

    public class ResponseMetadata<T>
    {
        public T? records;
    }
}
