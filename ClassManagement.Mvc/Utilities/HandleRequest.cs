using Newtonsoft.Json;
using System.Collections;
using System.Net.Http.Headers;
using System.Text;

namespace ClassManagement.Mvc.Utilities
{
    public static class HandleRequest
    {
        public static HttpContent RequestHandler<T>(T request)
        {
            var json = JsonConvert.SerializeObject(request);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public static HttpContent GetRequestMultipartFormContent(this object obj, string? userId)
        {
            var multipartContent = new MultipartFormDataContent();

            var properties = obj.GetType().GetProperties();

            if (!string.IsNullOrEmpty(userId)) multipartContent.Add(new StringContent(userId), "UserId");

            foreach (var prop in properties)
            {
                var value = prop.GetValue(obj);

                if (value != null)
                {
                    if (prop.PropertyType == typeof(IFormFile))
                    {
                        var file = (IFormFile)value;

                        byte[] data;

                        using (var br = new BinaryReader(file.OpenReadStream()))
                        {
                            data = br.ReadBytes((int)file.OpenReadStream().Length);
                        }

                        ByteArrayContent bytes = new ByteArrayContent(data);

                        bytes.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

                        multipartContent.Add(bytes, "Image", file.FileName);
                    }

                    else if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        IList list = (IList)prop.GetValue(obj);

                        foreach (var item in list) multipartContent.Add(new StringContent(item.ToString()), prop.Name);
                    }

                    else multipartContent.Add(new StringContent(value.ToString()), prop.Name);
                }
            }
            return multipartContent;
        }
    }
}
