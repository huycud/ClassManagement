using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClassManagement.Mvc.Integrations.Role;
using ClassManagement.Mvc.Models.Page;

namespace ClassManagement.Mvc.Areas.Admin.Views.ViewComponents
{
    public class RenderRoles(IRoleHttpClientService roleHttpClientService) : ViewComponent
    {
        private readonly IRoleHttpClientService _roleHttpClientService = roleHttpClientService;

        public async Task<IViewComponentResult> InvokeAsync(string? roleName)
        {
            var roles = await _roleHttpClientService.GetAsync(new CommonPageViewModel { PageIndex = 1, PageSize = int.MaxValue });

            return View(roles.Items?.Select(x => new SelectListItem
            {
                Text = x.Name,

                Value = x.Name,

                Selected = !string.IsNullOrEmpty(roleName) && roleName.ToUpper().Equals(x.Name)
            }));
        }
    }
}
