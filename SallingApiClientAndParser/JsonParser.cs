using Newtonsoft.Json;
using SallingApiJsonClass;

namespace SallingApiClientAndParser
{
    public class JsonParser
    {
        private string jsonText;
        public JsonParser(string jsonText)
        {
            this.jsonText = jsonText;
        }

        public List<DiscountProducts> ParseJsonText()
        {
            //Parses the Json text to a List of discount products. Json file format can be found at 
            //https://prod-oas-website.azurewebsites.net/#/Food%20Waste/getFoodWasteByStoreId
            List<DiscountProducts> res = JsonConvert.DeserializeObject<List<DiscountProducts>>(jsonText);
            return res;
        }
    }
}