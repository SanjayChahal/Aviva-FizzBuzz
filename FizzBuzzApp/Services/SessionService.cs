using System.Collections.Generic;
using FizzBuzzApp.Interfaces;
using FizzBuzzApp.Models;
 
namespace FizzBuzzApp.Services
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetFizzBuzzModel(List<FizzBuzzModel> model)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            session?.SetObject("FizzBuzzModel", model);
        }

        public List<FizzBuzzModel> GetFizzBuzzModel()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            return session?.GetObject<List<FizzBuzzModel>>("FizzBuzzModel") ?? new List<FizzBuzzModel>();
        }
    }
}


