using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClassManagement.Mvc.Integrations.Department;
using ClassManagement.Mvc.Models.Page;

namespace ClassManagement.Mvc.Areas.Admin.Views.ViewComponents
{
    public class RenderDepartments(IDepartmentHttpClientService httpClientService) : ViewComponent
    {
        private readonly IDepartmentHttpClientService _departmentHttpClientService = httpClientService;

        public async Task<IViewComponentResult> InvokeAsync(string? departmentId)
        {
            var departments = await _departmentHttpClientService.GetDepartmentAsync(new CommonPageViewModel { PageIndex = 1, PageSize = int.MaxValue });

            return View(departments.Items?.Select(x => new SelectListItem
            {
                Text = x.Name,

                Value = x.Id,

                Selected = !string.IsNullOrEmpty(departmentId) && departmentId.ToUpper().Equals(x.Id.ToUpper())
            }));
        }
    }
}
