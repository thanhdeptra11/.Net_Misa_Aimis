using System;

namespace Common.DTO
{
    public class OrganizationCreateDto
    {
        public Guid? ParentId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
    }

    public class OrganizationUpdateDto : OrganizationCreateDto
    {
        public Guid Id { get; set; }
    }
}
