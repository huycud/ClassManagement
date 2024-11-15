namespace ClassManagement.Mvc.Utilities
{
    class ClassManagementMvcDef
    {
        //Class
        public static readonly string ClassApi = "api/classes";

        public static readonly string GetAddStudentsToClass = "{0}/{1}/add-student-to-class";

        public static readonly string GetClassId = "{0}/{1}";

        public static readonly string GetClassesBySubject = "{0}?subjectId={1}&keyword={2}&pageIndex={3}&pageSize={4}&sortOrder={5}";

        public static readonly string GetStudentsNotExistInClass = "{0}/{1}/get-students-not-exist-in-class?keyword={2}&pageIndex={3}" +

                                                                    "&pageSize={4}&sortOrder={5}&isDisable={6}";

        public static readonly string GetClassesByClient = "{0}/get-by-client?clientId={1}&keyword={2}&pageIndex={3}&pageSize={4}&sortOrder={5}";

        //Department
        public static readonly string DepartmentApi = "api/departments";

        public static readonly string GetDepartments = "{0}?keyword={1}&pageIndex={2}&pageSize={3}&sortOrder={4}";

        public static readonly string GetDepartmentId = "{0}/{1}";

        //Role
        public static readonly string RoleApi = "api/roles";

        public static readonly string GetRoles = "{0}?keyword={1}&pageIndex={2}&pageSize={3}&sortOrder={4}";

        public static readonly string GetRolesByClient = "{0}/{1}/get-roles";

        //Semester
        public static readonly string SemesterApi = "api/semesters";

        public static readonly string GetSemesters = "{0}?keyword={1}&pageIndex={2}&pageSize={3}&sortOrder={4}";

        public static readonly string GetSemesterId = "{0}/{1}";

        //Subject
        public static readonly string SubjectApi = "api/subjects";

        public static readonly string GetSubjects = "{0}?departmentId={1}&keyword={2}&pageIndex={3}&pageSize={4}&sortOrder={5}";

        public static readonly string GetSubjectId = "{0}/{1}";

        //Notify
        public static readonly string NotifyApi = "api/notifies";

        public static readonly string GetNotifies = "{0}?userId={1}&type={2}&keyword={3}&pageIndex={4}&pageSize={5}&sortOrder={6}&isDeleted={7}";

        public static readonly string GetNotifyId = "{0}/{1}";

        public static readonly string GetChangeStatus = "{0}/{1}/change-status";

        //Client
        public static readonly string ClientApi = "api/clients";

        public static readonly string GetClientsByClass = "{0}/get-clients-by-class?classId={1}&keyword={2}&pageIndex={3}&pageSize={4}&sortOrder={5}";

        public static readonly string GetClientsByRole = "{0}/get-clients-by-role?roleName={1}&keyword={2}&pageIndex={3}&pageSize={4}&sortOrder={5}&isDisabled={6}";

        public static readonly string GetClientId = "{0}/{1}";

        //User
        public static readonly string AdminApi = "api/admins";

        public static readonly string TeacherApi = "api/teachers";

        public static readonly string StudentApi = "api/students";

        public static readonly string GetUpdatePassword = "{0}/{1}/update-password";

        public static readonly string GetUpdateAvatar = "{0}/{1}/update-avatar";

        //Authentication
        public static readonly string AuthenticationApi = "api/authentications";

        public static readonly string LoginApi = "{0}/logins";

        public static readonly string LogoutApi = "{0}/logout";
    }
}
