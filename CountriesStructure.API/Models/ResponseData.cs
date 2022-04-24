namespace CountriesStructure.API.Models
{
    public class ResponseData
    {
        public IEnumerable<string> PathFromDatabase { get; private set; }
        public IEnumerable<string> PathFromDataStructure { get; private set; }

        public ResponseData(IEnumerable<string> pathFromDatabase, IEnumerable<string> pathFromDataStructure)
        {
            PathFromDatabase = pathFromDatabase;
            PathFromDataStructure = pathFromDataStructure;
        }
    }
}
