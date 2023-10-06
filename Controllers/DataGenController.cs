using System.Dynamic;
using System.Net;
using API.Dto;
using API.Services;
using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc;
using Vehicle = Bogus.DataSets.Vehicle;

namespace API.Controllers;

public class DataGenController : BaseController
{
	//https://github.com/bchavez/Bogus
	[HttpGet]
	public IActionResult Internet(int ct=100)
	{
		var computerNames = Enumerable.Range(1, ct).Select(x => ServerNameGenerator.GenerateServerName()).ToList();
		var computerFaker = new Faker<ComputerRecord>();
		
		computerFaker.RuleFor(x => x.Name,f=>f.PickRandom(computerNames)+"."+f.Internet.DomainSuffix());
		computerFaker.RuleFor(x => x.IpAddress, f => f.Internet.IpAddress().ToString());
		computerFaker.RuleFor(x => x.Mac, f => f.Internet.Mac());
		computerFaker.RuleFor(x => x.Avatar, f => f.Internet.Avatar());
		computerFaker.RuleFor(x => x.DomainSuffix, f => f.Internet.DomainSuffix());
		computerFaker.RuleFor(x => x.Url, f => f.Internet.Url());

		
		var locationFaker = new Faker<Location>();
		computerFaker.RuleFor(x => x.Location, locationFaker.RuleFor(y => y.Country, z => z.Address.Country()));
		
		var computerRecords = computerFaker.Generate(ct).ToList();
		return Ok(computerRecords);
	}

	[HttpGet]
	public IActionResult Vehicles(int ct=100)
	{
		var vehicleFake = new Faker<MyVehicle>();
		vehicleFake.RuleFor(x => x.Manufacturer, f => f.Vehicle.Manufacturer());
		vehicleFake.RuleFor(x => x.Model, f => f.Vehicle.Model());

		return Ok(vehicleFake.Generate(ct).ToList());
	}
}