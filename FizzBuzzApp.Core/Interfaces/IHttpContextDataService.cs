
namespace FizzBuzzApp.Core.Interfaces
{
    public interface IHttpContextDataService
    {
        public void SaveData<T>(string key, T value);
        public T GetData<T>(string key);
    }
}
