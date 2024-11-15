using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClassManagement.Mvc.Integrations.Users.Manager;
using ClassManagement.Mvc.Models.Page;

namespace ClassManagement.Mvc.Areas.Admin.Views.ViewComponents
{
    public class RenderClientsByRoleName : ViewComponent
    {
        private readonly IAdminHttpClientService _adminHttpClientService;

        public RenderClientsByRoleName(IAdminHttpClientService adminHttpClientService)
        {
            _adminHttpClientService = adminHttpClientService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string roleName, int? teacherId)
        {
            var entities = await _adminHttpClientService.GetClientsByRoleNameAsync(

                new ClientRolePageViewModel
                {
                    RoleName = roleName,

                    PageIndex = 1,

                    PageSize = int.MaxValue
                });

            return View(entities.Items?.Select(x => new SelectListItem
            {
                Text = $"{x.Id} - {x.Lastname} {x.Firstname} - {x.DepartmentName}",

                Value = x.Id.ToString(),

                Selected = !string.IsNullOrEmpty(teacherId.ToString()) && x.Id == teacherId
            }));
        }
    }
}
