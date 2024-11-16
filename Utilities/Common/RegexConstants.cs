namespace Utilities.Common
{
    public static class RegexConstants
    {
        public const string VIETNAMESENAME = "^[aAàÀảẢãÃáÁạẠăĂằẰẳẲẵẴắẮặẶâÂầẦẩẨẫẪấẤậẬbBcCdDđĐeEèÈẻẺẽẼéÉẹẸêÊềỀểỂễỄếẾệỆ " +

                                        "fFgGhHiIìÌỉỈĩĨíÍịỊjJkKlLmMnNoOòÒỏỎõÕóÓọỌôÔồỒổỔỗỖốỐộỘơƠờỜởỞỡỠớỚợỢpPqQrRsStTu " +

                                        "UùÙủỦũŨúÚụỤưƯừỪửỬữỮứỨựỰvVwWxXyYỳỲỷỶỹỸýÝỵỴzZ ]+$";

        public const string EMAIL = @"^[\w-]+(\.[\w-]+)*@gmail\.com(\.vn)?$";

        public const string NAME = "^[a-zA-Z0-9]+$";

        public const string CLASSID = "^[^!@#$%^&*()_+=\\[\\]{};':\"\\\\|,<>\\/?`~]*$";
    }
}
