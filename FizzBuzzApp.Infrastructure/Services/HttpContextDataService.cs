using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using FizzBuzzApp.Core.Interfaces;


namespace FizzBuzzApp.Infrastructure.Services
{
    public class HttpContextDataService : IHttpContextDataService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextDataService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SaveData<T>(string key, T value)
        {
            var serializedValue = JsonConvert.SerializeObject(value);
            _httpContextAccessor.HttpContext?.Session.SetString(key, serializedValue);

        }

        public T GetData<T>(string key)
        {
            var serializedValue = _httpContextAccessor.HttpContext.Session.GetString(key);
            if (serializedValue != null)
            {
                return JsonConvert.DeserializeObject<T>(serializedValue);
            }
            return default(T); // Return default value if data not found
        }
    }
}
