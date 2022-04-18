namespace CountriesStructure.API.Models
{
    public class ResponseData
    {
        public IEnumerable<string> PathFromDatabase { get; set; }
        public IEnumerable<string> PathFromDataStructure { get; set; }

        public ResponseData(IEnumerable<string> pathFromDatabase, IEnumerable<string> pathFromDataStructure)
        {
            PathFromDatabase = pathFromDatabase;
            PathFromDataStructure = pathFromDataStructure;
        }
    }
}
