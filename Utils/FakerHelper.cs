using Bogus;
using FakeApi.Dto;

namespace FakeApi.Utils;

public static class FakerHelper
{
	public static void InitLocationFaker(Faker<ComputerRecord> computerFaker, Faker<Location> locationFaker)
	{
		computerFaker.RuleFor(x => x.Location, locationFaker
			.RuleFor(y => y.Country, z => z.Address.Country()));
		computerFaker.RuleFor(x => x.Location, locationFaker
			.RuleFor(y => y.Latitude, z => z.Address.Latitude()));
		computerFaker.RuleFor(x => x.Location, locationFaker
			.RuleFor(y => y.Longitude, z => z.Address.Longitude()));
	}

	public static void InitComputerFaker(Faker<ComputerRecord> computerFaker, List<string> computerNames)
	{
		computerFaker.RuleFor(x => x.Name, f => f.PickRandom(computerNames) + "." + f.Internet.DomainSuffix());
		computerFaker.RuleFor(x => x.IpAddress, f => f.Internet.IpAddress().ToString());
		computerFaker.RuleFor(x => x.Mac, f => f.Internet.Mac());
		computerFaker.RuleFor(x => x.Avatar, f => f.Internet.Avatar());
		computerFaker.RuleFor(x => x.DomainSuffix, f => f.Internet.DomainSuffix());
		computerFaker.RuleFor(x => x.Url, f => f.Internet.Url());
	}
}