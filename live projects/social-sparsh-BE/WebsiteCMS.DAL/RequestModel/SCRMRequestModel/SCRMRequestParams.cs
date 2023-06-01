namespace WebsiteCMS.DAL.RequestModel.SCRMRequestModel
{
    public class SCRMRequestParams
    {
        public string? search { get; set; }
        const int maxPageSize = 50;
        public int pageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int pageSize
        {
            get
            {
                return _pageSize;
            }

            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public string? Tags { get; set; }
        public int recordCount { get; set; }
        public string? sortBy { get; set; }
        public string? orderBy { get; set; }
        public string? isActive { get; set; }
    }
}
