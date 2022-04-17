// See https://aka.ms/new-console-template for more information
using CountriesStructure.Library;

Console.WriteLine("Hello, World!");

var continent = new ContinentNode();

continent
    .AddCountry("CAN", "")
    .AddCountry("USA", "CAN")
    .AddCountry("MEX", "USA")
    .AddCountry("GUA", "MEX")
    .AddCountry("BLZ", "MEX")
    .AddCountry("SAL", "GUA")
    .AddCountry("HON", "GUA")
    .AddCountry("NIC", "HON")
    .AddCountry("CRI", "NIC")
    .AddCountry("PAN", "CRI");

Console.WriteLine(continent);

