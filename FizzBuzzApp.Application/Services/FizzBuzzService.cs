using System.Drawing;
using FizzBuzzApp.Core;
using FizzBuzzApp.Core.Common;
using FizzBuzzApp.Core.Interfaces;
using FizzBuzzApp.Core.Models;

namespace FizzBuzzApp.Infrastructure.Services
{
    public class FizzBuzzService : IFizzBuzzService
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IHttpContextDataService _contaxtService;


        public FizzBuzzService(IDateTimeProvider dateTimeProvider, IHttpContextDataService contaxtService)
        {
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
            _contaxtService = contaxtService ?? throw new ArgumentNullException(nameof(contaxtService));
        }

        public List<FizzBuzzModel> GenerateFizzBuzz(int number)
        {
            var model = new List<FizzBuzzModel>();
            var dayInitial = _dateTimeProvider.CurrentDayOfWeek.ToString()[0];

            string output = number.ToString();
            ColorEnum color = ColorEnum.Black;  // Default color
            string fizz = dayInitial + "izz";
            string buzz = dayInitial + "uzz";

            if (number % 3 == 0 && number % 5 == 0)
            {
                output = fizz + " " + buzz;
                color = ColorEnum.Red;
            }

            else if (number % 3 == 0)
            {
                output = fizz;
                color = ColorEnum.Green;
            }
            else if (number % 5 == 0)
            {
                output = buzz;
                color = ColorEnum.Blue;
            }

            model.Add(new FizzBuzzModel { Number = number, Output = output, Color = color });

            return model;
        }

        public List<FizzBuzzModel> GetSavedSessionData()
        {
            return _contaxtService.GetData<List<FizzBuzzModel>>(Constants.SessionKeys.FizzBuzzModelKey) ?? new List<FizzBuzzModel>();
        }

        public void SaveModelData(List<FizzBuzzModel>? model)
        {
            if (model != null) {
                _contaxtService.SaveData<List<FizzBuzzModel>>(Constants.SessionKeys.FizzBuzzModelKey, model);
            }
        }


        public List<FizzBuzzModel> GetPagedFizzBuzzModel(List<FizzBuzzModel> allModels, int pageNumber, int pageSize)
        {
            if (allModels == null) throw new ArgumentNullException(nameof(allModels));

            int startIndex = (pageNumber - 1) * pageSize;
            return allModels.Skip(startIndex).Take(pageSize).ToList();
        }

        public int GetTotalPages(List<FizzBuzzModel> allModels, int pageSize)
        {
            if (allModels == null) throw new ArgumentNullException(nameof(allModels));

            return (int)Math.Ceiling((double)allModels.Count / pageSize);
        }
    }
}
