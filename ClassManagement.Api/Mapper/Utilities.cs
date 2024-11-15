using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.AppRole;
using Utilities.Common;
using Utilities.Handlers;

namespace ClassManagement.Api.Mapper
{
    internal static class Utilities
    {
        public static string CheckImagePath(User user)
        {
            while (user.Images is not null)
            {
                return user.Images.ImagePath;
            }
            return string.Empty;
        }

        public static string DescWithRole(BaseRoleRequest src)
        {
            switch (src.Name.ToUpper())
            {
                case RoleConstants.ADMIN_NAME:

                    return src.Description = RoleConstants.ADMIN_DESCRIPTION;

                case RoleConstants.TEACHER_NAME:

                    return src.Description = RoleConstants.TEACHER_DESCRIPTION;

                case RoleConstants.STUDENT_NAME:

                    return src.Description = RoleConstants.STUDENT_DESCRIPTION;
            }
            return src.Description;
        }

        public static string ConvertIdString(string id)
        {
            string normalizedId = Normalize.NormalizeString(id);

            string formattedId = Normalize.RemoveNonAlphanumeric(normalizedId.ToLower()).Trim('-');

            return formattedId;
        }
    }
}
