﻿namespace HRPortal.Core.DTO
{
    public class PaginatedGenericDto<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public T Result { get; set; }
        public decimal TotalStaffCount { get; set; }
    }
}
