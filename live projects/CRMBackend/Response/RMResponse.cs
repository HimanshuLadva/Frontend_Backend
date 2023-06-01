namespace CRMBackend.Response
{
    public class RMResponse
    {
        public bool success;
        public string message;
        public object? data;
        public string error;
    }

    public class ResponseMetadata<T>
    {
        public int page_number = 1;
        public int page_size = 0;
        public int total_record_count = 0;
        public T records;
    }
}
