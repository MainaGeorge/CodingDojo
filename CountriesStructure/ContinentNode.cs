using System.Collections;
using System.Text;

namespace CountriesStructure.Library;
#nullable disable

public class ContinentNode : IEnumerable<CountryNode>
{
    private CountryNode _countries;
    public ContinentNode AddCountry(string countryCode, string topNeighbourCode)
    {
        if (countryCode.Equals(topNeighbourCode, StringComparison.CurrentCultureIgnoreCase))
            throw new ArgumentException("No country can be the top boarder to itself");

        var country = new CountryNode(countryCode);
        _countries = AddCountry(_countries, country, topNeighbourCode);
        return this;
    }
    public CountryNode FindCountryNodeWithGivenCountryCode(string countryCode)
    {
        var countryNode = new List<CountryNode>();
        FindCountryNodeWithGivenCountryCode(_countries, countryNode, countryCode);

        return countryNode.Any() ? countryNode[0] : null;
    }
    public void PrintCountries()
    {

        
    }

    public override string ToString()
    {
        if (_countries is null) return base.ToString();

        var result = new StringBuilder();

        var countries = _countries;
        var stack = new Stack<CountryNode>();
        stack.Push(countries);
        while (stack.Any())
        {
            var current = stack.Pop();
            result.AppendLine(PrintCountry(current));
            if (current.LeftNeighbour != null) stack.Push(current.LeftNeighbour);
            if (current.RightNeighbour != null) stack.Push(current.RightNeighbour);
        }

        return result.ToString();
    }

    public IEnumerable<string> GetPathFromOriginToDestination(string destination, string origin = "USA")
    {
        var countriesToPassThrough = new List<string>();

        CalculatePaths(destination, origin, countriesToPassThrough);

        return countriesToPassThrough;
    }
    public IEnumerator<CountryNode> GetEnumerator()
    {
        if (_countries is null)
            throw new Exception("There are no countries in this continent");

        var countries = _countries;
        var stack = new Stack<CountryNode>();
        stack.Push(countries);

        while (stack.Any())
        {
            var current = stack.Pop();
            if (current.LeftNeighbour != null) stack.Push(current.LeftNeighbour);
            if (current.RightNeighbour != null) stack.Push(current.RightNeighbour);

            yield return current;
        }
    }
    private static CountryNode AddCountry(CountryNode rootNode, CountryNode childNode, string topNeighbourCode)
    {
        if (rootNode is null)
            return childNode;

        if (string.IsNullOrWhiteSpace(topNeighbourCode))
            throw new ArgumentException("Each country must have a top Neighbour");


        bool hasFoundParent = FindParentCountry(rootNode, childNode, topNeighbourCode);

        if (hasFoundParent) return rootNode;

        throw new ArgumentException("Each country must provide a valid top neigbour");

    }
    private void AddCountry(CountryNode countryNode, string topNeighbourCode)
    {
        if (countryNode.Code.Equals(topNeighbourCode, StringComparison.CurrentCultureIgnoreCase))
            throw new ArgumentException("No country can be the top boarder to itself");

        _countries = AddCountry(_countries, countryNode, topNeighbourCode);
    }
    private static bool FindParentCountry(CountryNode rootNode, CountryNode childNode, string topNeighbourCode)
    {
        var hasFoundParent = false;
        var stack = new Stack<CountryNode>();
        stack.Push(rootNode);

        while (stack.Any())
        {
            var current = stack.Pop();

            if (current.LeftNeighbour != null) stack.Push(current.LeftNeighbour);
            if (current.RightNeighbour != null) stack.Push(current.RightNeighbour);

            if (!current.Code.Equals(topNeighbourCode, StringComparison.OrdinalIgnoreCase)) continue;

            if(current.LeftNeighbour is not null && current.RightNeighbour is not null)
                throw new ArgumentException("A country can have only two side neighbors and a top one");

            childNode.TopNeighbour = current;
            hasFoundParent = true;

            if (current.LeftNeighbour is null) current.LeftNeighbour = childNode;
            else current.RightNeighbour = childNode;
        }

        return hasFoundParent;
    }
    private static string PrintCountry(CountryNode country)
    {
        return $"country: {country.Code} topNeighbour: {country.TopNeighbour?.Code ?? "None"} rightNeighbour:{country.RightNeighbour?.Code ?? "None"} LeftNeighbour: {country.LeftNeighbour?.Code ?? "None"}";
    }
    private void CalculatePaths(string destination, string origin,
        ICollection<string> countries)
    {
        var destinationNode = FindCountryNodeWithGivenCountryCode(destination);
        if (destinationNode == null)
            throw new Exception("You can not travel to a country that does not exist");

        while (destinationNode is not null)
        {
            countries.Add(destinationNode.Code);

            if (destinationNode.Code.Equals(origin, StringComparison.CurrentCultureIgnoreCase))
            {
                break;
            }

            destinationNode = destinationNode.TopNeighbour;
        }
    }
    private static void FindCountryNodeWithGivenCountryCode(CountryNode rootNode, ICollection<CountryNode> result, string countryCode)
    {
        if (rootNode == null) return;

        if (rootNode.Code.Equals(countryCode, StringComparison.CurrentCultureIgnoreCase))
        {
            result.Add(rootNode);
            return;
        }

        FindCountryNodeWithGivenCountryCode(rootNode.RightNeighbour, result, countryCode);
        FindCountryNodeWithGivenCountryCode(rootNode.LeftNeighbour, result, countryCode);

    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}