namespace webAPICSharp.Utils;
    public class QueryParameters
    {
        private int _maxPageSize = 50;
        private int _pageSize = 10;
        public int pageNumber { get; set; } = 1;
        public int pageSize 
        {
            get { return _pageSize; }
            set { _pageSize = (value > _maxPageSize) ? _maxPageSize : value; }
        }
    }