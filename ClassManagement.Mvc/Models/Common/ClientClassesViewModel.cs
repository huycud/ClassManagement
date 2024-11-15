using ClassManagement.Mvc.Models.Class;
using ClassManagement.Mvc.Models.Clients;
using ClassManagement.Mvc.Models.Page;

namespace ClassManagement.Mvc.Models.Common
{
    public class ClientClassesViewModel
    {
        public ClientViewModel Client { get; set; }
        public PageResultViewModel<ClassViewModel> Classes { get; set; }
    }
}
