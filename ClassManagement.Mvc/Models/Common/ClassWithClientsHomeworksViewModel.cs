using ClassManagement.Mvc.Models.Class;
using ClassManagement.Mvc.Models.Clients;
using ClassManagement.Mvc.Models.Homework;
using ClassManagement.Mvc.Models.Page;

namespace ClassManagement.Mvc.Models.Common
{
    public class ClassWithClientsHomeworksViewModel
    {
        public ClassViewModel Class { get; set; }
        public PageResultViewModel<ClientViewModel>? Clients { get; set; }
        public PageResultViewModel<HomeworkViewModel>? Homeworks { get; set; }
    }
}
