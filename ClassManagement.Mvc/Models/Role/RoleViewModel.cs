using System.ComponentModel.DataAnnotations;

namespace ClassManagement.Mvc.Models.Role
{
    public class RoleViewModel
    {
        [Display(Name = "Mã quyền")]
        public int Id { get; set; }

        [Display(Name = "Tên quyền")]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        public bool IsDisabled { get; set; }
    }
}
