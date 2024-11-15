namespace Utilities.Common
{
    public static class SystemConstants
    {
        //System

        public const string CONNECTIONSTRING = "DefaultConnection";

        public const string COOKIE_NAME = "userId";

        public const string ACCESSTOKEN_NAME = "AccessToken";

        public const string REFRESHTOKEN_NAME = "RefreshToken";

        public const string SCHEME_NAME = "Bearer";

        public const string DELETEUNCONFIRMEDEMAIL_JOB = "deleteUnconfirmedUsersJob";

        public const string LOGINPROVIDER_NAME = "ClassMngApi";

        //Folder name

        public const string EXERCISE_FOLDER = "Exercises";

        public const string ADMIN_FOLDER = "Images/Admin";

        public const string TEACHER_FOLDER = "Images/Teacher";

        public const string STUDENT_FOLDER = "Images/Student";

        //Template

        public const string CONFIRMEMAIL_TEMPLATE_NAME = "ConfirmEmail.html";

        public const string OUTPUTTEMPLATE = "[{Timestamp:yyyy-MM-dd HH:mm:ss}] {Level:u3} - {EnvironmentName} - {Message:lj}{NewLine}{Exception}";

        public const string RESET_PASSWORD_HTML_BODY = "Please click the link to reset your password: <a href=\"{0}\">Reser Password</a>";

        //Date format string

        public const string FORMAT_STRING = "HH:mm:ss dd/MM/yyyy";

        //Url
        public const string RESET_PASSWORD_URL = "{0}/reset-password?email={1}&resetToken={2}";
    }
}
