using System.Reflection;

namespace ClassManagement.Mvc.Models.Common.Errors
{
    public class ErrorInfoViewModel<T> where T : class
    {
        public PropertyInfo[] PropertyInfos { get; set; }
        public T Value { get; set; }
    }
}