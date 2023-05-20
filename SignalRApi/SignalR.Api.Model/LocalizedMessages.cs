using System;
using Newtonsoft.Json;

namespace SignalR.Api.Model
{
    public static class LocalizedErrorCodes
    {
        private static Dictionary<string, string> EnglishErrors = new();
        private static Dictionary<string, string> UrduErrors = new();

        public static LocalizedMessagesDto GetErrorMessage(string statusCode)
        {
            string englishMessage = GetEnglishError(statusCode);
            string urduMessage = GetUrduErrorMessage(statusCode);
            return new LocalizedMessagesDto(statusCode, englishMessage ?? "No Error found", urduMessage ?? "لم يتم العثور على خطأ");
        }
        private static string GetEnglishError(string statusCode)
        {
            if (EnglishErrors.Count == 0)
            {
                string fileName = "appsetting.error.english.json";
                string jsonString = File.ReadAllText(fileName);
                EnglishErrors = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString) ?? new();
            }
            return EnglishErrors.FirstOrDefault(x => x.Key == statusCode).Value;
        }

        private static string GetUrduErrorMessage(string statusCode)
        {
            if (UrduErrors.Count == 0)
            {
                string fileName = "appsetting.error.udru.json";
                string jsonString = File.ReadAllText(fileName);
                UrduErrors = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString) ?? new();

            }
            return UrduErrors.FirstOrDefault(x => x.Key == statusCode).Value;
        }
    }


}
