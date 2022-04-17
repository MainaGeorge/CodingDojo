namespace CountriesStructure.Library
#nullable disable
{
    public class CountryNode
    {
        public string Code { get; set; }
        public CountryNode LeftNeighbour { get; set; }

        public CountryNode RightNeighbour { get; set; }

        public CountryNode TopNeighbour { get; set; }

        public CountryNode(string code, CountryNode leftNeighbour = null, CountryNode rightNeighbour = null,
            CountryNode topNeighbour = null)
        {
            Code = code.ToUpper();
            LeftNeighbour = leftNeighbour;
            RightNeighbour = rightNeighbour;
            TopNeighbour = topNeighbour;
        }

        
    }
}