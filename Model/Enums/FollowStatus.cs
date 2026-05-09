using System.ComponentModel;

namespace Common.Enums
{
    public enum FollowStatus
    {
        [Description("Tất cả trạng thái")]
        All = 0,

        [Description("Đang theo dõi")]
        Following = 1,

        [Description("Ngừng theo dõi")]
        Unfollowed = 2
    }
}
