using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ClassManagement.Mvc.Models.Common.Errors;

namespace ClassManagement.Mvc.Utilities
{
    public static class HandleError<T> where T : class
    {
        public static async Task<ErrorInfoViewModel<T>> HandleModelState(HttpResponseMessage message, T model)
        {
            var content = await message.Content.ReadAsStringAsync();

            var jsonObject = JsonConvert.DeserializeObject<JObject>(content);

            Type errorViewModelType = typeof(T);

            var errorModel = ErrorHandler(jsonObject, model, errorViewModelType);

            PropertyInfo[] fields = errorViewModelType.GetProperties();

            return new ErrorInfoViewModel<T> { PropertyInfos = fields, Value = errorModel };
        }

        private static T ErrorHandler(JObject obj, T model, Type type)
        {
            PropertyInfo[] fields = type.GetProperties();

            foreach (PropertyInfo field in fields)
            {
                if (obj.ContainsKey("message"))
                {
                    var message = obj["message"].ToString();

                    if (field.Name == "Id")
                    {
                        if (message.Contains(field.Name)) field.SetValue(model, message);
                    }
                    else
                    {
                        if (message.ToUpper().Contains(field.Name.ToUpper())) field.SetValue(model, message.Replace("Password", " Password").Replace("UserName", "Username"));
                    }
                }

                else
                {
                    var errors = obj["errors"] as JObject;

                    foreach (var Model in errors.Properties())
                    {
                        if (Model.Name.ToUpper().Equals(field.Name.ToUpper())) field.SetValue(model, Model.Value[0].ToString());
                    }
                }
            }

            return model;
        }
    }
}
