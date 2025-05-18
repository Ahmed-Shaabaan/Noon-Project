using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace ITI.E_Commerce.Presentation
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly JsonSerializer _serializer = new();

        public LocalizedString this[string name]
        {
            get 
            {
                var value = GetString(name);
                return new LocalizedString(name, value);

            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get 
            {
                var actuaValue = this[name];
                return !actuaValue.ResourceNotFound
                    ? new LocalizedString(name, string.Format(actuaValue.Value, arguments))
                    : actuaValue;
            }
        }
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }

        private string GetString (string key)
        {
            var filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
            var fullFilePath = Path.GetFullPath (filePath);

            if (File.Exists(fullFilePath))
            {
                var res = GetValueFromJson(key, fullFilePath);
                return res;
            }
            return String.Empty;
        }

        private string GetValueFromJson(string propertyName, string filePath)
        {
            if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(filePath))
                return string.Empty;
            using FileStream  fileStream = new(filePath,FileMode.Open,FileAccess.Read , FileShare.Read);
            using StreamReader streamReader = new(fileStream);
            using JsonTextReader jsonTextReader = new(streamReader);

            while (jsonTextReader.Read())
            {
                if (jsonTextReader.TokenType == JsonToken.PropertyName 
                    && jsonTextReader.Value as string  == propertyName )
                {
                    jsonTextReader.Read();
                    return _serializer.Deserialize<string>(jsonTextReader);
                }
            }

            return string.Empty;

        }
    }
}
