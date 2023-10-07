using System.Text.Json;
using System.Text.Json.Serialization;
using Bogus;
using FakeApi.Dto;
using FakeApi.Services;
using FakeApi.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FakeApi.Controllers;

public class DataGenController : BaseController
{
	//https://github.com/bchavez/Bogus
	[HttpGet]
	public IActionResult Internet(int ct=100)
	{
		var computerNames = Enumerable.Range(1, ct).Select(x => ServerNameGenerator.GenerateServerName()).ToList();
		var computerFaker = new Faker<ComputerRecord>();
		FakerHelper.InitComputerFaker(computerFaker, computerNames);

		var locationFaker = new Faker<Location>();
		FakerHelper.InitLocationFaker(computerFaker, locationFaker);

		var computerRecords = computerFaker.Generate(ct);
		return Ok(computerRecords);
	}

	

	[HttpGet]
	public IActionResult Vehicles(int ct=100)
	{
		var text = System.IO.File.ReadAllText(Environment.CurrentDirectory + "/jsonData/vehicles.json");
		var converted = JsonSerializer.Deserialize<IEnumerable<VehicleRecord>>(text);
		return Ok(converted?.Take(ct));
	}
}