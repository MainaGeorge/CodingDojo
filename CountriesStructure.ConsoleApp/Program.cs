// See https://aka.ms/new-console-template for more information
using CountriesStructure.Library;

Console.WriteLine("Hello, World!");

var continent = new ContinentNode();

continent.AddCountry("CAN", "");
continent.AddCountry("USA", "CAN");
continent.AddCountry("MEX", "USA");
continent.AddCountry("GUA", "MEX");
continent.AddCountry("BLZ", "MEX");
continent.AddCountry("SAL", "GUA");
continent.AddCountry("HON", "GUA");
continent.AddCountry("NIC", "HON");
continent.AddCountry("CRI", "NIC");
continent.AddCountry("PAN", "CRI");

continent.PrintCountries();

// Console.WriteLine($"the answer is {continent.CalculatePaths("MEX")}");

// var mex = continent.FindCountryNodeWithGivenCountryCode("HON");
// Console.WriteLine($"Code:{mex.Code} right: {mex.RightNeighbour?.Code} left: {mex.LeftNeighbour?.Code} ");
//
var paths = continent.GetPathFromOriginToDestination("MEX", "CAN");
Console.WriteLine(string.Join("->", paths));
//continent.PrintCountries();
//Console.WriteLine(continent.Length);
//Console.WriteLine(continent.Length);