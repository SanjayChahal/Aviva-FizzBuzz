using System;
using FizzBuzzApp.Core.Models;

namespace FizzBuzzApp.Core.Interfaces
{
	public interface IFizzBuzzService
    {
        public List<FizzBuzzModel> GenerateFizzBuzz(int number);
        public List<FizzBuzzModel> GetPagedFizzBuzzModel(List<FizzBuzzModel> allModels, int pageNumber, int pageSize);
        public int GetTotalPages(List<FizzBuzzModel> allModels, int pageSize);
        public List<FizzBuzzModel> GetSavedSessionData();
        public void SaveModelData(List<FizzBuzzModel> model);
    }
}

