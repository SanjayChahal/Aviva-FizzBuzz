using FizzBuzzApp.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace FizzBuzzApp.Tests.Mocks
{
    public class MockHttpContextDataService : IHttpContextDataService
    {
        private readonly Dictionary<string, string> _dataStore = new Dictionary<string, string>();   

        public void SaveData<T>(string key, T value)
        {
            var serializedValue = JsonConvert.SerializeObject(value);
            _dataStore[key] = serializedValue;
        }

        public T GetData<T>(string key)
        {
            if (_dataStore.ContainsKey(key))
            {
                var serializedValue = _dataStore[key];
                return JsonConvert.DeserializeObject<T>(serializedValue);
            }
            return default(T); // Return default value if data not found
        }
    }
}
