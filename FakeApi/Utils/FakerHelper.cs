using System.CodeDom.Compiler;
using Bogus;
using FakeApi.Abstractions;
using FakeApi.Dto;
using Serilog;

namespace FakeApi.Utils;

public  class FakerHelper
{
	public IEnumerable<ComputerRecord> ComputerRecords(int ct=100)
	{
		var computerFaker = new  Faker<ComputerRecord>();
		
		computerFaker.RuleFor(x => x.Id, f => f.Random.Int(1));
		computerFaker.RuleFor(x => x.Name, f => f.Hacker.Adjective()+"-"+f.Hacker.Noun());
		computerFaker.RuleFor(x => x.IpAddress, f => f.Internet.IpAddress().ToString());
		computerFaker.RuleFor(x => x.Mac, f => f.Internet.Mac());
		computerFaker.RuleFor(x => x.Avatar, f => f.Internet.Avatar());
		computerFaker.RuleFor(x => x.DomainSuffix, f => f.Internet.DomainSuffix());
		computerFaker.RuleFor(x => x.Url, f => f.Internet.Url());
		return computerFaker.Generate(ct);
	}

	public IEnumerable<VehicleRecord> VehicleRecords(int ct = 100)
	{
		var faker = new Faker<VehicleRecord>();
		faker.RuleFor(x => x.Id, f => f.Random.Int(1));
		faker.RuleFor(x => x.Vin, f => f.Vehicle.Vin());
		faker.RuleFor(x => x.Manufacturer, f => f.Vehicle.Manufacturer());
		faker.RuleFor(x => x.Model, f => f.Vehicle.Model());
		faker.RuleFor(x => x.Type, f => f.Vehicle.Type());
		faker.RuleFor(x => x.Fuel, f => f.Vehicle.Fuel());
		
		return faker.Generate(ct);
	}
}

