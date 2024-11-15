using ClassManagement.Mvc.Models.Class;
using ClassManagement.Mvc.Models.Clients;
using ClassManagement.Mvc.Models.Page;

namespace ClassManagement.Mvc.Models.Common
{
    public class ClassClientsViewModel
    {
        public ClassViewModel Class { get; set; }
        public PageResultViewModel<ClientViewModel>? Clients { get; set; }
    }
}
