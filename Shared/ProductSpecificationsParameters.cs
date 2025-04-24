﻿namespace Shared
{
    public class ProductSpecificationsParameters
    {
        public int? TypeId {  get; set; }
        public int? BrandId { get; set; }
        public ProductSortOptions? Sort { get; set; }
        public string? Search { get; set; }
        public int PageIndex { get; set; } = 1;
        private const int MaxPageSize = 10;
        private int _pageSize = 5;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
