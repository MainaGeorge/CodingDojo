using CountriesStructure.Library;

namespace CountriesStructure.Tests
{
    public static class ContinentData
    {
        public static ContinentNode GetContinentTestData()
        {
            var continent = new ContinentNode();

            continent.AddCountry("CAN", "");
            continent.AddCountry("USA", "CAN");
            continent.AddCountry("MEX", "USA");
            continent.AddCountry("GUA", "MEX");
            
            return continent;
        }
    }
}
