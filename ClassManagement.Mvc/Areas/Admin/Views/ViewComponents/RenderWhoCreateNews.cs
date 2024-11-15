using Microsoft.AspNetCore.Mvc;
using ClassManagement.Mvc.Integrations.Users.Client;

namespace ClassManagement.Mvc.Areas.Admin.Views.ViewComponents
{
    public class RenderWhoCreateNews(IClientHttpService clientHttpService) : ViewComponent
    {
        private readonly IClientHttpService _clientHttpService = clientHttpService;

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var entity = await _clientHttpService.GetByIdAsync(id);

            return View(entity);
        }
    }
}
