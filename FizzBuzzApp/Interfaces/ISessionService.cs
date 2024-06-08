using System.Collections.Generic;
using FizzBuzzApp.Models;

namespace FizzBuzzApp.Interfaces
{
    public interface ISessionService
    {
        void SetFizzBuzzModel(List<FizzBuzzModel> model);
        List<FizzBuzzModel> GetFizzBuzzModel();
    }
}
