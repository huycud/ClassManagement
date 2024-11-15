namespace ClassManagement.Api.Tests
{
    static class ClassManagementApiDef
    {
        public static readonly string GetStudentsNotExistInClass = "get-students-not-exist-in-class/{0}?keyword={1}&pageIndex={2}&pageSize={3}&sortOrder={4}&isDisable={5}";

        public static readonly string GetClassesByClient = "get-by-client?clientId={0}&keyword={1}&pageIndex={2}&pageSize={3}&sortOrder={4}";

        public static readonly string GetClassesBySubject = "subjectId={0}&keyword={1}&pageIndex={2}&pageSize={3}&sortOrder={4}";

        public static readonly string GetClientsByClass = "get-clients-by-class?classId={0}&keyword={1}&pageIndex={2}&pageSize={3}&sortOrder={4}";

        public static readonly string GetClientsByRole = "get-clients-by-role?roleName={0}&keyword={1}&pageIndex={2}&pageSize={3}&sortOrder={4}&isDisabled={5}";

        public static readonly string GetPageUrl = "keyword={0}&pageIndex={1}&pageSize={2}&sortOrder={3}";

        public static readonly string GetNotifies = "userId={0}&type={1}&keyword={2}&pageIndex={3}&pageSize={4}&sortOrder={5}&isDeleted={6}";

        public static readonly string GetSubjects = "departmentId={0}&keyword={1}&pageIndex={2}&pageSize={3}&sortOrder={4}";

        public static readonly string GetLoginUrl = "{0}/login";

        public static readonly string GetLogoutUrl = "{0}/logout";
    }
}
