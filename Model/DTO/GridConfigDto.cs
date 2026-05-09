using System;

namespace Common.DTO
{
    public class GridConfigCreateDto
    {
        public Guid? OrganizationId { get; set; }
        public Guid? UserId { get; set; }
        public string GridId { get; set; } = string.Empty;
        public string ColumnField { get; set; } = string.Empty;
        public bool IsVisible { get; set; } = true;
        public bool IsPinned { get; set; } = false;
        public string? PinPosition { get; set; }
        public int ColumnOrder { get; set; }
        public int? ColumnWidth { get; set; }
    }

    public class GridConfigUpdateDto : GridConfigCreateDto
    {
        public Guid Id { get; set; }
    }
}
